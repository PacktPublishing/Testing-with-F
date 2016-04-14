module _1232OS_04_11
    
    open FsUnit
    open NUnit.Framework.Constraints
    open System.Text.RegularExpressions

    // NUnit: implement a new assert
    type MatchConstraint(n) =
        inherit Constraint() with
            override this.WriteDescriptionTo(writer : MessageWriter) : unit =
                writer.WritePredicate("matches")
                writer.WriteExpectedValue(sprintf "%s" n)
            override this.Matches(actual : obj) =
                match actual with
                | :? string as input -> Regex.IsMatch(input, n)
                | _ -> failwith "input must be of string type"
            
    let match' n = MatchConstraint(n)

    open NUnit.Framework

    [<Test>]
    let ``NUnit custom assert`` () =
        "2014-10-11" |> should match' "\d{4}-\d{2}-\d{2}"
        "11/10 2014" |> should not' (match' "\d{4}-\d{2}-\d{2}")