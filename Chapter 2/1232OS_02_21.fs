namespace chapter02

module _1232OS_02_21 =

    // map example
    let double = (*) 2
    let numbers = [1..5] |> List.map double

    // val numbers : int list = [2; 4; 6; 8; 10]

    // fold example
    let separateWithSpace = sprintf "%O %d"
    let joined = [1..5] |> List.fold separateWithSpace "Joined:"

    // val joined : string = "Joined: 1 2 3 4 5"

    // partition example
    let overSixteen = fun x -> x > 16
    let youthPartition = [10..23] |> List.partition overSixteen 

    // val youthPartition : int list * int list =
    //   ([17; 18; 19; 20; 21; 22; 23], [10; 11; 12; 13; 14; 15; 16])

    // reduce example
    let lesser a b = if a < b then a else b
    let min = [6; 34; 2; 75; 23] |> List.reduce lesser

    // val min : int = 2

    // fold2 example
    let multiplier = [for i in [1..12] -> (i % 2) + 1] // 2; 1; 2; 1...
    let multiply acc a b = acc + (a * b)
    let luhn ssn = (List.fold2 multiply 0 ssn multiplier) % 10

    let result = luhn [1; 9; 3; 8; 0; 8; 2; 0; 9; 0; 0; 5]

    // val result : int = 0
    
    