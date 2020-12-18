using System;
using System.Collections.Generic;
using System.Text;

namespace Azure.ResourceManager.Core
{
    public class ArmResource : ArmResourceOperations
    {
        public ArmResource(AzureResourceManagerClientOptions options, ArmResourceData resource)
            : base(options, resource.Id)
        {
            Data = resource;
        }

        public ArmResourceData Data { get; }
    }
}
