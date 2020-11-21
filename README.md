# TextTemplating

T4 templates using .net cli command for design time .tt transformation and possibility to use engine class for runtime .tt transformation 
Compatible with .NET 5 & .NET Core 3.1

### usage 
```
dotnet t4 trans -f Person.tt
```

## Goal
This project's goal is to bring the old T4 text templating code generating approach to the new .NET Core projects.

### Update 
The Visual Studio 2017 and Xamarin Studio now supports to process *.tt files in desing time, but this repo is maybe still useful who wants to process T4 templates in a dotnet core  project outside IDE (eg. in Linux or macOS with Visual Studio Code -> VSCODE)

## How to use

### As a command line tool
## install locally using nuget package
```
dotnet tool install TextTemplating.Tool
```

## Install globally in .net cli
```
dotnet tool install --global TextTemplating.Tool
```

Now you can use the `dotnet t4` command as a command line tool to transform templates at design-time, with the specified command line arguments.

Run `dotnet t4 -h` to see the usage.

Example:
```Batchfile
dotnet t4 proc -f DbBase.tt
```

### As a design time tool
Add the same packages mentioned above, then you can run `dotnet t4 trans -f Person.tt` to transform a text template.

Note: You can use all the packages that you have installed into your project when writing T4 template, so there may be no necessary to use "assembly" directive to reference assembly via assembly name(so I skipped these feature, you can also reference assembly via path).


### As a library
To transform templates at runtime, you can also use the `Engine` class.

*Sample is work in progres*

### As a service (Not Implemented)

# License
MIT
