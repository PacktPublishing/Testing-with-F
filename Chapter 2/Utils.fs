namespace chapter02

module Utils =

    // computational expression 
    type TimeBuilder () =
        let watch = new System.Diagnostics.Stopwatch()
        member this.Watch = watch

        member this.Bind(x, f) =
            watch.Start()
            let result = f x
            watch.Stop()
            result

        member this.Return(x) = x

