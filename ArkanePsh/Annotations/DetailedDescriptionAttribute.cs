#region header

// ArkanePsh - DetailedDescriptionAttribute.cs
// 
// Alistair J. R. Young
// Arkane Systems
// 
// Copyright Arkane Systems 2012-2019.  All rights reserved.
// 
// Created: 2019-06-22 11:29 AM

#endregion

#region using

using System ;

#endregion

namespace ArkaneSystems.ArkanePsh.Annotations
{
    [AttributeUsage (AttributeTargets.Class)]
    public class DetailedDescriptionAttribute : Attribute
    {
        public DetailedDescriptionAttribute (string text) => this.Text = text ;

        public string Text { get ; set ; }
    }
}
