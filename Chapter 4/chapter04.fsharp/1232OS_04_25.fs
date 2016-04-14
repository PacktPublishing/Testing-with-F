namespace chapter04.fsharp

module _1232OS_04_25 =

    // schema of the data in csv file
    type Customer = { ID : int; Name : string; Email : string; EnabledSubscription : bool }

    // turn the csv schema into Customer records
    let schema (record : string list) =
        assert (record.Length = 4)
        let id :: name :: email :: enabledSubscription :: [] = record
        {
            ID = System.Int32.Parse(id);
            Name = name;
            Email = email;
            EnabledSubscription = bool.Parse(enabledSubscription)
        }  

    let csvFileReader (filePath : string) = seq {   
            use streamReader = new System.IO.StreamReader(filePath)
            while not streamReader.EndOfStream do
                let line = streamReader.ReadLine()
                yield line.Split([|','; ';'|]) |> List.ofSeq
        }

    // load the whole csv file into list of Customer instances
    let getCustomers (getData : string -> seq<string list>) filePath =
        getData filePath 
            |> Seq.map schema
            |> Seq.toList

    open NUnit.Framework
    open FsUnit

    [<Test>]
    let ``should convert all lines from file into Customers`` () =
        // arrange
        let getData x = seq {
                yield ["1"; "John Doe"; "john.doe@test.com"; "true"];
                yield ["2"; "Jane Doe"; "jane.doe@test.com"; "false"];
                yield ["3"; "Mikael Lundin"; "hello@mikaellundin.com"; "False"]
            }

        // act & assert
        (getCustomers getData "").Length |> should equal 3

    

        
