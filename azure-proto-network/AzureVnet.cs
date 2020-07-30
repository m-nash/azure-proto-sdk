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
    }
}
