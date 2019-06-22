#region header

// ArkanePsh - ArkanePshObjectProviderBase.cs
// 
// Alistair J. R. Young
// Arkane Systems
// 
// Copyright Arkane Systems 2012-2019.  All rights reserved.
// 
// Created: 2019-06-22 3:58 PM

#endregion

#region using

using System.Collections.Generic ;
using System.Management.Automation ;
using System.Management.Automation.Provider ;

#endregion

namespace ArkaneSystems.ArkanePsh.Providers
{
    public abstract class ArkanePshObjectProviderBase : ContainerCmdletProvider, IContentCmdletProvider
    {
        private ArkanePshObjectDriveInfo ObjectDriveInfo => this.PSDriveInfo as ArkanePshObjectDriveInfo ;

        private PSObject CurrentObject
        {
            get
            {
                if (this.ObjectDriveInfo != null)
                    return this.ObjectDriveInfo.DriveObject ;

                return null ;
            }
        }

        private PSPropertyInfo GetProperty (string name)
        {
            if (this.CurrentObject != null)
                return this.CurrentObject.Properties[name] ;

            return null ;
        }

        private IEnumerable <ArkanePshVariable> GetProperties (string path)
        {
            if (this.CurrentObject == null)
                yield break ;

            if (string.IsNullOrEmpty (path))
            {
                foreach (PSPropertyInfo prop in this.CurrentObject.Properties)
                    yield return new ArkanePshVariable (prop.Name, prop.Value) ;
            }
            else
            {
                PSPropertyInfo prop = this.CurrentObject.Properties[path] ;

                if (prop != null)
                    yield return new ArkanePshVariable (prop.Name, prop.Value) ;
            }
        }

        protected override void ClearItem (string path)
        {
            if (this.CurrentObject != null)
                this.CurrentObject.Properties.Remove (path) ;
        }

        protected override void GetChildItems (string path, bool recurse)
        {
            foreach (ArkanePshVariable entry in this.GetProperties (path))
                this.WriteItemObject (entry, entry.Name, false) ;
        }

        protected override void GetChildNames (string path, ReturnContainers returnContainers)
        {
            foreach (ArkanePshVariable entry in this.GetProperties (path))
                this.WriteItemObject (entry.Name, entry.Name, false) ;
        }

        protected override void GetItem (string path)
        {
            if (this.CurrentObject != null)
                this.WriteItemObject (this.GetProperties (path), path, string.IsNullOrEmpty (path)) ;
        }

        protected override bool HasChildItems (string path)
        {
            if (this.CurrentObject == null)
                return false ;

            return string.IsNullOrEmpty (path) ;
        }

        protected override bool IsValidPath (string path)
        {
            // Altough this may seem strange, it's stolen right from
            // the MS.PS.C.SessionStateProviderBase class.

            if (this.CurrentObject == null)
                return false ;

            return !string.IsNullOrEmpty (path) ;
        }

        protected override bool ItemExists (string path) => string.IsNullOrEmpty (path) || (this.GetProperty (path) != null) ;

        protected override void RemoveItem (string path, bool recurse) { this.ClearItem (path) ; }

        protected override void SetItem (string path, object value)
        {
            if (this.CurrentObject != null)
            {
                PSPropertyInfo prop = this.GetProperty (path) ;

                if (prop != null)
                    prop.Value = value ;
                else
                    this.CurrentObject.Properties.Add (new PSNoteProperty (path, value)) ;
            }
        }

        #region IContentCmdletProvider Members

        private ArkanePshObjectProviderContent GetContent (string path)
        {
            if (this.CurrentObject != null)
                return new ArkanePshObjectProviderContent (this.CurrentObject, path) ;

            return null ;
        }

        public void ClearContent (string path)
        {
            PSPropertyInfo prop = this.GetProperty (path) ;

            if (prop != null)
                prop.Value = null ;
        }

        public IContentReader GetContentReader (string path) => this.GetContent (path) ;

        public IContentWriter GetContentWriter (string path) => this.GetContent (path) ;

        public object ClearContentDynamicParameters (string path) => null ;

        public object GetContentReaderDynamicParameters (string path) => null ;

        public object GetContentWriterDynamicParameters (string path) => null ;

        #endregion
    }
}
