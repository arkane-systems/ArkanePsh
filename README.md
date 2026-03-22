# ArkanePsh

ArkanePsh is a compiled PowerShell module implemented in C# (.NET 10), providing cmdlets for Windows system administration and development workflows.

## Features
- Compiled PowerShell cmdlets for automation and system management
- Modern C# codebase targeting .NET 10
- No script-based or hybrid module logic
- Easy import into PowerShell 7+ via DLL

## Getting Started

### Building
- Open `ArkanePsh/ArkanePsh.csproj` in Visual Studio 2026 or run:
  ```powershell
  dotnet build ArkanePsh/ArkanePsh.csproj -c Release
  ```
- The compiled DLL will be output to `ArkanePsh/bin/Release/net10.0`

### Loading the Module in PowerShell
```powershell
Import-Module ./ArkanePsh/bin/Release/net10.0/ArkanePsh.dll
```

### Listing Available Cmdlets
```powershell
Get-Command -Module ArkanePsh
```

## Requirements
- .NET 10 SDK
- PowerShell 7+

## License
See [LICENSE](https://github.com/arkane-systems/ArkanePsh/blob/master/LICENSE)
