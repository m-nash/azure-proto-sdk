using Azure.Core;
using System;
using System.Collections.Generic;
using System.Text;

namespace Azure.ResourceManager.Core.Tests
{
    public class ResourceBase : ResourceOperationsBase
    {
        public ResourceBase(ResourceBase operations)
            : base(operations.ClientOptions, operations.Id, operations.Credential, operations.BaseUri)
        {
        }

        public ResourceBase(ResourceBase options, ResourceIdentifier resourceId)
            : base(options.ClientOptions, resourceId, options.Credential, options.BaseUri)
        {
        }

        public ResourceBase(AzureResourceManagerClientOptions options, ResourceIdentifier resourceId, TokenCredential credential, Uri baseUri)
            : base(options, resourceId, credential, baseUri)
        {
        }

        protected override ResourceType ValidResourceType => ResourceGroupOperations.ResourceType;
    }
}
