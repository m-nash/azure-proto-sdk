using azure_proto_core;
using System;
using System.Collections.Generic;
using System.Text;

namespace azure_proto_network
{
    public class PublicIpCollection : ResourceCollectionOperations<PhPublicIPAddress>
    {
        public PublicIpCollection(ArmClientBase parent, ResourceIdentifier context) : base(parent, context)
        {
        }

        public PublicIpCollection(ArmClientBase parent, azure_proto_core.Resource context) : base(parent, context)
        {
        }

        protected override ResourceType ResourceType => "Microsoft.Network/publicIpAddresses";

        protected override ResourceOperations<PhPublicIPAddress> GetOperations(ResourceIdentifier identifier, Location location)
        {
            var resource = new ArmResource(identifier, location ?? DefaultLocation);
            return new PublicIpOperations(this, resource);
        }
    }
}
