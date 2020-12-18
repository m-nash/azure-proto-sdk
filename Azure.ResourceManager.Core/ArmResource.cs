using System;
using System.Collections.Generic;
using System.Text;

namespace Azure.ResourceManager.Core
{
    public class ArmResource : ArmResourceOperations
    {
        public ArmResource(AzureResourceManagerClientContext context, ArmResourceData resource, AzureResourceManagerClientOptions clientOption)
            : base(context, resource, clientOption)
        {
            Data = resource;
        }

        public ArmResourceData Data { get; }
    }
}
