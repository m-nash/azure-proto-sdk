using Azure.ResourceManager.Network;
using Azure.ResourceManager.Network.Models;
using azure_proto_core;
using System;
using System.Collections.Generic;

namespace azure_proto_network
{
    public class NetworkInterfaceCollection : ResourceCollectionOperations<PhNetworkInterface>
    {
        public NetworkInterfaceCollection(ArmClientContext parent, ResourceIdentifier context) : base(parent, context)
        {
        }

        public NetworkInterfaceCollection(ArmClientContext parent, azure_proto_core.Resource context) : base(parent, context)
        {
        }

        public NetworkInterfaceCollection(OperationsBase parent, ResourceIdentifier context) : base(parent, context)
        {
        }

        public NetworkInterfaceCollection(OperationsBase parent, azure_proto_core.Resource context) : base(parent, context)
        {
        }

        public override ResourceType ResourceType => "Microsoft.Network/networkInterfaces";

        protected override ResourceOperationsBase<PhNetworkInterface> GetOperations(ResourceIdentifier identifier, Location location)
        {
            var resource = new ArmResource(identifier, location ?? DefaultLocation);
            return new NetworkInterfaceOperations(this, resource);
        }
    }
}
