using Azure.ResourceManager.Network;
using Azure.ResourceManager.Network.Models;
using azure_proto_core;
using System.Collections.Generic;

namespace azure_proto_network
{
    /// <summary>
    /// TODO: refactor list methods to include type-specific lists, and allow Child Resources
    /// </summary>
    public class SubnetCollection : ResourceCollectionOperations
    {
        public SubnetCollection(ArmOperations parent, ResourceIdentifier context) : base(parent, context)
        {
        }

        public SubnetCollection(ArmOperations parent, azure_proto_core.Resource context) : base(parent, context)
        {
        }
        protected override ResourceType ResourceType => "Microsoft.Network/virtualNetworks/subnets";
    }
}
