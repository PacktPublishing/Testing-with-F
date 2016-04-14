namespace chapter04.fsharp

module _1232OS_04_06 =

    // get the items from l1 that are not in l2
    let difference l1 l2 = (set l1) - (set l2) |> Set.toList

    open NUnit.Framework

    [<Test>]
    let ``difference between [1..10] and [1; 3; 5; 7; 9] is [2; 4; 6; 8; 10]`` () =
        // arrange        
        let l1 = [1..10]
        let l2 = [1; 3; 5; 7; 9]

        // act
        let result = difference l1 l2

        // assert
        Assert.That(result, Is.EqualTo([2; 4; 6; 8; 10]))

    [<Test>]
    let ``difference between [1..10] and [] is [1..10]`` () =
        // arrange
        let l1 = [1..10]
        let l2 = []

        // act
        let result = difference l1 l2

        // assert
        Assert.That(result, Is.EqualTo([1..10]))
