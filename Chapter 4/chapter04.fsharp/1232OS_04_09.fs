namespace chapter04.fsharp

module _1232OS_04_09 =

    [<Measure>] type EUR
    [<Measure>] type SEK
    type Country = | Sweden | Germany | France

    let calculateVat country (amount : float<'u>) = 
        match country with
        | Sweden -> amount * 0.25
        | Germany -> amount * 0.19
        | France -> amount * 0.2        


    open NUnit.Framework
    open FsUnit

    [<Test>]
    let ``Sweden should have 25% VAT`` () =
        let amount = 200.<SEK>

        calculateVat Sweden amount |> should equal 50<SEK>

    [<Test>]
    let ``Germany should have 19% VAT`` () =
        // arrange
        let amount = 200.<EUR>
        // act
        calculateVat Germany amount
        //assert
        |> should equal 38<EUR>


