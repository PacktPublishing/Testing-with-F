namespace chapter02

module _1232OS_02_25 =

    // example record type
    type Link =
        { Title : string;
          Url : string }

    let link = { Title = "Whatever"; Url = "http://blog.mikaellundin.name" }
    let revised = { link with Title = "Mikael Lundin's weblog" }