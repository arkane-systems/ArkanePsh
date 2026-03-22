namespace ArkaneSystems.PowerShell
{
  /// <summary>
  /// Provides global environment variables and helpers for ArkanePsh cmdlets.
  /// </summary>
  //public static class ModuleEnvironment
  //{
  //  public static string MyDocuments => Environment.GetFolderPath (Environment.SpecialFolder.MyDocuments);

  //  public static string[] ExecutableExtensions { get; } = new[] { ".exe", ".ps1", ".cmd", ".py" };
  //  public static string[] ArchiveExtensions { get; } = new[] { ".7z", ".zip", ".gz", ".bz2", ".rar", ".tar", ".z" };

  //  public static WindowsIdentity WindowsIdentity => WindowsIdentity.GetCurrent ();
  //  public static WindowsPrincipal WindowsPrincipal => new WindowsPrincipal (WindowsIdentity);
  //  public static bool IsAdmin => WindowsPrincipal.IsInRole (WindowsBuiltInRole.Administrator);

  //  // Working folder can be overridden by environment variable or config in the future
  //  public static string WorkingFolder { get; set; } = "C:\\Working";

  //  /// <summary>
  //  /// Gets the default application path for a given file extension (e.g., "ps1").
  //  /// </summary>
  //  public static string GetApplicationPath(string extension)
  //  {
  //      if (string.IsNullOrWhiteSpace(extension))
  //          throw new ArgumentException("Extension must be provided.", nameof(extension));
  //      if (!extension.StartsWith("."))
  //          extension = "." + extension;
  //      using var extKey = Registry.ClassesRoot.OpenSubKey(extension);
  //      var defaultValue = extKey?.GetValue(null) as string;
  //      if (string.IsNullOrEmpty(defaultValue))
  //          throw new InvalidOperationException($"No application registered for extension '{extension}'.");
  //      using var commandKey = Registry.ClassesRoot.OpenSubKey($"{defaultValue}\\shell\\open\\command");
  //      var command = commandKey?.GetValue(null) as string;
  //      if (string.IsNullOrEmpty(command))
  //          throw new InvalidOperationException($"No open command found for extension '{extension}'.");
  //      // Extract the executable path (handles quoted and unquoted)
  //      var parts = command.Trim().Split(' ');
  //      var exePath = parts[0].Trim('"');
  //      if (!File.Exists(exePath))
  //          throw new FileNotFoundException($"Application not found: {exePath}");
  //      return exePath;
  //  }
  //}
}
