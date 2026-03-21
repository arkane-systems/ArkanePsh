using System;
using System.Security.Principal;

namespace ArkaneSystems.PowerShell
{
  /// <summary>
  /// Provides global environment variables and helpers for ArkanePsh cmdlets.
  /// </summary>
  public static class ModuleEnvironment
  {
    public static string MyDocuments => Environment.GetFolderPath (Environment.SpecialFolder.MyDocuments);

    public static string[] ExecutableExtensions { get; } = new[] { ".exe", ".ps1", ".cmd", ".py" };
    public static string[] ArchiveExtensions { get; } = new[] { ".7z", ".zip", ".gz", ".bz2", ".rar", ".tar", ".z" };

    public static WindowsIdentity WindowsIdentity => WindowsIdentity.GetCurrent ();
    public static WindowsPrincipal WindowsPrincipal => new WindowsPrincipal (WindowsIdentity);
    public static bool IsAdmin => WindowsPrincipal.IsInRole (WindowsBuiltInRole.Administrator);

    // Working folder can be overridden by environment variable or config in the future
    public static string WorkingFolder { get; set; } = "C:\\Working";
  }
}
