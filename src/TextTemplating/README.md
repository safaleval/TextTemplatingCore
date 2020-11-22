### pack with dependencies, build doesnt do it correcly!
dotnet pack
### push to nuget and add your api key 
dotnet nuget push ./nupkg/TextTemplating.Core.5.0.0.0.nupkg --source https://api.nuget.org/v3/index.json