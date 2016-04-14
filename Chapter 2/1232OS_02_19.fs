namespace chapter02

module _1232OS_02_19 =

    open NUnit.Framework
    open FsUnit

    [<Test>]
    let ``create tuple and break tuple`` () =
        // pattern matching
        let tuple = (1, 2)
        let a, b = tuple
        printfn "%d + %d = %d" a b (a + b)
        a |> should equal 1
        b |> should equal 2


    // instead of bool.TryParse
    // s -> bool choice
    let parseBool s =
        match bool.TryParse(s) with
        // success, value
        | true, b  -> Some(b)
        | false, _ -> None

    [<Test>]
    let ``should parse "true" as true`` () =
        parseBool "true" |> should equal (Some true)

    [<Test>]
    let ``should parse "false" as false`` () =
        parseBool "false" |> should equal (Some false)
    
    [<Test>]
    let ``cannot parse string gives none`` () =
        parseBool "FileNotFound" |> should equal None

    
        

