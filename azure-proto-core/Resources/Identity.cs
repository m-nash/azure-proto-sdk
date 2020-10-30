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

        public int CompareTo(Identity other)
        {
            if (Object.ReferenceEquals(null, other))
                return 1;
            return this.ResourceId.CompareTo(other.ResourceId);
        }

        public bool Equals(Identity other)
        {
            if (Object.ReferenceEquals(null, other))
                return false;
            else if (this.TenantId.Equals(other.TenantId) &&
                this.PrincipalId.Equals(other.PrincipalId) &&
                this.ClientId.Equals(other.ClientId) &&
                this.ResourceId.Equals(other.ResourceId) &&
                this.Kind.Equals(other.Kind))
                return true;
            else
                return false;            
        }
    }
}
