using Azure.ResourceManager.Network.Models;
using azure_proto_core;
using System;
using System.Collections.Generic;

namespace azure_proto_network
{
    public class PublicIpAddressCollection : AzureCollection<AzurePublicIpAddress>
    {
        public PublicIpAddressCollection(IResource resourceGroup) : base(resourceGroup) { }

        public AzurePublicIpAddress CreateOrUpdatePublicIpAddress(string name, AzurePublicIpAddress ipAddress)
        {
            var networkClient = Parent.Clients.NetworkClient;
            var ipResult = networkClient.PublicIPAddresses.StartCreateOrUpdate(Parent.Name, name, ipAddress.Model.Data as PublicIPAddress).WaitForCompletionAsync().Result;
            ipAddress = new AzurePublicIpAddress(Parent, new PhPublicIPAddress(ipResult.Value));
            return ipAddress;
        }

        protected override IEnumerable<AzurePublicIpAddress> GetItems()
        {
            throw new NotImplementedException();
        }

        protected override AzurePublicIpAddress Get(string ipName)
        {
            var networkClient = Parent.Clients.NetworkClient;
            var ipResult = networkClient.PublicIPAddresses.Get(Parent.Name, ipName);
            return new AzurePublicIpAddress(Parent, new PhPublicIPAddress(ipResult.Value));
        }
    }
}
