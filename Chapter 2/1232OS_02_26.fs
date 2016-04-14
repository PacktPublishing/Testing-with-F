namespace chapter02

module _1232OS_02_26 =

    type Vector = | X | Y | Z

    type Point(x : int, y : int, z : int) =
        
        // readonly properties
        member __.X = x
        member __.Y = y
        member __.Z = z

        // update
        member this.Update value = function
        | X -> Point(value, y, z)
        | Y -> Point(x, value, z)
        | Z -> Point(x, y, value)

        override this.Equals (obj) =
            match obj with
            | :? Point as p -> p.X = this.X && p.Y = this.Y && p.Z = this.Z
            | _ -> false

        override this.ToString () = sprintf "{%d, %d, %d}" x y z
            
    open NUnit.Framework
    open FsUnit

    [<Test>]
    let ``should update X value`` () =
        Point(1, 2, 3).Update 4 X |> should equal (Point(4, 2, 3))
    
    [<Test>]
    let ``should update Y value`` () =
        Point(1, 2, 3).Update 4 Y |> should equal (Point(1, 4, 3))

    [<Test>]
    let ``should update Z value`` () =
        Point(1, 2, 3).Update 4 Z |> should equal (Point(1, 2, 4))
