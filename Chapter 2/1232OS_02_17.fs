namespace chapter02

module _1232OS_02_17 = 

    // active pattern for sequences
    let (|Empty|NotEmpty|) sequence =
        if Seq.isEmpty sequence then Empty
        else NotEmpty sequence

    // example on usage of active pattern
    // join ["1"; "2"; "3"] -> "123"
    let rec join (s : seq<string>) = 
        match s with
        | Empty -> ""
        | NotEmpty s -> (Seq.head s) + join (Seq.skip 1 s)

    open NUnit.Framework
    open FsUnit

    [<Test>]
    let ``should match an empty sequence`` () =
        match [] with
        | NotEmpty _ -> Assert.Fail("should match [] as empty sequence")
        | Empty -> Assert.True(true)

    [<Test>]
    let ``should match a sequence`` () =
        let input = [1..10]
        match input with
        | NotEmpty seq -> seq |> should equal input
        | Empty -> Assert.Fail("should match [1..10] as non empty sequence")
