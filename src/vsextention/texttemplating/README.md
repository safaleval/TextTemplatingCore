# TextTemplating 

**T4**  and **CSX** scripts using .net cli command for design time  transformation and possibility to use engine class for runtime .tt transformation.

**Execute from context menu from VSCode!**

One of the very few tools that supports reflection and **web** projects!
Compatible with **.NET 5 & .NET Core 3.1**  cs projects

Scaffolding for .NET Core without IDE. Command line tool and designtime transformation tool, based on roslyn, no mono or 3rdparty dependencies.
 

## Features

Generate **any** text, **any** code or script template, **any** language from your domain c# code, include it in your continous integration... veeery useful stuff! See DesignTimeSample etc.

![features](https://raw.githubusercontent.com/Jacknq/TextTemplatingCore/master/src/vsextention/texttemplating/images/vscontextt.png)

## Requirements
 1) Install .NET Core SDK 3.1+ or higher  https://dotnet.microsoft.com/download/dotnet-core
 2) Install textemplating tool 
 ###### Install locally
```
dotnet tool install TextTemplating.Tool
```

##### or Install globally 
```
dotnet tool install --global TextTemplating.Tool
```
Add nuget package to csproj, if used with cs project
```xml
<PackageReference Include="TextTemplating.Core" Version="5.0.0" /> 
```


### Usage 
transform template on the fly
```
dotnet tt Person.tt
dotnet tt hello.csx

 or 
 
dotnet tt trans -f Person.tt
```
<!-- 
## Extension Settings

Include if your extension adds any VS Code settings through the `contributes.configuration` extension point.

For example:

This extension contributes the following settings:

* `myExtension.enable`: enable/disable this extension
* `myExtension.thing`: set to `blah` to do something

## Known Issues

Calling out known issues can help limit users opening duplicate issues against your extension. -->
### YOU CAN SUPPORT THIS PROJECT - DONATE HERE
https://www.paypal.com/donate?hosted_button_id=RGHZAXSMKBSS4

## Release Notes 
 Github repo
 https://github.com/Jacknq/TextTemplatingCore

<!-- ## Working with Markdown

**Note:** You can author your README using Visual Studio Code.  Here are some useful editor keyboard shortcuts:

* Split the editor (`Cmd+\` on macOS or `Ctrl+\` on Windows and Linux)
* Toggle preview (`Shift+CMD+V` on macOS or `Shift+Ctrl+V` on Windows and Linux)
* Press `Ctrl+Space` (Windows, Linux) or `Cmd+Space` (macOS) to see a list of Markdown snippets

### For more information

* [Visual Studio Code's Markdown Support](http://code.visualstudio.com/docs/languages/markdown)
* [Markdown Syntax Reference](https://help.github.com/articles/markdown-basics/) -->

**Enjoy!**
