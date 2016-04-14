namespace chapter11

module brains =

    type Button (value : int) =
        new () = Button(0)
        member __.Click () = Button((value % 4) + 1)
        member __.Value = value
        override __.ToString () = value.ToString()

    type Board (width : int, height : int, buttons :  Map<int*int, Button>) as x =

        let removeButton x y =
            buttons.Remove(x, y)
        
        // overloading constructor
        new (width : int, height : int) = Board(width, height, Map.empty)
        
        // get some button
        member __.Get x y =
            match buttons |> Map.tryFind (x, y) with
            | Some(button) -> Some(x, y, button)
            | None -> None

        // find surrounding buttons
        member this.Surrounding x y =
            [(x - 1, y); (x + 1, y); (x, y - 1); (x, y + 1)]
            |> List.choose (fun (x1, y1) -> this.Get x1 y1)

        member this.AddOrReplace x y button =
            match this.Get x y with
            | Some(_) -> Board(width, height, (buttons.Remove(x, y).Add((x, y), button)))
            | None -> Board(width, height, buttons.Add((x, y), button))

        // click a button
        member this.Click x y =
            let button =
                match (this.Get x y) with
                | Some(button) -> button
                | None -> (x, y, Button())

            let newButtons = button :: (this.Surrounding x y)
            List.fold (fun board (x1, y1, (button1 : Button)) -> this.AddOrReplace x1 y1 (button1.Click())) this newButtons

        override this.ToString () = 
            seq {
                for y in [0..height] do
                    yield [0..width]
                    |> List.map (fun x -> this.Get x y)
                    |> List.map (fun o -> match o with
                                          | Some(_, _, button) -> button.Value.ToString()
                                          | None -> "O")
                    |> List.reduce (sprintf "%s %s")
            }
            |> Seq.reduce (sprintf "%s\n%s")
            
            

        
