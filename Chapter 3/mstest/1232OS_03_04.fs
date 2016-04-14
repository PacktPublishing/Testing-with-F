namespace mstest

module _1232OS_03_04 =

    open Microsoft.VisualStudio.TestTools.UnitTesting

    [<TestClass>]
    type CalculatorShould () = 

        [<TestMethod>]
        member this.``add 1 and 2 with result of 3`` () =
            Assert.AreEqual(3, 1 + 2)