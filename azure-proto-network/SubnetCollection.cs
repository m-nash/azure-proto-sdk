using Azure.ResourceManager.Network;
using Azure.ResourceManager.Network.Models;
using azure_proto_core;
using System.Collections.Generic;

namespace azure_proto_network
{
    public class SubnetCollection : AzureCollection<AzureSubnet>
    {
        public SubnetCollection(AzureVnet vnet) : base(vnet) { }

        private NetworkManagementClient Client => ClientFactory.Instance.GetNetworkClient(Parent.Id.Subscription);

        public AzureSubnet CreateOrUpdateSubnets(AzureSubnet subnet)
        {
            AzureVnet vnet = Parent as AzureVnet;
            var subnetResult = Client.Subnets.StartCreateOrUpdate(vnet.Id.ResourceGroup, vnet.Name, subnet.Name, subnet.Model).WaitForCompletionAsync().Result;
            subnet = new AzureSubnet(vnet, new PhSubnet(subnetResult.Value, vnet.Location));
            return subnet;
        }

        protected override AzureSubnet Get(string subnetName)
        {
            AzureVnet vnet = Parent as AzureVnet;
            var subnetResult = Client.Subnets.Get(vnet.Id.ResourceGroup, vnet.Name, subnetName);
            return new AzureSubnet(vnet, new PhSubnet(subnetResult.Value, vnet.Location));
        }

        protected override IEnumerable<AzureSubnet> GetItems()
        {
            AzureVnet vnet = Parent as AzureVnet;
            foreach (var subnet in Client.Subnets.List(vnet.Id.ResourceGroup, vnet.Name))
            {
                yield return new AzureSubnet(vnet, new PhSubnet(subnet, vnet.Location));
            }
        }

        public AzureSubnet ConstructSubnet(string name, string cidr, AzureNetworkSecurityGroup group = null)
        {
            var subnet = new Subnet()
            {
                Name = name,
                AddressPrefix = cidr,
            };

            if (null != group)
            {
                subnet.NetworkSecurityGroup = group.Model;
            }

            return new AzureSubnet(Parent as AzureVnet, new PhSubnet(subnet, Parent.Location));
        }
    }
}
