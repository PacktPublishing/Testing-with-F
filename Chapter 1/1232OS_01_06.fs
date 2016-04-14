module _1232OS_01_06
    open System.Net
    open System.Text.RegularExpressions

    let rec crawl result url =
        // is duplicate if url exists in result
        let isDuplicate = result |> List.exists ((=) url)

        if isDuplicate then
            result
        else
            // create url
            let uri = new System.Uri(url)

            // create web client
            let client = new WebClient()

            // download html
            let html = client.DownloadString(url)

            // get all URL's
            let expression = new Regex(@"href=""(.*?)""")
            let captures = expression.Matches(html)
                          |> Seq.cast<Match>
                          |> Seq.map (fun m -> m.Groups.[1].Value)
                          |> Seq.toList

            // join result with crawling all captured urls
            List.collect (fun c -> crawl (result @ (captures |> List.filter ((=) c))) c) captures
