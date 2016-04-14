namespace chapter05

module _1232OS_05_03 =

    open NUnit.Framework
    open FsUnit

    open Microsoft.SqlServer.Management.Smo

    let dbFilePath = @"C:\Program Files\Microsoft SQL Server\MSSQL12.MSSQLSERVER\MSSQL\DATA\"
    let dbName = sprintf "chapter05_%s" (System.DateTime.Now.ToString("yyyyMMddHHmm"))

    [<TestFixtureSetUp>]
    let ``Setup database`` () : unit =
        let server = new Server(".")
        server.ConnectionContext.LoginSecure <- true
        server.ConnectionContext.Connect()
        try
            let restore = new Restore()
            restore.Database <- dbName
            restore.Action <- RestoreActionType.Database
            restore.Devices.AddDevice(@"C:\chapter05.bak", DeviceType.File)
            restore.ReplaceDatabase <- true
            restore.NoRecovery <- false
            restore.RelocateFiles.Add(new RelocateFile("Chapter05_Migrations", dbFilePath + dbName + "_Data.mdf")) |> ignore
            restore.RelocateFiles.Add(new RelocateFile("Chapter05_Migrations_log", dbFilePath + dbName + "_Log.ldf")) |> ignore
            restore.SqlRestore(server)
        finally
            server.ConnectionContext.Disconnect()

    [<TestFixtureTearDown>]
    let ``Tear down database`` () : unit =
        let server = new Server(".")
        server.ConnectionContext.LoginSecure <- true
        server.ConnectionContext.Connect()
        try
            server.KillDatabase(dbName)
        finally
            server.ConnectionContext.Disconnect()

    open System.Data
    open System.Data.Linq
    open Microsoft.FSharp.Data.TypeProviders
    open Microsoft.FSharp.Linq

    type Email = { FromAddress : string; ToAddress : string; Subject : string; Body : string }
    let connectionString = sprintf "Data Source=.;Initial Catalog=%s;Integrated Security=SSPI;" dbName
    type dbSchema = SqlDataConnection<"Data Source=.;Initial Catalog=Chapter05_Migrations;Integrated Security=SSPI;">
    let createdb () = let db = dbSchema.GetDataContext(connectionString)
                      db.DataContext.Log <- System.Console.Out
                      db

    let insert (email : Email) =
        let db = createdb()
        db.Email.InsertOnSubmit(new dbSchema.ServiceTypes.Email(FromAddress = email.FromAddress, ToAddress = email.ToAddress, Subject = email.Subject, Body = email.Body))
        db.DataContext.SubmitChanges()

    let getFrom fromAddress =
        let db = createdb()
        query {
            for row in db.Email do
            where (row.FromAddress = fromAddress)
            select { FromAddress = row.FromAddress; ToAddress = row.ToAddress; Subject = row.Subject; Body = row.Body }
        }

    [<Test>]
    let ``can get email by its from address`` () =
        // arrange
        let email = { FromAddress = "this-is-a@test.com"; ToAddress = "hello@mikaellundin.name"; Subject = "Test"; Body = "Will be queued for sending" }
        insert email

        // act
        let daEmail = getFrom email.FromAddress |> Seq.nth(0)

        // assert
        daEmail.FromAddress |> should equal email.FromAddress
        daEmail.ToAddress |> should equal email.ToAddress
        daEmail.Subject |> should equal email.Subject
        daEmail.Body |> should equal email.Body


