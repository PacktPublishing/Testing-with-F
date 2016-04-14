// unit test example

namespace Calculator

    module Calculator =
        let add x y = x + y


    module ``add functionality`` =

        open NUnit.Framework
        open FsUnit

        [<Test>]
        let ``should return 3 from adding 1 and 2`` () =
            Calculator.add 1 2 |> should equal 3
