namespace chapter06

module _1232OS_06_02 =

    open OpenQA.Selenium
    open OpenQA.Selenium.Chrome
    open NUnit.Framework
    
    [<Test>]
    let ``my blog should have last link in main navigation to github`` () =
        // open browser
        use browser = new ChromeDriver()

        // navigate to page
        browser.Navigate().GoToUrl("http://blog.mikaellundin.name")

        // find navigation
        let navigation = browser.FindElementByClassName("navigation")

        // extract links
        let links = navigation.FindElements(By.XPath("//li/a"))

        // get last link
        let lastLink = links.[links.Count - 1]

        // should contain github in address
        Assert.That(lastLink.GetAttribute("href"), Contains.Substring("github"))







