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
            if (this.Name.CompareTo(other.Name) == 0)
            {
                if (this.Product.CompareTo(other.Product) == 0)
                {
                    if (this.PromotionCode.CompareTo(other.PromotionCode) == 0)
                    {
                        if (this.Publisher.CompareTo(other.Publisher) == 0)
                        {
                            return this.Version.CompareTo(other.Version);
                        }
                        return this.Publisher.CompareTo(other.Publisher);
                    }
                    return this.PromotionCode.CompareTo(other.PromotionCode);
                }
                return this.Product.CompareTo(other.Product);
            }
            return this.Name.CompareTo(other.Name);
        }

        public bool Equals(Plan other)
        {
            if (other == null) return false;
            if (this.Name.Equals(other.Name))
            {
                if (this.Product.Equals(other.Product))
                {
                    if (this.PromotionCode.Equals(other.PromotionCode))
                    {
                        if (this.Publisher.Equals(other.Publisher))
                        {
                            return this.Version.Equals(other.Version);
                        }
                    }
                }
            }
            return false;
        }
    }
}
