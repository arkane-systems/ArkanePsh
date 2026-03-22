namespace ArkaneSystems.PowerShell;

/// <summary>
/// Extension methods of general use.
/// </summary>
public static class Extensions
{
  /// <summary>
  /// Adds parameters to the PowerShell command to make the variable global and constant. This is useful for setting module-level constants that should be accessible across the entire session.
  /// </summary>
  /// <param name="ps">A powershell command in creation.</param>
  /// <returns>The modified powershell command.</returns>
  public static System.Management.Automation.PowerShell MakeGlobalConstant (this System.Management.Automation.PowerShell ps)
    => ps.AddParameter ("Scope", "Global").AddParameter ("Option", "Constant");

  /// <summary>
  /// Adds parameters to the PowerShell command to make the variable global and read-only. This is useful for setting module-level constants that should be accessible across the entire session.
  /// </summary>
  /// <param name="ps">A powershell command in creation.</param>
  /// <returns>The modified powershell command.</returns>
  public static System.Management.Automation.PowerShell MakeGlobalReadOnly (this System.Management.Automation.PowerShell ps)
    => ps.AddParameter ("Scope", "Global").AddParameter ("Option", "ReadOnly");
}
