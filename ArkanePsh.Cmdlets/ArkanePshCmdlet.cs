using System;
using System.Management.Automation;

namespace ArkanePsh.Cmdlets
{
    /// <summary>
    /// Base class for all ArkanePsh cmdlets.
    /// Provides common functionality for error handling, logging, and parameter validation.
    /// </summary>
    public abstract class ArkanePshCmdlet : PSCmdlet
    {
        /// <summary>
        /// Write a non-terminating error with consistent formatting.
        /// </summary>
        /// <param name="message">Error message</param>
        /// <param name="errorCode">Unique error code for debugging</param>
        /// <param name="targetObject">The object the error occurred on</param>
        protected void WriteErrorMessage(string message, string errorCode, object? targetObject = null)
        {
            var error = new ErrorRecord(
                new InvalidOperationException(message),
                errorCode,
                ErrorCategory.InvalidOperation,
                targetObject
            );
            WriteError(error);
        }

        /// <summary>
        /// Write a terminating error and stop processing.
        /// </summary>
        /// <param name="message">Error message</param>
        /// <param name="errorCode">Unique error code for debugging</param>
        /// <param name="targetObject">The object the error occurred on</param>
        protected void ThrowTerminatingError(string message, string errorCode, object? targetObject = null)
        {
            var error = new ErrorRecord(
                new InvalidOperationException(message),
                errorCode,
                ErrorCategory.InvalidOperation,
                targetObject
            );
            ThrowTerminatingError(error);
        }

        /// <summary>
        /// Write debug information if -Debug is specified.
        /// </summary>
        /// <param name="message">Debug message</param>
        protected void WriteDebugMessage(string message)
        {
            if (MyInvocation.BoundParameters.ContainsKey("Debug"))
            {
                WriteDebug($"[{this.GetType().Name}] {message}");
            }
        }
    }
}
