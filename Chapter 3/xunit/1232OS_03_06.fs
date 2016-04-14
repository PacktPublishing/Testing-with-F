namespace chapter05.xunit

module _1232OS_03_06 =

    open Xunit

    [<Fact>]
    let ``calculator should add 1 and 2 with result of 3`` () =
        Assert.Equal(3, 1 + 2)