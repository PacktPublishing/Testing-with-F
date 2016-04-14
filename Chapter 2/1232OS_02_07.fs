namespace chapter02

module _1232OS_02_07 =

    // imperative programming
    let timesTwo_imperative numbers =
        let doubled = System.Collections.Generic.List<int>()

        for number in numbers do
            let double = number * 2
            doubled.Add(double)

        doubled
        
    // declarative programming
    let timesTwo_declarative numbers =
        let double = (*) 2
        Seq.map double numbers