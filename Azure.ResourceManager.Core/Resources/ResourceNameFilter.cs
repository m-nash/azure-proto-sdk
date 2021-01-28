// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT License.

using System;
using System.Collections.Generic;

namespace Azure.ResourceManager.Core.Resources
{
    /// <summary>
    /// A class representing a substring filter used in Azure API calls.
    /// </summary>
    public class ResourceNameFilter : GenericResourceFilter
    {
        /// <summary>
        /// Gets or sets the name.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets the resource group.
        /// </summary>
        public string ResourceGroup { get; set; }

        /// <summary>
        /// Converts a string into an <see cref="ResourceNameFilter"/>.
        /// </summary>
        /// <param name="nameString"> The string that can be match in any part of the resource name. </param>
        public static implicit operator ResourceNameFilter(string nameString)
        {
            return new ResourceNameFilter { Name = nameString };
        }

        /// <inheritdoc/>
        public override bool Equals(string other)
        {
            throw new NotImplementedException();
        }

        /// <inheritdoc/>
        public override bool Equals(GenericResourceFilter other)
        {
            throw new NotImplementedException();
        }

        /// <inheritdoc/>
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
}
