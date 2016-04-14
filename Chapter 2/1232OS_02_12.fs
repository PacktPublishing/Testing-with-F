namespace chapter02

module _1232OS_02_12 =

    open System.Collections.Generic

    type MemoizeBuilder () =   
        let cache = new Dictionary<_,_>()
       
        member this.Bind(x, f) = f x

        member this.Return(f) = fun n ->
            match cache.TryGetValue(n) with
            | true, value -> value
            | _ -> let value = f n
                   cache.Add(n, value)
                   value

    let memoize = new MemoizeBuilder()

    let rec fibonacci n =
        printfn "fibonacci(%d)" n
        match n with
        | n when n <= 2 -> 1
        | n -> fibonacci (n - 1) + fibonacci (n - 2)

    let rec fibonacciMemoized =
        memoize { return fun n ->
            printfn "fibonacci(%d)" n
            match n with
            | n when n <= 2 -> 1
            | n -> fibonacciMemoized (n - 1) + fibonacciMemoized (n - 2)
        }

    let rec fibonacciMemoizedInternal = 
        let cache = new Dictionary<_,_>()

        fun n -> 
            if (cache.ContainsKey(n)) then
                cache.[n]
            else                
                printfn "fibonacci(%d)" n
                match n with
                | n when n <= 2 -> cache.Add(n, 1); 1
                | n -> let result = fibonacciMemoizedInternal (n - 1) + fibonacciMemoizedInternal (n - 2)
                       cache.Add(n, result)
                       result