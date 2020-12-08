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
        public NetworkInterfaceContainer(ArmClientContext context, ResourceGroupData resourceGroup) : base(context, resourceGroup) { }

        internal NetworkInterfaceContainer(ArmClientContext context, ResourceIdentifier id) : base(context, id) { }

        public override ResourceType ResourceType => "Microsoft.Network/networkInterfaces";

        public override ArmResponse<NetworkInterface> Create(string name, NetworkInterfaceData resourceDetails, CancellationToken cancellationToken = default)
        {
            var operation = Operations.StartCreateOrUpdate(Id.ResourceGroup, name, resourceDetails, cancellationToken);
            return new PhArmResponse<NetworkInterface, Azure.ResourceManager.Network.Models.NetworkInterface>(
                operation.WaitForCompletionAsync(cancellationToken).ConfigureAwait(false).GetAwaiter().GetResult(),
                n => new NetworkInterface(base.ClientContext, new NetworkInterfaceData(n)));
        }

        public async override Task<ArmResponse<NetworkInterface>> CreateAsync(string name, NetworkInterfaceData resourceDetails, CancellationToken cancellationToken = default)
        {
            var operation = await Operations.StartCreateOrUpdateAsync(Id.ResourceGroup, name, resourceDetails, cancellationToken).ConfigureAwait(false);
            return new PhArmResponse<NetworkInterface, Azure.ResourceManager.Network.Models.NetworkInterface>(
                await operation.WaitForCompletionAsync(cancellationToken).ConfigureAwait(false),
                n => new NetworkInterface(base.ClientContext, new NetworkInterfaceData(n)));
        }

        public override ArmOperation<NetworkInterface> StartCreate(string name, NetworkInterfaceData resourceDetails, CancellationToken cancellationToken = default)
        {
            return new PhArmOperation<NetworkInterface, Azure.ResourceManager.Network.Models.NetworkInterface>(
                Operations.StartCreateOrUpdate(base.Id.ResourceGroup, name, resourceDetails, cancellationToken),
                n => new NetworkInterface(base.ClientContext, new NetworkInterfaceData(n)));
        }

        public async override Task<ArmOperation<NetworkInterface>> StartCreateAsync(string name, NetworkInterfaceData resourceDetails, CancellationToken cancellationToken = default)
        {
            return new PhArmOperation<NetworkInterface, Azure.ResourceManager.Network.Models.NetworkInterface>(
                await Operations.StartCreateOrUpdateAsync(base.Id.ResourceGroup, name, resourceDetails, cancellationToken).ConfigureAwait(false),
                n => new NetworkInterface(base.ClientContext, new NetworkInterfaceData(n)));
        }

        public ArmBuilder<NetworkInterface, NetworkInterfaceData> Construct(PublicIPAddressData ip, string subnetId, Location location = null)
        {
            var nic = new Azure.ResourceManager.Network.Models.NetworkInterface()
            {
                Location = location ?? base.DefaultLocation,
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
                Operations.List(base.Id.Name, cancellationToken),
                this.convertor());
        }

        public AsyncPageable<NetworkInterfaceOperations> ListAsync(CancellationToken cancellationToken = default)
        {
            var result = Operations.ListAsync(Id.Name, cancellationToken);
            return new PhWrappingAsyncPageable<Azure.ResourceManager.Network.Models.NetworkInterface, NetworkInterfaceOperations>(
                result,
                this.convertor());
        }

        public Pageable<ArmResourceOperations> ListByName(ArmSubstringFilter filter, int? top = null, CancellationToken cancellationToken = default)
        {
            ArmFilterCollection filters = new ArmFilterCollection(NetworkInterfaceData.ResourceType);
            filters.SubstringFilter = filter;
            return ResourceListOperations.ListAtContext<ArmResourceOperations, ArmResource>(ClientContext, Id, filters, top, cancellationToken);
        }

        public AsyncPageable<ArmResourceOperations> ListByNameAsync(ArmSubstringFilter filter, int? top = null, CancellationToken cancellationToken = default)
        {
            ArmFilterCollection filters = new ArmFilterCollection(NetworkInterfaceData.ResourceType);
            filters.SubstringFilter = filter;
            return ResourceListOperations.ListAtContextAsync<ArmResourceOperations, ArmResource>(ClientContext, Id, filters, top, cancellationToken);
        }
        private Func<Azure.ResourceManager.Network.Models.NetworkInterface, NetworkInterface> convertor()
        {
            return s => new NetworkInterface(ClientContext, new NetworkInterfaceData(s));
        }

        internal NetworkInterfacesOperations Operations => GetClient<NetworkManagementClient>((uri, cred) => new NetworkManagementClient(Id.Subscription, uri, cred)).NetworkInterfaces;
    }
}
