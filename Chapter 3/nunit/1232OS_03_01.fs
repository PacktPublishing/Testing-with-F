namespace chapter05.nunit

module _1232OS_03_01 =
    
    open System

    // is the year a leap year?
    let isLeapYear year =
        try
            let date = DateTime(year, 2, 29)
            true
        with
            | :? ArgumentOutOfRangeException -> false

    open NUnit.Framework
    open FsUnit

    [<Test>]
    let ``2014 should not be a leap year`` () =
        isLeapYear 2014 |> should not' (equal true)

    [<Test>]
    let ``2016 should be a leap year`` () =
        isLeapYear 2016 |> should equal true
