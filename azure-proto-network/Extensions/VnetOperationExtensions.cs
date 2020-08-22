using Azure;
using Azure.ResourceManager.Network.Models;
using azure_proto_core;
using System.Threading;
using System.Threading.Tasks;

namespace azure_proto_network
{
    public static class VnetOperationExtensions
    {
        public static SubnetOperations Subnet(this ResourceOperations<PhVirtualNetwork> operations, TrackedResource subnet)
        {
            return new SubnetOperations(operations, subnet);
        }

        public static SubnetOperations Subnet(this ResourceOperations<PhVirtualNetwork> operations, ResourceIdentifier subnet)
        {
            return new SubnetOperations(operations, subnet);
        }

        public static SubnetOperations Subnet(this ResourceOperations<PhVirtualNetwork> operations, string subnet)
        {
            return new SubnetOperations(operations, $"{operations.Context}/subnets/{subnet}");
        }

        public static SubnetContainer ConstructSubnet(this ResourceOperations<PhVirtualNetwork> operations, string name, string cidr, Location location = null, PhNetworkSecurityGroup group = null)
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

            return new SubnetContainer(operations, new PhSubnet(subnet, location ?? operations.DefaultLocation));
        }

        public static ArmOperation<ResourceOperations<PhSubnet>> CreateSubnet(this ResourceOperations<PhVirtualNetwork> operations, string name, PhSubnet resourceDetails)
        {
            return GetSubnetContainer(operations).Create(name, resourceDetails);
        }

        public static Task<ArmOperation<ResourceOperations<PhSubnet>>> CreateSubnetAsync(this ResourceOperations<PhVirtualNetwork> operations, string name, PhSubnet resourceDetails, CancellationToken cancellationToken = default)
        {
            return GetSubnetContainer(operations).CreateAsync(name, resourceDetails, cancellationToken);
        }

        public static Pageable<ResourceOperations<PhSubnet>> ListSubnets(this ResourceOperations<PhVirtualNetwork> operations, CancellationToken cancellationToken = default)
        {
            return GetSubnetCollection(operations).List(null, null, cancellationToken);
        }

        public static AsyncPageable<ResourceOperations<PhSubnet>> ListSubnetsAsync(this ResourceOperations<PhVirtualNetwork> operations, CancellationToken cancellationToken = default)
        {
            return GetSubnetCollection(operations).ListAsync(null, null, cancellationToken);
        }

        internal static SubnetContainer GetSubnetContainer(ResourceOperations<PhVirtualNetwork> operations )
        {
            return new SubnetContainer(operations, operations.Context);
        }

        internal static SubnetCollection GetSubnetCollection(ResourceOperations<PhVirtualNetwork> operations )
        {
            return new SubnetCollection(operations, operations.Context);
        }
    }
}
