using azure_proto_sdk.Management;
using System;

namespace azure_proto_sdk.Network
{
    public class PublicIpAddressCollection : AzureCollection<AzurePublicIpAddress>
    {
        private AzureResourceGroup resourceGroup;

        public PublicIpAddressCollection(AzureResourceGroup resourceGroup)
        {
            this.resourceGroup = resourceGroup;
        }

        protected override void LoadValues()
        {
            throw new NotImplementedException();
        }

        public AzurePublicIpAddress CreateOrUpdatePublicIpAddress(string name, AzurePublicIpAddress ipAddress)
        {
            var networkClient = resourceGroup.Parent.Parent.NetworkClient;
            var ipResult = networkClient.PublicIPAddresses.StartCreateOrUpdate(resourceGroup.Name, name, ipAddress.Model).WaitForCompletionAsync().Result;
            ipAddress = new AzurePublicIpAddress(resourceGroup, ipResult);
            Add(ipAddress.Model.Name, ipAddress);
            return ipAddress;
        }
    }
}
