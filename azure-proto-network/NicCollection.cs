using Azure.ResourceManager.Network;
using Azure.ResourceManager.Network.Models;
using azure_proto_core;
using System;
using System.Collections.Generic;

namespace azure_proto_network
{
    public class NicCollection : ResourceCollectionOperations<PhNetworkInterface>
    {
        public NicCollection(ArmClientBase parent, ResourceIdentifier context) : base(parent, context)
        {

        }

        public NicCollection(ArmClientBase parent, azure_proto_core.Resource context) : base(parent, context)
        {

        }

        protected override ResourceType ResourceType => "Microsoft.Network/networkInterfaces";

        protected override ResourceOperations<PhNetworkInterface> GetOperations(ResourceIdentifier identifier, Location location)
        {
            var resource = new ArmResource(identifier, location ?? DefaultLocation);
            return new NicOperations(this, resource);
        }
    }
}
