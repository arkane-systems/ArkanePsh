# ArkanePsh Legacy-to-Compiled Migration Mapping

This document maps each function, alias, type extension, and variable from the legacy script-based module to its planned compiled C# cmdlet or equivalent in the new .NET 10 module.

---

## 1. Profile Management
- **Edit-HostProfile** → `EditHostProfileCmdlet` (`Edit-HostProfile`)
- **Edit-Profile** → `EditProfileCmdlet` (`Edit-Profile`)
- **Update-HostProfile** (Refresh-HostProfile) → `UpdateHostProfileCmdlet` (`Update-HostProfile`)
- **Update-Profile** → `UpdateProfileCmdlet` (`Update-Profile`)

## 2. History Management
- **Export-History** → `ExportHistoryCmdlet` (`Export-History`)
  - Alias: `shy` (see Aliases section)
- **Import-History** → `ImportHistoryCmdlet` (`Import-History`)
  - Alias: `lhy`

## 3. System/Environment Info
- **Get-CurrentTime** → `GetCurrentTimeCmdlet` (`Get-CurrentTime`)
  - Alias: `now`
- **Get-DotNetInstallDirectory** → `GetDotNetInstallDirectoryCmdlet` (`Get-DotNetInstallDirectory`)
- **Get-LoadedAssemblies** → `GetLoadedAssembliesCmdlet` (`Get-LoadedAssemblies`)
- **Get-LoadedTypes** → `GetLoadedTypesCmdlet` (`Get-LoadedTypes`)
- **Get-SpecialFolder** → `GetSpecialFolderCmdlet` (`Get-SpecialFolder`)
- **Test-Is64Bit** → `TestIs64BitCmdlet` (`Test-Is64Bit`)
- **Test-IsIseHost** → `TestIsIseHostCmdlet` (`Test-IsIseHost`)
- **Test-IsLaptop** → `TestIsLaptopCmdlet` (`Test-IsLaptop`)

## 4. .NET/Development Tools
- **Install-Assembly** → `InstallAssemblyCmdlet` (`Install-Assembly`)
- **Invoke-BuildEngine** → `InvokeBuildEngineCmdlet` (`Invoke-BuildEngine`)
- **Invoke-CSharpCompiler** → `InvokeCSharpCompilerCmdlet` (`Invoke-CSharpCompiler`)
- **Invoke-IlAssembler** → `InvokeIlAssemblerCmdlet` (`Invoke-IlAssembler`)

## 5. Navigation/Location
- **Set-LocationOver** → `SetLocationOverCmdlet` (`Set-LocationOver`)
  - Alias: `over`
- **Set-LocationRoot** → `SetLocationRootCmdlet` (`Set-LocationRoot`)
  - Alias: `root`
- **Set-LocationUp** → `SetLocationUpCmdlet` (`Set-LocationUp`)
  - Alias: `up`
- **Set-LocationDesktop** → `SetLocationDesktopCmdlet` (`Set-LocationDesktop`)
  - Alias: `go-desktop`
- **Set-LocationHome** → `SetLocationHomeCmdlet` (`Set-LocationHome`)
  - Alias: `go-home`
- **Set-LocationProfile** → `SetLocationProfileCmdlet` (`Set-LocationProfile`)
  - Alias: `go-profile`
- **Set-LocationTemp** → `SetLocationTempCmdlet` (`Set-LocationTemp`)
  - Alias: `go-temp`
- **Set-LocationWorking** → `SetLocationWorkingCmdlet` (`Set-LocationWorking`)
  - Alias: `go-working`
- **Use-Location** → `UseLocationCmdlet` (`Use-Location`)
  - Alias: `within`
- **nd** (make and enter directory) → `NewAndEnterDirectoryCmdlet` (`New-AndEnterDirectory`)

## 6. Utility/General
- **Get-ApplicationPath** → `GetApplicationPathCmdlet` (`Get-ApplicationPath`)
- **Out-TempFile** → `OutTempFileCmdlet` (`Out-TempFile`)
- **Use-Culture** → `UseCultureCmdlet` (`Use-Culture`)
- **Invoke-Nothing** → `InvokeNothingCmdlet` (`Invoke-Nothing`)
- **Get-Scripts** → `GetScriptsCmdlet` (`Get-Scripts`)

## 7. File/Directory Operations
- **del?** (delete with confirmation) → `RemoveItemConfirmCmdlet` (`Remove-ItemConfirm`)
- **del!** (delete with force) → `RemoveItemForceCmdlet` (`Remove-ItemForce`)
- **zap** (obliterate directory) → `RemoveDirectoryRecursiveCmdlet` (`Remove-DirectoryRecursive`)

## 8. Aliases for System Cmdlets
- **dd** → `Push-Location`
- **du** → `Pop-Location`
- **jobs** → `Get-Job`
- **new** → `New-Object`
  - These may be documented as recommended user aliases, but not implemented as C# cmdlets.

---

## Type Extensions
- **System.Int32.ToRoman()**
  - Migrate as a C# extension method or static helper: `Int32Extensions.ToRoman(this int value)`
- **System.Management.ManagementObject#root\cimv2\Win32_ComputerSystem.DomainRoleStr**
  - Migrate as a C# extension method or static helper: `ManagementObjectExtensions.GetDomainRoleStr(this ManagementObject obj)`

---

## Global Variables / Initialization
- `$myDocuments`, `$executableExtensions`, `$archiveExtensions`, `$windowsIdentity`, `$windowsPrincipal`, `$isAdmin`, `$workingFolder`
  - Migrate as static properties or utility methods in a `ModuleEnvironment` or similar class.

---

## Parameter/Behavior Notes
- All cmdlets should use PowerShell parameter attributes for binding, validation, and pipeline support as appropriate.
- Aliases should be implemented in the module manifest or via PowerShell script, not in C#.
- Type extensions should be implemented as C# extension methods, not as .ps1xml.
- Any PowerShell-specific behaviors (e.g., Add-History, Start-Process) should use the appropriate .NET or PowerShell SDK APIs.

---

## Type Extensions Migration Plan
- **System.Int32.ToRoman()**
  - Implement as a C# extension method:
    ```csharp
    public static class Int32Extensions
    {
        public static string ToRoman(this int value) { /* ... */ }
    }
    ```
- **System.Management.ManagementObject#root\\cimv2\\Win32_ComputerSystem.DomainRoleStr**
  - Implement as a C# extension method or static helper:
    ```csharp
    public static class ManagementObjectExtensions
    {
        public static string GetDomainRoleStr(this ManagementObject obj) { /* ... */ }
    }
    ```

## Global Variable Initialization Migration Plan
- **$myDocuments**: Static property, e.g. `ModuleEnvironment.MyDocuments => Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments)`
- **$executableExtensions, $archiveExtensions**: Static readonly arrays in a utility class
- **$windowsIdentity, $windowsPrincipal, $isAdmin**: Static properties using .NET APIs
- **$workingFolder**: Static property with default value, overridable via config/environment

Implement these in a `ModuleEnvironment` or similar static class for use by all cmdlets.

---

## Implementation Notes
- All type extensions and global variables should be accessible to cmdlets via static methods/properties.
- No PowerShell script-based type extension or variable initialization will remain; all logic is in C#.

---

**This mapping is the authoritative plan for the migration of ArkanePsh.Legacy to the compiled module.**
