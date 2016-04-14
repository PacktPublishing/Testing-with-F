namespace chapter04.fsharp

type Customer = { ID : int; FullName : string }

// stub code
module db =
    let getCustomerByID id = { ID = id; FullName = "Mikael Lundin" }

