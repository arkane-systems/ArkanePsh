#region header

// ArkanePsh - ArkanePshObjectDriveInfo.cs
// 
// Alistair J. R. Young
// Arkane Systems
// 
// Copyright Arkane Systems 2012-2019.  All rights reserved.
// 
// Created: 2019-06-22 3:55 PM

#endregion

#region using

using System.Management.Automation ;

#endregion

namespace ArkaneSystems.ArkanePsh.Providers
{
    public class ArkanePshObjectDriveInfo : PSDriveInfo
    {
        public ArkanePshObjectDriveInfo (string name, ProviderInfo provider, string description, PSObject obj)
            : base (name, provider, string.Empty, description, null) =>
            this.DriveObject = obj ;

        public PSObject DriveObject { get ; }
    }
}
