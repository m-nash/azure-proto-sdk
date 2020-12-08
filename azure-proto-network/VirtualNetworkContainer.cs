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
    public class VirtualNetworkContainer : ResourceContainerOperations<VirtualNetwork, VirtualNetworkData>
    {
        public VirtualNetworkContainer(ArmClientContext context, ResourceGroupData resourceGroup) : base(context, resourceGroup) { }

        internal VirtualNetworkContainer(ArmClientContext context, ResourceIdentifier id) : base(context, id) { }

        public override ResourceType ResourceType => "Microsoft.Network/virtualNetworks";

        public override ArmResponse<VirtualNetwork> Create(string name, VirtualNetworkData resourceDetails, CancellationToken cancellationToken = default)
        {
            var operation = Operations.StartCreateOrUpdate(Id.ResourceGroup, name, resourceDetails, cancellationToken);
            return new PhArmResponse<VirtualNetwork, Azure.ResourceManager.Network.Models.VirtualNetwork>(
                operation.WaitForCompletionAsync(cancellationToken).ConfigureAwait(false).GetAwaiter().GetResult(),
                n => new VirtualNetwork(base.ClientContext, new VirtualNetworkData(n)));
        }

        public async override Task<ArmResponse<VirtualNetwork>> CreateAsync(string name, VirtualNetworkData resourceDetails, CancellationToken cancellationToken = default)
        {
            var operation = await Operations.StartCreateOrUpdateAsync(Id.ResourceGroup, name, resourceDetails, cancellationToken).ConfigureAwait(false);
            return new PhArmResponse<VirtualNetwork, Azure.ResourceManager.Network.Models.VirtualNetwork>(
                await operation.WaitForCompletionAsync(cancellationToken).ConfigureAwait(false),
                n => new VirtualNetwork(base.ClientContext, new VirtualNetworkData(n)));
        }

        public override ArmOperation<VirtualNetwork> StartCreate(string name, VirtualNetworkData resourceDetails, CancellationToken cancellationToken = default)
        {
            return new PhArmOperation<VirtualNetwork, Azure.ResourceManager.Network.Models.VirtualNetwork>(
                Operations.StartCreateOrUpdate(base.Id.ResourceGroup, name, resourceDetails, cancellationToken),
                n => new VirtualNetwork(base.ClientContext, new VirtualNetworkData(n)));
        }

        public async override Task<ArmOperation<VirtualNetwork>> StartCreateAsync(string name, VirtualNetworkData resourceDetails, CancellationToken cancellationToken = default)
        {
            return new PhArmOperation<VirtualNetwork, Azure.ResourceManager.Network.Models.VirtualNetwork>(
                await Operations.StartCreateOrUpdateAsync(base.Id.ResourceGroup, name, resourceDetails, cancellationToken).ConfigureAwait(false),
                n => new VirtualNetwork(base.ClientContext, new VirtualNetworkData(n)));
        }

        public ArmBuilder<VirtualNetwork, VirtualNetworkData> Construct(string vnetCidr, Location location = null)
        {
            var vnet = new Azure.ResourceManager.Network.Models.VirtualNetwork()
            {
                Location = location ?? base.DefaultLocation,
                AddressSpace = new AddressSpace() { AddressPrefixes = new List<string>() { vnetCidr } },
            };
            return new ArmBuilder<VirtualNetwork, VirtualNetworkData>(this, new VirtualNetworkData(vnet));
        }

        public Pageable<VirtualNetwork> List(CancellationToken cancellationToken = default)
        {
            return new PhWrappingPageable<Azure.ResourceManager.Network.Models.VirtualNetwork, VirtualNetwork>(
                Operations.List(base.Id.Name, cancellationToken),
                this.convertor());
        }

        public AsyncPageable<VirtualNetwork> ListAsync(CancellationToken cancellationToken = default)
        {
            return new PhWrappingAsyncPageable<Azure.ResourceManager.Network.Models.VirtualNetwork, VirtualNetwork>(
                Operations.ListAsync(base.Id.Name, cancellationToken),
                this.convertor());
        }

        public Pageable<ArmResourceOperations> ListByName(ArmSubstringFilter filter, int? top = null, CancellationToken cancellationToken = default)
        {
            ArmFilterCollection filters = new ArmFilterCollection(VirtualNetworkData.ResourceType);
            filters.SubstringFilter = filter;
            return ResourceListOperations.ListAtContext<ArmResourceOperations, ArmResource>(ClientContext, Id, filters, top, cancellationToken);
        }

        public AsyncPageable<ArmResourceOperations> ListByNameAsync(ArmSubstringFilter filter, int? top = null, CancellationToken cancellationToken = default)
        {
            ArmFilterCollection filters = new ArmFilterCollection(VirtualNetworkData.ResourceType);
            filters.SubstringFilter = filter;
            return ResourceListOperations.ListAtContextAsync<ArmResourceOperations, ArmResource>(ClientContext, Id, filters, top, cancellationToken);
        }
        private  Func<Azure.ResourceManager.Network.Models.VirtualNetwork, VirtualNetwork> convertor()
        {
            return s => new VirtualNetwork(ClientContext, new VirtualNetworkData(s));
        }
        internal VirtualNetworksOperations Operations => GetClient<NetworkManagementClient>((uri, cred) => new NetworkManagementClient(Id.Subscription, uri, cred)).VirtualNetworks;
    }
}
