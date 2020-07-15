using Azure.ResourceManager.Network.Models;
using azure_proto_core;

namespace azure_proto_network
{
    public class AzureVnet : AzureResource<VirtualNetwork>
    {
        public SubnetCollection Subnets { get; private set; }

        public override string Name => Model.Name;

        public override string Id => Model.Id;

        public AzureVnet(IResource resourceGroup, VirtualNetwork vnet) : base(resourceGroup, vnet)
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
