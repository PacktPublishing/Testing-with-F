namespace chapter04.fsharp

module _1232OS_04_13 =

    // example quotation
    <@ 1 + 1 @>

    // test data
    let fibonacci = [1; 1; 2; 3; 5; 8; 13]

    open NUnit.Framework
    open Swensen.Unquote

    [<Test>]
    let ``fibonacci sequence should start with 1, 1, 2, 3, 5`` () =
        test <@ fibonacci |> Seq.take 5 |> List.ofSeq = [1; 1; 2; 3; 5] @>

    // BAD CODE: really inefficient way of finding prime numbers
    let rec primes maxLimit = [2..maxLimit] |> List.filter (fun x -> 
        not (List.exists (fun y -> x % y = 0) [2..(x - 1)]))

    [<Test>]
    let ``prime numbers under 10 are 2, 3, 5, 7, 9`` () =
        test <@ primes 10 = [2; 3; 5; 7; 9] @> // fail
        