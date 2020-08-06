using System;
using System.Diagnostics.CodeAnalysis;

namespace azure_proto_core
{
    /// <summary>
    /// Extensible representation of immutable identity kind.
    /// TODO: Implement comparison methods and standard operator overloads for immutable types
    /// </summary>
    public struct IdentityKind : IEquatable<IdentityKind>, IEquatable<string>, IComparable<IdentityKind>, IComparable<string>
    {
        public static IdentityKind SystemAssigned = new IdentityKind("SystemAssigned");
        public static IdentityKind UserAssigned = new IdentityKind("UserAssigned");
        public IdentityKind(string kind)
        {
            Value = kind;
        }

        public string Value { get; private set; }
        public int CompareTo([AllowNull] IdentityKind other)
        {
            throw new NotImplementedException();
        }

        public int CompareTo([AllowNull] string other)
        {
            throw new NotImplementedException();
        }

        public bool Equals([AllowNull] IdentityKind other)
        {
            throw new NotImplementedException();
        }

        public bool Equals([AllowNull] string other)
        {
            throw new NotImplementedException();
        }
    }
}
