using System.Diagnostics;
using System.Management.Automation;

namespace ArkaneSystems.PowerShell.Commands.Profile
{
  /// <summary>
  /// Opens the current user's profile (all hosts) in the default editor.
  /// </summary>
  [Cmdlet (VerbsData.Edit, ArkanePshNouns.Profile)]
  [OutputType (typeof (void))]
  public class EditProfileCmdlet : CmdletBase
  {
    protected override void ProcessRecord ()
    {
      try
      {
        // Use PowerShell's $profile.CurrentUserAllHosts variable
        var profileAllHosts = this.SessionState.PSVariable.GetValue("profile") as PSObject;
        var allHostsPath = profileAllHosts?.Properties["CurrentUserAllHosts"]?.Value as string;
        if (string.IsNullOrEmpty (allHostsPath))
        {
          this.ThrowTerminatingError ("Could not determine the current user's all-hosts profile path.", "ProfileAllHostsPathNotFound");
          return;
        }

        var psi = new ProcessStartInfo
        {
          FileName = ModuleEnvironment.GetApplicationPath("ps1"),
          Arguments = allHostsPath,
          UseShellExecute = true
        };
        _ = Process.Start (psi);
      }
      catch (System.Exception ex)
      {
        this.WriteErrorMessage ($"Failed to open all-hosts profile: {ex.Message}", "EditProfileFailed");
      }
    }
  }
}
