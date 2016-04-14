namespace chapter04.fsharp

module _1232OS_04_17 =
    
    open chapter04.fsharp._1232OS_04_16

    // stub, this would be the real implementation
    type DefaultDataAccess() =
        interface IDataAccess with 
            member this.GetCustomerByID id = { ID = 1; FullName = "Mikael Lundin" }
            member this.FindCustomerByName fullName = Some ({ ID = 1; FullName = "Mikael Lundin" })
            member this.UpdateCustomerName id fullName = { ID = 1; FullName = "Mikael Lundin" }
            member this.DeleteCustomerByID id = true
    
    open System.Configuration
    open System.Collections.Specialized

    // TryGetValue extension method to NameValueCollection
    type NameValueCollection with
        member this.TryGetValue (key : string) =
            if this.Get(key) = null then
                None
            else
                Some (this.Get key)

    let dataAccess : IDataAccess =
        match ConfigurationManager.AppSettings.TryGetValue("DataAccess") with
        | Some "InMemory" -> new InMemoryDataAccess() :> IDataAccess
        | Some _ | None -> new DefaultDataAccess() :> IDataAccess
        
    // usage
    let fullName = (dataAccess.GetCustomerByID 1).FullName