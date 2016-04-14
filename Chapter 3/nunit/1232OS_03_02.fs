namespace chapter05.nunit

module _1232OS_03_02 =

    open Newtonsoft.Json
    let toJson = JsonConvert.SerializeObject

    open NUnit.Framework
    open FsUnit

    type Vote = { Name : string; Vote : int }

    [<Test>]
    let ``should serialize vote into json`` () =
        let vote = { Name = "Mikael Lundin"; Vote = 5 }
        vote |> toJson |> should equal @"{""Name"":""Mikael Lundin"",""Vote"":5}"
