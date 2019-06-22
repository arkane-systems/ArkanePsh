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

using System.Management.Automation ;

using JetBrains.Annotations ;

#endregion

namespace ArkaneSystems.ArkanePsh.Commands
{
    /// <summary>
    ///     Implementation of invoke-nothing cmdlet. Does nothing.
    /// </summary>
    [Cmdlet (VerbsLifecycle.Invoke, ArkanePshNouns.Nothing)]
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

        /// <inheritdoc />
        protected override void EndProcessing ()
        {
            this.WriteObject (this.InputObject) ;
            base.EndProcessing () ;
        }
    }
}
