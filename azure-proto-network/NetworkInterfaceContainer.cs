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
    public class NetworkInterfaceContainer : ResourceContainerOperations<NetworkInterface, NetworkInterfaceData>
    {
        internal NetworkInterfaceContainer(ArmClientContext context, ResourceGroupData resourceGroup, ArmClientOptions clientOptions) : base(context, resourceGroup, clientOptions) { }

        internal NetworkInterfaceContainer(ArmClientContext context, ResourceIdentifier id, ArmClientOptions clientOptions) : base(context, id, clientOptions) { }

        public override ResourceType ResourceType => "Microsoft.Network/networkInterfaces";

        internal NetworkInterfacesOperations Operations => GetClient<NetworkManagementClient>((uri, cred) => new NetworkManagementClient(Id.Subscription, uri, cred, 
            ArmClientOptions.Convert<NetworkManagementClientOptions>(ClientOptions))).NetworkInterfaces;

        public override ArmResponse<NetworkInterface> Create(string name, NetworkInterfaceData resourceDetails, CancellationToken cancellationToken = default)
        {
            var operation = Operations.StartCreateOrUpdate(Id.ResourceGroup, name, resourceDetails, cancellationToken);
            return new PhArmResponse<NetworkInterface, Azure.ResourceManager.Network.Models.NetworkInterface>(
                operation.WaitForCompletionAsync(cancellationToken).ConfigureAwait(false).GetAwaiter().GetResult(),
                n => new NetworkInterface(ClientContext, new NetworkInterfaceData(n), ClientOptions));
        }

        public async override Task<ArmResponse<NetworkInterface>> CreateAsync(string name, NetworkInterfaceData resourceDetails, CancellationToken cancellationToken = default)
        {
            var operation = await Operations.StartCreateOrUpdateAsync(Id.ResourceGroup, name, resourceDetails, cancellationToken).ConfigureAwait(false);
            return new PhArmResponse<NetworkInterface, Azure.ResourceManager.Network.Models.NetworkInterface>(
                await operation.WaitForCompletionAsync(cancellationToken).ConfigureAwait(false),
                n => new NetworkInterface(ClientContext, new NetworkInterfaceData(n), ClientOptions));
        }

        public override ArmOperation<NetworkInterface> StartCreate(string name, NetworkInterfaceData resourceDetails, CancellationToken cancellationToken = default)
        {
            return new PhArmOperation<NetworkInterface, Azure.ResourceManager.Network.Models.NetworkInterface>(
                Operations.StartCreateOrUpdate(Id.ResourceGroup, name, resourceDetails, cancellationToken),
                n => new NetworkInterface(ClientContext, new NetworkInterfaceData(n), ClientOptions));
        }

        public async override Task<ArmOperation<NetworkInterface>> StartCreateAsync(string name, NetworkInterfaceData resourceDetails, CancellationToken cancellationToken = default)
        {
            return new PhArmOperation<NetworkInterface, Azure.ResourceManager.Network.Models.NetworkInterface>(
                await Operations.StartCreateOrUpdateAsync(Id.ResourceGroup, name, resourceDetails, cancellationToken).ConfigureAwait(false),
                n => new NetworkInterface(ClientContext, new NetworkInterfaceData(n), ClientOptions));
        }

        public ArmBuilder<NetworkInterface, NetworkInterfaceData> Construct(PublicIPAddressData ip, string subnetId, Location location = null)
        {
            var nic = new Azure.ResourceManager.Network.Models.NetworkInterface()
            {
                Location = location ?? DefaultLocation,
                IpConfigurations = new List<NetworkInterfaceIPConfiguration>()
                {
                    new NetworkInterfaceIPConfiguration()
                    {
                        Name = "Primary",
                        Primary = true,
                        Subnet = new Azure.ResourceManager.Network.Models.Subnet() { Id = subnetId },
                        PrivateIPAllocationMethod = IPAllocationMethod.Dynamic,
                        PublicIPAddress = new PublicIPAddress() { Id = ip.Id }
                    }
                }
            };

            return new ArmBuilder<NetworkInterface, NetworkInterfaceData>(this, new NetworkInterfaceData(nic));
        }

        public Pageable<NetworkInterfaceOperations> List(CancellationToken cancellationToken = default)
        {
            return new PhWrappingPageable<Azure.ResourceManager.Network.Models.NetworkInterface, NetworkInterfaceOperations>(
                Operations.List(Id.Name, cancellationToken),
                this.convertor());
        }

        public AsyncPageable<NetworkInterfaceOperations> ListAsync(CancellationToken cancellationToken = default)
        {
            var result = Operations.ListAsync(Id.Name, cancellationToken);
            return new PhWrappingAsyncPageable<Azure.ResourceManager.Network.Models.NetworkInterface, NetworkInterfaceOperations>(
                result,
                this.convertor());
        }

        public Pageable<ArmResource> ListByName(ArmSubstringFilter filter, int? top = null, CancellationToken cancellationToken = default)
        {
            ArmFilterCollection filters = new ArmFilterCollection(NetworkInterfaceData.ResourceType);
            filters.SubstringFilter = filter;
            return ResourceListOperations.ListAtContext<ArmResource, ArmResourceData>(ClientContext, ClientOptions, Id, filters, top, cancellationToken);
        }

        public AsyncPageable<ArmResource> ListByNameAsync(ArmSubstringFilter filter, int? top = null, CancellationToken cancellationToken = default)
        {
            ArmFilterCollection filters = new ArmFilterCollection(NetworkInterfaceData.ResourceType);
            filters.SubstringFilter = filter;
            return ResourceListOperations.ListAtContextAsync<ArmResource, ArmResourceData>(ClientContext, ClientOptions, Id, filters, top, cancellationToken);
        }
        private Func<Azure.ResourceManager.Network.Models.NetworkInterface, NetworkInterface> convertor()
        {
            return s => new NetworkInterface(ClientContext, new NetworkInterfaceData(s), ClientOptions);
        }
    }
}
