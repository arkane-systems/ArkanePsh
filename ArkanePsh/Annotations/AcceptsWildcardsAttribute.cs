#region header

// ArkanePsh - AcceptsWildcardsAttribute.cs
// 
// Alistair J. R. Young
// Arkane Systems
// 
// Copyright Arkane Systems 2012-2019.  All rights reserved.
// 
// Created: 2019-06-22 11:31 AM

#endregion

#region using

using System ;

#endregion

namespace ArkaneSystems.ArkanePsh.Annotations
{
    [AttributeUsage (AttributeTargets.Property)]
    public class AcceptsWildcardsAttribute : Attribute
    {
        public AcceptsWildcardsAttribute (bool value) => this.Value = value ;

        public bool Value { get ; set ; }
    }
}
