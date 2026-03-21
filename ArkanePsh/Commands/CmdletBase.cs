using System;
using System.Management.Automation;

namespace ArkaneSystems.PowerShell.Commands
{
  /// <summary>
  /// Base class for all ArkanePsh cmdlets.
  /// Provides common functionality for disposal, error handling, logging, and parameter validation.
  /// </summary>
  public abstract class CmdletBase : PSCmdlet, IDisposable
  {

    #region Error and debug message helpers

    /// <summary>
    /// Write a non-terminating error with consistent formatting.
    /// </summary>
    /// <param name="message">Error message</param>
    /// <param name="errorCode">Unique error code for debugging</param>
    /// <param name="targetObject">The object the error occurred on</param>
    protected void WriteErrorMessage (string message, string errorCode, object? targetObject = null)
    {
      var error = new ErrorRecord(
                new InvalidOperationException(message),
                errorCode,
                ErrorCategory.InvalidOperation,
                targetObject
            );
      this.WriteError (error);
    }

    /// <summary>
    /// Write a terminating error and stop processing.
    /// </summary>
    /// <param name="message">Error message</param>
    /// <param name="errorCode">Unique error code for debugging</param>
    /// <param name="targetObject">The object the error occurred on</param>
    protected void ThrowTerminatingError (string message, string errorCode, object? targetObject = null)
    {
      var error = new ErrorRecord(
                new InvalidOperationException(message),
                errorCode,
                ErrorCategory.InvalidOperation,
                targetObject
            );
      this.ThrowTerminatingError (error);
    }

    /// <summary>
    /// Write debug information if -Debug is specified.
    /// </summary>
    /// <param name="message">Debug message</param>
    protected void WriteDebugMessage (string message)
    {
      if (this.MyInvocation.BoundParameters.ContainsKey ("Debug"))
      {
        this.WriteDebug ($"[{this.GetType ().Name}] {message}");
      }
    }

    #endregion Error and debug message helpers

    #region Information

    private string? cmdletName ;
    public string CmdletName
    {
      get
      {
        if (this.cmdletName == null)
        {
          // All subclasses must have a CmdletAttribute, so this should never be null.
          // If it is, we'll get a NullReferenceException, which is fine because it indicates a programming error.
          var cmdletAttr = (CmdletAttribute) Attribute.GetCustomAttribute (this.GetType (), typeof (CmdletAttribute))! ;
          if (cmdletAttr != null)
            this.cmdletName = cmdletAttr.VerbName + "-" + cmdletAttr.NounName;
        }

        return this.cmdletName!;
      }
    }

    #endregion Information

    #region Disposal

    /// <summary>
    /// Releases all resources used by the current instance of the class. 
    /// </summary>
    /// <remarks>Call this method when you are finished using the object to free unmanaged resources and
    /// perform other cleanup operations. After calling this method, the object should not be used further.</remarks>
    public void Dispose ()
    {
      try
      {
        this.Dispose (true);
      }
      finally
      {
        GC.SuppressFinalize (this);
      }
    }

    /// <summary>
    ///     Releases the unmanaged resources used by the
    ///     cmdlet and optionally releases the managed resources.
    /// </summary>
    /// <param name="disposing">
    ///     true to release both managed and unmanaged resources;
    ///     false to release only unmanaged resources.
    /// </param>
    protected virtual void Dispose (bool disposing)
    {
      // Override in derived classes to dispose managed resources.
    }

    #endregion Disposal

  }
}
