namespace chapter05.nunit

module _1232OS_03_07 =
    
    // find date of midsummer eve in Sweden
    let midsummerEve year = 
        // get a day in June
        let dateInJune day = new System.DateTime(year, 6, day)
        // is specified date a friday
        let isFriday (date : System.DateTime) = date.DayOfWeek = System.DayOfWeek.Friday
        // find first friday between 19-26 june
        [19..26] |> List.map dateInJune |> List.find isFriday
        
    open NUnit.Framework

    // testing using NUnit
    [<Test>]
    let ``midsummer eve in 2014 should occur Jun 20`` () =
        // test
        let result = midsummerEve 2014

        // assert
        Assert.That(result.ToString("MMM d"), Is.EqualTo("Jun 20"))

    open FsUnit

    // testing using fsunit
    [<Test>]
    let ``midsummer eve in 2014 should occur Jun 19`` () =
        (midsummerEve 2015).ToString("MMM d") |> should equal "Jun 19"
