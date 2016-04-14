namespace ``difference between a and b``

module ``when a is one-to-ten and b is even numbers`` =

    open NUnit.Framework
    open chapter04.fsharp._1232OS_04_06

    // arrange
    let a = [1..10]
    let b = [2; 4; 6; 8; 10]

    [<Test>]
    let ``expected result is [1; 3; 5; 7; 9]`` () =
        // act
        let result = difference a b

        // assert
        CollectionAssert.AreEqual([1; 3; 5; 7; 9], result)

module ``when a is one-to-ten and b is one-to-ten`` =
    
    open NUnit.Framework
    open chapter04.fsharp._1232OS_04_06

    // arrange
    let a = [1..10]
    let b = [1..10]

    [<Test>]
    let ``expected result is empty List`` () =
        // act
        let result = difference a b

        // assert
        CollectionAssert.IsEmpty(result)
