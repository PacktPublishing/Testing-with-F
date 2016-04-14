namespace chapter05.code

module DataAccess =
    open System
    open System.Data
    open System.Data.Linq
    open Microsoft.FSharp.Data.TypeProviders
    open Microsoft.FSharp.Linq

    type dbSchema = SqlDataConnection<"Data Source=.;Initial Catalog=Chapter05;Integrated Security=SSPI;">
    let db = dbSchema.GetDataContext()

    // Enable the logging of database activity to the console.
    db.DataContext.Log <- System.Console.Out

    type User = { ID : int; UserName : string; Email : string }

    // get user by name
    let getUser name =
        query {
            for row in db.User do
            where (row.UserName = name)
            select { ID = row.ID; UserName = row.UserName; Email = row.Email }
        } |> Seq.tryFind (fun _ -> true)

    open NUnit.Framework
    open FsUnit

    [<Test>]
    let ``should return user with email hello@mikaellundin.name when requesting username mikaellundin`` () =
        // act
        let user = getUser "mikaellundin"

        // assert
        user |> Option.isSome |> should be True
        user.Value.Email |> should equal "hello@mikaellundin.name"

    [<Test>]
    let ``should be able to retrieve user e-mail from database`` () =
        let dbUser = new dbSchema.ServiceTypes.User(ID = -1, UserName = "testuser", Email = "test@test.com")
        
        // setup
        db.User.InsertOnSubmit(dbUser)
        db.DataContext.SubmitChanges()

        // act
        let user = getUser dbUser.UserName

        // assert
        user |> Option.isSome |> should be True
        user.Value.Email |> should equal dbUser.Email

        // teardown
        db.User.DeleteOnSubmit(dbUser)
        db.DataContext.SubmitChanges()

    [<Test>]
    let ``should be able to get username from database`` () =
        let dbUser = new dbSchema.ServiceTypes.User(ID = -1, UserName = "testuser", Email = "test@test.com")

        // setup
        db.Connection.Open()
        let transaction = db.Connection.BeginTransaction(isolationLevel = IsolationLevel.Serializable)
        db.DataContext.Transaction <- transaction
        db.User.InsertOnSubmit(dbUser)
        db.DataContext.SubmitChanges()

        try
            // act
            let user = getUser dbUser.UserName

            // assert
            user |> Option.isSome |> should be True
            user.Value.UserName |> should equal dbUser.UserName
        
        finally
            // teardown
            transaction.Rollback()
            db.Connection.Close()