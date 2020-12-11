using Azure;
using Azure.ResourceManager.Network;
using Azure.ResourceManager.Network.Models;
using azure_proto_core;
using azure_proto_core.Resources;
using System.Threading;
using System.Threading.Tasks;
using System;
using azure_proto_core.Adapters;

namespace azure_proto_network
{
    public class PublicIpAddressContainer : ResourceContainerOperations<PublicIpAddress, PublicIPAddressData>
    {
        internal PublicIpAddressContainer(ArmResourceOperations genericOperations)
            : base(genericOperations.ClientContext,genericOperations.Id, genericOperations.ClientOptions)
        {
        }

        internal PublicIpAddressContainer(ArmClientContext context, ResourceGroupData resourceGroup, ArmClientOptions clientOptions)
            : base(context, resourceGroup, clientOptions)
        {
        }

        internal PublicIpAddressContainer(ArmClientContext context, ResourceIdentifier id, ArmClientOptions clientOptions)
            : base(context, id, clientOptions)
        {
        }

        internal PublicIPAddressesOperations Operations => GetClient<NetworkManagementClient>((uri, cred) => new NetworkManagementClient(Id.Subscription, uri, cred,
            ArmClientOptions.Convert<NetworkManagementClientOptions>(ClientOptions))).PublicIPAddresses;

        public override ResourceType ResourceType => "Microsoft.Network/publicIpAddresses";

        public override ArmResponse<PublicIpAddress> Create(string name, PublicIPAddressData resourceDetails, CancellationToken cancellationToken = default)
        {
            var operation = Operations.StartCreateOrUpdate(Id.ResourceGroup, name, resourceDetails, cancellationToken);
            return new PhArmResponse<PublicIpAddress, PublicIPAddress>(
                operation.WaitForCompletionAsync(cancellationToken).ConfigureAwait(false).GetAwaiter().GetResult(),
                n => new PublicIpAddress(ClientContext, new PublicIPAddressData(n), ClientOptions));
        }

        public async override Task<ArmResponse<PublicIpAddress>> CreateAsync(string name, PublicIPAddressData resourceDetails, CancellationToken cancellationToken = default)
        {
            var operation = await Operations.StartCreateOrUpdateAsync(Id.ResourceGroup, name, resourceDetails, cancellationToken).ConfigureAwait(false);
            return new PhArmResponse<PublicIpAddress, PublicIPAddress>(
                await operation.WaitForCompletionAsync(cancellationToken).ConfigureAwait(false),
                n => new PublicIpAddress(ClientContext, new PublicIPAddressData(n), ClientOptions));
        }

        public override ArmOperation<PublicIpAddress> StartCreate(string name, PublicIPAddressData resourceDetails, CancellationToken cancellationToken = default)
        {
            return new PhArmOperation<PublicIpAddress, PublicIPAddress>(
                Operations.StartCreateOrUpdate(Id.ResourceGroup, name, resourceDetails, cancellationToken),
                n => new PublicIpAddress(ClientContext, new PublicIPAddressData(n), ClientOptions));
        }

        public async override Task<ArmOperation<PublicIpAddress>> StartCreateAsync(string name, PublicIPAddressData resourceDetails, CancellationToken cancellationToken = default)
        {
            return new PhArmOperation<PublicIpAddress, PublicIPAddress>(
                await Operations.StartCreateOrUpdateAsync(Id.ResourceGroup, name, resourceDetails, cancellationToken).ConfigureAwait(false),
                n => new PublicIpAddress(ClientContext, new PublicIPAddressData(n), ClientOptions));
        }

        public ArmBuilder<PublicIpAddress, PublicIPAddressData> Construct(Location location = null)
        {
            var ipAddress = new PublicIPAddress()
            {
                PublicIPAddressVersion = IPVersion.IPv4.ToString(),
                PublicIPAllocationMethod = IPAllocationMethod.Dynamic,
                Location = location ?? DefaultLocation,
            };

            return new ArmBuilder<PublicIpAddress, PublicIPAddressData>(this, new PublicIPAddressData(ipAddress));
        }

        public Pageable<PublicIpAddress> List(CancellationToken cancellationToken = default)
        {
            return new PhWrappingPageable<PublicIPAddress, PublicIpAddress>(
                Operations.List(Id.Name, cancellationToken),
                this.convertor());
        }

        public AsyncPageable<PublicIpAddress> ListAsync(CancellationToken cancellationToken = default)
        {
            return new PhWrappingAsyncPageable<PublicIPAddress, PublicIpAddress>(
                Operations.ListAsync(Id.Name, cancellationToken),
                this.convertor());
        }

        public Pageable<ArmResourceOperations> ListByName(ArmSubstringFilter filter, int? top = null, CancellationToken cancellationToken = default)
        {
            ArmFilterCollection filters = new ArmFilterCollection(PublicIPAddressData.ResourceType);
            filters.SubstringFilter = filter;
            return ResourceListOperations.ListAtContext<ArmResourceOperations, ArmResource>(ClientContext, ClientOptions, Id, filters, top, cancellationToken);
        }

        public AsyncPageable<ArmResourceOperations> ListByNameAsync(ArmSubstringFilter filter, int? top = null, CancellationToken cancellationToken = default)
        {
            ArmFilterCollection filters = new ArmFilterCollection(PublicIPAddressData.ResourceType);
            filters.SubstringFilter = filter;
            return ResourceListOperations.ListAtContextAsync<ArmResourceOperations, ArmResource>(ClientContext, ClientOptions, Id, filters, top, cancellationToken);
        }

        public Pageable<PublicIpAddress> ListByNameExpanded(ArmSubstringFilter filter, int? top = null, CancellationToken cancellationToken = default)
        {
            var results = ListByName(filter, top, cancellationToken);
            return new PhWrappingPageable<ArmResourceOperations, PublicIpAddress>(results, s => new PublicIpAddressOperations(s).Get().Value);
        }

        public AsyncPageable<PublicIpAddress> ListByNameExpandedAsync(ArmSubstringFilter filter, int? top = null, CancellationToken cancellationToken = default)
        {
            var results = ListByNameAsync(filter, top, cancellationToken);
            return new PhWrappingAsyncPageable<ArmResourceOperations, PublicIpAddress>(results, s => new PublicIpAddressOperations(s).Get().Value);
        }

        private Func<PublicIPAddress, PublicIpAddress> convertor()
        {
            return s => new PublicIpAddress(ClientContext, new PublicIPAddressData(s), ClientOptions);
        }
    }
}
