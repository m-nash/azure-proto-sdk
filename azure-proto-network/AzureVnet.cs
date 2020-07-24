using Azure.ResourceManager.Network.Models;
using azure_proto_core;

namespace azure_proto_network
{
    public class AzureVnet : AzureResource<VirtualNetwork>
    {
        public SubnetCollection Subnets { get; private set; }
        public override VirtualNetwork Data { get ; protected set; }

        public AzureVnet(TrackedResource resourceGroup, PhVirtualNetwork vnet) : base(vnet.Id, vnet.Location)
        {
            Data = vnet.Data;
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
