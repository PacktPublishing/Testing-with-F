module Program

    open canopy
    open runner
    open System

    [<EntryPoint>]
    let main argv = 

        //start an instance of the firefox browser
        start firefox

        // grouping for tests
        context "Blog start page"

        // define a test
        "blog title should contain Mikael Lundin" &&& fun _ ->
            // navigate
            url "http://blog.mikaellundin.name"

            // assert
            contains "MIKAEL LUNDIN" (read "#blog-title")

        // define a test
        "number of post excerpts should be 5" &&& fun _ ->
            // navigate
            url "http://blog.mikaellundin.name"

            // assert
            count ".post-excerpt" 5

        //run all tests
        run()

        // close browser window
        quit()

        // return an integer exit code
        0
