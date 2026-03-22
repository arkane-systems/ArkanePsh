using System;
using System.Management.Automation;
using System.Reflection;

namespace ArkaneSystems.PowerShell;

/// <summary>
///   Script code to run when this binary module is imported. This is where we can set up global state, import other modules, etc.
/// </summary>
public class ModuleInitializer : IModuleAssemblyInitializer
{
  /// <summary>
  /// Performs actions required when the module is imported into a PowerShell session.
  /// </summary>
  public void OnImport ()
  {
    try
    {
      // Set constant variables for ArkanePsh cmdlet and external reference.
      using (var ps = System.Management.Automation.PowerShell.Create (RunspaceMode.CurrentRunspace))
      {
        // Version.
        string version = Assembly.GetCallingAssembly ().GetName ().Version?.ToString () ?? "0.0.0";

        ps.AddCommand ("Set-Variable")
          .AddParameter ("Name", "ArkanePshVersion")
          .AddParameter ("Value", version)
          .MakeGlobalConstant ()
          .Invoke ();
      }
    }
    catch (Exception ex)
    {
      Console.Error.WriteLine ($"Error during module initialization: {ex.Message}");
    }
  }
}
