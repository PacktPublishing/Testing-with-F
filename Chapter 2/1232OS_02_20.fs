namespace chapter02

module _1232OS_02_20 =

    // not very optimized way of getting factors of n
    let factors n = 
        let rec _factors = function
        | 1 -> [1]
        | k when n % k = 0 -> k :: _factors (k - 1)
        | k -> _factors (k - 1)

        _factors (n / 2)

    open NUnit.Framework
    open FsUnit

    [<Test>]
    let ``factors of 10 should be 5 2 1`` () =
        factors 10 |> should equal [5; 2; 1]

    [<Test>]
    let ``factors of 21 should be 7 3 1`` () =
        factors 21 |> should equal [7; 3; 1]

    