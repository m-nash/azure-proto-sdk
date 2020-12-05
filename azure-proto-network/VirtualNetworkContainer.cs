using Azure;
using Azure.ResourceManager.Network;
using Azure.ResourceManager.Network.Models;
using azure_proto_core;
using azure_proto_core.Resources;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using System;
using azure_proto_core.Adapters;

namespace azure_proto_network
{
    public class VirtualNetworkContainer : ResourceContainerOperations<VirtualNetworkOperations, PhVirtualNetwork>
    {
        public VirtualNetworkContainer(ArmClientContext context, PhResourceGroup resourceGroup, ArmClientOptions clientOptions) : base(context, resourceGroup, clientOptions) { }

        public override ResourceType ResourceType => "Microsoft.Network/virtualNetworks";

        public override ArmResponse<VirtualNetworkOperations> Create(string name, PhVirtualNetwork resourceDetails, CancellationToken cancellationToken = default)
        {
            var operation = Operations.StartCreateOrUpdate(Id.ResourceGroup, name, resourceDetails, cancellationToken);
            return new PhArmResponse<VirtualNetworkOperations, VirtualNetwork>(
                operation.WaitForCompletionAsync(cancellationToken).ConfigureAwait(false).GetAwaiter().GetResult(),
                n => new VirtualNetworkOperations(ClientContext, new PhVirtualNetwork(n), this.ClientOptions));
        }

        public async override Task<ArmResponse<VirtualNetworkOperations>> CreateAsync(string name, PhVirtualNetwork resourceDetails, CancellationToken cancellationToken = default)
        {
            var operation = await Operations.StartCreateOrUpdateAsync(Id.ResourceGroup, name, resourceDetails, cancellationToken).ConfigureAwait(false);
            return new PhArmResponse<VirtualNetworkOperations, VirtualNetwork>(
                await operation.WaitForCompletionAsync(cancellationToken).ConfigureAwait(false),
                n => new VirtualNetworkOperations(ClientContext, new PhVirtualNetwork(n), this.ClientOptions));
        }

        public override ArmOperation<VirtualNetworkOperations> StartCreate(string name, PhVirtualNetwork resourceDetails, CancellationToken cancellationToken = default)
        {
            return new PhArmOperation<VirtualNetworkOperations, VirtualNetwork>(
                Operations.StartCreateOrUpdate(Id.ResourceGroup, name, resourceDetails, cancellationToken),
                n => new VirtualNetworkOperations(ClientContext, new PhVirtualNetwork(n), this.ClientOptions));
        }

        public async override Task<ArmOperation<VirtualNetworkOperations>> StartCreateAsync(string name, PhVirtualNetwork resourceDetails, CancellationToken cancellationToken = default)
        {
            return new PhArmOperation<VirtualNetworkOperations, VirtualNetwork>(
                await Operations.StartCreateOrUpdateAsync(Id.ResourceGroup, name, resourceDetails, cancellationToken).ConfigureAwait(false),
                n => new VirtualNetworkOperations(ClientContext, new PhVirtualNetwork(n), this.ClientOptions));
        }

        public ArmBuilder<VirtualNetworkOperations, PhVirtualNetwork> Construct(string vnetCidr, Location location = null)
        {
            var vnet = new VirtualNetwork()
            {
                Location = location ?? DefaultLocation,
                AddressSpace = new AddressSpace() { AddressPrefixes = new List<string>() { vnetCidr } },
            };
            return new ArmBuilder<VirtualNetworkOperations, PhVirtualNetwork>(this, new PhVirtualNetwork(vnet));
        }

        public Pageable<VirtualNetworkOperations> List(CancellationToken cancellationToken = default)
        {
            return new PhWrappingPageable<VirtualNetwork, VirtualNetworkOperations>(
                Operations.List(Id.Name, cancellationToken),
                this.convertor());
        }

        public AsyncPageable<VirtualNetworkOperations> ListAsync(CancellationToken cancellationToken = default)
        {
            return new PhWrappingAsyncPageable<VirtualNetwork, VirtualNetworkOperations>(
                Operations.ListAsync(Id.Name, cancellationToken),
                this.convertor());
        }

        public Pageable<ArmResourceOperations> ListByName(ArmSubstringFilter filter, int? top = null, CancellationToken cancellationToken = default)
        {
            ArmFilterCollection filters = new ArmFilterCollection(PhVirtualNetwork.ResourceType);
            filters.SubstringFilter = filter;
            return ResourceListOperations.ListAtContext<ArmResourceOperations, ArmResource>(ClientContext, Id, filters, top, cancellationToken);
        }

        public AsyncPageable<ArmResourceOperations> ListByNameAsync(ArmSubstringFilter filter, int? top = null, CancellationToken cancellationToken = default)
        {
            ArmFilterCollection filters = new ArmFilterCollection(PhVirtualNetwork.ResourceType);
            filters.SubstringFilter = filter;
            return ResourceListOperations.ListAtContextAsync<ArmResourceOperations, ArmResource>(ClientContext, Id, filters, top, cancellationToken);
        }
        private Func<VirtualNetwork, VirtualNetworkOperations> convertor()
        {
            return s => new VirtualNetworkOperations(ClientContext, new PhVirtualNetwork(s), this.ClientOptions);
        }
        internal VirtualNetworksOperations Operations => GetClient<NetworkManagementClient>((uri, cred) => new NetworkManagementClient(Id.Subscription, uri, cred,
                    ArmClientOptions.convert<NetworkManagementClientOptions>(this.ClientOptions))).VirtualNetworks;
    }
}
