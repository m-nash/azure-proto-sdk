using Azure.ResourceManager.Network.Models;
using azure_proto_core;
using System.Collections.Generic;

namespace azure_proto_network
{
    public class SubnetCollection : AzureCollection<AzureSubnet>
    {
        public SubnetCollection(AzureVnet vnet) : base(vnet) { }

        public AzureSubnet CreateOrUpdateSubnets(AzureSubnet subnet)
        {
            var networkClient = Parent.Clients.NetworkClient;
            AzureVnet vnet = Parent as AzureVnet;
            var subnetResult = networkClient.Subnets.StartCreateOrUpdate(vnet.Parent.Name, vnet.Model.Name, subnet.Model.Name, subnet.Model.Data as Subnet).WaitForCompletionAsync().Result;
            subnet = new AzureSubnet(vnet, new PhSubnet(subnetResult.Value, vnet.Location));
            return subnet;
        }

        protected override AzureSubnet Get(string subnetName)
        {
            var networkClient = Parent.Clients.NetworkClient;
            AzureVnet vnet = Parent as AzureVnet;
            var subnetResult = networkClient.Subnets.Get(vnet.Parent.Name, vnet.Name, subnetName);
            return new AzureSubnet(vnet, new PhSubnet(subnetResult.Value, vnet.Location));
        }

        public override IEnumerable<AzureSubnet> GetItems()
        {
            var networkClient = Parent.Clients.NetworkClient;
            AzureVnet vnet = Parent as AzureVnet;
            foreach (var subnet in networkClient.Subnets.List(vnet.Parent.Name, vnet.Name))
            {
                yield return new AzureSubnet(vnet, new PhSubnet(subnet, vnet.Location));
            }
        }
    }
}
