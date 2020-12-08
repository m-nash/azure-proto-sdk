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
    public class VirtualNetworkContainer : ResourceContainerOperations<XVirtualNetwork, PhVirtualNetwork>
    {
        public VirtualNetworkContainer(ArmResourceOperations genericOperations) : base(genericOperations.ClientContext, genericOperations.Id, genericOperations.ClientOptions) { }
        internal VirtualNetworkContainer(ArmClientContext context, PhResourceGroup resourceGroup, ArmClientOptions clientOptions) : base(context, resourceGroup, clientOptions) { }

        internal VirtualNetworkContainer(ArmClientContext context, ResourceIdentifier id, ArmClientOptions clientOptions) : base(context, id, clientOptions) { }

        public override ResourceType ResourceType => "Microsoft.Network/virtualNetworks";
        internal VirtualNetworksOperations Operations => GetClient<NetworkManagementClient>((uri, cred) => new NetworkManagementClient(Id.Subscription, uri, cred,
            ArmClientOptions.convert<NetworkManagementClientOptions>(ClientOptions))).VirtualNetworks;

        public override ArmResponse<XVirtualNetwork> Create(string name, PhVirtualNetwork resourceDetails, CancellationToken cancellationToken = default)
        {
            var operation = Operations.StartCreateOrUpdate(Id.ResourceGroup, name, resourceDetails, cancellationToken);
            return new PhArmResponse<XVirtualNetwork, VirtualNetwork>(
                operation.WaitForCompletionAsync(cancellationToken).ConfigureAwait(false).GetAwaiter().GetResult(),
                n => new XVirtualNetwork(ClientContext, new PhVirtualNetwork(n), ClientOptions));
        }

        public async override Task<ArmResponse<XVirtualNetwork>> CreateAsync(string name, PhVirtualNetwork resourceDetails, CancellationToken cancellationToken = default)
        {
            var operation = await Operations.StartCreateOrUpdateAsync(Id.ResourceGroup, name, resourceDetails, cancellationToken).ConfigureAwait(false);
            return new PhArmResponse<XVirtualNetwork, VirtualNetwork>(
                await operation.WaitForCompletionAsync(cancellationToken).ConfigureAwait(false),
                n => new XVirtualNetwork(ClientContext, new PhVirtualNetwork(n), ClientOptions));
        }

        public override ArmOperation<XVirtualNetwork> StartCreate(string name, PhVirtualNetwork resourceDetails, CancellationToken cancellationToken = default)
        {
            return new PhArmOperation<XVirtualNetwork, VirtualNetwork>(
                Operations.StartCreateOrUpdate(Id.ResourceGroup, name, resourceDetails, cancellationToken),
                n => new XVirtualNetwork(ClientContext, new PhVirtualNetwork(n), ClientOptions));
        }

        public async override Task<ArmOperation<XVirtualNetwork>> StartCreateAsync(string name, PhVirtualNetwork resourceDetails, CancellationToken cancellationToken = default)
        {
            return new PhArmOperation<XVirtualNetwork, VirtualNetwork>(
                await Operations.StartCreateOrUpdateAsync(Id.ResourceGroup, name, resourceDetails, cancellationToken).ConfigureAwait(false),
                n => new XVirtualNetwork(ClientContext, new PhVirtualNetwork(n), ClientOptions));
        }

        public ArmBuilder<XVirtualNetwork, PhVirtualNetwork> Construct(string vnetCidr, Location location = null)
        {
            var vnet = new VirtualNetwork()
            {
                Location = location ?? DefaultLocation,
                AddressSpace = new AddressSpace() { AddressPrefixes = new List<string>() { vnetCidr } },
            };
            return new ArmBuilder<XVirtualNetwork, PhVirtualNetwork>(this, new PhVirtualNetwork(vnet));
        }

        public Pageable<XVirtualNetwork> List(CancellationToken cancellationToken = default)
        {
            return new PhWrappingPageable<VirtualNetwork, XVirtualNetwork>(
                Operations.List(Id.Name, cancellationToken),
                this.convertor());
        }

        public AsyncPageable<XVirtualNetwork> ListAsync(CancellationToken cancellationToken = default)
        {
            return new PhWrappingAsyncPageable<VirtualNetwork, XVirtualNetwork>(
                Operations.ListAsync(Id.Name, cancellationToken),
                this.convertor());
        }

        public Pageable<ArmResourceOperations> ListByName(ArmSubstringFilter filter, int? top = null, CancellationToken cancellationToken = default)
        {
            ArmFilterCollection filters = new ArmFilterCollection(PhVirtualNetwork.ResourceType);
            filters.SubstringFilter = filter;
            return ResourceListOperations.ListAtContext<ArmResourceOperations, ArmResource>(ClientContext, ClientOptions, Id, filters, top, cancellationToken);
        }

        public AsyncPageable<ArmResourceOperations> ListByNameAsync(ArmSubstringFilter filter, int? top = null, CancellationToken cancellationToken = default)
        {
            ArmFilterCollection filters = new ArmFilterCollection(PhVirtualNetwork.ResourceType);
            filters.SubstringFilter = filter;
            return ResourceListOperations.ListAtContextAsync<ArmResourceOperations, ArmResource>(ClientContext, ClientOptions, Id, filters, top, cancellationToken);
        }
        private Func<VirtualNetwork, XVirtualNetwork> convertor()
        {
            return s => new XVirtualNetwork(ClientContext, new PhVirtualNetwork(s), ClientOptions);
        }
    }
}
