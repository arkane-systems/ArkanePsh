using ArkaneSystems.PowerShell;
using ArkaneSystems.PowerShell.Commands;
using System.Diagnostics;
using System.Management.Automation;

namespace ArkanePsh.Commands.Profile
{
  /// <summary>
  /// Opens the current user's host profile in the default editor.
  /// </summary>
  [Cmdlet (VerbsData.Edit, ArkanePshNouns.HostProfile)]
  [OutputType (typeof (void))]
  public class EditHostProfileCmdlet : CmdletBase
  {
    protected override void ProcessRecord ()
    {
      try
      {
        // Use PowerShell's $profile variable for the current host
        var profilePath = this.SessionState.PSVariable.GetValue("profile") as string;
        if (string.IsNullOrEmpty (profilePath))
        {
          this.ThrowTerminatingError ("Could not determine the current host profile path.", "ProfilePathNotFound");
          return;
        }
        // Use the default editor for .ps1 files
        var psi = new ProcessStartInfo
        {
          FileName = ModuleEnvironment.GetApplicationPath("ps1"),
          Arguments = profilePath,
          UseShellExecute = true
        };
        Process.Start (psi);
      }
      catch (System.Exception ex)
      {
        this.WriteErrorMessage ($"Failed to open host profile: {ex.Message}", "EditHostProfileFailed");
      }
    }
  }
}
