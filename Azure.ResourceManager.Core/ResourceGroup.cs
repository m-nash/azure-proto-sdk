// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT License.

namespace Azure.ResourceManager.Core
{
    public class ResourceGroup : ResourceGroupOperations
    {
        internal ResourceGroup(AzureResourceManagerClientOptions options, ResourceGroupData resource)
            : base(options, resource)
        {
            Data = resource;
        }

        public ResourceGroupData Data { get; private set; }
    }
}
