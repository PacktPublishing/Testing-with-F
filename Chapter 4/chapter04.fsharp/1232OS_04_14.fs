namespace chapter04.fsharp

module _1232OS_04_14 =

    open Swensen.Unquote
    open NUnit.Framework

    [<Test>]
    let ``NUnit: asserting with Unquote`` () =
        test <@ 1 + 2 = 4 - 1 @>
        test <@ 1 + 2 <> 4 @>
        test <@ 42 < 1337 @>
        test <@ 1337 > 42 @>
        raises<System.NullReferenceException> <@ (null : string).Length @>
        raises<exn> <@ System.String.Format(null, null) @>
        <@ (1+2)/3 @> |> reduceFully |> List.map decompile
        unquote <@ [for i in 1..5 -> i * i] = ([1..5] |> List.map (fun i -> i * i)) @>

