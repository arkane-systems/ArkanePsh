#region header

// ArkanePsh - CmdletBase.cs
// 
// Alistair J. R. Young
// Arkane Systems
// 
// Copyright Arkane Systems 2012-2019.  All rights reserved.
// 
// Created: 2019-06-22 1:24 PM

#endregion

#region using

using System ;
using System.Management.Automation ;

#endregion

namespace ArkaneSystems.ArkanePsh.Commands
{
    public abstract class CmdletBase : PSCmdlet, IDisposable
    {
        #region Disposal

        /// <inheritdoc />
        public void Dispose ()
        {
            try
            {
                this.Dispose (true) ;
            }
            finally
            {
                GC.SuppressFinalize (this) ;
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
        protected virtual void Dispose (bool disposing) { }

        #endregion

        #region Informational methods

        private string cmdletName ;

        public string CmdletName
        {
            get
            {
                if (this.cmdletName == null)
                {
                    var cmdletAttr = (CmdletAttribute) Attribute.GetCustomAttribute (this.GetType (), typeof (CmdletAttribute)) ;
                    if (cmdletAttr != null)
                        this.cmdletName = cmdletAttr.VerbName + "-" + cmdletAttr.NounName ;
                }

                return this.cmdletName ;
            }
        }

        protected static ArkanePshContext Context => ArkanePshContext.Instance ;

        #endregion
    }
}
