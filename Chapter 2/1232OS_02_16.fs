namespace chapter02

module _1232OS_02_16 =
       
    type PaymentStatus = | Unpaid | Paid | Overpaid | PartlyPaid
    type Order = { id : string; paymentStatus : PaymentStatus }

    // update payment status in database
    let updatePaymentStatus order status =
        (fun (save : Order -> Order)  -> 
            let newOrder = { order with paymentStatus = status }
            save newOrder
        )

    open NUnit.Framework
    open FsUnit

    [<Test>]
    let ``should save an order with updated status`` () =
        // setup
        let order = { id = "12890"; paymentStatus = Unpaid}
        let nothing = (fun arg -> arg)

        // test
        let persistedOrder = updatePaymentStatus order Paid nothing

        // assert
        persistedOrder.paymentStatus |> should equal Paid


