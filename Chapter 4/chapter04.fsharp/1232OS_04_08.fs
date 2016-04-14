namespace chapter04.fsharp

module _1232OS_04_08 =

    let result = 42

    open NUnit.Framework
    Assert.That(result, Is.EqualTo(42))

    open FsUnit
    result |> should equal 42