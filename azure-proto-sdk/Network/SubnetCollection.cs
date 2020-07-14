using System;

namespace azure_proto_sdk.Network
{
    public class SubnetCollection : AzureCollection<AzureSubnet>
    {
        private AzureVnet vnet;

        public SubnetCollection(AzureVnet vnet)
        {
            this.vnet = vnet;
        }

        protected override void LoadValues()
        {
            throw new NotImplementedException();
        }

        public AzureSubnet CreateOrUpdateSubnets(AzureSubnet subnet)
        {
            var networkClient = vnet.Parent.Parent.Parent.NetworkClient;
            var subnetResult = networkClient.Subnets.StartCreateOrUpdate(vnet.Parent.Name, vnet.Model.Name, subnet.Model.Name, subnet.Model).WaitForCompletionAsync().Result;
            subnet = new AzureSubnet(vnet, subnetResult);
            Add(subnet.Model.Name, subnet);
            return subnet;
        }
    }
}
