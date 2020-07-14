using Azure.ResourceManager.Network.Models;
using azure_proto_sdk.Management;
using System;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;
using System.Text;

namespace azure_proto_sdk.Network
{
    public class AzurePublicIpAddress : AzureResource<AzureResourceGroup, PublicIPAddress>
    {
        public AzurePublicIpAddress(AzureResourceGroup resourceGroup, PublicIPAddress ip) : base(resourceGroup, ip) { }
    }
}
