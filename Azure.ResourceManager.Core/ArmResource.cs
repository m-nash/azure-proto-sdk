using System;
using System.Collections.Generic;
using System.Text;

namespace Azure.ResourceManager.Core
{
    public class ArmResource : ArmResourceOperations
    {
        public ArmResource(AzureResourceManagerClientOptions options, ArmResourceData resource, ApiVersionsBase apiVersion)
            : base(options, resource, apiVersion)
        {
            Data = resource;
        }

        public ArmResourceData Data { get; }
    }
}
