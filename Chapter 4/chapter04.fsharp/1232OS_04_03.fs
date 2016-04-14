namespace ``PriceCalculator will calculate the price of an order``

module ``for an empty order`` =
    
    open Company.System.BL.Calculators

    open NUnit.Framework

    let priceCalculator = PriceCalculator()                

    type Order = { OrderLines : string list}
    let order = { OrderLines = []}
            
    [<Test>]
    let ``the price should be 0`` () =
        let result = priceCalculator.GetPrice(order)
        Assert.That(result, Is.EqualTo(0))