namespace chapter05.code._parallel

module AssemblyInfo =
    open NUnit.Framework
    [<assembly: LevelOfParallelization(5)>] 
    do ()

module _1232OS_05_06 =

    open System
    open System.Data
    open System.Data.Linq
    open Microsoft.FSharp.Data.TypeProviders
    open Microsoft.FSharp.Linq

    type dbSchema = SqlDataConnection<"Data Source=.;Initial Catalog=Chapter05_Migrations;Integrated Security=SSPI;", StoredProcedures = true>
    
    // get address history of person with ssn number
    let getAddressHistory ssn (db : dbSchema.ServiceTypes.SimpleDataContextTypes.Chapter05_Migrations) = 
        query {
            for addressHistory in db.NationalRegistrationAddress do
            join person in db.Person on (addressHistory.Person = person.ID)
            join address in db.Address on (addressHistory.Address = address.ID)
            where (person.SSN = ssn)
            select address
        }

    let getHabitantsAtAddress streetName streetNumber streetLetter postalCode (db : dbSchema.ServiceTypes.SimpleDataContextTypes.Chapter05_Migrations) =
        query {
            for addressHistory in db.NationalRegistrationAddress do
            join person in db.Person on (addressHistory.Person = person.ID)
            join address in db.Address on (addressHistory.Address = address.ID)
            where (address.StreetName = streetName &&
                   address.StreetNumber = streetNumber &&
                   address.StreetLetter = streetLetter &&
                   address.PostalCode = postalCode)
            select person            
        }

    open NUnit.Framework

    [<Test>]
    [<Parallelizable(ParallelScope.Self)>]
    let ``should get address history from SSN number`` () =
        let db = dbSchema.GetDataContext()
        db.DataContext.Log <- System.Console.Out

        let ssn = "196403063374"

        // setup
        db.Connection.Open()
        let transaction = db.Connection.BeginTransaction(isolationLevel = IsolationLevel.ReadCommitted)
        db.DataContext.Transaction <- transaction
        
        db.SetupTestData() |> ignore // <-- here the db is prepped with test data

        try
            // act
            let addresses = getAddressHistory ssn db |> Seq.toList

            // assert
            Assert.That(addresses.Length, Is.EqualTo(2))
        
        finally
            // teardown
            transaction.Rollback()
            db.Connection.Close()

    [<Test>]
    [<Parallelizable(ParallelScope.Self)>]
    let ``should return empty address history when not found`` () =
        let db = dbSchema.GetDataContext()
        db.DataContext.Log <- System.Console.Out

        let ssn = "1234567891234"

        // setup
        db.Connection.Open()
        let transaction = db.Connection.BeginTransaction(isolationLevel = IsolationLevel.ReadCommitted)
        db.DataContext.Transaction <- transaction

        db.SetupTestData() |> ignore // <-- here the db is prepped with test data

        try
            // act
            let addresses = getAddressHistory ssn db |> Seq.toList

            // assert
            Assert.That(addresses.Length, Is.EqualTo(0))

        finally
            // teardown
            transaction.Rollback()
            db.Connection.Close()

    [<Test>]
    [<Parallelizable(ParallelScope.Self)>]
    let ``should get all habitants of address`` () =
        let db = dbSchema.GetDataContext()
        db.DataContext.Log <- System.Console.Out

        let streetName, streetNumber, streetLetter, postalCode =
            ("Lantgatan", "38", null, "12559")

        // setup
        db.Connection.Open()
        let transaction = db.Connection.BeginTransaction(isolationLevel = IsolationLevel.ReadCommitted)
        db.DataContext.Transaction <- transaction

        db.SetupTestData() |> ignore // <-- here the db is prepped with test data

        try
            // act
            let persons = getHabitantsAtAddress streetName streetNumber streetLetter postalCode db 
                          |> Seq.toList
                          |> List.map (fun person -> person.SSN)

            // assert
            Assert.That(persons, Is.EqualTo(["193808209005"; "194212259005"; "196403063374"]))
        
        finally
            // teardown
            transaction.Rollback()
            db.Connection.Close()

    [<Test>]
    [<Parallelizable(ParallelScope.Self)>]
    let ``should get the only habitant of an address`` () =
        let db = dbSchema.GetDataContext()
        db.DataContext.Log <- System.Console.Out

        let streetName, streetNumber, streetLetter, postalCode =
            ("Stångmästarevägen", "11-13", "A", "15955")

        // setup
        db.Connection.Open()
        let transaction = db.Connection.BeginTransaction(isolationLevel = IsolationLevel.ReadCommitted)
        db.DataContext.Transaction <- transaction

        db.SetupTestData() |> ignore // <-- here the db is prepped with test data

        try
            // act
            let persons = getHabitantsAtAddress streetName streetNumber streetLetter postalCode db 
                          |> Seq.toList
                          |> List.map (fun person -> person.SSN)

            // assert
            Assert.That(persons, Is.EqualTo(["196403063374"]))
        
        finally
            // teardown
            transaction.Rollback()
            db.Connection.Close()

    [<Test>]
    [<Parallelizable(ParallelScope.Self)>]
    let ``should get empty result of habitants when address doesn't exist`` () =
        let db = dbSchema.GetDataContext()
        db.DataContext.Log <- System.Console.Out

        let streetName, streetNumber, streetLetter, postalCode =
            ("Teststreet", "99", null, "99999")

        // setup
        db.Connection.Open()
        let transaction = db.Connection.BeginTransaction(isolationLevel = IsolationLevel.ReadCommitted)
        db.DataContext.Transaction <- transaction

        db.SetupTestData() |> ignore // <-- here the db is prepped with test data

        try
            // act
            let persons = getHabitantsAtAddress streetName streetNumber streetLetter postalCode db 
                          |> Seq.toList

            // assert
            Assert.That(persons, Is.EqualTo(List.empty))
        
        finally
            // teardown
            transaction.Rollback()
            db.Connection.Close()
