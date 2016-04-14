namespace chapter02

module _1232OS_02_02 =
    
    // F# implementation of String.Join
    let rec join separator = function
        | [] -> ""
        | hd :: [] -> hd.ToString()
        | hd :: tl -> hd.ToString() + separator + (join separator tl)

    open NUnit.Framework
    open FsUnit

    [<Test>]
    let ``should join [1..3] to '1, 2, 3' with separator ', '`` () =
        [1..3] |> join ", " |> should equal "1, 2, 3"

    [<Test>]
    let ``should return empty string from empty list`` () =
        [] |> join ", " |> should equal System.String.Empty