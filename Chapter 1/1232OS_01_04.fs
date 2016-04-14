module _1232OS_01_04
    open NUnit.Framework
    open FsUnit

    // System Under Test
    let div x y = x / y

    // Test
    (div 5 2) |> should equal 2
    (fun () -> div 5 0 |> ignore) |> should throw typeof<System.DivideByZeroException>
