module GameOfLifeDefinitions

open TickSpec
open NUnit.Framework
open Microsoft.FSharp.Reflection

open GameOfLife

let mutable cell = Dead(0, 0)
let mutable cells = []
let mutable result = []

let [<Given>] ``a (live|dead) cell`` = function
    | "live" -> cell <- Live(0, 0)
    | "dead" -> cell <- Dead(0, 0)
    | _ -> failwith "expected: dead or live"

let [<Given>] ``has (\d) live neighbours?`` (x) = 
    let rec _internal x =        
        match x with
        | 0 -> [cell]
        | 1 -> Live(-1, 0) :: _internal (x - 1)
        | 2 -> Live(1, 0) :: _internal (x - 1)
        | 3 -> Live(0, -1) :: _internal (x - 1)
        | 4 -> Live(0, 1) :: _internal (x - 1)
        | _ -> failwith "expected: 4 >= neighbours >= 0"
    cells <- _internal x

let [<When>] ``turn turns`` () =
    result <- GameOfLife.next cells

let [<Then>] ``the cell (dies|lives)`` = function    
    | "dies" -> Assert.True(GameOfLife.isDead (0, 0) result, "Expected cell to die")
    | "lives" -> Assert.True(GameOfLife.isLive (0, 0) result, "Expected cell to live")
    | _ -> failwith "expected: dies or lives"