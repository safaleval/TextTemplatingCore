# TextTemplating

T4 templates using .net cli command for design time .tt transformation and possibility to use engine class for runtime .tt transformation 
Compatible with **.NET 5 & .NET Core 3.1**
https://www.nuget.org/packages/TextTemplating.Tool/
### Usage 
transform template on the fly
```
dotnet tt trans -f Person.tt
```

## Goal
T4 scaffolding for .NET Core without IDE.
Command line tool and designtime transformation tool,
T4 support for .net 5 and .net core 3.1+, transform .tt files from your command line.
Based on roslyn, no mono or 3rdparty dependencies.

### Update 
The Visual Studio 2017 and Xamarin Studio now supports to process *.tt files in desing time, but this repo is maybe still useful who wants to process T4 templates in a dotnet core  project outside IDE (eg. in Win, Linux or macOS with Visual Studio Code -> VSCODE)

## How to install

### As a command line tool
### create manifest file
```
dotnet new tool-manifest
```
### Install locally
```
dotnet tool install TextTemplating.Tool
```

### Install globally 
```
dotnet tool install --global TextTemplating.Tool
```
Add nuget package
```xml
<PackageReference Include="TextTemplating.Core" Version="5.0.0" /> 
```
Now you can use the `dotnet tt` command as a command line tool to transform templates at design-time, with the specified command line arguments.
## How to Use
Run `dotnet tt -h` to see the usage.


### As a design time tool
Add the same packages mentioned above, then you can run `dotnet tt trans -f Person.tt` to transform a text template.

Note: You can use all the packages that you have installed into your project when writing T4 template, so there may be no necessary to use "assembly" directive to reference assembly via assembly name(so I skipped these feature, you can also reference assembly via path).

### As tool you can call dynamically at runtime - that generate csharp class from tt template 
```
dotnet tt proc -f person.tt 
```

### As a library
To transform templates at runtime, you can also use the `Engine` class.

*Sample is work in progres*


# License
MIT
