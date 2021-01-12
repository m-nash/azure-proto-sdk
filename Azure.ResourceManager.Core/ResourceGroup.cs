// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT License.

namespace Azure.ResourceManager.Core
{
    /// <summary>
    /// A class representing a ResourceGroup along with the instance operations that can be performed on it.
    /// </summary>
    public class ResourceGroup : ResourceGroupOperations
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ResourceGroup"/> class.
        /// </summary>
        /// <param name="options"> The client parameters to use in these operations. </param>
        /// <param name="resource"> The ResourceGroupData to use in these operations. </param>
        internal ResourceGroup(AzureResourceManagerClientOptions options, ResourceGroupData resource)
            : base(options, resource)
        {
            Data = resource;
        }

        /// <summary>
        /// Gets the data representing this ResourceGroup.
        /// </summary>
        public ResourceGroupData Data { get; private set; }
    }
}
