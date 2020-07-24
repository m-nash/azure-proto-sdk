using Azure.ResourceManager.Network.Models;
using azure_proto_core;

namespace azure_proto_network
{
    public class AzureVnet : AzureResource
    {
        public SubnetCollection Subnets { get; private set; }

        public AzureVnet(IResource resourceGroup, PhVirtualNetwork vnet) : base(resourceGroup, vnet)
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
            return new AzureSubnet(this, new PhSubnet(subnet, Location));
        }
    }
}
