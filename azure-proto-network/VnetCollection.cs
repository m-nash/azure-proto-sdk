using Azure.ResourceManager.Network.Models;
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
            var vnetResult = networkClient.VirtualNetworks.StartCreateOrUpdate(Parent.Name, name, vnet.Model.Data as VirtualNetwork).WaitForCompletionAsync().Result;
            var avnet = new AzureVnet(Parent, new PhVirtualNetwork(vnetResult.Value));
            Add(avnet.Model.Name, avnet);
            return avnet;
        }

        protected override void LoadValues()
        {
            var networkClient = Parent.Clients.NetworkClient;
            foreach(var vnet in networkClient.VirtualNetworks.List(Parent.Name))
            {
                Add(vnet.Name, new AzureVnet(Parent, new PhVirtualNetwork(vnet)));
            }
        }
    }
}
