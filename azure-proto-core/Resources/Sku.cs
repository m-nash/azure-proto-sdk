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

            int compareResult = 0;
            if ((compareResult = string.Compare(Name, other.Name)) == 0 &&
                (compareResult = string.Compare(Family, other.Family)) == 0 &&
                (compareResult = string.Compare(Size, other.Size)) == 0 &&
                (compareResult = string.Compare(Tier, other.Tier)) == 0)
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

            if (object.Equals(Name, other.Name) &&
                object.Equals(Family, other.Family) &&
                object.Equals(Size, other.Size) &&
                object.Equals(Tier, other.Tier) &&
                object.Equals(Capacity, other.Capacity))
            {
                return true;
            }

            return false;
        }
    }
}
