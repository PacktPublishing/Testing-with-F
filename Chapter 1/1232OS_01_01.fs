namespace chapter01

    module OrderDatabase =
        type Order = { Number : int; IsPaid : bool; SendInvoice : bool }
        let getAllUnpaid () = [{ Number = 1; IsPaid = false; SendInvoice = false }]
        let update order = ()

    module OrderService =
        type OrderStatus = | NotSet | Paid | Unpaid | OverPaid | PartlyPaid

        let getOrderStatus orderNumber = Paid

    module WindowsService =
        
        open OrderService

        let run () =
            // get all orders
            OrderDatabase.getAllUnpaid() 
            |> Seq.map(fun order ->

                // for each order
                let mutable returnOrder = order
                let mutable orderStatus = OrderService.NotSet

                try
                    // while status not found
                    while orderStatus = NotSet do
                        // try get order status
                        orderStatus <- OrderService.getOrderStatus order.Number

                        // set result depending on order status
                        returnOrder <- 
                            match orderStatus with
                            // paid or overpaid get correct status
                            | Paid | OverPaid -> { order with IsPaid = true }
                            // unpaid
                            | Unpaid | PartlyPaid -> { order with IsPaid = false; SendInvoice = true }
                            // unknown status, try again later
                            | _ -> returnOrder
                with
                    | _ -> printf "Unknown error"

                returnOrder)

            // update database with payment status
            |> Seq.iter (OrderDatabase.update)
