module GameOfLife

type Cell = | Dead of x : int * y : int | Live of x : int * y : int

// get coordinate from a cell
let coord = function | Live(x, y) -> x, y | Dead(x, y) -> x, y

// get if the cell on the coordinate is live
let isLive coordinate cells = 
    cells |> List.exists (fun cell -> cell |> coord = coordinate)

// get if the cell on the coordinate is dead
let isDead coordinate cells =
    not (isLive coordinate cells)

let live = function Live(_,_) -> true | _ -> false
let dead = function Dead(_,_) -> true | _ -> false

// get all surrounding cells
let neighbours (cell : Cell) cells =
    let x, y = cell |> coord
    let neighbours = [(x, y - 1); (x, y + 1); (x - 1, y); (x + 1, y)]

    neighbours 
    |> List.map(fun coord ->
        match isLive coord cells with
        | true -> Live(coord)
        | false -> Dead(coord))
    
// is list length less than n
let moreThanOneLessThanThree (list : 'a list) = 
    list.Length > 1 && list.Length < 4

// get the next play board
let next (cells : Cell list) =
    printfn "%A" cells
    // Scenario 1: Any live cell with fewer than two live neighbours dies, as if caused by under-population.
    // Scenario 3: Any live cell with more than three live neighbours dies, as if by overcrowding.
    let oldCells =
        cells
        |> List.filter (fun cell -> (neighbours cell cells) |> List.filter live |> moreThanOneLessThanThree)
    
    // Scenario 4: Any dead cell with exactly three live neighbours becomes a live cell, as if by reproduction.
    let newCells = 
        cells
        |> List.collect(fun cell -> neighbours cell cells)
        |> List.filter dead
        |> List.filter (fun cell -> neighbours cell cells |> List.filter live |> List.length |> ((=) 3))
        |> List.map (fun cell -> Live(cell |> coord))

    oldCells @ newCells