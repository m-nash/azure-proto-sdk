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

            if (Family == null && other.Family != null)
            {
                return -1;
            }

            if (Family != null)
            {
                var compareFamilyResult = Family.CompareTo(other.Family);

                if (compareFamilyResult != 0)
                {
                    return compareFamilyResult;
                }
            }

            if (Size == null && other.Size != null)
            {
                return -1;
            }

            if (Size != null)
            {
                var compareSizeResult = Size.CompareTo(other.Size);

                if (compareSizeResult != 0)
                {
                    return compareSizeResult;
                }
            }

            if (Tier == null && other.Tier != null)
            {
                return -1;
            }

            if (Tier != null)
            {
                var compareTierResult = Tier.CompareTo(other.Tier);

                if (compareTierResult != 0)
                {
                    return compareTierResult;
                }
            }

            if (Capacity == null && other.Capacity == null)
            {
                return 0;
            }

            if (Capacity == null)
            {
                return -1;
            }

            if (other.Capacity == null)
            {
                return 1;
            }

            return Capacity.Value.CompareTo(other.Capacity.Value);
        }

        public bool Equals(Sku other)
        {
            if (other == null)
            {
                return false;
            }

            if (Name == null && other.Name != null || Name != null && !Name.Equals(other.Name))
            {
                return false;
            }

            if (Family == null && other.Family != null || Family != null && !Family.Equals(other.Family))
            {
                return false;
            }

            if (Size == null && other.Size != null || Size != null && !Size.Equals(other.Size))
            {
                return false;
            }

            if (Tier == null && other.Tier != null || Tier != null && !Tier.Equals(other.Tier))
            {
                return false;
            }

            if (Capacity == null && other.Capacity != null ||
                Capacity != null && !Capacity.Equals(other.Capacity))
            {
                return false;
            }

            return true;
        }
    }
}
