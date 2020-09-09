using Azure.ResourceManager.Network;
using Azure.ResourceManager.Network.Models;
using azure_proto_core;
using System.Collections.Generic;
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

        //TODO make the StartCreate and Create consistent
        public override ArmOperation<ResourceOperationsBase<PhVirtualNetwork>> Create(string name, PhVirtualNetwork resourceDetails)
        {
            return new PhArmOperation<ResourceOperationsBase<PhVirtualNetwork>, VirtualNetwork>(Operations.StartCreateOrUpdate(Context.ResourceGroup, name, resourceDetails), n => Vnet(new PhVirtualNetwork(n)));
        }

        public async override Task<ArmOperation<ResourceOperationsBase<PhVirtualNetwork>>> CreateAsync(string name, PhVirtualNetwork resourceDetails, CancellationToken cancellationToken = default)
        {
            return new PhArmOperation<ResourceOperationsBase<PhVirtualNetwork>, VirtualNetwork>(await Operations.StartCreateOrUpdateAsync(Context.ResourceGroup, name, resourceDetails, cancellationToken), n => Vnet(new PhVirtualNetwork(n)));
        }

        internal VirtualNetworkOperations Vnet(TrackedResource vnet)
        {
            return new VirtualNetworkOperations(this, vnet);
        }

        internal VirtualNetworksOperations Operations => GetClient<NetworkManagementClient>((uri, cred) => new NetworkManagementClient(Context.Subscription, uri, cred)).VirtualNetworks;
    }
}
