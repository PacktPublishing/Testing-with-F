namespace chapter04
module _1232OS_04_30 =

    type Customer = { 
        ID : int
        Name : string 
    }

    let compareByID (c1 : Customer) (c2 : Customer) =
        -1 * c1.ID.CompareTo(c2.ID)
        
    open NUnit.Framework
    open FsUnit

    // using currying to set the stage
    let compareWithFive customer =
        compareByID customer { ID = 5; Name = "Mikael Lundin" }

    [<Test>]
    let ``customer 3 is more than customer 5`` () =
        compareWithFive { ID = 3; Name = "John James" } |> should equal 1

    [<Test>]
    let ``customer 7 is less than customer 5`` () =
        compareWithFive { ID = 7; Name = "Milo Miazaki" } |> should equal -1

    [<Test>]
    let ``customer 5 is equal to customer 5`` () =
        compareWithFive { ID = 5; Name = "Mikael Lundin" } |> should equal 0