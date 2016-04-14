module _1232OS_01_02
    open NUnit.Framework
    open FsUnit

    // System Under Test
    let div x y = x / y

    // Test
    div 10 2 |> should equal 5
