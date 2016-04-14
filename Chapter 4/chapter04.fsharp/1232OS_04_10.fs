namespace chapter04.fsharp

module _1232OS_04_10 =

    open FsUnit

    let result = 42

    [<NUnit.Framework.Test>]
    let ``multiple assertions`` () =
        // assertions
        result |> should equal 42
        result |> should not' (equal 43)
        result |> should (equalWithin 5) 40
        result |> should not' ((equalWithin 1) 40)
        "$12" |> should startWith "$"
        "$12" |> should endWith "12"
        "$12" |> should not' (startWith "€")
        "$12" |> should not' (endWith "14")
        [1..10] |> should contain 5
        [1; 1; 2; 3; 5; 8; 13] |> should not' (contain 7)
        let getPersonById id = failwith "id cannot be less than 0"
        (fun () -> getPersonById -1 |> ignore) |> should throw typeof<System.Exception>
        // true or false
        1 = 1 |> should be True
        1 = 2 |> should be False
        
        // strings as result
        "" |> should be EmptyString
        null |> should be NullOrEmptyString

        // null is nasty in functional programming
        [] |> should not' (be Null)

        // same reference
        let person1 = new System.Object()
        let person2 = person1
        person1 |> should be (sameAs person2)

        // not same reference, because copy by value
        let a = System.DateTime.Now
        let b = a
        a |> should not' (be (sameAs b))

        // greater and lesser
        result |> should be (greaterThan 0)
        result |> should not' (be lessThan 0)

        // of type
        result |> should be ofExactType<int>

        // list assertions
        [] |> should be Empty
        [1; 2; 3] |> should not' (be Empty)