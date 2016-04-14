namespace chapter04.fsharp

module _1232OS_04_18 =

    // implementation detail
    let sb = System.Text.StringBuilder()

    // concat values of a list
    // BAD CODE don't do this
    let concat (separator : string) (items : string list) =
        // clear from any previous concatenations
        sb.Clear()

        // append all values to string builder
        for item in items do
            sb.Append(item) |> ignore
            sb.Append(separator)
        
        // remove last separator
        if not items.IsEmpty then
            sb.Remove(sb.Length - separator.Length, separator.Length) |> ignore

        sb.ToString()

    open NUnit.Framework
    open FsUnit

    [<Test>]
    let ``should remove last separator from result`` () =
        let data = ["The"; "quick"; "brown"; "fox"; "jumps"; "over"; "the"; "lazy"; "dog"]
        concat " " data |> should not' (endWith " ")

    [<Test>]
    let ``should clear the string builder before second concatenation`` () =
        let data = ["The"; "quick"; "brown"; "fox"; "jumps"; "over"; "the"; "lazy"; "dog"]
        concat " " data |> ignore
        concat " " data |> ignore
        sb.ToString() |> should equal "The quick brown fox jumps over the lazy dog"

    // Not thread safe
    // let data = ['a'..'z'] |> List.map (fun c -> c.ToString())
    // Array.Parallel.init 51 (fun _ -> concat "" data)

module _1232OS_04_18_2 =
    
    open NUnit.Framework
    open FsUnit

    // no longer in use
    let sb = System.Text.StringBuilder()

    // concat values of a list
    let concat separator = List.reduce (fun s1 s2 -> sprintf "%s%s%s" s1 separator s2)

    [<Test>]
    let ``should remove last separator from result`` () =
        let data = ["The"; "quick"; "brown"; "fox"; "jumps"; "over"; "the"; "lazy"; "dog"]
        concat " " data |> should not' (endWith " ")

    [<Test>]
    let ``should clear the string builder before second concatenation`` () =
        let data = ["The"; "quick"; "brown"; "fox"; "jumps"; "over"; "the"; "lazy"; "dog"]
        concat " " data |> ignore
        concat " " data |> ignore
        sb.ToString() |> should equal "The quick brown fox jumps over the lazy dog"

    open Swensen.Unquote

    [<Test>]
    let ``cannot concatenate with null separator`` () =
        raises<exn> <@ concat null ["a"; "b"] @>

    [<Test>]
    let ``should return empty string for empty list`` () =
        concat " " List.Empty |> should equal ""

    [<Test>]
    let ``should return item from a one item list`` () =
        concat " " ["a"] |> should equal "a"

    [<Test>]
    let ``should return concatenated result from string list with empty separator`` () =
        concat System.String.Empty ["a"; "b"; "c"] |> should equal "abc"

    [<Test>]
    let ``should be able to form a comma separated value`` () =
        concat ", " ["a"; "b"; "c"] |> should equal "a, b, c"