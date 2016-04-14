namespace chapter05

module _1232OS_05_05 =

    open System
    open System.Data
    open System.Data.Linq
    open Microsoft.FSharp.Data.TypeProviders
    open Microsoft.FSharp.Linq

    type dbSchema = SqlDataConnection<"Data Source=.;Initial Catalog=Chapter05_Migrations;Integrated Security=SSPI;", StoredProcedures = true>
    let db = dbSchema.GetDataContext()

    // Enable the logging of database activity to the console.
    db.DataContext.Log <- System.Console.Out

    // get address history of person with ssn number
    let getAddressHistory ssn = 
        query {
            for addressHistory in db.NationalRegistrationAddress do
            join person in db.Person on (addressHistory.Person = person.ID)
            join address in db.Address on (addressHistory.Address = address.ID)
            where (person.SSN = ssn)
            select address
        }

    open NUnit.Framework
    open FsUnit

    [<Test>]
    let ``should get address history from SSN number`` () =
        let ssn = "196403063374"

        // setup
        db.Connection.Open()
        let transaction = db.Connection.BeginTransaction(isolationLevel = IsolationLevel.Serializable)
        db.DataContext.Transaction <- transaction

        db.SetupTestData() |> ignore // <-- here the db is prepped with test data

        try
            // act
            let addresses = getAddressHistory ssn |> Seq.toList

            // assert
            addresses.Length |> should equal 2
        
        finally
            // teardown
            transaction.Rollback()
            db.Connection.Close()

