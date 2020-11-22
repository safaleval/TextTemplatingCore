### build the project
dotnet build
### it creates nuget package under ./nupkg that 
### can be installed by other projects
[localtools](https://docs.microsoft.com/en-us/dotnet/core/tools/local-tools-how-to-use)
### how to create .net cli tool 
[https://docs.microsoft.com/en-us/dotnet/core/tools/global-tools-how-to-create](https://docs.microsoft.com/en-us/dotnet/core/tools/global-tools-how-to-create)

dotnet nuget push ./nupkg/TextTemplating.Tool.5.0.0.nupkg --source https://api.nuget.org/v3/index.json