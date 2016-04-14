namespace chapter05.code

module _1232OS_05_07 =

    open System
    open System.Data
    open System.Data.Linq
    open Microsoft.FSharp.Data.TypeProviders
    open Microsoft.FSharp.Linq

    type dbSchema = SqlDataConnection<"Data Source=.;Initial Catalog=Chapter05_Migrations;Integrated Security=SSPI;">

    // find an address by a search string
    let searchAddress q (db : dbSchema.ServiceTypes.SimpleDataContextTypes.Chapter05_Migrations) =
        query {
            for address in db.Address do
            where ((address.StreetName.StartsWith q) ||
                  (address.StreetNumber = q) ||
                  (address.PostalCode.StartsWith q))
            select address
        }

    open NUnit.Framework
    open FsUnit

    [<TestCase("Lant", true)>]   // match start of street name
    [<TestCase("gatan", false)>] // not matching end of street name
    [<TestCase("L", true)>]      // matching single letter
    [<TestCase("38", true)>]     // matching whole street number
    [<TestCase("3", false)>]      // not matching part of street number
    [<TestCase("12559", true)>]  // matching whole postal code
    [<TestCase("125", true)>]    // matching start of postal code
    [<TestCase("59", false)>]    // not matching end of postal code
    let ``should query address register`` q expectedFind =
        // setup
        let db = dbSchema.GetDataContext()
        db.Connection.Open()
        let transaction = db.Connection.BeginTransaction(isolationLevel = IsolationLevel.Serializable)
        db.DataContext.Transaction <- transaction

        db.SetupTestData() |> ignore // <-- here the db is prepped with test data

        try
            // act
            let addresses = searchAddress q db |> Seq.toList

            // assert
            let found = addresses 
                        |> Seq.exists (fun address -> 
                            (address.StreetName = "Lantgatan") &&
                            (address.StreetNumber = "38") &&
                            (address.PostalCode = "12559"))

            found |> should equal expectedFind
        
        finally
            // teardown
            transaction.Rollback()
            db.Connection.Close()


