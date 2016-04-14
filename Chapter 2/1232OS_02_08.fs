namespace chapter02

module _1232OS_02_08 =

    // join values together to a comma separated string
    let rec join = function
    | [] -> ""
    | hd :: [] -> hd
    | hd :: tl -> hd + ", " + (join tl)

    // [1..5] |> List.map (fun n -> n.ToString()) |> join
    // join ["1"; "2"; "3"; "4"; "5"]
    //   <- join ["2"; "3"; "4"; "5"]
    //     <- join ["3"; "4"; "5"]
    //       <- join ["4"; "5"]
    //         <- join ["5"]
    //           <- join []

    open NUnit.Framework
    open FsUnit
    open chapter02.Utils

    [<Test>]
    let ``should join [1..5] to "1, 2, 3, 4, 5"`` () =
        [1..5] |> List.map (fun n -> n.ToString()) |> join |> should equal "1, 2, 3, 4, 5"

// stack overflow is a nasty exception that is difficult to catch in a test
//    [<Test>]
//    let ``should crash at 1000000 items with stack overflow`` () =
//        let values = [1..100000] |> List.map (fun n -> n.ToString())
//        (fun () -> (join values) |> ignore) |> should throw typeof<System.StackOverflowException>