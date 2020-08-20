using azure_proto_core;
using System;
using System.Collections.Generic;
using System.Text;

namespace azure_proto_network
{
    public class PublicIpCollection : ResourceCollectionOperations
    {
        public PublicIpCollection(ArmOperations parent, ResourceIdentifier context) : base(parent, context)
        {
        }

        public PublicIpCollection(ArmOperations parent, azure_proto_core.Resource context) : base(parent, context)
        {
        }

        protected override ResourceType ResourceType => "Microsoft.Network/publicIpAddresses";
    }
}
