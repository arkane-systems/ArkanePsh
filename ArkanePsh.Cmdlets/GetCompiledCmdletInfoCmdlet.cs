using System;
using System.Management.Automation;

namespace ArkanePsh.Cmdlets
{
    /// <summary>
    /// Template cmdlet demonstrating the pattern for compiled ArkanePsh cmdlets.
    /// Remove or replace this with actual cmdlet implementations.
    /// </summary>
    [Cmdlet(VerbsCommon.Get, "CompiledCmdletInfo")]
    [OutputType(typeof(string))]
    public class GetCompiledCmdletInfoCmdlet : ArkanePshCmdlet
    {
        [Parameter(Mandatory = false, ValueFromPipeline = true)]
        public string? Name { get; set; }

        protected override void ProcessRecord()
        {
            try
            {
                var info = $"ArkanePsh Compiled Cmdlets Module - Version {this.GetType().Assembly.GetName().Version}";
                if (!string.IsNullOrEmpty(Name))
                {
                    info += $" (Requested by: {Name})";
                }
                WriteObject(info);
            }
            catch (Exception ex)
            {
                WriteErrorMessage($"Failed to get cmdlet info: {ex.Message}", "GetCmdletInfoFailed");
            }
        }
    }
}
