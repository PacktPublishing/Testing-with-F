// define includes
#I @"packages/FAKE.3.5.5/tools/"
#r @"FakeLib.dll"
open Fake
open Fake.FileUtils
open Fake.MSBuildHelper
open Fake.MSTest

// constants
let buildDir = "./output/build"
let deployDir = "./output/deploy"
let testDir = "./output/test"
let srcDir = "."

// define targets
Target "Clean" (fun _ ->
    CleanDirs [buildDir; deployDir; testDir]
)

Target "RestorePackages" (fun _ ->
    !! (srcDir + "/**/packages.config")
        |> Seq.iter (fun config ->
            config |>
                RestorePackage (fun p ->
                    { p with
                        Sources = "https://www.nuget.org/api/v2/" :: p.Sources
                        OutputPath = "./packages"})
                )
)

Target "BuildTest" (fun _ ->
    let projectFiles = !! (srcDir + "/**/*.fsproj")
    MSBuildDebug testDir "Build" projectFiles
        |> Log "Test build: "
)

Target "Test" (fun _ ->
    // mstest
    [testDir + "/chapter03.mstest.dll"]
      |> MSTest (fun p -> p)

    // nunit
    [testDir + "/chapter03.nunit.dll"]
      |> NUnit (fun p ->
        { p with
            DisableShadowCopy = true;
            ToolPath = "./packages/NUnit.Runners.2.6.3/tools";
            OutputFile = testDir + "/chapter03.nunit-TestResults.xml"}
      )

    // xunit
    [testDir + "/chapter03.xunit.dll"]
      |> xUnit (fun p ->
        { p with
            ToolPath = "./packages/xunit.runners.1.9.2/tools/xunit.console.clr4.exe";
            ShadowCopy = false;
            HtmlOutput = true;
            XmlOutput = true;
            OutputDir = testDir
        }
      )
)

// define dependencies
"Clean"
    ==> "RestorePackages"
    ==> "BuildTest"
    ==> "Test"

// execute
RunTargetOrDefault "Test"