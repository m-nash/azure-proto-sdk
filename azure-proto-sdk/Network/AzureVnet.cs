using Azure.ResourceManager.Network.Models;
using azure_proto_sdk.Management;
using System;
using System.Collections.Generic;
using System.Text;

namespace azure_proto_sdk.Network
{
    public class AzureVnet : AzureResource<AzureResourceGroup, VirtualNetwork>
    {
        public SubnetCollection Subnets { get; private set; }

        public AzureVnet(AzureResourceGroup resourceGroup, VirtualNetwork vnet) : base(resourceGroup, vnet)
        {
            Subnets = new SubnetCollection(this);
        }

        public AzureSubnet ConstructSubnet(string name, string cidr)
        {
            var subnet = new Subnet()
            {
                Name = name,
                AddressPrefix = cidr,
            };
            return new AzureSubnet(this, subnet);
        }
    }
}
