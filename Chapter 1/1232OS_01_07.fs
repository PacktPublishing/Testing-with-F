module _1232OS_01_07
    open System.Net
    open System.Text.RegularExpressions

    // item exist in list -> true
    let isDuplicate result url = List.exists ((=) url) result

    // return html for url
    let getHtml url = (new WebClient()).DownloadString(new System.Uri(url))

    // extract a-tag hrefs from html
    let getUrls html = Regex.Matches(html, @"href=""(.*?)""")
                        |> Seq.cast<Match> 
                        |> Seq.map (fun m -> m.Groups.[1].Value) 
                        |> Seq.toList

    // return list except item
    let except item list = List.filter ((=) item) list

    // merge crawl of urls with result
    let merge crawl result urls = List.collect (fun url -> crawl (result @ (urls |> except url)) url) urls

    // crawl url unless already crawled it
    let rec crawl result url =
        if isDuplicate result url then
            result
        else
            (getHtml url) |> getUrls |> merge crawl result