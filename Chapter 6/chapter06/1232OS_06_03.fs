namespace chapter06

module _1232OS_06_03 =

    open System.Configuration
    open OpenQA.Selenium
    open OpenQA.Selenium.Internal
    open OpenQA.Selenium.Chrome
    open NUnit.Framework

    // represents <a href="">text</a>
    type Link = { Href : string; Text : string }
        
    // helper method to create a link from IWebElement
    type Link with
        static member Create (element : IWebElement) =
            { Href = element.GetAttribute("href"); Text = element.Text }

    // wrapping up list of links into a Navigation type
    type Navigation = Link list

    // represents the start page
    type StartPage (browser : IWebDriver) =
        let urlPart = "/"
        let baseUrl = ConfigurationManager.AppSettings.["BaseUrl"]

        let navigationClassName = "navigation"
        let navigationLinkXPath = "//li/a"

        // move the browser session to the start page
        member this.NavigateTo () =
            browser.Navigate().GoToUrl(baseUrl + urlPart)

        // the main navigation on the page
        member this.Navigation : Navigation =
            let navigationElement = browser.FindElement(By.ClassName(navigationClassName))
            let linkElements = navigationElement.FindElements(By.XPath(navigationLinkXPath))
            [ for linkElement in linkElements -> Link.Create(linkElement) ]

    [<Test>]
    let ``my blog should have a link to twitter in main navigation`` () =
        // open browser
        use browser = new ChromeDriver()

        // create a start page
        let startPage = new StartPage(browser)

        // navigate to page
        startPage.NavigateTo()

        // find navigation
        let navigation = startPage.Navigation

        // has link to twitter
        let hasLinkToTwitter = navigation |> List.exists (fun link -> link.Href.Contains("twitter"))

        // assert
        Assert.True(hasLinkToTwitter)