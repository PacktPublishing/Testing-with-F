namespace chapter04.fsharp

module _1232OS_04_24 =

    open _1232OS_04_21
    open NUnit.Framework
    open FsUnit

    type CustomerRepository () =
        member this.Load filePath (csvReader : ICsvReader) = 
            csvReader.Load filePath 
                |> Seq.map schema
                |> Seq.toList

    [<Test>]
    let ``should parse data row into Customer`` () =
        // arrange
        let data = ["1"; "John Doe"; "john.doe@test.com"; "true"]

        // interface implementation by object expression
        let csvReader =
            { new ICsvReader with
                member this.Load filePath = seq { yield data }}

        // act
        let firstCustomer = (CustomerRepository().Load ""  csvReader).Item(0)

        // assert
        firstCustomer.ID |> should equal 1
        firstCustomer.Name |> should equal "John Doe"
        firstCustomer.Email |> should equal "john.doe@test.com"
        firstCustomer.EnabledSubscription |> should equal true
