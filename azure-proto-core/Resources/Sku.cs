// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT License.

using System;

namespace azure_proto_core
{
    /// <summary>
    ///     Representaion of ARM SKU
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
            if (other == null)
            {
                return 1;
            }

            if (object.ReferenceEquals(this, other))
            {
                return 0;
            }

            int compareResult = 0;
            if ((compareResult = string.Compare(Name, other.Name, StringComparison.InvariantCultureIgnoreCase)) == 0 &&
                (compareResult = string.Compare(Family, other.Family, StringComparison.InvariantCultureIgnoreCase)) == 0 &&
                (compareResult = string.Compare(Size, other.Size, StringComparison.InvariantCultureIgnoreCase)) == 0 &&
                (compareResult = string.Compare(Tier, other.Tier, StringComparison.InvariantCultureIgnoreCase)) == 0)
            {
                return Nullable.Compare<long>(Capacity, other.Capacity);
            }

            return compareResult;
        }

        public bool Equals(Sku other)
        {
            if (other == null)
            {
                return false;
            }

            if (object.ReferenceEquals(this, other))
            {
                return true;
            }

            return string.Equals(Name, other.Name, StringComparison.InvariantCultureIgnoreCase) &&
                string.Equals(Family, other.Family, StringComparison.InvariantCultureIgnoreCase) &&
                string.Equals(Size, other.Size, StringComparison.InvariantCultureIgnoreCase) &&
                string.Equals(Tier, other.Tier, StringComparison.InvariantCultureIgnoreCase) &&
                long.Equals(Capacity, other.Capacity);
        }
    }
}
