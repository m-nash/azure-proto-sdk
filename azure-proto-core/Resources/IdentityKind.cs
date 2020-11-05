
using System;
using System.Diagnostics.CodeAnalysis;

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
            bool? checkNull = checkBothNull(other.Value);
            if (checkNull == true)
                return 0;
            else if (checkNull == false)
                return -1;
            return this.Value.CompareTo(other.Value);
        }

        public int CompareTo(string other)
        {
            bool? checkNull = checkBothNull(other);
            if (checkNull == true)
                return 0;
            else if (checkNull == false)
                return -1;
            return this.Value.CompareTo(other);
        }

        public bool Equals(IdentityKind other)
        {
            bool? checkNull = checkBothNull(other.Value);
            if (checkNull == true)
                return true;
            if (checkNull == false)
                return false;
            return this.Value.Equals(other.Value);
        }

        public bool Equals(string other)
        {
            bool? checkNull = checkBothNull(other);
            if (checkNull == true)
                return true;
            if (checkNull == false)
                return false;
            return this.Value.Equals(other);
        }

        private bool? checkBothNull(string other)
        {
            bool? result = null;
            if (this.Value == null && other == null)
                result = true;
            else if (this.Value == null)
                result = false;
            return result;
        }
    }
}