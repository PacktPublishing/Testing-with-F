namespace chapter02

module _1232OS_02_14 =

    open System.Net
    open System.Text.RegularExpressions

    // get string form url, pass to success function or run failure function
    let webget_bad (url : string) success failure =
        let client = new System.Net.WebClient()

        try
            try
                // get content from web request
                let content = client.DownloadString(url)

                // match all href attributes in html code
                let matches = Regex.Matches(content, "href=\"(.+?)\"")

                // get all matching groups
                [for m in matches -> m.Groups.[1].Value]
            with
                | ex -> failure(ex)
        finally
            client.Dispose()

    // get string form url, pass to success function or run failure function
    let webget (url : string) success failure =
        let client = new System.Net.WebClient()

        try
            try
                let content = client.DownloadString(url)
                success(content)
            with
                | ex -> failure(ex)
        finally
            client.Dispose()

    // success: parse links from html
    let parseLinks html =
        let matches = Regex.Matches(html, "href=\"(.+?)\"")
        [for m in matches -> m.Groups.[1].Value]

    // failure: print exception message and return empty list
    let printExceptionMessage (ex : System.Exception) =
        printfn "Failed: %s" ex.Message; []
        
    // webget "http://blog.mikaellundin.name" parseLinks printExceptionMessage