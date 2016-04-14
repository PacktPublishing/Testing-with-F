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

    // insert an e-mail to the database
    val insert : Email -> unit

    // get all queued e-mails from database
    val getAll : unit -> Email list

    // delete an e-mail from the database
    val delete : int -> unit
