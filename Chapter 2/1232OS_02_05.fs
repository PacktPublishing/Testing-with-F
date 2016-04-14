namespace chapter02

module _1232OS_02_05 =
    open System.Diagnostics.Contracts

    // reverse a string
    let rec reverse = function
    | "" -> ""
    | s  -> s.[s.Length - 1].ToString() + (reverse (s.Substring(0, s.Length - 1)))

    open NUnit.Framework
    open FsUnit

    [<Test>]
    let ``should reverse 'abcde' as 'edcba'`` () =
        reverse "abcde" |> should equal "edcba"

    [<Test>]
    let ``should reverse empty string as empty string`` () =
        reverse "" |> should equal ""