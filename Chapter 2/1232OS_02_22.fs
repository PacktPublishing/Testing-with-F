namespace chapter02

module _1232OS_02_22 =

    // For multiples of three print "Fizz" instead of the number
    // and for multiples of five print "Buzz"
    // for multiples of both write "Fizzbuzz"
    let fizzbuzz =
        let rec _fizzbuzz n =
            seq {
                match n with
                | n when n % 15 = 0 -> yield "Fizzbuzz"
                | n when n % 3 = 0  -> yield "Fizz"
                | n when n % 5 = 0 -> yield "Buzz"
                | n -> yield n.ToString()
                
                yield! _fizzbuzz (n + 1)
            }

        _fizzbuzz 1

    open NUnit.Framework
    open FsUnit

    [<Test>]
    let ``should verify the first 15 computations of the fizzbuzz sequence`` () =
        fizzbuzz 
            |> Seq.take 15 
            |> Seq.reduce (sprintf "%s %s") 
            |> should equal "1 2 Fizz 4 Buzz Fizz 7 8 Fizz Buzz 11 Fizz 13 14 Fizzbuzz"

