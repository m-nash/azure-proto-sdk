using azure_proto_core;

namespace azure_proto_network
{
    public static class VnetOperationExtensions
    {
        public static SubnetOperations Subnet(this ResourceOperationsBase<PhVirtualNetwork> virtualNetwork, TrackedResource subnet)
        {
            return new SubnetOperations(virtualNetwork, subnet);
        }

        public static SubnetOperations Subnet(this ResourceOperationsBase<PhVirtualNetwork> virtualNetwork, ResourceIdentifier subnet)
        {
            return new SubnetOperations(virtualNetwork, subnet);
        }

        public static SubnetOperations Subnet(this ResourceOperationsBase<PhVirtualNetwork> virtualNetwork, string subnet)
        {
            return new SubnetOperations(virtualNetwork, $"{virtualNetwork.Id}/subnets/{subnet}");
        }

        public static SubnetContainer Subnets(this ResourceOperationsBase<PhVirtualNetwork> virtualNetwork)
        {
            return new SubnetContainer(virtualNetwork.ClientContext, virtualNetwork.Id);
        }
    }
}
