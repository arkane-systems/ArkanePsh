# ArkanePsh

ArkanePsh is a compiled PowerShell module implemented in C# (.NET 10), providing cmdlets for Windows system administration and development workflows. This project is **not** a hybrid or script-based module—**all functionality is implemented in compiled C#** and distributed as a binary module (DLL) for import into PowerShell 7+.

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
- The compiled DLL will be output to `ArkanePsh/bin/`

### Loading the Module in PowerShell
```powershell
Import-Module ./ArkanePsh/bin/ArkanePsh.dll -Force
```

### Listing Available Cmdlets
```powershell
Get-Command -Module ArkanePsh
```

## Adding New Cmdlets
1. Add a new `.cs` file in `ArkanePsh/`
2. Inherit from `ArkanePshCmdlet`
3. Decorate with `[Cmdlet]` and `[OutputType]` attributes
4. Implement `ProcessRecord()`
5. Use base class helpers for error handling and debug output

## Project Structure
- `ArkanePsh/ArkanePsh.csproj` - Project file
- `ArkanePsh/ArkanePshCmdlet.cs` - Base class for cmdlets
- `ArkanePsh/GetCompiledCmdletInfoCmdlet.cs` - Example cmdlet
- `ArkanePsh/bin/ArkanePsh.dll` - Compiled module binary

## Requirements
- .NET 10 SDK
- PowerShell 7+

## License
See [LICENSE](https://github.com/arkane-systems/ArkanePsh/blob/master/LICENSE)

---
**Note:** All previous script-based or hybrid module logic is deprecated and not part of this module. Only the compiled DLL is relevant for usage and development.
