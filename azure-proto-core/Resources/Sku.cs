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
            if (this.Name.CompareTo(other.Name) == 0)
            {
                if (this.Family.CompareTo(other.Family) == 0)
                {
                    if (this.Size.CompareTo(other.Size) == 0)
                    {
                        if (this.Tier.CompareTo(other.Tier) == 0)
                        {
                            if (this.Capacity == null && other.Capacity == null) return 0;
                            if (other.Capacity == null) return 1;
                            if (this.Capacity == null) return -1;
                            return this.Capacity.Value.CompareTo(other.Capacity.Value);
                        }
                        return this.Tier.CompareTo(other.Tier);
                    }
                    return this.Size.CompareTo(other.Size);
                }
                return this.Family.CompareTo(other.Family);
            }
            return this.Name.CompareTo(other.Name);
        }

        public bool Equals(Sku other)
        {
            if (other == null) return false;
            if (this.Name.Equals(other.Name))
            {
                if (this.Family.Equals(other.Family))
                {
                    if (this.Size.Equals(other.Size))
                    {
                        if (this.Tier.Equals(other.Tier))
                        {
                            if (this.Capacity == null && other.Capacity == null) return true;
                            if (this.Capacity == null || other.Capacity == null) return false;
                            return this.Capacity.Equals(other.Capacity);
                        }
                    }
                }
            }
            return false;
        }
    }
}
