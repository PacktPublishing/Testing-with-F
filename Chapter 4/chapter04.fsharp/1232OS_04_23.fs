namespace chapter04.fsharp

module _1232OS_04_23 =

    open _1232OS_04_21
    open NUnit.Framework
    open FsUnit

    type CustomerRepository () =
        let mutable csvReader = new CsvReader() :> ICsvReader
       
        // extension property
        member this.CsvReader
            with get() = csvReader
            and set(value) = csvReader <- value
        
        member this.Load filePath = 
            csvReader.Load filePath 
                |> Seq.map schema
                |> Seq.toList

    [<Test>]
    let ``should parse EnabledSubscription as bool`` () =
        // arrange
        let customerRepository = CustomerRepository()
        let data = ["1"; "John Doe"; "john.doe@test.com"; "true"]

        // interface implementation by object expression
        let csvReader =
            { new ICsvReader with
                member this.Load filePath = seq { yield data }}

        // exchange internal CsvReader with our test double
        customerRepository.CsvReader <- csvReader

        // act & assert
        let firstCustomer = customerRepository.Load("").Item(0)
        firstCustomer.EnabledSubscription |> should be True
