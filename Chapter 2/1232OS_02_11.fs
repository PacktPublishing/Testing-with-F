namespace chapter02

module _1232OS_02_11 =

    open System

    // parse a roman numeral and return its value
    let parse (s : string) =
        // mutable value
        let mutable input = s
        let mutable result = 0

        let romanNumerals = 
            [|("M",1000); ("CM" ,900); ("D",500); ("CD",400); ("C",100 ); 
            ("XC",90); ("L",50); ("XL",40); ("X",10 ); ("IX",9); ("V",5); 
            ("IV",4); ("I", 1)|]

        while not (String.IsNullOrEmpty input) do
            let mutable found = false
            let mutable i = 0

            // iterate over romanNumerals matching it with input string
            while (not found) || i < romanNumerals.Length do
                let romanNumeral, value = romanNumerals.[i] 

                // does input start with current romanNumeral?
                if input.StartsWith(romanNumeral, StringComparison.CurrentCultureIgnoreCase) then
                    result <- result + value
                    input <- input.Substring(romanNumeral.Length)
                    found <- true

                // iterate
                i <- i + 1

            // no roman numeral found at beginning of string
            if (not found) then
                failwith "invalid roman numeral"

        result
        
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