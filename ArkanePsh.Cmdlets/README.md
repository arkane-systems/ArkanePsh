# ArkanePsh.Cmdlets - Compiled Cmdlet Assembly

This C# project produces the binary module for the hybrid ArkanePsh PowerShell module.

## Project Structure

- **ArkanePshCmdlet.cs** - Base class for all compiled cmdlets with common functionality
  - Error handling (terminating and non-terminating)
  - Debug message support
  - Parameter validation helpers

- **GetCompiledCmdletInfoCmdlet.cs** - Template/placeholder cmdlet demonstrating the pattern
  - Shows proper cmdlet naming (`Get-CompiledCmdletInfo`)
  - Demonstrates parameter binding with `[Parameter]` attributes
  - Shows error handling via base class
  - Remove or replace this with actual cmdlet implementations

## Building

### From Visual Studio
Open `ArkanePsh.sln` and build the `ArkanePsh.Cmdlets` project.

### From Command Line
```powershell
dotnet build ArkanePsh.Cmdlets/ArkanePsh.Cmdlets.csproj -c Release
```

### Output
Built assemblies are automatically copied to `ArkanePsh/bin/` via post-build event.

## Adding New Cmdlets

1. Create a new `.cs` file in `ArkanePsh.Cmdlets/`
2. Inherit from `ArkanePshCmdlet` base class
3. Decorate with `[Cmdlet]` and `[OutputType]` attributes
4. Implement `ProcessRecord()` (and `BeginProcessing()`/`EndProcessing()` if needed)
5. Use base class methods for error handling:
   - `WriteErrorMessage()` - Non-terminating error
   - `ThrowTerminatingError()` - Terminating error
   - `WriteDebugMessage()` - Debug info

### Example Cmdlet Template

```csharp
using System;
using System.Management.Automation;

namespace ArkanePsh.Cmdlets
{
    [Cmdlet(VerbsCommon.Get, "Something")]
    [OutputType(typeof(string))]
    public class GetSomethingCmdlet : ArkanePshCmdlet
    {
        [Parameter(Position = 0, Mandatory = true, ValueFromPipeline = true)]
        public string InputPath { get; set; } = string.Empty;

        [Parameter(Mandatory = false)]
        public SwitchParameter Force { get; set; }

        protected override void BeginProcessing()
        {
            WriteDebugMessage($"Processing with Force={Force}");
        }

        protected override void ProcessRecord()
        {
            try
            {
                // Implementation here
                WriteObject(result);
            }
            catch (Exception ex)
            {
                WriteErrorMessage($"Failed to process '{InputPath}': {ex.Message}", "GetSomethingFailed", InputPath);
            }
        }
    }
}
```

## Naming Conventions

- **Class names**: `{Verb}{Noun}Cmdlet` (e.g., `GetCompiledCmdletInfoCmdlet`)
- **Cmdlet names**: Verb-Noun (e.g., `Get-CompiledCmdletInfo`) - use PowerShell approved verbs
- **Error codes**: `{VerbNoun}{ErrorDescription}` (e.g., `GetSomethingFailed`)
- **Parameters**: camelCase (e.g., `InputPath`, `Force`)

## PowerShell SDK

The project uses `System.Management.Automation` v7.4.0, which targets .NET 6.0+. This ensures compatibility with:
- PowerShell 7.0 and later
- .NET Framework 4.7.2+ (via compatibility layer)
- Windows PowerShell 5.1 (with caveats—test thoroughly)

## Post-Build Steps

The `.csproj` file includes an automatic post-build target that copies the compiled assembly to `ArkanePsh/bin/` for module loading.

## Dependencies

Currently only depends on `System.Management.Automation`. No external NuGet packages are referenced to keep the module lightweight.

## Versioning

The assembly version is set to `0.1.0` and should be updated in the `.csproj` file in sync with the module manifest version.
