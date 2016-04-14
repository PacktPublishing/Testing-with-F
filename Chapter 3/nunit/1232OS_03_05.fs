namespace chapter05.nunit

module _1232OS_03_05 =

    open NUnit.Framework

    [<Test>]
    let ``calculator should add 1 and 2 with result of 3`` () =
        Assert.That(3, Is.EqualTo(1 + 2))

