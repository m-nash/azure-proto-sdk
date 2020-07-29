using System;
using System.Diagnostics.CodeAnalysis;

namespace azure_proto_core
{
    /// <summary>
    /// Representation of a publisher plan for marketplace RPs.
    /// TODO: Implement Comparison operators and overloads
    /// </summary>
    public class Plan : IEquatable<Plan>, IComparable<Plan>
    {
        public string Name { get; set; }
        public string Publisher { get; set; }
        public string Product { get; set; }
        public string PromotionCode { get; set; }
        public string Version { get; set; }

        public int CompareTo([AllowNull] Plan other)
        {
            throw new NotImplementedException();
        }

        public bool Equals([AllowNull] Plan other)
        {
            throw new NotImplementedException();
        }
    }



}
