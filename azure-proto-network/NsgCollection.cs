using Azure.ResourceManager.Network;
using Azure.ResourceManager.Network.Models;
using azure_proto_core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Schema;

namespace azure_proto_network
{
    public class NsgCollection : ResourceCollectionOperations
    {
        public NsgCollection(ArmOperations parent, ResourceIdentifier context) : base(parent, context)
        {
        }

        public NsgCollection(ArmOperations parent, azure_proto_core.Resource context) : base(parent, context)
        {
        }

        protected override ResourceType ResourceType => "Microsoft.Network/networkSecurityGroups";
    }
}
