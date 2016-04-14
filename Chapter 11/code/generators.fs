namespace chapter11

module generators =
    
    let fizzbuzz = function
    | d when d % 15 = 0 -> "fizzbuzz"
    | d when d % 3 = 0 -> "fizz"
    | d when d % 5 = 0 -> "buzz"
    | d -> sprintf "%d" d



