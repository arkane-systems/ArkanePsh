#region header

// ArkanePsh - ArkanePshSettingsProvider.cs
// 
// Alistair J. R. Young
// Arkane Systems
// 
// Copyright Arkane Systems 2012-2019.  All rights reserved.
// 
// Created: 2019-06-22 3:48 PM

#endregion

#region using

using System ;
using System.Collections.Generic ;
using System.Collections.ObjectModel ;
using System.Management.Automation ;
using System.Management.Automation.Provider ;

#endregion

namespace ArkaneSystems.ArkanePsh.Providers
{
    [CmdletProvider ("ArkanePshSettings", ProviderCapabilities.None)]
    public class ArkanePshSettingsProvider : ArkanePshObjectProviderBase, IPropertyCmdletProvider
    {
        #region Nested type: ArkanePshSettingsProperty

        private sealed class ArkanePshSettingsProperty : Dictionary <string, object>
        {
            public ArkanePshSettingsProperty () : base (StringComparer.OrdinalIgnoreCase) { }

            public void RemoveRange (IEnumerable <string> keys)
            {
                foreach (string key in keys)
                    this.Remove (key) ;
            }
        }

        #endregion

        #region Nested type: PscxSettingsDriveInfo

        private sealed class ArkanePshSettingsDriveInfo : ArkanePshObjectDriveInfo
        {
            public ArkanePshSettingsDriveInfo (ProviderInfo provider, PSObject obj)
                : base ("Arkane", provider, "ArkanePsh settings and published data", obj)
            { }

            private readonly Dictionary <string, ArkanePshSettingsProperty> _properties =
                new Dictionary <string, ArkanePshSettingsProperty> (StringComparer.OrdinalIgnoreCase) ;

            public void ClearProperty (string path, Collection <string> propertiesToClear)
            {
                ArkanePshSettingsProperty itemProperties ;

                if (this._properties.TryGetValue (path, out itemProperties))
                {
                    if ((propertiesToClear == null) || (propertiesToClear.Count == 0))
                        this._properties.Remove (path) ;
                    else
                        itemProperties.RemoveRange (propertiesToClear) ;
                }
            }

            public PSObject GetProperty (string path, Collection <string> propertiesToGet)
            {
                PSObject                  result = null ;
                ArkanePshSettingsProperty itemProperties ;

                if (this._properties.TryGetValue (path, out itemProperties))
                {
                    result = new PSObject () ;
                    bool getAll = propertiesToGet.Count == 0 ;

                    foreach (KeyValuePair <string, object> entry in itemProperties)
                    {
                        if (getAll || ArkanePshSettingsDriveInfo.ContainsOrdinalCI (propertiesToGet, entry.Key))
                            result.Properties.Add (new PSNoteProperty (entry.Key, entry.Value)) ;
                    }
                }

                return result ;
            }

            public void SetProperty (string path, PSObject value)
            {
                ArkanePshSettingsProperty itemProperties ;

                if (!this._properties.TryGetValue (path, out itemProperties))
                {
                    itemProperties         = new ArkanePshSettingsProperty () ;
                    this._properties[path] = itemProperties ;
                }

                foreach (PSPropertyInfo pspi in value.Properties)
                    itemProperties[pspi.Name] = pspi.Value ;
            }

            private static bool ContainsOrdinalCI (IEnumerable <string> strings, string value)
            {
                foreach (string s in strings)
                {
                    if (StringComparer.OrdinalIgnoreCase.Equals (s, value))
                        return true ;
                }

                return false ;
            }
        }

        #endregion

        private ArkanePshSettingsDriveInfo SettingsDriveInfo => PSDriveInfo as ArkanePshSettingsDriveInfo ;

        public void ClearProperty (string path, Collection <string> properties)
        {
            if ((this.SettingsDriveInfo != null) && !string.IsNullOrEmpty (path))
                this.SettingsDriveInfo.ClearProperty (path, properties) ;
        }

        public void GetProperty (string path, Collection <string> properties)
        {
            if ((this.SettingsDriveInfo != null) && !string.IsNullOrEmpty (path))
            {
                PSObject values = this.SettingsDriveInfo.GetProperty (path, properties) ;

                if (values != null)
                    WritePropertyObject (values, path) ;
            }
        }

        public void SetProperty (string path, PSObject propertyValue)
        {
            if ((this.SettingsDriveInfo != null) && !string.IsNullOrEmpty (path))
                this.SettingsDriveInfo.SetProperty (path, propertyValue) ;
        }

        public object ClearPropertyDynamicParameters (string path, Collection <string> propertyToClear) => null ;

        public object GetPropertyDynamicParameters (string path, Collection <string> providerSpecificPickList) => null ;

        public object SetPropertyDynamicParameters (string path, PSObject propertyValue) => null ;

        protected override Collection <PSDriveInfo> InitializeDefaultDrives () =>
            new Collection <PSDriveInfo> {new ArkanePshSettingsDriveInfo (ProviderInfo, ArkanePshContext.InstanceAsPsObject)} ;

        protected override void ClearItem (string path)
        {
            base.ClearItem (path) ;
            this.ClearProperty (path, null) ;
        }

        protected override void RemoveItem (string path, bool recurse)
        {
            base.RemoveItem (path, recurse) ;
            this.ClearProperty (path, null) ;
        }
    }
}
