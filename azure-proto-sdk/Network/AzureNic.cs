using Azure.ResourceManager.Network.Models;
using azure_proto_sdk.Management;
using System;
using System.Collections.Generic;
using System.Text;

namespace azure_proto_sdk.Network
{
    public class AzureNic : AzureResource<AzureResourceGroup, NetworkInterface>
    {
        public AzureNic(AzureResourceGroup resourceGroup, NetworkInterface nic) : base(resourceGroup, nic) { }
    }
}
