using Azure.ResourceManager.Network.Models;
using azure_proto_core;
using System;

namespace azure_proto_network
{
    public class NicCollection : AzureCollection<AzureNic>
    {
        public NicCollection(IResource resourceGroup) : base(resourceGroup) { }

        protected override void LoadValues()
        {
            throw new NotImplementedException();
        }

        public AzureNic CreateOrUpdateNic(string name, AzureNic nic)
        {
            var networkClient = Parent.Clients.NetworkClient;
            var nicResult = networkClient.NetworkInterfaces.StartCreateOrUpdate(Parent.Name, name, nic.Model.Data as NetworkInterface).WaitForCompletionAsync().Result;
            nic = new AzureNic(Parent, new PhNetworkInterface(nicResult.Value));
            Add(nic.Model.Name, nic);
            return nic;
        }

        protected override AzureNic GetSingleValue(string key)
        {
            var networkClient = Parent.Clients.NetworkClient;
            var nicResult = networkClient.NetworkInterfaces.Get(Parent.Name, key);
            return new AzureNic(Parent, new PhNetworkInterface(nicResult.Value));
        }
    }
}
