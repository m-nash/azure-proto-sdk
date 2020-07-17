using Azure.ResourceManager.Network.Models;
using azure_proto_core;
using System;
using System.Collections.Generic;

namespace azure_proto_network
{
    public class NicCollection : AzureCollection<AzureNic>
    {
        public NicCollection(IResource resourceGroup) : base(resourceGroup) { }

        public AzureNic CreateOrUpdateNic(string name, AzureNic nic)
        {
            var networkClient = Parent.Clients.NetworkClient;
            var nicResult = networkClient.NetworkInterfaces.StartCreateOrUpdate(Parent.Name, name, nic.Model.Data as NetworkInterface).WaitForCompletionAsync().Result;
            nic = new AzureNic(Parent, new PhNetworkInterface(nicResult.Value));
            return nic;
        }

        public override IEnumerable<AzureNic> GetItems()
        {
            throw new NotImplementedException();
        }

        protected override AzureNic Get(string nicName)
        {
            var networkClient = Parent.Clients.NetworkClient;
            var nicResult = networkClient.NetworkInterfaces.Get(Parent.Name, nicName);
            return new AzureNic(Parent, new PhNetworkInterface(nicResult.Value));
        }
    }
}
