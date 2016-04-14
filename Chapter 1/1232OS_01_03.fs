module _1232OS_01_03
    open NUnit.Framework
    open FsUnit

    // System Under Test
    let div y x = x / y

    // Test
    (div 10 2 ) |> should equal 5
