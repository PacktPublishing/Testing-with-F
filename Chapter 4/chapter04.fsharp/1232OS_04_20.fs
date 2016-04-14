namespace chapter04.fsharp

module _1232OS_04_20 =

    open NUnit.Framework
    open FsUnit

    [<Test>]
    let ``should evaluate 23 as a prime number`` =
        Prime.isPrime 23 |> should be True

    [<Test>]
    let ``should evaluate prime factors of 26 as 2 and 13`` =
        Prime.primeFactors 26 |> should equal [2; 13]

