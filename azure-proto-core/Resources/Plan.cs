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
            if (this.Name != null)
            {
                int compareNameResult = this.Name.CompareTo(other.Name);
                if (compareNameResult != 0) return compareNameResult;
            }
            if (this.Product == null && other.Product != null) return -1;
            if (this.Product != null)
            {
                int compareProductResult = this.Product.CompareTo(other.Product);
                if (compareProductResult != 0) return compareProductResult;
            }
            if (this.PromotionCode == null && other.PromotionCode != null) return -1;
            if (this.PromotionCode != null)
            {
                int comparePromotionCodeResult = this.PromotionCode.CompareTo(other.PromotionCode);
                if (comparePromotionCodeResult != 0) return comparePromotionCodeResult;
            }
            if (this.Publisher == null && other.Publisher != null) return -1;
            if (this.Publisher != null)
            {
                int comparePublisherResult = this.Publisher.CompareTo(other.Publisher);
                if (comparePublisherResult != 0) return comparePublisherResult;
            }
            if (this.Version == null && other.Version != null) return -1;
            if (this.Version != null)
            {
                int compareVersionResult = this.Version.CompareTo(other.Version);
                if (compareVersionResult != 0) return compareVersionResult;
            }
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
