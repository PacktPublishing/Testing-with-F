// integration test example

namespace BL
    type Customer = { name : string; address : string }

    module CustomerRepository =
        let save (customer : Customer) = 1

        let get id = { name = "Mikael Lundin"; address = "Drottninggatan 82 Stockholm" }

namespace ``Customer repository``
    open BL

    module ``save functionality`` =
        open NUnit.Framework
        open FsUnit

        [<Test>]
        let ``should store new user to data storage`` =
            // setup
            let newCustomer = { name = "Mikael Lundin"; address = "Drottninggatan 82 Stockholm" }

            // test, storing new customer to database
            let customerID = CustomerRepository.save newCustomer

            // assert
            let dbCustomer = CustomerRepository.get customerID
            dbCustomer |> should equal newCustomer


