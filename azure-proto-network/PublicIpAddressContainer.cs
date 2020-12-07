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
    public class PublicIpAddressContainer : ResourceContainerOperations<XPublicIpAddress, PhPublicIPAddress>
    {
        public PublicIpAddressContainer(ArmClientContext context, PhResourceGroup resourceGroup, ArmClientOptions clientOptions) : base(context, resourceGroup, clientOptions) { }

        internal PublicIpAddressContainer(ArmClientContext context, ResourceIdentifier id) : base(context, id) { }

        public override ResourceType ResourceType => "Microsoft.Network/publicIpAddresses";

        public override ArmResponse<XPublicIpAddress> Create(string name, PhPublicIPAddress resourceDetails, CancellationToken cancellationToken = default)
        {
            var operation = Operations.StartCreateOrUpdate(Id.ResourceGroup, name, resourceDetails, cancellationToken);
            return new PhArmResponse<XPublicIpAddress, PublicIPAddress>(
                operation.WaitForCompletionAsync(cancellationToken).ConfigureAwait(false).GetAwaiter().GetResult(),
                n => new XPublicIpAddress(ClientContext, new PhPublicIPAddress(n), this.ClientOptions));
        }

        public async override Task<ArmResponse<XPublicIpAddress>> CreateAsync(string name, PhPublicIPAddress resourceDetails, CancellationToken cancellationToken = default)
        {
            var operation = await Operations.StartCreateOrUpdateAsync(Id.ResourceGroup, name, resourceDetails, cancellationToken).ConfigureAwait(false);
            return new PhArmResponse<XPublicIpAddress, PublicIPAddress>(
                await operation.WaitForCompletionAsync(cancellationToken).ConfigureAwait(false),
                n => new XPublicIpAddress(ClientContext, new PhPublicIPAddress(n), this.ClientOptions));
        }

        public override ArmOperation<XPublicIpAddress> StartCreate(string name, PhPublicIPAddress resourceDetails, CancellationToken cancellationToken = default)
        {
            return new PhArmOperation<XPublicIpAddress, PublicIPAddress>(
                Operations.StartCreateOrUpdate(Id.ResourceGroup, name, resourceDetails, cancellationToken),
                n => new XPublicIpAddress(ClientContext, new PhPublicIPAddress(n), this.ClientOptions));
        }

        public async override Task<ArmOperation<XPublicIpAddress>> StartCreateAsync(string name, PhPublicIPAddress resourceDetails, CancellationToken cancellationToken = default)
        {
            return new PhArmOperation<XPublicIpAddress, PublicIPAddress>(
                await Operations.StartCreateOrUpdateAsync(Id.ResourceGroup, name, resourceDetails, cancellationToken).ConfigureAwait(false),
                n => new XPublicIpAddress(ClientContext, new PhPublicIPAddress(n), this.ClientOptions));
        }

        public ArmBuilder<XPublicIpAddress, PhPublicIPAddress> Construct(Location location = null)
        {
            var ipAddress = new PublicIPAddress()
            {
                PublicIPAddressVersion = IPVersion.IPv4.ToString(),
                PublicIPAllocationMethod = IPAllocationMethod.Dynamic,
                Location = location ?? DefaultLocation,
            };

            return new ArmBuilder<XPublicIpAddress, PhPublicIPAddress>(this, new PhPublicIPAddress(ipAddress));
        }

        public Pageable<XPublicIpAddress> List(CancellationToken cancellationToken = default)
        {
            return new PhWrappingPageable<PublicIPAddress, XPublicIpAddress>(
                Operations.List(Id.Name, cancellationToken),
                this.convertor());
        }

        public AsyncPageable<XPublicIpAddress> ListAsync(CancellationToken cancellationToken = default)
        {
            return new PhWrappingAsyncPageable<PublicIPAddress, XPublicIpAddress>(
                Operations.ListAsync(Id.Name, cancellationToken),
                this.convertor());
        }

        public Pageable<ArmResourceOperations> ListByName(ArmSubstringFilter filter, int? top = null, CancellationToken cancellationToken = default)
        {
            ArmFilterCollection filters = new ArmFilterCollection(PhPublicIPAddress.ResourceType);
            filters.SubstringFilter = filter;
            return ResourceListOperations.ListAtContext<ArmResourceOperations, ArmResource>(ClientContext, Id, filters, top, cancellationToken);
        }

        public AsyncPageable<ArmResourceOperations> ListByNameAsync(ArmSubstringFilter filter, int? top = null, CancellationToken cancellationToken = default)
        {
            ArmFilterCollection filters = new ArmFilterCollection(PhPublicIPAddress.ResourceType);
            filters.SubstringFilter = filter;
            return ResourceListOperations.ListAtContextAsync<ArmResourceOperations, ArmResource>(ClientContext, Id, filters, top, cancellationToken);
        }
        private Func<PublicIPAddress, XPublicIpAddress> convertor()
        {
            return s => new XPublicIpAddress(ClientContext, new PhPublicIPAddress(s), this.ClientOptions);
        }

        internal PublicIPAddressesOperations Operations => GetClient<NetworkManagementClient>((uri, cred) => new NetworkManagementClient(Id.Subscription, uri, cred,
                    ArmClientOptions.convert<NetworkManagementClientOptions>(this.ClientOptions))).PublicIPAddresses;
    }
}
