using Azure.ResourceManager.Network;
using Azure.ResourceManager.Network.Models;
using azure_proto_core;
using System.Collections.Generic;

namespace azure_proto_network
{
    public class SubnetCollection : AzureCollection<AzureSubnet>
    {
        public SubnetCollection(AzureVnet vnet) : base(vnet) { }

        private NetworkManagementClient Client => ClientFactory.Instance.GetNetworkClient(((Parent as AzureVnet).Parent as AzureResourceGroupBase).Parent.Id);

        public AzureSubnet CreateOrUpdateSubnets(AzureSubnet subnet)
        {
            AzureVnet vnet = Parent as AzureVnet;
            var subnetResult = Client.Subnets.StartCreateOrUpdate(vnet.Parent.Name, vnet.Model.Name, subnet.Model.Name, subnet.Model.Data as Subnet).WaitForCompletionAsync().Result;
            subnet = new AzureSubnet(vnet, new PhSubnet(subnetResult.Value, vnet.Location));
            return subnet;
        }

        protected override AzureSubnet Get(string subnetName)
        {
            AzureVnet vnet = Parent as AzureVnet;
            var subnetResult = Client.Subnets.Get(vnet.Parent.Name, vnet.Name, subnetName);
            return new AzureSubnet(vnet, new PhSubnet(subnetResult.Value, vnet.Location));
        }

        protected override IEnumerable<AzureSubnet> GetItems()
        {
            AzureVnet vnet = Parent as AzureVnet;
            foreach (var subnet in Client.Subnets.List(vnet.Parent.Name, vnet.Name))
            {
                yield return new AzureSubnet(vnet, new PhSubnet(subnet, vnet.Location));
            }
        }
    }
}
