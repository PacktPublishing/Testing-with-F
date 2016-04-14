module _1232OS_04

    type User = { ID : int; Password : string }

    type IUserDataAccess =
        abstract member GetUser: int -> User
        abstract member GetUsers: unit -> User list
        abstract member UpdateUser: User -> bool
        abstract member CreateUser: User -> User
        abstract member DeleteUser: int -> bool
   
    // update a user with new password     
    let resetUserPassword (dataAccess : IUserDataAccess) user length =
        // define password alphabet
        let alphabet = ['a'..'z'] @ ['A'..'Z'] @ ['0'..'9'] @ ['@'; '$'; '#'; ','; '.']

        // pick random character from alphabet
        let random seed alphabet : char =  List.nth alphabet ((System.Random(seed)).Next(alphabet.Length))

        // create a string out of random characters
        let password = [for i in 1..length -> random i alphabet] 
                        |> List.fold (fun acc value -> sprintf "%s%c" acc value) ""

        // create new user instance
        let user = {user with Password = password}

        // store user to database and return
        dataAccess.UpdateUser user |> ignore
        user
        

    open NUnit.Framework
    open FsUnit

    [<Test>]
    let ``should generate a new password on user`` () =
        // arrange
        let dataAccess =
            { new IUserDataAccess with
                member this.GetUser id = { ID = id; Password = "" }
                member this.GetUsers () = []
                member this.UpdateUser user = true
                member this.CreateUser user = user
                member this.DeleteUser id = true }

        // act
        let user = resetUserPassword dataAccess { ID = 1; Password = "" } 12

        // assert
        user.Password |> should haveLength 12

    open Foq

    [<Test>]
    let ``generated password should always be unique`` () =
        // arrange
        let dataAccess = 
            Mock<IUserDataAccess>()
                .Setup(fun x -> <@ x.UpdateUser(any()) @>).Returns(true)
                .Create()

        // act
        let user1 =  resetUserPassword dataAccess { ID = 1; Password = "" } 12
        let user2 = resetUserPassword dataAccess { ID = 2; Password = "" } 12

        // assert
        user1.Password |> should not' (equal user2.Password)


    // delete a list of users
    let deleteUsers users (dataAccess : IUserDataAccess) =
        users 
            |> List.map (fun user -> dataAccess.DeleteUser user.ID)
            |> List.zip users

    [<Test>]
    let ``should report status on deleted users`` () =
        // arrange
        let users = [|{ ID = 1; Password = "pass1" };
                        { ID = 2; Password = "pass1" };
                        { ID = 3; Password = "pass1" }|]

        let dataAccess =
            Mock<IUserDataAccess>()
                .Setup(fun da -> <@ da.DeleteUser(users.[0].ID) @>).Returns(true)
                .Setup(fun da -> <@ da.DeleteUser(users.[1].ID) @>).Returns(false)
                .Setup(fun da -> <@ da.DeleteUser(users.[2].ID) @>).Returns(true)
                .Create()

        // act
        let result1 :: result2 :: result3 :: [] = deleteUsers (users |> List.ofArray) dataAccess

        // assert
        result1 |> should equal (users.[0], true)
        result2 |> should equal (users.[1], false)
        result3 |> should equal (users.[2], true)
    
    let update (dataAccess : IUserDataAccess) user =
        try
            dataAccess.UpdateUser user
        with
        // user has not been persisted before updating
        | :? System.Data.MissingPrimaryKeyException -> false
        
    [<Test>]
    let ``should return false when updating a user that doesn't exist`` () =
        // arrange
        let dataAccess =
            Mock<IUserDataAccess>()
                .Setup(fun da -> <@ da.UpdateUser(any()) @>)
                    .Raises<System.Data.MissingPrimaryKeyException>()
                .Create()

        // act
        let result = update dataAccess { ID = 1; Password = "pass1" } 

        // assert
        result |> should be False
       


        