using System;
using System.Collections.Generic;
using System.Text;

namespace azure_proto_core
{
    public class ProviderResources : ArmResourceOperations
    {
        public ProviderResources(ArmOperations parent, ResourceIdentifier context) : base(parent, context)
        {
        }

        public ProviderResources(ArmOperations parent, Resource context) : this(parent, context?.Id)
        {
        }

        protected override ResourceType ResourceType => ResourceType.None;



    }
}
