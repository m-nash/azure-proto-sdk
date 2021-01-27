// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT License.

using System;
using System.Collections.Generic;

namespace Azure.ResourceManager.Core.Resources
{
    /// <summary>
    /// A class representing a substring filter used in Azure API calls.
    /// </summary>
    public class ArmSubstringFilter : ArmResourceFilter
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
        /// Converts a string into an <see cref="ArmSubstringFilter"/>.
        /// </summary>
        /// <param name="nameString">hgjhg hjhgjh</param>
        public static implicit operator ArmSubstringFilter(string nameString)
        {
            return new ArmSubstringFilter { Name = nameString };
        }

        /// <inheritdoc/>
        public override bool Equals(string other)
        {
            throw new NotImplementedException();
        }

        /// <inheritdoc/>
        public override bool Equals(ArmResourceFilter other)
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
