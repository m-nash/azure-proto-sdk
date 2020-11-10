using System;

namespace azure_proto_core
{
    /// <summary>
    /// Extensible representation of immutable identity kind.
    /// </summary>
    public struct IdentityKind : IEquatable<IdentityKind>, IEquatable<string>, IComparable<IdentityKind>, IComparable<string>
    {
        public static readonly IdentityKind SystemAssigned = new IdentityKind("SystemAssigned");
        public static readonly IdentityKind UserAssigned = new IdentityKind("UserAssigned");
        public static readonly IdentityKind SystemAndUserAssigned = new IdentityKind("SystemAndUserAssigned");
        public IdentityKind(string kind)
        {
            Value = kind;
        }

        public string Value { get; private set; }
        public int CompareTo(IdentityKind other)
        {
            if (this.Value == null && other.Value == null)
                return 0;
            else if (this.Value == null)
                return -1;
            else
                return this.Value.CompareTo(other.Value);
        }

        public int CompareTo(string other)
        {
            if (this.Value == null && other == null)
                return 0;
            else if (this.Value == null)
                return -1;
            else
                return this.Value.CompareTo(other);
        }

        public bool Equals(IdentityKind other)
        {
            if (this.Value == null && other.Value == null)
                return true;
            else if (this.Value == null)
                return false;
            else
                return this.Value.Equals(other.Value);
        }

        public bool Equals(string other)
        {
            if (this.Value == null && other == null)
                return true;
            else if (this.Value == null)
                return false;
            else
                return this.Value.Equals(other);
        }
    }
}
