namespace chapter10

module _1232OS_10_04 =

    type ICustomerService =
        abstract member GetCustomers : unit -> (int * string) list

    type IFileSystem =
        abstract member AppendLineToFile : string * string -> unit

    type ICache =
        abstract member Add : string * string -> unit
        abstract member Get : string -> string

    type CacheJob(customerService, fileSystem) =
        member this.Execute() = ()

    open NUnit.Framework
    open Rhino.Mocks

    // don't
    [<Test>]
    let ``should first get customers from customer service and then store them to hard drive`` () =
        // arrange
        let customerService = MockRepository.GenerateMock<ICustomerService>()
        let fileSystem = MockRepository.GenerateMock<IFileSystem>()
        let cacheJob = CacheJob(customerService, fileSystem)

        // setup mocks
        customerService.Expect(fun service -> service.GetCustomers()).Return([(1, "Mikael Lundin")]) |> ignore
        fileSystem.Expect(fun fs -> fs.AppendLineToFile("customer.txt", "1,Mikael Lundin")) |> ignore

        // act
        cacheJob.Execute() |> ignore
        
        // assert
        customerService.VerifyAllExpectations()
        fileSystem.VerifyAllExpectations()

    // do
    // simplify the SUT or implement a vertical slice


       
