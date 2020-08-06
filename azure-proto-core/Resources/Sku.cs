using System;
using System.Diagnostics.CodeAnalysis;

namespace azure_proto_core
{
    /// <summary>
    /// Representaion of ARM SKU
    /// TODO: Implement comparison methods and operator overloads
    /// </summary>
    public class Sku : IEquatable<Sku>, IComparable<Sku>
    {
        public string Name { get; set; }
        public string Tier { get; set; }
        public string Family { get; set; }
        public string Size { get; set; }
        public long? Capacity { get; set; }

        public int CompareTo([AllowNull] Sku other)
        {
            throw new NotImplementedException();
        }

        public bool Equals([AllowNull] Sku other)
        {
            throw new NotImplementedException();
        }
    }
}
