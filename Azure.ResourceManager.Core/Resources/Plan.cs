﻿// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT License.

using System;

namespace Azure.ResourceManager.Core
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
                return 1;

            if (object.ReferenceEquals(this, other))
                return 0;

            int compareResult = 0;
            if ((compareResult = string.Compare(Name, other.Name, StringComparison.InvariantCultureIgnoreCase)) == 0 &&
                (compareResult = string.Compare(Product, other.Product, StringComparison.InvariantCultureIgnoreCase)) == 0 &&
                (compareResult = string.Compare(PromotionCode, other.PromotionCode, StringComparison.InvariantCultureIgnoreCase)) == 0 &&
                (compareResult = string.Compare(Publisher, other.Publisher, StringComparison.InvariantCultureIgnoreCase)) == 0 &&
                (compareResult = string.Compare(Version, other.Version, StringComparison.InvariantCultureIgnoreCase)) == 0)
            {
                return 0;
            }

            return compareResult;
        }

        public bool Equals(Plan other)
        {
            if (other == null)
                return false;

            if (object.ReferenceEquals(this, other))
                return true;

            return string.Equals(Name, other.Name, StringComparison.InvariantCultureIgnoreCase) &&
                string.Equals(Product, other.Product, StringComparison.InvariantCultureIgnoreCase) &&
                string.Equals(PromotionCode, other.PromotionCode, StringComparison.InvariantCultureIgnoreCase) &&
                string.Equals(Publisher, other.Publisher, StringComparison.InvariantCultureIgnoreCase) &&
                string.Equals(Version, other.Version, StringComparison.InvariantCultureIgnoreCase);
        }
    }
}
