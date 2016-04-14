namespace chapter04.fsharp

module _1232OS_04_21 =

    // loading and processing a csv file line by line
    type ICsvReader =
        // open file and return sequence of values per line
        abstract member Load : string -> seq<string list>

    // implementation of ICsvReader
    type CsvReader () =
        interface ICsvReader with
            member this.Load filePath = seq {   
                    use streamReader = new System.IO.StreamReader(filePath)
                    while not streamReader.EndOfStream do
                        let line = streamReader.ReadLine()
                        yield line.Split([|','; ';'|]) |> List.ofSeq
                }

    // schema of the data in csv file
    type Customer = { ID : int; Name : string; Email : string; EnabledSubscription : bool }

    // turn the csv schema into Customer records
    let schema (record : string list) =
        assert (record.Length = 4)
        let id :: name :: email :: enabledSubscription :: [] = record
        {
            ID = System.Int32.Parse(id)
            Name = name
            Email = email
            EnabledSubscription = bool.Parse(enabledSubscription)
        }  

    // load the whole csv file into list of Customer instances
    let getCustomersFromCsvFile filePath (csvReader : ICsvReader) =
        csvReader.Load filePath 
            |> Seq.map schema
            |> Seq.toList

    open NUnit.Framework
    open FsUnit

    [<Test>]
    let ``should convert all lines from file into Customers`` () =
        // arrange
        let data = [
                ["1"; "John Doe"; "john.doe@test.com"; "true"];
                ["2"; "Jane Doe"; "jane.doe@test.com"; "false"];
                ["3"; "Mikael Lundin"; "hello@mikaellundin.com"; "False"]
            ]

        // interface implementation by object expression
        let csvReader =
            { new ICsvReader with
                member this.Load filePath = data |> List.toSeq }

        // act & assert
        (getCustomersFromCsvFile "" csvReader).Length |> should equal data.Length
        
    

        
