namespace chapter04.fsharp.xunit

module _1232OS_04_12 =

    open System.Text.RegularExpressions
    open NHamcrest
    open NHamcrest.Core        

    // test assertion for regular expression matching
    let match' pattern = 
        CustomMatcher<obj>(sprintf "Matches %s" pattern, fun c -> 
            match c with
            | :? string as input -> Regex.IsMatch(input, pattern)
            | _ -> false)

    open Xunit
    open FsUnit.Xunit

    [<Fact>]
    let ``Xunit custom assert`` () =
        "2014-10-11" |> should match' "\d{4}-\d{2}-\d{2}"
        "11/10 2014" |> should not' (match' "\d{4}-\d{2}-\d{2}")