# ArkanePsh Copilot Instructions

## Project Overview
**ArkanePsh** is a personal PowerShell module collection providing utility functions for Windows system administration and development workflows. This is a **PowerShell Module** project (not .NET/C#), organized as a single-module package with ~580 functions exported via manifest.

### Key Files
- `ArkanePsh/ArkanePsh.psd1` - Module manifest (metadata, exports, dependencies)
- `ArkanePsh/ArkanePsh.psm1` - Core module implementation (~583 lines, all exported functions)
- `ArkanePsh/ArkanePsh.Types.ps1xml` - Custom type extensions (e.g., `.ToRoman()` on Int32)
- `ArkanePsh/InitVariables.ps1` - Module-level variable initialization (profile helpers, admin detection)
- `ArkanePsh/ArkanePsh.slnx` - Solution file for the project (use this instead of creating a new .sln file)

## Architecture Patterns

### Module Exports Pattern
Every function follows this template:function Verb-Noun { 
    param([param_types]$paramNames)
    # implementation
}
Export-ModuleMember -Function Verb-Noun [[-Alias shortName]]- All functions are **explicitly exported** (no implicit exports)
- Functions follow **Verb-Noun naming** from PowerShell cmdlet conventions
- Single-letter aliases common: `now` → `Get-CurrentTime`, `up` → `Set-LocationUp`, `shy`/`lhy` → history functions
- **New functions MUST be exported**, else they won't be visible to module consumers

### Global Variables
Initialized in `InitVariables.ps1`, read-only with `AllScope`:
- `$myDocuments` - User documents folder (from System.Environment)
- `$executableExtensions`, `$archiveExtensions` - Extension lists
- `$windowsIdentity`, `$windowsPrincipal`, `$isAdmin` - Security context (computed at load)
- `$workingFolder` - "C:\Working" (intended to be overridden in profile)

These are **global scope, read-only, AllScope**—they persist across reloads.

### Type Extensions Pattern
`ArkanePsh.Types.ps1xml` extends built-in types with custom methods:<Type>
  <Name>System.Int32</Name>
  <Members>
    <ScriptMethod>
      <Name>ToRoman</Name>
      <Script>...</Script>
    </ScriptMethod>
  </Members>
</Type>This allows `(42).ToRoman()` syntax. **When adding methods, update the `.ps1xml` file**, not code comments.

## Critical Workflows & Development Commands

### Testing the Module
- Load the module into current session: Import-Module .\ArkanePsh\ArkanePsh.psd1 -Force- Verify exports: Get-Module ArkanePsh | Select -ExpandProperty ExportedFunctions- Test a single function: Test-IsLaptop
Get-CurrentTime
Edit-HostProfile
### Adding New Functions
1. Add function to `ArkanePsh.psm1` with full comment-based help (`.Synopsis`, `.Description`, `.Parameter`, `.Example`)
2. Call `Export-ModuleMember -Function FunctionName [-Alias shortName]` immediately after
3. Aliases are common—prefer short, intuitive names (e.g., `up`, `now`, `root`)
4. If function extends a type, also add entry to `.ps1xml`

### Common Function Categories
Functions in this module cluster into:
- **Profile management**: Edit-Profile, Update-Profile, etc.
- **Navigation shortcuts**: Set-LocationUp (`up`), Set-LocationRoot (`root`), Set-LocationOver (`over`)
- **System info**: Get-CurrentTime, Test-IsLaptop, Test-Is64Bit, Get-DotNetInstallDirectory
- **History**: Export-History (`shy`), Import-History (`lhy`)
- **Dev tools**: Invoke-BuildEngine, Invoke-CSharpCompiler, Install-Assembly (all wrap .NET runtime tools)
- **Utility**: Out-TempFile, Get-ApplicationPath, Use-Culture

## Key Design Decisions

### Why Explicit Exports?
Every function uses `Export-ModuleMember` to ensure only intended functions are public. This prevents accidentally exposing helper functions.

### Why Registry Lookups in Get-ApplicationPath?
The function queries `HKLM:\Software\Classes\` to dynamically resolve default applications for file extensions—no hardcoding. This is extensible but requires error handling.

### Why InitVariables.ps1?
Module-level setup (admin checks, folder paths, defaults) is separated from function definitions for clarity and to establish global context before any function runs.

### Why Types.ps1xml?
Extending built-in types allows elegant syntax (`(42).ToRoman()`) and keeps domain logic decoupled from core functions. The file is loaded automatically by the manifest.

## Code Style & Conventions

- **Capitalization**: Strictly PascalCase for function names (Verb-Noun), camelCase for parameters
- **Comments**: Function headers use block comment-based help (Comment-based help syntax is standard in PowerShell)
- **Error handling**: Functions use `-ErrorAction Stop` for critical registry/WMI queries; Write-Error for user-facing errors
- **Scope**: Prefer `-Scope Global` for Set-Variable/New-Alias to ensure cross-session persistence
- **Parameters**: Use full `param()` blocks, not implicit parameter lists
- **Pipeline support**: Functions like `Out-TempFile` use `[Parameter(ValueFromPipeline=$true)]` for pipeline input
- **String interpolation**: Double-quoted strings for variable expansion; single quotes for literals

## Common Pitfalls for AI Agents

1. **Forgetting Export-ModuleMember**: New functions must be explicitly exported or they won't load
2. **Not updating .ps1xml**: Custom type methods belong there, not as standalone functions
3. **Using -ErrorAction SilentlyContinue carelessly**: Only suppress errors where you handle the fallback
4. **Hardcoding paths**: Always use System.Environment and registry for dynamic paths (see Get-ApplicationPath)
5. **Missing help blocks**: All exported functions should have `.Synopsis`, `.Description`, and `.Example`
6. **Alias confusion**: Check existing aliases before adding new ones to avoid conflicts

## Integration Points

- **No external dependencies**: ArkanePsh uses only built-in PowerShell cmdlets and .NET Framework APIs
- **WMI queries**: Used in `Test-IsLaptop` for hardware detection; may fail on non-Windows or restricted environments
- **Registry access**: `Get-ApplicationPath` reads from HKLM; requires appropriate permissions
- **Profile integration**: Functions like `Edit-Profile` rely on `$profile` automatic variables
- **.NET runtime**: Helper functions invoke .NET tools (csc.exe, msbuild.exe, ilasm.exe) from runtime directory

## Unified Module Outcome
- Merge existing `ArkanePsh.pssproj` script functionality with new `ArkanePsh.Cmdlets` functionality.
- Migrate legacy functionality into `ArkanePsh.Cmdlets` to ensure a cohesive module experience.

---

**Last updated**: Generated from codebase analysis  
**Version reference**: ArkanePsh 0.1
