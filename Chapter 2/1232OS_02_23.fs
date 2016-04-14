namespace chapter02

module _1232OS_02_23 =

    open System.Net

    // get url content and transform it
    let webGet transformer urls =
        let _download url =
            let webClient = new WebClient()

            try
                let uri = new System.Uri(url)
                webClient.DownloadString(uri)
                
            finally
                webClient.Dispose()

        List.map (_download >> transformer) urls

    open NUnit.Framework
    open FsUnit
    open System.Text.RegularExpressions

    [<Test>]
    let ``should download and get outgoing URLs`` () =
        let urls = [
            "http://blog.mikaellundin.name/2014/08/31/how-to-fix-your-sony-pulse-headset.html";
            "http://blog.mikaellundin.name/2014/05/02/tdd-is-not-dead.html";
            "http://blog.mikaellundin.name/2014/04/25/bugs-and-defects.html"]

        let getLinks content =
            let matches = Regex.Matches(content, @"href=""(.+?)""")
            [for m in matches -> m.Groups.[1].Value]

        // get all links from urls
        let links = webGet getLinks urls |> List.collect (fun n -> n)
        
        // assert
        links |> should contain "//mikaellundin.name"
