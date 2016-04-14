namespace chapter06

module _1232OS_06_05 = 

    open System.Net
    open CsQuery
    open NUnit.Framework
    open FsUnit

    [<Test>]
    let ``there should be at least 5 blog posts pushed from start page`` () =
        // open new web client
        use client = new WebClient()

        // download the html we're testing
        let html = client.DownloadString("http://blog.mikaellundin.name")

        // parse the DOM
        let dom = CQ(html)

        // query the DOM for the blog posts
        let posts = dom.[".post-excerpt"]

        // assert that number of posts > 4
        posts.Length |> should be (greaterThan 4)