using System;
using System.Diagnostics.CodeAnalysis;

namespace azure_proto_core
{
    /// <summary>
    /// Representation of a publisher plan for marketplace RPs.
    /// </summary>
    public class Plan : IEquatable<Plan>, IComparable<Plan>
    {
        public string Name { get; set; }
        public string Publisher { get; set; }
        public string Product { get; set; }
        public string PromotionCode { get; set; }
        public string Version { get; set; }

        public int CompareTo(Plan other)
        {
            if (other == null) return 1;
            if (this.Name == null && other.Name != null) return -1;
            if (this.Name != null && this.Name.CompareTo(other.Name) != 0) return this.Name.CompareTo(other.Name);
            if (this.Product == null && other.Product != null) return -1;
            if (this.Product != null && this.Product.CompareTo(other.Product) != 0) return this.Product.CompareTo(other.Product);
            if (this.PromotionCode == null && other.PromotionCode != null) return -1;
            if (this.PromotionCode != null && this.PromotionCode.CompareTo(other.PromotionCode) != 0) return this.PromotionCode.CompareTo(other.PromotionCode);
            if (this.Publisher == null && other.Publisher != null) return -1;
            if (this.Publisher != null && this.Publisher.CompareTo(other.Publisher) != 0) return this.Publisher.CompareTo(other.Publisher);
            if (this.Version == null && other.Version != null) return -1;
            if (this.Version != null && this.Version.CompareTo(other.Version) != 0) return this.Version.CompareTo(other.Version);
            return 0;
        }

        public bool Equals(Plan other)
        {
            if (other == null) return false;
            if (this.Name == null && other.Name != null || this.Name != null && !this.Name.Equals(other.Name)) return false;
            if (this.Product == null && other.Product != null || this.Product != null && !this.Product.Equals(other.Product)) return false;
            if (this.PromotionCode == null && other.PromotionCode != null || this.PromotionCode != null && !this.PromotionCode.Equals(other.PromotionCode)) return false;
            if (this.Publisher == null && other.Publisher != null || this.Publisher != null && !this.Publisher.Equals(other.Publisher)) return false;
            if (this.Version == null && other.Version != null || this.Version != null && !this.Version.Equals(other.Version)) return false;
            return true;
        }
    }
}
