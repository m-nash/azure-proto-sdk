using System;
using System.Diagnostics.CodeAnalysis;

namespace azure_proto_core
{
    /// <summary>
    /// Representaion of ARM SKU
    /// </summary>
    public class Sku : IEquatable<Sku>, IComparable<Sku>
    {
        public string Name { get; set; }
        public string Tier { get; set; }
        public string Family { get; set; }
        public string Size { get; set; }
        public long? Capacity { get; set; }

        public int CompareTo(Sku other)
        {
            if (other == null) return 1;
            if (this.Name == null && other.Name != null) return -1;
            if (this.Name != null && this.Name.CompareTo(other.Name) != 0) return this.Name.CompareTo(other.Name);
            if (this.Family == null && other.Family != null) return -1;
            if (this.Family != null && this.Family.CompareTo(other.Family) != 0) return this.Family.CompareTo(other.Family);
            if (this.Size == null && other.Size != null) return -1;
            if (this.Size != null && this.Size.CompareTo(other.Size) != 0) return this.Size.CompareTo(other.Size);
            if (this.Tier == null && other.Tier != null) return -1;
            if (this.Tier != null && this.Tier.CompareTo(other.Tier) != 0) return this.Tier.CompareTo(other.Tier);
            if (this.Capacity == null && other.Capacity == null) return 0;
            if (this.Capacity == null) return -1;
            if (other.Capacity == null) return 1;
            return this.Capacity.Value.CompareTo(other.Capacity.Value);
        }

        public bool Equals(Sku other)
        {
            if (other == null) return false;
            if (this.Name == null && other.Name != null || this.Name != null && !this.Name.Equals(other.Name)) return false;
            if (this.Family == null && other.Family != null || this.Family != null && !this.Family.Equals(other.Family)) return false;
            if (this.Size == null && other.Size != null || this.Size != null && !this.Size.Equals(other.Size)) return false;
            if (this.Tier == null && other.Tier != null || this.Tier != null && !this.Tier.Equals(other.Tier)) return false;
            if (this.Capacity == null && other.Capacity != null || this.Capacity != null && !this.Capacity.Equals(other.Capacity)) return false;
            return true;
        }
    }
}
