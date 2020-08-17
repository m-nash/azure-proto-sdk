using Azure.ResourceManager.Network;
using Azure.ResourceManager.Network.Models;
using azure_proto_core;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace azure_proto_network
{
    public class VnetContainer : ArmResourceContainerOperations<PhVirtualNetwork, ArmOperation<PhVirtualNetwork>>
    {
        public VnetContainer(ArmOperations parent, ResourceIdentifier context) : base(parent, context)
        {
        }

        public VnetContainer(ArmOperations parent, azure_proto_core.Resource context) : base(parent, context)
        {
        }

        protected override ResourceType ResourceType => "Microsoft.Network/virtualNetworks";

        public override ArmOperation<PhVirtualNetwork> Create(string name, PhVirtualNetwork resourceDetails)
        {
            return new PhArmOperation<PhVirtualNetwork, VirtualNetwork>(Operations.StartCreateOrUpdate(Context.ResourceGroup, name, resourceDetails), n => new PhVirtualNetwork(n));
        }

        public async override Task<ArmOperation<PhVirtualNetwork>> CreateAsync(string name, PhVirtualNetwork resourceDetails, CancellationToken cancellationToken = default)
        {
            return new PhArmOperation<PhVirtualNetwork, VirtualNetwork>(await Operations.StartCreateOrUpdateAsync(Context.ResourceGroup, name, resourceDetails, cancellationToken), n => new PhVirtualNetwork(n));
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

        public VnetOperations Vnet(TrackedResource vnet)
        {
            return new VnetOperations(this, vnet);
        }

        public VnetOperations Vnet(ResourceIdentifier vnet)
        {
            return new VnetOperations(this, vnet);
        }

        public VnetOperations Vnet(string vnetName)
        {
            return new VnetOperations(this, $"{Context}/providers/Microsoft.Network/virtualNetworks/{vnetName}");
        }


        internal VirtualNetworksOperations Operations => GetClient<NetworkManagementClient>((uri, cred) => new NetworkManagementClient(Context.Subscription, uri, cred)).VirtualNetworks;

    }
}
