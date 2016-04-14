namespace chapter04.fsharp

module _1232OS_04_26 =

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

    // return value should match the function signature: unit -> seq<string list>
    let csvFileReader (filePath : string) = 
        (fun _ -> seq {   
            use streamReader = new System.IO.StreamReader(filePath)
            while not streamReader.EndOfStream do
                let line = streamReader.ReadLine()
                yield line.Split([|','; ';'|]) |> List.ofSeq
        })

    // getData: unit -> seq<string list>
    let getCustomers getData =
        getData() |> Seq.map schema |> Seq.toList

    // usage
    getCustomers (csvFileReader "")

    open NUnit.Framework
    open FsUnit

    [<Test>]
    let ``should convert all lines from file into Customers`` () =
        // arrange
        let getData = (fun _ -> seq {
                yield ["1"; "John Doe"; "john.doe@test.com"; "true"];
                yield ["2"; "Jane Doe"; "jane.doe@test.com"; "false"];
            })

        // act & assert
        (getCustomers getData).Length |> should equal 2
