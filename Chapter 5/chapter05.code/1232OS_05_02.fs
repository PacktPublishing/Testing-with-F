namespace chapter05

module _1232OS_05_02 =

    open FluentMigrator

    type Email = { FromAddress : string; ToAddress : string; Subject : string; Body : string }

    [<Migration(1L)>]
    type CreateEmailTable () =
        inherit Migration()

        let tableName = "Email"

        override this.Up () =
            ignore <| this.Create.Table(tableName)
                .WithColumn("ID").AsInt32().NotNullable().PrimaryKey().Identity()
                .WithColumn("FromAddress").AsString().NotNullable()
                .WithColumn("ToAddress").AsString().NotNullable()
                .WithColumn("Subject").AsString().NotNullable()
                .WithColumn("Body").AsString().NotNullable()

            ignore <| this.Insert.IntoTable(tableName)
                .Row({ FromAddress = "test@test.com"; 
                       ToAddress = "hello@mikaellundin.name"; 
                       Subject = "Hello"; 
                       Body = "World" })

        override this.Down () =
            ignore <| this.Delete.Table(tableName)

    open NUnit.Framework
    open FsUnit

    open FluentMigrator
    open FluentMigrator.Runner
    open FluentMigrator.Runner.Initialization
    open FluentMigrator.Runner.Announcers

    // sharing database name between tests
    let mutable dbName = ""

    type MigrationOptions () =
        interface IMigrationProcessorOptions with
            member this.PreviewOnly = false
            member this.Timeout = 60
            member this.ProviderSwitches = ""

    let connectionString dbName = sprintf "Data Source=.;Initial Catalog=%s;Integrated Security=SSPI;" dbName

    [<TestFixtureSetUp>]
    let ``create and migrate database`` () =
        // constants
        dbName <- sprintf "chapter05_%s" (System.DateTime.Now.ToString("yyyyMMddHHmm"))
        
        // create database
        use connection = new System.Data.SqlClient.SqlConnection(connectionString "master")
        use createCommand = new System.Data.SqlClient.SqlCommand("CREATE DATABASE " + dbName, connection)
        connection.Open()
        createCommand.ExecuteNonQuery() |> ignore

        // build database from migrations
        let announcer = new TextWriterAnnouncer(System.Diagnostics.Debug.WriteLine)
        let assembly = System.Reflection.Assembly.GetExecutingAssembly()
        let migrationContext = new RunnerContext(announcer)
        migrationContext.Namespace <- "chapter05"
        let options = new MigrationOptions()
        let factory = new FluentMigrator.Runner.Processors.SqlServer.SqlServer2012ProcessorFactory()
        let processor = factory.Create((connectionString dbName), announcer, options)
        let runner = new MigrationRunner(assembly, migrationContext, processor);
        runner.MigrateUp(true);

    [<TestFixtureTearDown>]
    let ``drop the database`` () =
        // drop database
        use connection = new System.Data.SqlClient.SqlConnection(connectionString "master")
        use dropCommand = new System.Data.SqlClient.SqlCommand("DROP DATABASE " + dbName, connection)
        dropCommand.ExecuteNonQuery() |> ignore

    open System.Data
    open System.Data.Linq
    open Microsoft.FSharp.Data.TypeProviders
    open Microsoft.FSharp.Linq

    type dbSchema = SqlDataConnection<"Data Source=.;Initial Catalog=Chapter05_Migrations;Integrated Security=SSPI;">
    let createdb () = let db = dbSchema.GetDataContext(connectionString dbName)
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
    let ``can insert into email table`` () =
        // arrange
        let email = { FromAddress = "my@test.com"; ToAddress = "hello@mikaellundin.name"; Subject = "Test"; Body = "Will be queued for sending" }

        // act
        insert email

        // assert
        let daEmail = getFrom email.FromAddress |> Seq.nth(0)
        daEmail.FromAddress |> should equal email.FromAddress
        daEmail.ToAddress |> should equal email.ToAddress
        daEmail.Subject |> should equal email.Subject
        daEmail.Body |> should equal email.Body
