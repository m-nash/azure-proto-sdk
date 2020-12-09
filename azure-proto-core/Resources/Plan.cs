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

            int compareResult = 0;
            if ((compareResult = string.Compare(Name, other.Name)) == 0 &&
                (compareResult = string.Compare(Product, other.Product)) == 0 &&
                (compareResult = string.Compare(PromotionCode, other.PromotionCode)) == 0 &&
                (compareResult = string.Compare(Publisher, other.Publisher)) == 0 &&
                (compareResult = string.Compare(Version, other.Version)) == 0)
            {
                return 0;
            }

            return compareResult;
        }

        public bool Equals(Plan other)
        {
            if (other == null)
            {
                return false;
            }

            if (object.Equals(Name, other.Name) &&
                object.Equals(Product, other.Product) &&
                object.Equals(PromotionCode, other.PromotionCode) &&
                object.Equals(Publisher, other.Publisher) &&
                object.Equals(Version, other.Version))
            {
                return true;
            }

            return false;
        }
    }
}
