namespace chapter06

module _1232OS_06_04 =

    open OpenQA.Selenium
    open OpenQA.Selenium.Internal
    open OpenQA.Selenium.PhantomJS
    open NUnit.Framework
    open FsUnit

    [<Test>]
    let ``homepage should have 'Mikael Lundin' in the title`` () =
        // arrange
        use browser = new PhantomJSDriver()
        
        // act
        browser.Navigate().GoToUrl("http://mikaellundin.name")
        
        // assert
        browser.Title |> should contain "Mikael Lundin"

