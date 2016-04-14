namespace chapter02

module _1232OS_02_18 =

    open System.Net

    // download contents to url
    let download url =
        printfn "Start: %s" url
        async {
            // create client
            let webClient = new WebClient()

            try
                let uri = System.Uri(url)

                // download string
                return! webClient.AsyncDownloadString(uri)
                
            finally
                printfn "Finish: %s" url
                webClient.Dispose()
        }


    open NUnit.Framework
    open FsUnit

    [<Test>]
    let ``should download three urls in parallel`` () =
        let baseUrl = "http://blog.mikaellundin.name"
        let paths = [
            "/2014/08/31/how-to-fix-your-sony-pulse-headset.html";
            "/2014/05/02/tdd-is-not-dead.html";
            "/2014/04/25/bugs-and-defects.html"]

        // build urls
        let urls = paths |> List.map (fun path -> baseUrl + path)

        // test & await
        let result = urls |> List.map (download) |> Async.Parallel |> Async.RunSynchronously

        // assert
        Assert.That((result |> (Seq.nth 0)), Is.StringContaining "Sony Pulse Headset")
        Assert.That((result |> (Seq.nth 1)), Is.StringContaining "Writing code is not easy")
        Assert.That((result |> (Seq.nth 2)), Is.StringContaining "Lexical Errors")