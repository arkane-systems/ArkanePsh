#region header

// ArkanePsh - NoOp.cs
// 
// Alistair J. R. Young
// Arkane Systems
// 
// Copyright Arkane Systems 2012-2019.  All rights reserved.
// 
// Created: 2019-06-22 9:52 AM

#endregion

#region using

using System ;
using System.ComponentModel ;
using System.Management.Automation ;

using ArkaneSystems.ArkanePsh.Annotations ;

using JetBrains.Annotations ;

#endregion

namespace ArkaneSystems.ArkanePsh.Commands
{
    /// <summary>
    ///     Implementation of invoke-nothing cmdlet. Does nothing.
    /// </summary>
    [Cmdlet (VerbsLifecycle.Invoke, ArkanePshNouns.Nothing)]
    [Description ("Does nothing.")]
    [DetailedDescription ("Does nothing, either succeeding or failing.")]
    public class NoOp : PSCmdlet
    {
        /// <summary>
        ///     Input object to the cmdlet.
        /// </summary>
        [Parameter (Position            = 0,
            ValueFromRemainingArguments = true,
            ValueFromPipeline           = true,
            HelpMessage                 = "Something to do nothing to.")]
        [UsedImplicitly]
        public object InputObject { get ; set ; }

        [Parameter (HelpMessage = "Fail at nothing.")]
        public SwitchParameter Fail { get ; set ; } = false ;

        /// <inheritdoc />
        protected override void EndProcessing ()
        {
            if (this.Fail)
                this.ThrowTerminatingError (new ErrorRecord (new InvalidOperationException ("Doing nothing and failing."),
                                                             "FailNaught",
                                                             ErrorCategory.NotEnabled,
                                                             this.InputObject)) ;

            this.WriteVerbose ("Doing nothing and succeeding.") ;

            this.WriteObject (this.InputObject) ;
            base.EndProcessing () ;
        }
    }
}
