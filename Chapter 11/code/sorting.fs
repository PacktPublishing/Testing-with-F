namespace chapter11

module sorting =
    // remove first occurance of item from list
    let rec remove item = function
    | [] -> []
    | hd :: tl when hd = item -> tl
    | hd :: tl -> hd :: (remove item tl)

    // naive implementation of selection sort
    let rec sort = function
        | [] -> []
        | l ->  let value = (List.min l)
                value :: (sort (l |> remove value))

    open FsUnit
    open NUnit.Framework

    [<Test>]
    let ``should sort [1; 3; 5; 3; 1] as [1; 1; 3; 3; 5]`` () =
        sort [1; 3; 5; 3; 1] |> should equal [1; 1; 3; 3; 5]

