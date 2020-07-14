using Azure.ResourceManager.Network.Models;
using azure_proto_sdk.Management;
using System;
using System.Collections.Generic;
using System.Text;

namespace azure_proto_sdk.Network
{
    public class NicCollection : AzureCollection<AzureNic>
    {
        private AzureResourceGroup resourceGroup;

        public NicCollection(AzureResourceGroup resourceGroup)
        {
            this.resourceGroup = resourceGroup;
        }

        protected override void LoadValues()
        {
            throw new NotImplementedException();
        }

        internal AzureNic CreateOrUpdateNic(string name, AzureNic nic)
        {
            var networkClient = resourceGroup.Parent.Parent.NetworkClient;
            var nicResult = networkClient.NetworkInterfaces.StartCreateOrUpdate(resourceGroup.Name, name, nic.Model).WaitForCompletionAsync().Result;
            nic = new AzureNic(resourceGroup, nicResult);
            Add(nic.Model.Name, nic);
            return nic;
        }
    }
}
