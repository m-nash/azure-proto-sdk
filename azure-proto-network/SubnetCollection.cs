using azure_proto_core;
using System;

namespace azure_proto_network
{
    public class SubnetCollection : AzureCollection<AzureSubnet>
    {
        public SubnetCollection(AzureVnet vnet) : base(vnet) { }

        protected override void LoadValues()
        {
            throw new NotImplementedException();
        }

        public AzureSubnet CreateOrUpdateSubnets(AzureSubnet subnet)
        {
            var networkClient = Parent.Clients.NetworkClient;
            AzureVnet vnet = Parent as AzureVnet;
            var subnetResult = networkClient.Subnets.StartCreateOrUpdate(vnet.Parent.Name, vnet.Model.Name, subnet.Model.Name, subnet.Model).WaitForCompletionAsync().Result;
            subnet = new AzureSubnet(vnet, subnetResult);
            Add(subnet.Model.Name, subnet);
            return subnet;
        }
    }
}
