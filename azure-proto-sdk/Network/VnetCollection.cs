using azure_proto_sdk.Management;
using System;

namespace azure_proto_sdk.Network
{
    public class VnetCollection : AzureCollection<AzureVnet>
    {
        private AzureResourceGroup resourceGroup;

        public VnetCollection(AzureResourceGroup resourceGroup)
        {
            this.resourceGroup = resourceGroup;
        }

        public AzureVnet CreateOrUpdateVNet(string name, AzureVnet vnet)
        {
            var networkClient = resourceGroup.Parent.Parent.NetworkClient;
            var vnetResult = networkClient.VirtualNetworks.StartCreateOrUpdate(resourceGroup.Name, name, vnet.Model).WaitForCompletionAsync().Result;
            var avnet = new AzureVnet(resourceGroup, vnetResult);
            Add(avnet.Model.Name, avnet);
            return avnet;
        }

        protected override void LoadValues()
        {
            throw new NotImplementedException();
        }
    }
}
