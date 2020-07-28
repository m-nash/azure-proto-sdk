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
    public class NetworkSecurityGroupCollection : AzureCollection<AzureNetworkSecurityGroup>
    {
        public NetworkSecurityGroupCollection(TrackedResource parent) : base(parent)
        {
        }

        private NetworkManagementClient Client => ClientFactory.Instance.GetNetworkClient(Parent.Id.Subscription);

        protected override AzureNetworkSecurityGroup Get(string name)
        {
            return new AzureNetworkSecurityGroup(Parent, Client.NetworkSecurityGroups.Get(Parent.Id.ResourceGroup, name)?.Value);
        }

        protected override IEnumerable<AzureNetworkSecurityGroup> GetItems()
        {
            return Client.NetworkSecurityGroups.ListAll().Select(g => new AzureNetworkSecurityGroup(Parent, g));
        }

        public AzureNetworkSecurityGroup CreateOrUpdateNsgs(AzureNetworkSecurityGroup nsg)
        {
            return new AzureNetworkSecurityGroup(Parent, Client.NetworkSecurityGroups.StartCreateOrUpdate(Parent.Id.ResourceGroup, nsg.Name, nsg.Model).Value);
        }
    }
}
