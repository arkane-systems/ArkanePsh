#region header

// ArkanePsh - ArkanePshObjectProviderContent.cs
// 
// Alistair J. R. Young
// Arkane Systems
// 
// Copyright Arkane Systems 2012-2019.  All rights reserved.
// 
// Created: 2019-06-22 4:02 PM

#endregion

#region using

using System ;
using System.Collections ;
using System.IO ;
using System.Management.Automation ;
using System.Management.Automation.Provider ;

#endregion

namespace ArkaneSystems.ArkanePsh.Providers
{
    internal sealed class ArkanePshObjectProviderContent : IContentReader, IContentWriter
    {
        public ArkanePshObjectProviderContent (PSObject obj, string path)
        {
            this._object = obj ;
            this._path   = path ;
        }

        private readonly PSObject _object ;
        private readonly string   _path ;

        private bool _contentRead ;

        public void Close () { }

        public void Dispose () { }

        public void Seek (long offset, SeekOrigin origin) { throw new NotSupportedException () ; }

        public IList Read (long readCount)
        {
            PSPropertyInfo info   = this._object.Properties[this._path] ;
            IList          result = null ;

            if (!this._contentRead && (info != null))
            {
                object value = info.Value ;
                result = value as IList ;

                if (result == null)
                    result = new[] {value} ;

                this._contentRead = true ;
            }

            return result ;
        }

        public IList Write (IList content)
        {
            object value = content ;

            if (content.Count == 1)
                value = content[0] ;

            PSPropertyInfo info = this._object.Properties[this._path] ;

            if (info != null)
                info.Value = value ;
            else
                this._object.Properties.Add (new PSNoteProperty (this._path, value)) ;

            return content ;
        }
    }
}
