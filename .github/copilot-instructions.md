# ArkanePsh Copilot Instructions

## Project Overview
**ArkanePsh** is a compiled PowerShell module implemented in C# (.NET 10), distributed as a binary module (DLL) for import into PowerShell 7+, providing cmdlets for Windows system administration and development workflows.

### Key Files
- `ArkanePsh/ArkanePsh.csproj` - C# project file targeting .NET 10
- `ArkanePsh/CmdletBase.cs` - Base class for all compiled cmdlets
- `ArkanePsh/README.md` - Module documentation

## Architecture Patterns

### Cmdlet Pattern
Each cmdlet is implemented as a C# class inheriting from `CmdletBase` (which itself inherits from `PSCmdlet`).
- Use `[Cmdlet]` and `[OutputType]` attributes for PowerShell integration
- Implement `ProcessRecord()` (and optionally `BeginProcessing()`/`EndProcessing()`)
- Use base class helpers for error handling and debug output
- Cmdlet class names follow `{Verb}{Noun}Cmdlet` (e.g., `GetSomethingCmdlet`)
- Cmdlet names follow PowerShell Verb-Noun convention (e.g., `Get-Something`)

### Building and Loading
- Build with Visual Studio or `dotnet build ArkanePsh/ArkanePsh.csproj -c Release`
- The compiled DLL is output to `ArkanePsh/bin/Release/net10.0`
- Import into PowerShell with `Import-Module ./ArkanePsh/bin/Release/net10.0/ArkanePsh.dll`

## Critical Workflows & Development Commands

### Adding New Cmdlets
1. Add a new `.cs` file in the appropriate `ArkanePsh/Commands` subfolder
2. Inherit from `CmdletBase`
3. Decorate with `[Cmdlet]` and `[OutputType]` attributes
4. Implement `ProcessRecord()`
5. Use base class methods for error handling and debug output

### Testing the Module
- Import the compiled DLL into PowerShell: `Import-Module ./ArkanePsh/bin/Release/net10.0/ArkanePsh.dll`
- List available cmdlets: `Get-Command -Module ArkanePsh`
- Run and test individual cmdlets as needed

## Code Style & Conventions
- **Class names**: `{Verb}{Noun}Cmdlet` (PascalCase)
- **Cmdlet names**: Verb-Noun (PowerShell approved verbs)
- **Parameters**: PascalCase
- **Error handling**: Use base class helpers for consistent error reporting
- **Debug output**: Use `WriteDebugMessage()`

## Common Pitfalls for AI Agents
1. **Do not add script-based or hybrid module patterns**: All functionality is compiled C#
2. **Do not reference .psm1, .psd1, or script-based exports**: Only the compiled DLL is relevant
3. **Do not use Export-ModuleMember or PowerShell script module conventions**
4. **Do not add PowerShell aliases or script-based type extensions**
5. **All module logic must be implemented in C# cmdlets**

## Integration Points
- **No script-based module logic**: All logic is in compiled C#
- **Targeting .NET 10 and PowerShell 7+**

---

**Last updated**: Post-migration, compiled-only paradigm
**Version reference**: ArkanePsh 0.2
