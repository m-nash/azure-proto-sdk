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
            var model = Client.NetworkSecurityGroups.Get(Parent.Id.ResourceGroup, name).Value;
            return new AzureNetworkSecurityGroup(Parent, model, name);
        }

        /// <summary>
        /// Make this a yield return, so enumeration can be short-circuited
        /// TODO: We have separate methods for List by resource group and by subscription for this item, how to represent?
        /// </summary>
        /// <returns></returns>
        protected override IEnumerable<AzureNetworkSecurityGroup> GetItems()
        {
            foreach (var nsg in Client.NetworkSecurityGroups.List(Parent.Id.ResourceGroup))
            {
                yield return new AzureNetworkSecurityGroup(Parent, nsg, nsg?.Name);
            }
        }

        public AzureNetworkSecurityGroup CreateOrUpdateNsgs(AzureNetworkSecurityGroup nsg)
        {
            var result = Client.NetworkSecurityGroups.StartCreateOrUpdate(Parent.Id.ResourceGroup, nsg.Name, nsg.Model).WaitForCompletionAsync().Result.Value;
            return new AzureNetworkSecurityGroup(Parent, result, result?.Name);
        }
    }
}
