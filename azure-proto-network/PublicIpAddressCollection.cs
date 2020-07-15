using azure_proto_core;
using System;

namespace azure_proto_network
{
    public class PublicIpAddressCollection : AzureCollection<AzurePublicIpAddress>
    {
        public PublicIpAddressCollection(IResource resourceGroup) : base(resourceGroup) { }

        protected override void LoadValues()
        {
            throw new NotImplementedException();
        }

        public AzurePublicIpAddress CreateOrUpdatePublicIpAddress(string name, AzurePublicIpAddress ipAddress)
        {
            var networkClient = Parent.Clients.NetworkClient;
            var ipResult = networkClient.PublicIPAddresses.StartCreateOrUpdate(Parent.Name, name, ipAddress.Model).WaitForCompletionAsync().Result;
            ipAddress = new AzurePublicIpAddress(Parent, ipResult);
            Add(ipAddress.Model.Name, ipAddress);
            return ipAddress;
        }
    }
}
