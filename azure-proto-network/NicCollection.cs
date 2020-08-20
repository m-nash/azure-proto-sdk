using Azure.ResourceManager.Network;
using Azure.ResourceManager.Network.Models;
using azure_proto_core;
using System;
using System.Collections.Generic;

namespace azure_proto_network
{
    public class NicCollection : ResourceCollectionOperations
    {
        public NicCollection(ArmOperations parent, ResourceIdentifier context) : base(parent, context)
        {

        }

        public NicCollection(ArmOperations parent, azure_proto_core.Resource context) : base(parent, context)
        {

        }

        protected override ResourceType ResourceType => "Microsoft.Network/networkInterfaces";
    }
}
