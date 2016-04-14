namespace chapter10

module _1232OS_10_03 =

    open NUnit.Framework
    open FsUnit

    type Credentials(userName, password) =
        member __.UserName = userName
        member __.Password = password

    type ValidationResult =
        | Success
        | UserNameEmpty
        | PasswordEmpty

    let validate (credentials : Credentials) = seq {
        if credentials.UserName = "" then
            yield UserNameEmpty
        if credentials.Password = "" then
            yield PasswordEmpty
        if credentials.UserName = "user" && credentials.Password = "secret" then
            yield Success
        }

    // don't
    [<Test>]
    let ``username and password cannot be empty string`` () =
        // arrange
        let credentials = Credentials(System.String.Empty, System.String.Empty)

        // act
        let result = validate(credentials)

        // assert
        result |> should contain UserNameEmpty
        result |> should contain PasswordEmpty

    // do
    [<Test>]
    let ``username cannot be empty string`` () =
        // arrange
        let credentials = Credentials(System.String.Empty, "secret")

        // act
        let result = validate(credentials)

        // assert
        result |> should contain UserNameEmpty

    // do
    [<Test>]
    let ``password cannot be empty string`` () =
        // arrange
        let credentials = Credentials("user", System.String.Empty)

        // act
        let result = validate(credentials)

        // assert
        result |> should contain PasswordEmpty

