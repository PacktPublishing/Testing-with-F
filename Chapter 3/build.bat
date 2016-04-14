@ECHO OFF
.nuget\nuget.exe restore .nuget\packages.config -PackagesDirectory packages\ -Source "https://www.nuget.org/api/v2/"
packages\FAKE.3.5.5\tools\FAKE.exe build.fsx
