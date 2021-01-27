// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT License.

using System.Collections.Generic;

namespace Azure.ResourceManager.Core.Resources
{
    /// <summary>
    /// A class representing a collection of arm filters.
    /// </summary>
    public class ArmFilterCollection
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ArmFilterCollection"/> class.
        /// </summary>
        public ArmFilterCollection()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ArmFilterCollection"/> class.
        /// </summary>
        /// <param name="type"> The resource type to filter by. </param>
        public ArmFilterCollection(ResourceType type)
        {
            ResourceTypeFilter = new ArmResourceTypeFilter(type);
        }

        /// <summary>
        /// Gets or sets the substring filter to use in the collection.
        /// </summary>
        public ArmSubstringFilter SubstringFilter { get; set; }

        /// <summary>
        /// Gets the resource type filter to use in the collection.
        /// </summary>
        public ArmResourceTypeFilter ResourceTypeFilter { get; }

        /// <summary>
        /// Gets or sets the tag filter to use in the collection.
        /// </summary>
        public ArmTagFilter TagFilter { get; set; }

        /// <inheritdoc/>
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
