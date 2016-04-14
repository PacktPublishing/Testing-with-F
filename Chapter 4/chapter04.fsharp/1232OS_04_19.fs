namespace chapter04.fsharp

module Prime =

    // cache
    let mutable cache = [2]

    // sieve [2..10] -> [2; 3; 5; 7]
    let rec sieve = function
    | [] -> []
    | hd :: tl -> hd :: sieve(tl |> List.filter (fun x -> x % hd > 0))

    // expand [2] 10 -> [2; 3; 5; 7]
    let expand input n =
        let max = input |> List.max
        if n <= max then
            // no expansion
            input
        else
            // expand and recalculate
            input @ [max + 1..n] |> sieve
    
    // lessThanHalfOf 10 4 -> true    
    let lessOrHalfOf n = (fun x -> x <= (n / 2))

    // returns that n is evenly divisible to number
    let evenlyDivisible n = (fun x -> n % x = 0)

    // isPrime 13 -> true
    let isPrime n = 
        // update sieve
        cache <- expand cache (n / 2)
        // not evenly divisible by any number in sieve
        cache 
            |> Seq.takeWhile (lessOrHalfOf n)
            |> Seq.exists (evenlyDivisible n)
            |> not

    // primeFactors 26 -> [2; 13]
    let primeFactors n =
        // update sieve
        cache <- expand cache (n / 2)
        // all evenly divisible by n
        cache
            |> Seq.takeWhile (lessOrHalfOf n)
            |> Seq.filter (evenlyDivisible n)
            |> Seq.toList