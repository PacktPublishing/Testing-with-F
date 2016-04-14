namespace chapter04.fsharp

module _1232OS_04_22 =

    open _1232OS_04_21
    open NUnit.Framework
    open FsUnit

    type CustomerRepository (csvReader : ICsvReader) =
        member this.Load filePath = 
            csvReader.Load filePath 
                |> Seq.map schema
                |> Seq.toList

    [<Test>]
    let ``should parse ID as int`` () =
        // arrange
        let data = ["1"; "John Doe"; "john.doe@test.com"; "true"]

        // interface implementation by object expression
        let csvReader =
            { new ICsvReader with
                member this.Load filePath = seq { yield data }}

        // act & assert
        let firstCustomer = CustomerRepository(csvReader).Load("").Item(0)
        firstCustomer.ID |> should equal 1

