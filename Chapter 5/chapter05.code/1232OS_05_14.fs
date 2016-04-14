namespace chapter05.code

module _1232OS_05_14 =

    open System
    open System.ServiceModel
    open Microsoft.FSharp.Linq
    open Microsoft.FSharp.Data.TypeProviders

    type ValidationService = WsdlService<"http://localhost:53076/ValidationService.svc?wsdl">

    // mapping the generated classes into a discriminated union
    type ValidationStatus = 
        | Valid 
        | Invalid 
        | Error of message : string
        static member Create (input : ValidationService.ServiceTypes.chapter05.code.service.ValidationResult) =
            match input.Status with
            | ValidationService.ServiceTypes.chapter05.code.service.ValidationStatus.Valid -> Valid
            | ValidationService.ServiceTypes.chapter05.code.service.ValidationStatus.Invalid -> Invalid
            | ValidationService.ServiceTypes.chapter05.code.service.ValidationStatus.Error -> Error(input.ErrorMessage)
            | _ -> failwith "Unknown validation status" 

    // wrapper that provides a functional interface
    let validateEmail (service : ValidationService.ServiceTypes.SimpleDataContextTypes.ValidationServiceClient) input =
        let result = service.ValidateEmail(input)
        match result |> ValidationStatus.Create with
        | Valid -> true
        | Invalid -> false
        | Error message -> failwith ("Validating e-mail cause following error: " + message)

    open NUnit.Framework
    open FsUnit

    [<TestCase("hello@mikaellundin.name")>]
    [<TestCase("mikael.lundin@litemedia.se")>]
    [<TestCase("mikael.lundin@valtech.se")>]
    [<TestCase("mikael.lundin@litemedia.info")>]
    let ``should validate as e-mail address`` (input) =
        // arrange
        let service = ValidationService.GetBasicHttpBinding_IValidationService()

        // act / assert
        (validateEmail service input) |> should be True

    [<TestCase("not an e-mail")>]
    [<TestCase("not@email")>]
    [<TestCase("not@an@email")>]
    [<TestCase("@notanemail")>]
    [<TestCase("notanemail@")>]
    [<TestCase("@notanemail@")>]
    let ``should not match as an e-mail address`` (input) =
        // arrange
        let service = ValidationService.GetBasicHttpBinding_IValidationService()

        // act / assert
        (validateEmail service input) |> should be False

    [<Test>]
    let ``should fail validation with e-mail is null`` () =
        // arrange
        let service = ValidationService.GetBasicHttpBinding_IValidationService()
        
        // act / assert
        (fun () -> (validateEmail service null) |> ignore) |> should throw typeof<System.Exception>


        
