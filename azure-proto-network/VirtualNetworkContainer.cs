using Azure.ResourceManager.Network;
using Azure.ResourceManager.Network.Models;
using azure_proto_core;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace azure_proto_network
{
    public class VirtualNetworkContainer : ResourceContainerOperations<VirtualNetworkOperations, PhVirtualNetwork>
    {
        public VirtualNetworkContainer(ArmClientContext parent, ResourceIdentifier context) : base(parent, context)
        {
        }

        public VirtualNetworkContainer(ArmClientContext parent, azure_proto_core.Resource context) : base(parent, context)
        {
        }

        public VirtualNetworkContainer(OperationsBase parent, ResourceIdentifier context) : base(parent, context)
        {
        }

        public override ResourceType ResourceType => "Microsoft.Network/virtualNetworks";

        public override ArmResponse<VirtualNetworkOperations> Create(string name, PhVirtualNetwork resourceDetails, CancellationToken cancellationToken = default)
        {
            var operation = Operations.StartCreateOrUpdate(Id.ResourceGroup, name, resourceDetails, cancellationToken);
            return new PhArmResponse<VirtualNetworkOperations, VirtualNetwork>(
                operation.WaitForCompletionAsync(cancellationToken).ConfigureAwait(false).GetAwaiter().GetResult(),
                n => new VirtualNetworkOperations(this, new PhVirtualNetwork(n)));
        }

        public async override Task<ArmResponse<VirtualNetworkOperations>> CreateAsync(string name, PhVirtualNetwork resourceDetails, CancellationToken cancellationToken = default)
        {
            var operation = await Operations.StartCreateOrUpdateAsync(Id.ResourceGroup, name, resourceDetails, cancellationToken).ConfigureAwait(false);
            return new PhArmResponse<VirtualNetworkOperations, VirtualNetwork>(
                await operation.WaitForCompletionAsync(cancellationToken).ConfigureAwait(false),
                n => new VirtualNetworkOperations(this, new PhVirtualNetwork(n)));
        }

        public override ArmOperation<VirtualNetworkOperations> StartCreate(string name, PhVirtualNetwork resourceDetails, CancellationToken cancellationToken = default)
        {
            return new PhArmOperation<VirtualNetworkOperations, VirtualNetwork>(
                Operations.StartCreateOrUpdate(Id.ResourceGroup, name, resourceDetails, cancellationToken),
                n => new VirtualNetworkOperations(this, new PhVirtualNetwork(n)));
        }

        public async override Task<ArmOperation<VirtualNetworkOperations>> StartCreateAsync(string name, PhVirtualNetwork resourceDetails, CancellationToken cancellationToken = default)
        {
            return new PhArmOperation<VirtualNetworkOperations, VirtualNetwork>(
                await Operations.StartCreateOrUpdateAsync(Id.ResourceGroup, name, resourceDetails, cancellationToken).ConfigureAwait(false),
                n => new VirtualNetworkOperations(this, new PhVirtualNetwork(n)));
        }

        public ArmBuilder<VirtualNetworkOperations, PhVirtualNetwork> Construct(string vnetCidr, Location location = null)
        {
            var vnet = new VirtualNetwork()
            {
                Location = location ?? DefaultLocation,
                AddressSpace = new AddressSpace() { AddressPrefixes = new List<string>() { vnetCidr } },
            };
            return new ArmBuilder<VirtualNetworkOperations, PhVirtualNetwork>(new VirtualNetworkContainer(this, Id), new PhVirtualNetwork(vnet));
        }

        internal VirtualNetworksOperations Operations => GetClient<NetworkManagementClient>((uri, cred) => new NetworkManagementClient(Id.Subscription, uri, cred)).VirtualNetworks;
    }
}
