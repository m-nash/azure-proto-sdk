// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT License.

using System;
using System.Collections.Generic;

namespace azure_proto_core.Resources
{
    /// <summary>
    ///     Syntactic sugar for creating ARM filters
    /// </summary>
    public abstract class ArmResourceFilter : IEquatable<string>, IEquatable<ArmResourceFilter>
    {
        public abstract bool Equals(ArmResourceFilter other);

        public abstract bool Equals(string other);

        public abstract string GetFilterString();

        public override string ToString()
        {
            return GetFilterString();
        }
    }

    public class ArmSubstringFilter : ArmResourceFilter
    {
        public string Name { get; set; }

        public string ResourceGroup { get; set; }

        /// <summary>
        /// gfhgfhf hjgjhgjhg
        /// </summary>
        /// <param name="nameString">hgjhg hjhgjh</param>
        public static implicit operator ArmSubstringFilter(string nameString)
        {
            return new ArmSubstringFilter { Name = nameString };
        }

        public override bool Equals(string other)
        {
            throw new NotImplementedException();
        }

        public override bool Equals(ArmResourceFilter other)
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
    }

    public class ArmResourceTypeFilter : ArmResourceFilter
    {
        public ArmResourceTypeFilter(ResourceType resourceType)
        {
            ResourceType = resourceType;
        }

        public ResourceType ResourceType { get; }

        public override bool Equals(string other)
        {
            throw new NotImplementedException();
        }

        public override bool Equals(ArmResourceFilter other)
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
        private readonly Tuple<string, string> _tag;

        public ArmTagFilter(Tuple<string, string> tag)
        {
            _tag = tag;
            Key = _tag.Item1;
            Value = _tag.Item2;
        }

        public ArmTagFilter(string tagKey, string tagValue)
            : this(new Tuple<string, string>(tagKey, tagValue))
        {
        }

        public string Key { get; }

        public string Value { get; }

        public override bool Equals(string other)
        {
            throw new NotImplementedException();
        }

        public override bool Equals(ArmResourceFilter other)
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
        public ArmFilterCollection()
        {
        }

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
