namespace chapter04.fsharp

module _1232OS_04_16 =

    // type Customer is defined in utils.fs

    type IDataAccess =
        abstract member GetCustomerByID : int -> Customer
        abstract member FindCustomerByName : string -> Customer option
        abstract member UpdateCustomerName : int -> string -> Customer
        abstract member DeleteCustomerByID : int -> bool

    type InMemoryDataAccess() =
        let data = new System.Collections.Generic.Dictionary<int, Customer>()

        // expose the add method
        member this.Add customer = data.Add(customer.ID, customer)

        interface IDataAccess with 
            // throw exception if not found
            member this.GetCustomerByID id = 
                data.[id]

            member this.FindCustomerByName fullName = 
                data.Values |> Seq.tryFind (fun customer -> customer.FullName = fullName)

            member this.UpdateCustomerName id fullName =
                data.[id] <- { data.[id] with FullName = fullName }
                data.[id]

            member this.DeleteCustomerByID id =
                data.Remove(id)

    open NUnit.Framework
    open FsUnit
    open Swensen.Unquote

    [<Test>]
    let ``should get a customer by its ID`` () =
        // arrange
        let customer = { ID = 1; FullName = "Mikael Lundin" }
        let dataAccess = new InMemoryDataAccess()
        dataAccess.Add customer

        // act & assert
        (dataAccess :> IDataAccess).GetCustomerByID 1 |> should equal customer

    [<Test>]
    let ``should find customer by full name`` () =
        // arrange
        let customer = { ID = 1; FullName = "Mikael Lundin" }
        let dataAccess = new InMemoryDataAccess()
        dataAccess.Add customer

        // act & assert
        (dataAccess :> IDataAccess).FindCustomerByName customer.FullName |> should equal (Some customer)

    [<Test>]
    let ``should return None when customer is not found by name`` () =
        // arrange
        let customer = { ID = 1; FullName = "Mikael Lundin" }
        let dataAccess = new InMemoryDataAccess()
        dataAccess.Add customer

        // act & assert
        (dataAccess :> IDataAccess).FindCustomerByName "Another Name" |> should equal None
        
    [<Test>]
    let ``should return updated record when updating customer name`` () =
        // arrange
        let customer = { ID = 1; FullName = "Mikael Lundin" }
        let dataAccess = new InMemoryDataAccess()
        dataAccess.Add customer           

        // act
        let result = (dataAccess :> IDataAccess).UpdateCustomerName customer.ID "Guy Fawkes"

        // assert
        let updatedCustomer = (dataAccess :> IDataAccess).GetCustomerByID customer.ID
        result.FullName |> should equal "Guy Fawkes"
        result |> should be (sameAs updatedCustomer)

    [<Test>]
    let ``cannot get customer after it has been deleted`` () =
        // arrange
        let customer = { ID = 1; FullName = "Mikael Lundin" }
        let dataAccess = new InMemoryDataAccess()
        dataAccess.Add customer

        // act
        ignore <| (dataAccess :> IDataAccess).DeleteCustomerByID customer.ID

        // assert
        raises<exn> <@ (dataAccess :> IDataAccess).GetCustomerByID customer.ID @>