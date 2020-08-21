using Azure.ResourceManager.Network;
using Azure.ResourceManager.Network.Models;
using azure_proto_core;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace azure_proto_network
{
    public class VnetContainer : ResourceContainerOperations<PhVirtualNetwork>
    {
        public VnetContainer(ArmOperations parent, ResourceIdentifier context) : base(parent, context)
        {
        }

        public VnetContainer(ArmOperations parent, azure_proto_core.Resource context) : base(parent, context)
        {
        }

        protected override ResourceType ResourceType => "Microsoft.Network/virtualNetworks";

        public override ArmOperation<ResourceOperations<PhVirtualNetwork>> Create(string name, PhVirtualNetwork resourceDetails)
        {
            return new PhArmOperation<ResourceOperations<PhVirtualNetwork>, VirtualNetwork>(Operations.StartCreateOrUpdate(Context.ResourceGroup, name, resourceDetails), n => Vnet(new PhVirtualNetwork(n)));
        }

        public async override Task<ArmOperation<ResourceOperations<PhVirtualNetwork>>> CreateAsync(string name, PhVirtualNetwork resourceDetails, CancellationToken cancellationToken = default)
        {
            return new PhArmOperation<ResourceOperations<PhVirtualNetwork>, VirtualNetwork>(await Operations.StartCreateOrUpdateAsync(Context.ResourceGroup, name, resourceDetails, cancellationToken), n => Vnet(new PhVirtualNetwork(n)));
        }

        public PhVirtualNetwork ConstructVnet(string vnetCidr, Location location = null)
        {
            var vnet = new VirtualNetwork()
            {
                Location = location ?? DefaultLocation,
                AddressSpace = new AddressSpace() { AddressPrefixes = new List<string>() { vnetCidr } },
            };
            return new PhVirtualNetwork(vnet);
        }

        internal VnetOperations Vnet(TrackedResource vnet)
        {
            return new VnetOperations(this, vnet);
        }

        internal VirtualNetworksOperations Operations => GetClient<NetworkManagementClient>((uri, cred) => new NetworkManagementClient(Context.Subscription, uri, cred)).VirtualNetworks;

    }
}
