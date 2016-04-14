namespace chapter04.fsharp

module _1232OS_04_28 =

    type Customer = { ID : int; Name : string }

    type ICustomerService =
        abstract member GetCustomers : unit -> Customer list

    type ICustomerDataAccess = 
        abstract member GetCustomer : int -> Customer option
        abstract member InsertCustomer : Customer -> unit
        abstract member UpdateCustomer : Customer -> unit

    let synchronize (service : ICustomerService) (dataAccess : ICustomerDataAccess) =
        // get customers from service
        let customers = service.GetCustomers()

        // partition into inserts and updates
        let inserts, updates = 
            customers |> List.partition (fun customer -> None = dataAccess.GetCustomer customer.ID)

        // insert all new records
        for customer in inserts do
            dataAccess.InsertCustomer customer

        // update all existing records
        for customer in updates do
            dataAccess.UpdateCustomer customer

    open NUnit.Framework
    open FsUnit
    open Rhino.Mocks

    [<Test>]
    let ``should update customers that are already in database`` () =
        // arrange
        let data = [|{ ID = 1; Name = "John Doe" }; { ID = 2; Name = "Jane Doe" }|]
        
        let customerService = MockRepository.GenerateMock<ICustomerService>();
        let customerDataAccess = MockRepository.GenerateMock<ICustomerDataAccess>();

        // setup getting data
        customerService.Expect(fun service -> service.GetCustomers()).Return(data |> Seq.toList);

        // setup try getting customers from database
        customerDataAccess.Expect(fun da -> da.GetCustomer 1).Return(Some data.[0])
        customerDataAccess.Expect(fun da -> da.GetCustomer 2).Return(None)

        // act
        synchronize customerService customerDataAccess

        // assert
        customerDataAccess.AssertWasCalled(fun da -> da.UpdateCustomer(data.[0]))
        customerService.VerifyAllExpectations()
        customerDataAccess.VerifyAllExpectations()
        
        

