using azure_proto_core;
using System;
using System.Collections.Generic;
using System.Text;

namespace azure_proto_network
{
    public class PublicIpAddressCollection : ResourceCollectionOperations<PhPublicIPAddress>
    {
        public PublicIpAddressCollection(ArmClientBase parent, ResourceIdentifier context) : base(parent, context)
        {
        }

        public PublicIpAddressCollection(ArmClientBase parent, azure_proto_core.Resource context) : base(parent, context)
        {
        }

        protected override ResourceType ResourceType => "Microsoft.Network/publicIpAddresses";

        protected override ResourceClientBase<PhPublicIPAddress> GetOperations(ResourceIdentifier identifier, Location location)
        {
            var resource = new ArmResource(identifier, location ?? DefaultLocation);
            return new PublicIpAddressOperations(this, resource);
        }
    }
}
