using Azure.ResourceManager.Network;
using Azure.ResourceManager.Network.Models;
using azure_proto_core;
using System.Collections.Generic;

namespace azure_proto_network
{
    public class VnetCollection : ResourceCollectionOperations
    {
        public VnetCollection(ArmOperations parent, ResourceIdentifier context) : base(parent, context)
        {
        }

        public VnetCollection(ArmOperations parent, azure_proto_core.Resource context) : base(parent, context)
        {
        }

        protected override ResourceType ResourceType => "Microsoft.Network/virtualNetworks";
    }

}
