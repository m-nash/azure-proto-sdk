using Azure;
using Azure.ResourceManager.Network;
using Azure.ResourceManager.Network.Models;
using Azure.ResourceManager.Core;
using Azure.ResourceManager.Core.Adapters;
using Azure.ResourceManager.Core.Resources;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using System;

namespace azure_proto_network
{
    public class VirtualNetworkContainer : ResourceContainerBase<VirtualNetwork, VirtualNetworkData>
    {
        internal VirtualNetworkContainer(ArmResourceOperations genericOperations)
            : base(genericOperations.ClientContext, genericOperations.Id, genericOperations.ClientOptions)
        {
        }

        internal VirtualNetworkContainer(AzureResourceManagerClientContext context, ResourceGroupData resourceGroup, AzureResourceManagerClientOptions clientOptions)
            : base(context, resourceGroup, clientOptions)
        {
        }

        internal VirtualNetworkContainer(AzureResourceManagerClientContext context, ResourceIdentifier id, AzureResourceManagerClientOptions clientOptions)
            : base(context, id, clientOptions)
        {
        }

        public override ResourceType ResourceType => "Microsoft.Network/virtualNetworks";

        internal VirtualNetworksOperations Operations => GetClient<NetworkManagementClient>((uri, cred) => new NetworkManagementClient(Id.Subscription, uri, cred,
            AzureResourceManagerClientOptions.Convert<NetworkManagementClientOptions>(ClientOptions))).VirtualNetworks;

        public override ArmResponse<VirtualNetwork> Create(string name, VirtualNetworkData resourceDetails, CancellationToken cancellationToken = default)
        {
            var operation = Operations.StartCreateOrUpdate(Id.ResourceGroup, name, resourceDetails, cancellationToken);
            return new PhArmResponse<VirtualNetwork, Azure.ResourceManager.Network.Models.VirtualNetwork>(
                operation.WaitForCompletionAsync(cancellationToken).ConfigureAwait(false).GetAwaiter().GetResult(),
                n => new VirtualNetwork(ClientContext, new VirtualNetworkData(n), ClientOptions));
        }

        public async override Task<ArmResponse<VirtualNetwork>> CreateAsync(string name, VirtualNetworkData resourceDetails, CancellationToken cancellationToken = default)
        {
            var operation = await Operations.StartCreateOrUpdateAsync(Id.ResourceGroup, name, resourceDetails, cancellationToken).ConfigureAwait(false);
            return new PhArmResponse<VirtualNetwork, Azure.ResourceManager.Network.Models.VirtualNetwork>(
                await operation.WaitForCompletionAsync(cancellationToken).ConfigureAwait(false),
                n => new VirtualNetwork(ClientContext, new VirtualNetworkData(n), ClientOptions));
        }

        public override ArmOperation<VirtualNetwork> StartCreate(string name, VirtualNetworkData resourceDetails, CancellationToken cancellationToken = default)
        {
            return new PhArmOperation<VirtualNetwork, Azure.ResourceManager.Network.Models.VirtualNetwork>(
                Operations.StartCreateOrUpdate(Id.ResourceGroup, name, resourceDetails, cancellationToken),
                n => new VirtualNetwork(ClientContext, new VirtualNetworkData(n), ClientOptions));
        }

        public async override Task<ArmOperation<VirtualNetwork>> StartCreateAsync(string name, VirtualNetworkData resourceDetails, CancellationToken cancellationToken = default)
        {
            return new PhArmOperation<VirtualNetwork, Azure.ResourceManager.Network.Models.VirtualNetwork>(
                await Operations.StartCreateOrUpdateAsync(Id.ResourceGroup, name, resourceDetails, cancellationToken).ConfigureAwait(false),
                n => new VirtualNetwork(ClientContext, new VirtualNetworkData(n), ClientOptions));
        }

        public ArmBuilder<VirtualNetwork, VirtualNetworkData> Construct(string vnetCidr, Location location = null)
        {
            var vnet = new Azure.ResourceManager.Network.Models.VirtualNetwork()
            {
                Location = location ?? DefaultLocation,
                AddressSpace = new AddressSpace() { AddressPrefixes = new List<string>() { vnetCidr } },
            };

            return new ArmBuilder<VirtualNetwork, VirtualNetworkData>(this, new VirtualNetworkData(vnet));
        }

        public Pageable<VirtualNetwork> List(CancellationToken cancellationToken = default)
        {
            return new PhWrappingPageable<Azure.ResourceManager.Network.Models.VirtualNetwork, VirtualNetwork>(
                Operations.List(Id.Name, cancellationToken),
                this.convertor());
        }

        public AsyncPageable<VirtualNetwork> ListAsync(CancellationToken cancellationToken = default)
        {
            return new PhWrappingAsyncPageable<Azure.ResourceManager.Network.Models.VirtualNetwork, VirtualNetwork>(
                Operations.ListAsync(Id.Name, cancellationToken),
                this.convertor());
        }

        public Pageable<ArmResource> ListByName(ArmSubstringFilter filter, int? top = null, CancellationToken cancellationToken = default)
        {
            ArmFilterCollection filters = new ArmFilterCollection(VirtualNetworkData.ResourceType);
            filters.SubstringFilter = filter;
            return ResourceListOperations.ListAtContext<ArmResource, ArmResourceData>(ClientContext, ClientOptions, Id, filters, top, cancellationToken);
        }

        public AsyncPageable<ArmResource> ListByNameAsync(ArmSubstringFilter filter, int? top = null, CancellationToken cancellationToken = default)
        {
            ArmFilterCollection filters = new ArmFilterCollection(VirtualNetworkData.ResourceType);
            filters.SubstringFilter = filter;
            return ResourceListOperations.ListAtContextAsync<ArmResource, ArmResourceData>(ClientContext, ClientOptions, Id, filters, top, cancellationToken);
        }

        public Pageable<VirtualNetwork> ListByNameExpanded(ArmSubstringFilter filter, int? top = null, CancellationToken cancellationToken = default)
        {
            var results = ListByName(filter, top, cancellationToken);
            return new PhWrappingPageable<ArmResource, VirtualNetwork>(results, s => new VirtualNetworkOperations(s).Get().Value);
        }

        public AsyncPageable<VirtualNetwork> ListByNameExpandedAsync(ArmSubstringFilter filter, int? top = null, CancellationToken cancellationToken = default)
        {
            var results = ListByNameAsync(filter, top, cancellationToken);
            return new PhWrappingAsyncPageable<ArmResource, VirtualNetwork>(results, s => new VirtualNetworkOperations(s).Get().Value);
        }

        private  Func<Azure.ResourceManager.Network.Models.VirtualNetwork, VirtualNetwork> convertor()
        {
            return s => new VirtualNetwork(ClientContext, new VirtualNetworkData(s), ClientOptions);
        }
    }
}
