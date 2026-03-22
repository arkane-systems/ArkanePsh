using System;
using System.ComponentModel;
using System.Management.Automation;

namespace ArkaneSystems.PowerShell.Commands;

/// <summary>
///   Implementation of invoke-nothing cmdlet. This cmdlet does nothing and is useful for testing and debugging purposes.
/// </summary>
[Cmdlet (VerbsLifecycle.Invoke, ArkanePshNouns.Nothing)]
[Description ("Does nothing.")]
public class InvokeNothingCmdlet : CmdletBase
{
  /// <summary>
  ///     Input object to the cmdlet.
  /// </summary>
  [Parameter (Position = 0,
      ValueFromRemainingArguments = true,
      ValueFromPipeline = true,
      HelpMessage = "Something to do nothing to.")]
  public object? InputObject { get; set; }

  [Parameter (HelpMessage = "Fail at nothing.")]
  public SwitchParameter Fail { get; set; } = false;

  /// <inheritdoc />
  protected override void EndProcessing ()
  {
    if (this.Fail)
      this.ThrowTerminatingError (new ErrorRecord (new InvalidOperationException ("Doing nothing and failing."),
                                                   "FailNaught",
                                                   ErrorCategory.NotEnabled,
                                                   this.InputObject));

    this.WriteVerbose ("Doing nothing and succeeding.");

    this.WriteObject (this.InputObject);
    base.EndProcessing ();
  }
}
