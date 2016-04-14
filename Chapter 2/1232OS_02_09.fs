namespace chapter02

module _1232OS_02_09 =

    // join values together to a comma separated string
    let join list =
        let rec _join res = function
        | [] -> res
        | hd :: tl -> _join (res + ", " + hd) tl

        if list = List.Empty then
            ""
        else
            (_join (List.head list) (List.tail list))
        
    open NUnit.Framework
    open FsUnit

    [<Test>]
    let ``should join [1..5] to "1, 2, 3, 4, 5"`` () =
        [1..5] |> List.map (fun n -> n.ToString()) |> join |> should equal "1, 2, 3, 4, 5"
            
    [<Test>]
    let ``should be able to handle 100000 items`` () =
        let values = [1..10000] |> List.map (fun n -> n.ToString())
        (fun () -> (join values) |> ignore) |> should not' (throw typeof<System.StackOverflowException>)
            
                        