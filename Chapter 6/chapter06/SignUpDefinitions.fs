module SignUpDefinitions

open TickSpec
open NUnit.Framework

let [<Given>] ``I've entered my name and agreed to terms and conditions`` () =
    // shortened for brewity: setup
    ()

[<When("I enter {0} into the e-mail field", "(.*?)")>]
let [<When>] ``I enter (.*?) into the e-mail field`` (email : string) =
    // shortened for brewity: write the e-mail into the field on the page
    printf "%A" email

[<Then("the page should let me know the registration was {0}", "(true|false)")>]
let [<Then>] ``the page should let me know the registration was (true|false)`` (success : bool) =
    // shortened for brewity: verify what the system did after entering e-mail
    if success then
        Assert.True(success);
    else
        Assert.False(success);
