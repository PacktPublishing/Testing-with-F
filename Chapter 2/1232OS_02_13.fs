namespace chapter02

module _1232OS_02_13 =

    open System.Text.RegularExpressions

    // matching input to pattern and return matching values
    let (|Matches|) (pattern : string) (input : string) =
        let matches = Regex.Matches(input, pattern)
        [for m in matches -> m.Value]

    // parse a serial key and concat the parts
    let parseSerialKey = function
    | Matches @"\w{5}" values -> System.String.Join("", values)

