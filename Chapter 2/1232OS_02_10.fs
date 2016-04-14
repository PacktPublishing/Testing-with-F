namespace chapter02

module _1232OS_02_10 =
    
    open System
    
    // partial active pattern to match start of string
    let (|StartsWith|_|) (p : string) (s : string) =
        if s.StartsWith(p, StringComparison.CurrentCultureIgnoreCase) then
            Some(s.Substring(p.Length))
        else
            None

    // parse a roman numeral and return its value
    let rec parse = function
    | "" -> 0
    | StartsWith "M" rest  -> 1000 + (parse rest)
    | StartsWith "CM" rest -> 900 + (parse rest)
    | StartsWith "D" rest  -> 500 + (parse rest)
    | StartsWith "CD" rest -> 400 + (parse rest)
    | StartsWith "C" rest  -> 100 + (parse rest)
    | StartsWith "XC" rest -> 90 + (parse rest)
    | StartsWith "L" rest  -> 50 + (parse rest)
    | StartsWith "XL" rest -> 40 + (parse rest)
    | StartsWith "X" rest  -> 10 + (parse rest)
    | StartsWith "IX" rest -> 9 + (parse rest)
    | StartsWith "V" rest  -> 5 + (parse rest)
    | StartsWith "IV" rest -> 4 + (parse rest)
    | StartsWith "I" rest  -> 1 + (parse rest)
    | _ -> failwith "invalid roman numeral"


    open NUnit.Framework
    open FsUnit

    [<Test>]
    let ``MCMLIV should parse as 1954`` () =
        parse "MCMLIV" |> should equal 1954

    [<Test>]
    let ``MCMXC should parse as 1990`` () =
        parse "MCMXC" |> should equal 1990

    [<Test>]
    let ``MMXIV should parse as 2014`` () =
        parse "MMXIV" |> should equal 2014
