using Azure.ResourceManager.Network;
using Azure.ResourceManager.Network.Models;
using azure_proto_core;
using System.Collections.Generic;

namespace azure_proto_network
{
    public class VnetCollection : AzureCollection<AzureVnet>
    {
        public VnetCollection(TrackedResource resourceGroup) : base(resourceGroup) { }

        private NetworkManagementClient Client => ClientFactory.Instance.GetNetworkClient(Parent.Id.Subscription);

        public AzureVnet CreateOrUpdateVNet(string name, AzureVnet vnet)
        {
            var vnetResult = Client.VirtualNetworks.StartCreateOrUpdate(Parent.Name, name, vnet.Model).WaitForCompletionAsync().Result;
            var avnet = new AzureVnet(Parent, new PhVirtualNetwork(vnetResult.Value));
            return avnet;
        }

        protected override IEnumerable<AzureVnet> GetItems()
        {
            foreach (var vnet in Client.VirtualNetworks.List(Parent.Name))
            {
                yield return new AzureVnet(Parent, new PhVirtualNetwork(vnet));
            }
        }

        protected override AzureVnet Get(string vnetName)
        {
            var vnetResult = Client.VirtualNetworks.Get(Parent.Name, vnetName);
            return new AzureVnet(Parent, new PhVirtualNetwork(vnetResult.Value));
        }
    }
}
