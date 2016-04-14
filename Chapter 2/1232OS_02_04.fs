namespace chapter02

module _1232OS_02_04 =

    // optimization, only create the string builder once
    // NOTE Not thread safe
    let mutable stringBuilder = new System.Text.StringBuilder()

    // F# implementation of String.Join
    let join (separator : string) list = 
        // create a mutable state
        stringBuilder.Clear()
        let mutable hasValues = false
        
        // iterate over the incoming list
        for s in list do
            hasValues <- true
            stringBuilder
                .Append(s.ToString())
                .Append(separator)
                |> ignore
            
        // if list hasValues remove last separator
        if hasValues then
            stringBuilder.Remove(stringBuilder.Length - separator.Length, separator.Length) |> ignore

        // get result
        stringBuilder.ToString()

    open NUnit.Framework
    open FsUnit

    [<Test>]
    let ``should join [1..3] to '1, 2, 3' with separator ', '`` () =
        [1..3] |> join ", " |> should equal "1, 2, 3"

    [<Test>]
    let ``should return empty string from empty list`` () =
        [] |> join ", " |> should equal System.String.Empty
            
