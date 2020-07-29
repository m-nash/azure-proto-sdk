using Azure.ResourceManager.Network.Models;
using azure_proto_core;

namespace azure_proto_network
{
    public class AzureVnet : AzureEntity<PhVirtualNetwork>
    {
        public SubnetCollection Subnets { get; private set; }

        public AzureVnet(TrackedResource resourceGroup, PhVirtualNetwork vnet) : base(resourceGroup, vnet)
        {
            Subnets = new SubnetCollection(this);
        }

        public AzureSubnet ConstructSubnet(string name, string cidr, AzureNetworkSecurityGroup group = null)
        {
            var subnet = new Subnet()
            {
                Name = name,
                AddressPrefix = cidr,
            };

            if (null != group)
            {
                subnet.NetworkSecurityGroup = group.Model;
            }

            return new AzureSubnet(this, new PhSubnet(subnet, Location));
        }
    }
}
