using System;
using System.Diagnostics.CodeAnalysis;

namespace azure_proto_core
{
    /// <summary>
    /// Represents a managed identity
    /// TODO: fill in properties, implement comparison and equality methods and operator overloads
    /// </summary>
    public class Identity : IEquatable<Identity>, IComparable<Identity>
    {
        public Guid TenantId { get; set; }

        public Guid PrincipalId { get; set; }

        public Guid ClientId { get; set; }

        public ResourceIdentifier ResourceId { get; set; }

        public IdentityKind Kind { get; set; }

        public int CompareTo([AllowNull] Identity other)
        {
            throw new NotImplementedException();
        }

        public bool Equals([AllowNull] Identity other)
        {
            throw new NotImplementedException();
        }
    }



}
