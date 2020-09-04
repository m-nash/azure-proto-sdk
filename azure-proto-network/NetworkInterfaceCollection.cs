using Azure.ResourceManager.Network;
using Azure.ResourceManager.Network.Models;
using azure_proto_core;
using System;
using System.Collections.Generic;

namespace azure_proto_network
{
    public class NetworkInterfaceCollection : ResourceCollectionOperations<PhNetworkInterface>
    {
        public NetworkInterfaceCollection(ArmClientBase parent, ResourceIdentifier context) : base(parent, context)
        {

        }

        public NetworkInterfaceCollection(ArmClientBase parent, azure_proto_core.Resource context) : base(parent, context)
        {

        }

        protected override ResourceType ResourceType => "Microsoft.Network/networkInterfaces";

        protected override ResourceClientBase<PhNetworkInterface> GetOperations(ResourceIdentifier identifier, Location location)
        {
            var resource = new ArmResource(identifier, location ?? DefaultLocation);
            return new NetworkInterfaceOperations(this, resource);
        }
    }
}
