# ArkanePsh Migration Baseline (Step 1)

## Scope
Baseline captured from:
- `ArkanePsh/ArkanePsh.psm1`
- `ArkanePsh/InitVariables.ps1`
- `ArkanePsh/ArkanePsh.Types.ps1xml`
- `ArkanePsh/ArkanePsh.psd1`

This file defines the functional parity target for compiled migration.

## Exported Cmdlets (compiled in current hybrid)
From manifest `CmdletsToExport`:
- `Get-CompiledCmdletInfo`
- `Get-CurrentTime`
- `Get-DotNetInstallDirectory`
- `Test-Is64Bit`

## Exported Functions (script)
From manifest `FunctionsToExport` + script module:
- `Edit-HostProfile`
- `Edit-Profile`
- `Export-History`
- `Get-ApplicationPath`
- `Get-LoadedAssemblies`
- `Get-LoadedTypes`
- `Get-SpecialFolder`
- `Import-History`
- `Install-Assembly`
- `Invoke-BuildEngine`
- `Invoke-CSharpCompiler`
- `Invoke-IlAssembler`
- `Invoke-Nothing`
- `Out-TempFile`
- `Set-LocationOver`
- `Set-LocationRoot`
- `Set-LocationUp`
- `Test-IsISEHost`
- `Test-IsLaptop`
- `Refresh-HostProfile` *(manifest export; implemented as `Update-HostProfile` in script, naming mismatch to resolve during migration)*
- `Update-Profile`
- `Use-Culture`
- `Use-Location`
- `del?`
- `del!`
- `Get-Scripts`
- `nd`
- `zap`
- `Set-LocationDesktop`
- `Set-LocationHome`
- `Set-LocationProfile`
- `Set-LocationTemp`
- `Set-LocationWorking`

## Exported Aliases
From manifest `AliasesToExport` + script module alias creation:
- `shy` -> `Import-History` *(script currently creates this for Export-History path; verify intended mapping during migration)*
- `now` -> `Get-CurrentTime`
- `lhy` -> `Import-History`
- `over` -> `Set-LocationOver`
- `root` -> `Set-LocationRoot`
- `up` -> `Set-LocationUp`
- `within` -> `Use-Location`
- `dd` -> `Push-Location`
- `du` -> `Pop-Location`
- `jobs` -> `Get-Job`
- `new` -> `New-Object`
- `go-desktop` -> `Set-LocationDesktop`
- `go-home` -> `Set-LocationHome`
- `go-profile` -> `Set-LocationProfile`
- `go-temp` -> `Set-LocationTemp`
- `go-working` -> `Set-LocationWorking`

## Module Initialization Variables
From `InitVariables.ps1`:
- `$myDocuments`
- `$executableExtensions`
- `$archiveExtensions`
- `$windowsIdentity`
- `$windowsPrincipal`
- `$isAdmin`
- `$workingFolder` *(overridable global variable)*

Notes:
- Most variables are created as global + all-scope + read-only.
- Initialization currently runs through `ScriptsToProcess` in manifest.

## Type Extensions
From `ArkanePsh.Types.ps1xml`:
1. `System.Int32`
   - ScriptMethod: `ToRoman()`
2. `System.Management.ManagementObject#root\cimv2\Win32_ComputerSystem`
   - ScriptProperty: `DomainRoleStr`

## Behavior Categories to Preserve in Compiled Migration
- Profile editing/reloading
- History import/export convenience
- Registry-based application association lookup (`Get-ApplicationPath`)
- Runtime/tool invocation wrappers (`installutil`, `msbuild`, `csc`, `ilasm`)
- Location navigation helpers + aliases
- Laptop detection via WMI/CIM
- ScriptBlock execution in alternate culture/location contexts
- Temporary file output/open lifecycle (`Out-TempFile`)

## Compatibility Risks Identified
- Function naming mismatch: `Update-HostProfile` implementation vs `Refresh-HostProfile` export in manifest
- Nonstandard command names (`del?`, `del!`, `nd`, `zap`) are easier to keep as script wrappers/aliases over compiled cmdlets
- Type extensions in `.ps1xml` may remain as type data even after compiled migration
- Windows-specific behaviors (registry, WMI, ISE, .NET tool paths) need explicit platform handling in compiled code

## Step 1 Exit Criteria Status
- [x] Inventory of exports captured
- [x] Variables and type data captured
- [x] Functional parity checklist established
- [x] Known mismatches/risks documented
 
## Step 2 Completion (Current Session)
- Implemented compiled cmdlets in `ArkanePsh.Cmdlets`:
  - `Get-CurrentTime`
  - `Get-DotNetInstallDirectory` (supports `-AsInfo`)
  - `Test-Is64Bit`
- Updated script module ownership:
  - Removed conflicting script implementations for the three migrated commands.
  - Kept `now` alias exported and mapped to `Get-CurrentTime`.
  - Added private helper `Get-DotNetRuntimeDirectory` for existing script wrappers (`Install-Assembly`, `Invoke-BuildEngine`, `Invoke-CSharpCompiler`, `Invoke-IlAssembler`).
- Corrected project output behavior:
  - `ArkanePsh.Cmdlets.csproj` post-build copy now uses `$(TargetPath)`.
- Recovered file rewrite issues:
  - Normalized `ArkanePsh.slnx` line endings.
  - Normalized `ArkanePsh.psd1` encoding.
- Validation:
  - `dotnet build ArkanePsh.slnx` succeeds.
  - Imported module resolves `Get-CurrentTime`, `Get-DotNetInstallDirectory`, and `Test-Is64Bit` as compiled cmdlets.

## Step 2 Exit Criteria Status
- [x] Compiled parity cmdlets added for initial migrated set
- [x] Script/compiled command ownership conflicts resolved for migrated set
- [x] Module imports with migrated commands exposed as cmdlets
- [x] Solution/project artifacts stable after rewrite recovery
