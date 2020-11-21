### create manifest to use local .net cli tool
dotnet new tool-manifest
### isntall locally .net cli tool created in separate project (build separate project fist)
## relative path to folder with nuget package
dotnet tool install --add-source ./../../src/Texttemplating.cli/nupkg TextTemplating.Tool
### use the tool
dotnet t4 -h
dotnet t4 trans -f Person.tt
dotnet t4 trans -f Crud.tt  
## awesome