#region header

// ArkanePsh - ArkanePshVariable.cs
// 
// Alistair J. R. Young
// Arkane Systems
// 
// Copyright Arkane Systems 2012-2019.  All rights reserved.
// 
// Created: 2019-06-22 3:58 PM

#endregion

namespace ArkaneSystems.ArkanePsh.Providers
{
    public class ArkanePshVariable
    {
        public ArkanePshVariable (string name, object value)
        {
            this.Name  = name ;
            this.Value = value ;
        }

        public string Name { get ; }

        public object Value { get ; }
    }
}
