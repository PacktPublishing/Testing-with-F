namespace chapter02

module _1232OS_02_06 =
    open System.Diagnostics.Contracts

    // monad that isolates console tracing
    type TraceBuilder () =
        let trace message = printfn "%A" message

        member this.Bind(x, f) = trace x |> ignore; f x

        member this.Return(x) = trace x |> ignore; x

    let trace = TraceBuilder()

    // reverse a string
    let rec reverse = function
    | "" -> ""
    | s  -> 
        trace {
            return s.[s.Length - 1].ToString() + (reverse (s.Substring(0, s.Length - 1)))
        }

    open NUnit.Framework
    open FsUnit

    [<Test>]
    let ``should reverse 'abcde' as 'edcba'`` () =
        reverse "abcde" |> should equal "edcba"

    [<Test>]
    let ``should reverse empty string as empty string`` () =
        reverse "" |> should equal ""