namespace ``selection sort``

open chapter11.sorting

module ``sorting a list of numbers`` =

    open FsCheck
    open FsCheck.Xunit

    [<Property>]
    let ``once is same as twice`` (list : int list) =
        (sort list) = sort (sort list)

    [<Property>]
    let ``first item is the smallest`` (list : int list) =
        (not list.IsEmpty) ==> 
            lazy(List.min list = List.head (sort list))

    [<Property>]
    let ``last item is the largest`` (list : int list) =
        (not list.IsEmpty) ==> 
            lazy(List.max list = List.head (List.rev (sort list)))

    [<Property>]
    let ``has same numbers as original`` (list : int list) =
        let exists item = List.exists ((=) item)
        let sorted = sort list

        (sorted.Length = list.Length)
            |@ "same length" .&.
        (sorted |> List.forall (fun x -> list |> (exists x)))
            |@ "all elements exists"

    [<Property>]
    let rec ``is ordered result`` (list : int list) = 
        let rec _ordered = function
        | [] -> true
        | hd :: [] -> true
        | fst :: snd :: tl -> (fst <= snd) && (_ordered (snd :: tl))

        _ordered (sort list)