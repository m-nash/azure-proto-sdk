using Azure.ResourceManager.Network.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace azure_proto_sdk.Network
{
    public class AzureSubnet : AzureResource<AzureVnet, Subnet>
    {
        public AzureSubnet(AzureVnet vnet, Subnet model) : base(vnet, model) { }
    }
}
