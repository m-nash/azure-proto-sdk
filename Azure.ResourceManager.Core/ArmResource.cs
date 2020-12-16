using System;
using System.Collections.Generic;
using System.Text;

namespace Azure.ResourceManager.Core
{
    public class ArmResource : ArmResourceOperations
    {
        public ArmResource(ArmClientContext context, ArmResourceData resource, ArmClientOptions clientOption)
            : base(context, resource.Id, clientOption)
        {
            Data = resource;
        }

        public ArmResourceData Data { get; }
    }
}