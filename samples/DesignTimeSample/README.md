### create manifest to use local .net cli tool
dotnet new tool-manifest
### isntall locally .net cli tool created in separate project (build separate project fist)
dotnet tool install --add-source ./../../src/Texttemplating.cli/nupkg TextTemplatigToolsNet
### use the tool
dotnet t4 -h
dotnet t4 trans -f Person.tt
dotnet t4 trans -f Crud.tt  
## awesome