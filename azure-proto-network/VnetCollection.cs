using azure_proto_core;
using System;

namespace azure_proto_network
{
    public class VnetCollection : AzureCollection<AzureVnet>
    {
        public VnetCollection(IResource resourceGroup) : base(resourceGroup) { }

        public AzureVnet CreateOrUpdateVNet(string name, AzureVnet vnet)
        {
            var networkClient = Parent.Clients.NetworkClient;
            var vnetResult = networkClient.VirtualNetworks.StartCreateOrUpdate(Parent.Name, name, vnet.Model).WaitForCompletionAsync().Result;
            var avnet = new AzureVnet(Parent, vnetResult);
            Add(avnet.Model.Name, avnet);
            return avnet;
        }

        protected override void LoadValues()
        {
            throw new NotImplementedException();
        }
    }
}
