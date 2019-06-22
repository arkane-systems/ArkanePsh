#region header

// ArkanePsh - RelatedLinkAttribute.cs
// 
// Alistair J. R. Young
// Arkane Systems
// 
// Copyright Arkane Systems 2012-2019.  All rights reserved.
// 
// Created: 2019-06-22 11:30 AM

#endregion

#region using

using System ;
using System.Management.Automation ;

using JetBrains.Annotations ;

#endregion

namespace ArkaneSystems.ArkanePsh.Annotations
{
    [AttributeUsage (AttributeTargets.Class, AllowMultiple = true)]
    public class RelatedLinkAttribute : Attribute
    {
        public RelatedLinkAttribute ([NotNull] Type cmdletType)
        {
            var attr = (CmdletAttribute) Attribute.GetCustomAttribute (cmdletType, typeof (CmdletAttribute)) ;
        
            if (attr == null)
                throw new ArgumentException ($"The type {cmdletType} is not a cmdlet.") ;

            this.Text = attr.VerbName + '-' + attr.NounName ;
        }

        public RelatedLinkAttribute (string text) => this.Text = text ;

        public string Text { get ; set ; }
    }
}
