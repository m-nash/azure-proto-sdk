// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT License.

using System;

namespace azure_proto_core
{
    /// <summary>
    ///     Representation of a publisher plan for marketplace RPs.
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
            if (other == null)
            {
                return 1;
            }

            if (Name == null && other.Name != null)
            {
                return -1;
            }

            if (Name != null)
            {
                var compareNameResult = Name.CompareTo(other.Name);

                if (compareNameResult != 0)
                {
                    return compareNameResult;
                }
            }

            if (Product == null && other.Product != null)
            {
                return -1;
            }

            if (Product != null)
            {
                var compareProductResult = Product.CompareTo(other.Product);

                if (compareProductResult != 0)
                {
                    return compareProductResult;
                }
            }

            if (PromotionCode == null && other.PromotionCode != null)
            {
                return -1;
            }

            if (PromotionCode != null)
            {
                var comparePromotionCodeResult = PromotionCode.CompareTo(other.PromotionCode);

                if (comparePromotionCodeResult != 0)
                {
                    return comparePromotionCodeResult;
                }
            }

            if (Publisher == null && other.Publisher != null)
            {
                return -1;
            }

            if (Publisher != null)
            {
                var comparePublisherResult = Publisher.CompareTo(other.Publisher);

                if (comparePublisherResult != 0)
                {
                    return comparePublisherResult;
                }
            }

            if (Version == null && other.Version != null)
            {
                return -1;
            }

            if (Version != null)
            {
                var compareVersionResult = Version.CompareTo(other.Version);

                if (compareVersionResult != 0)
                {
                    return compareVersionResult;
                }
            }

            return 0;
        }

        public bool Equals(Plan other)
        {
            if (other == null)
            {
                return false;
            }

            if (Name == null && other.Name != null || Name != null && !Name.Equals(other.Name))
            {
                return false;
            }

            if (Product == null && other.Product != null || Product != null && !Product.Equals(other.Product))
            {
                return false;
            }

            if (PromotionCode == null && other.PromotionCode != null ||
                PromotionCode != null && !PromotionCode.Equals(other.PromotionCode))
            {
                return false;
            }

            if (Publisher == null && other.Publisher != null ||
                Publisher != null && !Publisher.Equals(other.Publisher))
            {
                return false;
            }

            if (Version == null && other.Version != null || Version != null && !Version.Equals(other.Version))
            {
                return false;
            }

            return true;
        }
    }
}
