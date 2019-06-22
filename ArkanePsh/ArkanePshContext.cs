#region header

// ArkanePsh - ArkanePshContext.cs
// 
// Alistair J. R. Young
// Arkane Systems
// 
// Copyright Arkane Systems 2012-2019.  All rights reserved.
// 
// Created: 2019-06-22 2:25 PM

#endregion

#region using

using System ;
using System.Collections ;
using System.IO ;
using System.Management.Automation ;
using System.Security.Principal ;

#endregion

namespace ArkaneSystems.ArkanePsh
{
    /// <summary>
    ///     A single container for all ArkanePsh variables, including preference and session variables.
    /// </summary>
    public sealed class ArkanePshContext
    {
        public string ArkanePshHome { get ; } = Path.GetDirectoryName (typeof (ArkanePshContext).Assembly.Location) ;

        public bool Is64BitProcess { get ; } = IntPtr.Size == 8 ;

        public bool IsAdmin => this.WindowsPrincipal.IsInRole (WindowsBuiltInRole.Administrator) ;

        public Hashtable Preferences { get ; } = new Hashtable (StringComparer.OrdinalIgnoreCase) ;

        public Hashtable Session { get ; } = new Hashtable (StringComparer.OrdinalIgnoreCase) ;

        public Version Version { get ; } = typeof (ArkanePshContext).Assembly.GetName ().Version ;

        public NTAccount WindowsAccount { get ; private set ; }

        public WindowsIdentity WindowsIdentity { get ; } = WindowsIdentity.GetCurrent () ;

        public WindowsPrincipal WindowsPrincipal { get ; private set ; } 

        #region Construction and instancing

        public static ArkanePshContext Instance { get ; } = new ArkanePshContext () ;

        public static PSObject InstanceAsPsObject => PSObject.AsPSObject (ArkanePshContext.Instance) ;

        /// <summary>
        ///     Construct an instance.
        /// </summary>
        private ArkanePshContext ()
        {
            this.WindowsPrincipal = new WindowsPrincipal (this.WindowsIdentity) ;
            this.WindowsAccount = (NTAccount) this.WindowsIdentity.User.Translate (typeof (NTAccount)) ;

            this.Preferences["CD_GetChildItem"]            = false ;
            this.Preferences["CD_EchoNewLocation"]         = true ;
        }

        #endregion
    }
}
