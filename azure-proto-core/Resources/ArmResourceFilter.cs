using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.CompilerServices;
using System.Text;

namespace azure_proto_core.Resources
{
    /// <summary>
    /// Syntactic sugar for creating ARM filters
    /// </summary>
    public abstract class ArmResourceFilter : IEquatable<string>, IEquatable<ArmResourceFilter>
    {
        public ArmResourceFilter()
        {
        }
        public abstract bool Equals([AllowNull] string other);

        public abstract string GetFilterString();

        public abstract bool Equals([AllowNull] ArmResourceFilter other);

        public override string ToString()
        {
            return $"$filter={GetFilterString()}";
        }
    }

    public class ArmSubstringFilter : ArmResourceFilter
    {
        public string Name { get; set; }

        public string ResourceGroup { get; set; }
        public override bool Equals([AllowNull] string other)
        {
            throw new NotImplementedException();
        }

        public override bool Equals([AllowNull] ArmResourceFilter other)
        {
            throw new NotImplementedException();
        }

        public override string GetFilterString()
        {
            var builder = new List<string>();
            if (!string.IsNullOrWhiteSpace(Name))
            {
                builder.Add($"substringof({Name}, name)");
            }

            if (!string.IsNullOrWhiteSpace(ResourceGroup))
            {
                builder.Add($"substringof({ResourceGroup}, name)");
            }

            return string.Join(" and ", builder);
        }

        public static implicit operator ArmSubstringFilter(string nameString) => new ArmSubstringFilter { Name = nameString};
    }

    public class ArmResourceTypeFilter : ArmResourceFilter
    {
        public ArmResourceTypeFilter( ResourceType resourceType)
        {
            ResourceType = resourceType;
        }

        public ResourceType ResourceType { get; }

        public override bool Equals([AllowNull] string other)
        {
            throw new NotImplementedException();
        }

        public override bool Equals([AllowNull] ArmResourceFilter other)
        {
            throw new NotImplementedException();
        }

        public override string GetFilterString()
        {
           return $"resourceType eq '{ResourceType}'";
        }
    }

    public class ArmFilterCollection
    {
        public ArmFilterCollection(ResourceType type)
        {
            ResourceTypeFilter = new ArmResourceTypeFilter(type);
        }
        public ArmSubstringFilter SubstringFilter {get; set;}

        public ArmResourceTypeFilter ResourceTypeFilter { get; }

        public override string ToString()
        {
            var builder = new List<string>();
            builder.Add(ResourceTypeFilter.GetFilterString());
            var substring = SubstringFilter?.GetFilterString();
            if (!string.IsNullOrWhiteSpace(substring))
            {
                builder.Add(substring);
            }

            return $"$filter={string.Join(" and ", builder)}";
        }
    }
}
