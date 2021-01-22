// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT License.

using System;

namespace Azure.ResourceManager.Core.Resources
{
    /// <summary>
    /// A class representing a resource type filter used in Azure API calls.
    /// </summary>
    public class ArmResourceTypeFilter : ArmResourceFilter
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ArmResourceTypeFilter"/> class.
        /// </summary>
        /// <param name="resourceType"> The resource type to filter by. </param>
        public ArmResourceTypeFilter(ResourceType resourceType)
        {
            ResourceType = resourceType;
        }

        /// <summary>
        /// Gets the resource type to filter by.
        /// </summary>
        public ResourceType ResourceType { get; }

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
            return $"resourceType EQ '{ResourceType}'";
        }
    }
}
