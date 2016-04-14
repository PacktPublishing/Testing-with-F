namespace chapter04.fsharp

module _1232OS_04_15 =
    
    let cache = new System.Collections.Generic.Dictionary<int,obj>()

    let getCustomerFullNameByID id =
        if cache.ContainsKey(id) then
            (cache.[id] :?> Customer).FullName
        else
            // get from database
            // NOTE: stub code
            let customer = db.getCustomerByID id
            cache.[id] <- customer
            customer.FullName

