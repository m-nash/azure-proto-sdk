using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;

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
            return GetFilterString();
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
                builder.Add($"substringof('{Name}', name)");
            }

            if (!string.IsNullOrWhiteSpace(ResourceGroup))
            {
                builder.Add($"substringof('{ResourceGroup}', name)");
            }

            return string.Join(" and ", builder);
        }

        public static implicit operator ArmSubstringFilter(string nameString) => new ArmSubstringFilter { Name = nameString };
    }

    public class ArmResourceTypeFilter : ArmResourceFilter
    {
        public ArmResourceTypeFilter(ResourceType resourceType)
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
            return $"resourceType EQ '{ResourceType}'";
        }
    }

    public class ArmTagFilter : ArmResourceFilter
    {
        Tuple<string, string> _tag;

        public string Key { get; private set; }
        public string Value { get; private set; }

        public ArmTagFilter(Tuple<string, string> tag)
        {
            _tag = tag;
            Key = _tag.Item1;
            Value = _tag.Item2;
        }

        public ArmTagFilter(string tagKey, string tagValue) : this(new Tuple<string, string>(tagKey, tagValue)) { }

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
            return $"tagName eq '{_tag.Item1}' and tagValue eq '{_tag.Item2}'";
        }
    }

    public class ArmFilterCollection
    {
        public ArmFilterCollection() { }

        public ArmFilterCollection(ResourceType type)
        {
            ResourceTypeFilter = new ArmResourceTypeFilter(type);
        }

        public ArmSubstringFilter SubstringFilter { get; set; }

        public ArmResourceTypeFilter ResourceTypeFilter { get; }

        public ArmTagFilter TagFilter { get; set; }

        public override string ToString()
        {
            var builder = new List<string>();

            var substring = ResourceTypeFilter?.GetFilterString();
            if (!string.IsNullOrWhiteSpace(substring))
            {
                builder.Add(substring);
            }

            substring = SubstringFilter?.GetFilterString();
            if (!string.IsNullOrWhiteSpace(substring))
            {
                builder.Add(substring);
            }

            substring = TagFilter?.GetFilterString();
            if (!string.IsNullOrWhiteSpace(substring))
            {
                builder.Add(substring);
            }

            return $"{string.Join(" and ", builder)}";
        }
    }
}
