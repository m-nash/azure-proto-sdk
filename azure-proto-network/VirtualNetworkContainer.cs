using Azure.ResourceManager.Network;
using Azure.ResourceManager.Network.Models;
using azure_proto_core;
using System.Threading;
using System.Threading.Tasks;

namespace azure_proto_network
{
    public class VirtualNetworkContainer : ResourceContainerOperations<PhVirtualNetwork>
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

        internal VirtualNetworkOperations Vnet(TrackedResource vnet)
        {
            return new VirtualNetworkOperations(this, vnet);
        }

        //TODO make the StartCreate and Create consistent
        public override ArmResponse<ResourceOperationsBase<PhVirtualNetwork>> Create(string name, PhVirtualNetwork resourceDetails, CancellationToken cancellationToken = default)
        {
            var operation = Operations.StartCreateOrUpdate(Id.ResourceGroup, name, resourceDetails, cancellationToken);
            return new PhArmResponse<ResourceOperationsBase<PhVirtualNetwork>, VirtualNetwork>(
                operation.WaitForCompletionAsync(cancellationToken).ConfigureAwait(false).GetAwaiter().GetResult(),
                n => Vnet(new PhVirtualNetwork(n)));
        }

        public async override Task<ArmResponse<ResourceOperationsBase<PhVirtualNetwork>>> CreateAsync(string name, PhVirtualNetwork resourceDetails, CancellationToken cancellationToken = default)
        {
            var operation = await Operations.StartCreateOrUpdateAsync(Id.ResourceGroup, name, resourceDetails, cancellationToken).ConfigureAwait(false);
            return new PhArmResponse<ResourceOperationsBase<PhVirtualNetwork>, VirtualNetwork>(
                await operation.WaitForCompletionAsync(cancellationToken).ConfigureAwait(false),
                n => Vnet(new PhVirtualNetwork(n)));
        }

        public override ArmOperation<ResourceOperationsBase<PhVirtualNetwork>> StartCreate(string name, PhVirtualNetwork resourceDetails, CancellationToken cancellationToken = default)
        {
            return new PhArmOperation<ResourceOperationsBase<PhVirtualNetwork>, VirtualNetwork>(
                Operations.StartCreateOrUpdate(Id.ResourceGroup, name, resourceDetails, cancellationToken),
                n => Vnet(new PhVirtualNetwork(n)));
        }

        public async override Task<ArmOperation<ResourceOperationsBase<PhVirtualNetwork>>> StartCreateAsync(string name, PhVirtualNetwork resourceDetails, CancellationToken cancellationToken = default)
        {
            return new PhArmOperation<ResourceOperationsBase<PhVirtualNetwork>, VirtualNetwork>(
                await Operations.StartCreateOrUpdateAsync(Id.ResourceGroup, name, resourceDetails, cancellationToken).ConfigureAwait(false),
                n => Vnet(new PhVirtualNetwork(n)));
        }

        internal VirtualNetworksOperations Operations => GetClient<NetworkManagementClient>((uri, cred) => new NetworkManagementClient(Id.Subscription, uri, cred)).VirtualNetworks;
    }
}
