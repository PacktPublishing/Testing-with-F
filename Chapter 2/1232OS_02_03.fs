namespace chapter02

module _1232OS_02_03 =

    // F# implementation of String.Join
    let join separator = 
        // use separator to separate two strings
        let _separate s1 s2 = s1 + separator + s2
        // reduce list using the separator helper function
        List.reduce _separate

    open NUnit.Framework
    open FsUnit

    [<Test>]
    let ``should join [1..3] to '1, 2, 3' with separator ', '`` () =
        ["1"; "2"; "3"] |> join ", " |> should equal "1, 2, 3"

    [<Test>]
    let ``cannot join on empty list`` () =
        (fun () -> [] |> join ", " |> ignore) |> should throw typeof<System.ArgumentException>