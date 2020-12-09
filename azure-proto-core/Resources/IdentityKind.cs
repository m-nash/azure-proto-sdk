// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT License.

using System;

namespace azure_proto_core
{
    /// <summary>
    ///     Extensible representation of immutable identity kind.
    /// </summary>
    public struct IdentityKind : IEquatable<IdentityKind>, IEquatable<string>, IComparable<IdentityKind>,
        IComparable<string>
    {
        public static readonly IdentityKind SystemAssigned = new IdentityKind("SystemAssigned");
        public static readonly IdentityKind UserAssigned = new IdentityKind("UserAssigned");
        public static readonly IdentityKind SystemAndUserAssigned = new IdentityKind("SystemAndUserAssigned");

        public IdentityKind(string kind)
        {
            Value = kind;
        }

        public string Value { get; }

        public int CompareTo(IdentityKind other)
        {
            if (Value == null && other.Value == null)
            {
                return 0;
            }

            if (Value == null)
            {
                return -1;
            }

            return Value.CompareTo(other.Value);
        }

        public int CompareTo(string other)
        {
            if (Value == null && other == null)
            {
                return 0;
            }

            if (Value == null)
            {
                return -1;
            }

            return Value.CompareTo(other);
        }

        public bool Equals(IdentityKind other)
        {
            if (Value == null && other.Value == null)
            {
                return true;
            }

            if (Value == null)
            {
                return false;
            }

            return Value.Equals(other.Value);
        }

        public bool Equals(string other)
        {
            if (Value == null && other == null)
            {
                return true;
            }

            if (Value == null)
            {
                return false;
            }

            return Value.Equals(other);
        }
    }
}
