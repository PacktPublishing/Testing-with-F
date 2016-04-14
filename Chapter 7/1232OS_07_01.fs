namespace code

module _1232_02_01 =

    open NUnit.Framework
    open FsUnit

    // System Under Test
    let rec fibonacci = function
        | n when n < 2 -> 1
        | n -> fibonacci (n - 2) + fibonacci (n - 1)

    let fibonacciSeq x = [0..(x - 1)] |> List.map fibonacci
    
    [<Test>]
    let returns_correct_result () = 
        fibonacciSeq 5 |> should equal [1; 1; 2; 3; 5]

    [<Test>]
    let ``should expect 1 to be the first fibonnaci number`` () = 
        fibonacci 0 |> should equal 1

    [<Test>]
    let ``should expect 1 to be the second fibonnaci number`` () = 
        fibonacci 1 |> should equal 1

    [<Test>]
    let ``should expect 5 to be the fifth fibonnaci number`` () = 
        fibonacci 4 |> should equal 5

    [<Test>]
    let ``should expect 1, 1, 2, 3, 5 to be the five first fibonnaci numbers`` () = 
        fibonacciSeq 5 |> should equal [1; 1; 2; 3; 5]