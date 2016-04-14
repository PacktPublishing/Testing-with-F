namespace chapter02

module _1232OS_02_15 =
    
    type PaymentStatus = | Unpaid | Paid | Overpaid | PartlyPaid
    type Order = { id : string; paymentStatus : PaymentStatus }

    // interface that describes the function to persist an order
    type IPersistOrder = 
        abstract member Save: Order -> Order

    // update payment status in database
    let updatePaymentStatus order status =
        (fun (dataAccess : IPersistOrder) -> 
            let newOrder = { order with paymentStatus = status }
            dataAccess.Save(newOrder)
        )

    open NUnit.Framework
    open FsUnit

    type StubPersistOrder () = 
        interface IPersistOrder with
            member this.Save m = m

    [<Test>]
    let ``should save an order with updated status`` () =
        // setup
        let order = { id = "12890"; paymentStatus = Unpaid}
        let orderDA = new StubPersistOrder()

        // test
        let persistedOrder = updatePaymentStatus order Paid orderDA

        // assert
        persistedOrder.paymentStatus |> should equal Paid
    

