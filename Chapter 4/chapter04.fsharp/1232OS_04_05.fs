namespace ``PriceCalculator will calculate the price of an order``

module ``for an empty order `` =
    
    open Company.System.BL.Calculators
    open NUnit.Framework

    type Order = { OrderLines : string list}
            
    [<Test>]
    let ``the price should be 0`` () =
        // arrange
        let priceCalculator = PriceCalculator()
        let order = { OrderLines = []}

        // act
        let result = priceCalculator.GetPrice(order)

        // assert
        Assert.That(result, Is.EqualTo(0))