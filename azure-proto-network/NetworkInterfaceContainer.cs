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
    public class NetworkInterfaceContainer : ResourceContainerOperations<XNetworkInterface, PhNetworkInterface>
    {
        public NetworkInterfaceContainer(ArmClientContext context, PhResourceGroup resourceGroup) : base(context, resourceGroup) { }

        internal NetworkInterfaceContainer(ArmClientContext context, ResourceIdentifier id) : base(context, id) { }

        public override ResourceType ResourceType => "Microsoft.Network/networkInterfaces";

        public override ArmResponse<XNetworkInterface> Create(string name, PhNetworkInterface resourceDetails, CancellationToken cancellationToken = default)
        {
            var operation = Operations.StartCreateOrUpdate(Id.ResourceGroup, name, resourceDetails, cancellationToken);
            return new PhArmResponse<XNetworkInterface, NetworkInterface>(
                operation.WaitForCompletionAsync(cancellationToken).ConfigureAwait(false).GetAwaiter().GetResult(),
                n => new XNetworkInterface(ClientContext, new PhNetworkInterface(n)));
        }

        public async override Task<ArmResponse<XNetworkInterface>> CreateAsync(string name, PhNetworkInterface resourceDetails, CancellationToken cancellationToken = default)
        {
            var operation = await Operations.StartCreateOrUpdateAsync(Id.ResourceGroup, name, resourceDetails, cancellationToken).ConfigureAwait(false);
            return new PhArmResponse<XNetworkInterface, NetworkInterface>(
                await operation.WaitForCompletionAsync(cancellationToken).ConfigureAwait(false),
                n => new XNetworkInterface(ClientContext, new PhNetworkInterface(n)));
        }

        public override ArmOperation<XNetworkInterface> StartCreate(string name, PhNetworkInterface resourceDetails, CancellationToken cancellationToken = default)
        {
            return new PhArmOperation<XNetworkInterface, NetworkInterface>(
                Operations.StartCreateOrUpdate(Id.ResourceGroup, name, resourceDetails, cancellationToken),
                n => new XNetworkInterface(ClientContext, new PhNetworkInterface(n)));
        }

        public async override Task<ArmOperation<XNetworkInterface>> StartCreateAsync(string name, PhNetworkInterface resourceDetails, CancellationToken cancellationToken = default)
        {
            return new PhArmOperation<XNetworkInterface, NetworkInterface>(
                await Operations.StartCreateOrUpdateAsync(Id.ResourceGroup, name, resourceDetails, cancellationToken).ConfigureAwait(false),
                n => new XNetworkInterface(ClientContext, new PhNetworkInterface(n)));
        }

        public ArmBuilder<XNetworkInterface, PhNetworkInterface> Construct(PhPublicIPAddress ip, string subnetId, Location location = null)
        {
            var nic = new NetworkInterface()
            {
                Location = location ?? DefaultLocation,
                IpConfigurations = new List<NetworkInterfaceIPConfiguration>()
                {
                    new NetworkInterfaceIPConfiguration()
                    {
                        Name = "Primary",
                        Primary = true,
                        Subnet = new Subnet() { Id = subnetId },
                        PrivateIPAllocationMethod = IPAllocationMethod.Dynamic,
                        PublicIPAddress = new PublicIPAddress() { Id = ip.Id }
                    }
                }
            };

            return new ArmBuilder<XNetworkInterface, PhNetworkInterface>(this, new PhNetworkInterface(nic));
        }

        public Pageable<NetworkInterfaceOperations> List(CancellationToken cancellationToken = default)
        {
            return new PhWrappingPageable<NetworkInterface, NetworkInterfaceOperations>(
                Operations.List(Id.Name, cancellationToken),
                this.convertor());
        }

        public AsyncPageable<NetworkInterfaceOperations> ListAsync(CancellationToken cancellationToken = default)
        {
            var result = Operations.ListAsync(Id.Name, cancellationToken);
            return new PhWrappingAsyncPageable<NetworkInterface, NetworkInterfaceOperations>(
                result,
                this.convertor());
        }

        public Pageable<ArmResourceOperations> ListByName(ArmSubstringFilter filter, int? top = null, CancellationToken cancellationToken = default)
        {
            ArmFilterCollection filters = new ArmFilterCollection(PhNetworkInterface.ResourceType);
            filters.SubstringFilter = filter;
            return ResourceListOperations.ListAtContext<ArmResourceOperations, ArmResource>(ClientContext, Id, filters, top, cancellationToken);
        }

        public AsyncPageable<ArmResourceOperations> ListByNameAsync(ArmSubstringFilter filter, int? top = null, CancellationToken cancellationToken = default)
        {
            ArmFilterCollection filters = new ArmFilterCollection(PhNetworkInterface.ResourceType);
            filters.SubstringFilter = filter;
            return ResourceListOperations.ListAtContextAsync<ArmResourceOperations, ArmResource>(ClientContext, Id, filters, top, cancellationToken);
        }
        private Func<NetworkInterface, XNetworkInterface> convertor()
        {
            return s => new XNetworkInterface(ClientContext, new PhNetworkInterface(s));
        }

        internal NetworkInterfacesOperations Operations => GetClient<NetworkManagementClient>((uri, cred) => new NetworkManagementClient(Id.Subscription, uri, cred)).NetworkInterfaces;
    }
}
