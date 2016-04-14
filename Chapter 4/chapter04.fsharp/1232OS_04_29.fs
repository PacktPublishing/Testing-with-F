namespace chapter06.fsharp

// this domain object shouldn't really be defined in the data access module
type Email = { 
    ID : int option
    ToAddress : string; 
    FromAddress : string; 
    Subject : string; 
    Body : string 
}

module EmailDataAccess =
    
    open System
    open System.Data
    open System.Data.Linq
    open Microsoft.FSharp.Data.TypeProviders
    open Microsoft.FSharp.Linq

    type dbSchema = SqlDataConnection<"Data Source=.;Initial Catalog=Chapter06;Integrated Security=SSPI;">
    let db = dbSchema.GetDataContext()

    // Enable the logging of database activity to the console.
    db.DataContext.Log <- System.Console.Out

    // insert an e-mail to the database
    let insert (email : Email) =
        let entity = new dbSchema.ServiceTypes.Email(ToAddress = email.ToAddress,
                                                     FromAddress = email.FromAddress,
                                                     Subject = email.Subject,
                                                     Body = email.Body)
        db.Email.InsertOnSubmit(entity)
        db.DataContext.SubmitChanges()

    // get all queued e-mails from database
    let getAll () = 
        query {
            for row in db.Email do
            select { 
                ID = Some row.ID; 
                ToAddress = row.ToAddress; 
                FromAddress = row.FromAddress; 
                Subject = row.Subject; 
                Body = row.Body }
        } |> Seq.toList

    // delete an e-mail from the database
    let delete id = 
        query {
            for row in db.Email do
            where (row.ID = id)
            select row
        } |> db.Email.DeleteAllOnSubmit
        db.DataContext.SubmitChanges()
        
module EmailQueue =
    // push e-mails on the queue
    let push (daInsert : Email -> unit) email =
        daInsert email

    // pop e-mail from the queue
    let pop (daGetAll : unit -> Email list) (daDelete : int -> unit) =
        seq {
            for email in daGetAll() do
                daDelete email.ID.Value |> ignore
                yield email
        }

    open NUnit.Framework
    open FsUnit

    [<Test>]
    let ``should set an ID on e-mails that has been in queue`` () =
        let email = { 
                ID = None; 
                ToAddress = "hello@mikaellundin.name"; 
                FromAddress = "hello@mikaellundin.name"; 
                Subject = "Test message"; 
                Body = "Test body" 
            }

        // push the e-mail
        push EmailDataAccess.insert email |> ignore

        // pop the e-mail
        let popped = pop EmailDataAccess.getAll EmailDataAccess.delete |> Seq.nth(0)

        // assert
        popped.ID |> should not' (equal None)

    [<Test>]
    let ``should delete record when iterating on pop sequence`` () =
        // arrange
        let email = { 
                ID = None; 
                ToAddress = "hello@mikaellundin.name"; 
                FromAddress = "hello@mikaellundin.name"; 
                Subject = "Test message"; 
                Body = "Test body" 
            }

        // stub implementation of EmailDataAccess
        let db = System.Collections.Generic.Dictionary<int, Email>()
        let insert email = db.Add (0, { email with ID = Some 0 })
        let getAll () = db.Values |> Seq.cast<Email> |> Seq.toList
        let delete id = ignore <| db.Remove id

        // act
        push insert email |> ignore
        pop getAll delete |> Seq.nth(0) |> ignore

        // assert
        db.Count |> should equal 0