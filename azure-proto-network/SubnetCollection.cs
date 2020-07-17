using Azure.ResourceManager.Network.Models;
using azure_proto_core;

namespace azure_proto_network
{
    public class SubnetCollection : AzureCollection<AzureSubnet>
    {
        public SubnetCollection(AzureVnet vnet) : base(vnet) { }

        protected override void LoadValues()
        {
            var networkClient = Parent.Clients.NetworkClient;
            AzureVnet vnet = Parent as AzureVnet;
            foreach(var subnet in networkClient.Subnets.List(vnet.Parent.Name, vnet.Name))
            {
                Add(subnet.Name, new AzureSubnet(vnet, new PhSubnet(subnet)));
            }
        }

        public AzureSubnet CreateOrUpdateSubnets(AzureSubnet subnet)
        {
            var networkClient = Parent.Clients.NetworkClient;
            AzureVnet vnet = Parent as AzureVnet;
            var subnetResult = networkClient.Subnets.StartCreateOrUpdate(vnet.Parent.Name, vnet.Model.Name, subnet.Model.Name, subnet.Model.Data as Subnet).WaitForCompletionAsync().Result;
            subnet = new AzureSubnet(vnet, new PhSubnet(subnetResult.Value));
            Add(subnet.Model.Name, subnet);
            return subnet;
        }

        protected override AzureSubnet GetSingleValue(string key)
        {
            var networkClient = Parent.Clients.NetworkClient;
            AzureVnet vnet = Parent as AzureVnet;
            var subnetResult = networkClient.Subnets.Get(vnet.Parent.Name, vnet.Name, key);
            return new AzureSubnet(vnet, new PhSubnet(subnetResult.Value));
        }
    }
}
