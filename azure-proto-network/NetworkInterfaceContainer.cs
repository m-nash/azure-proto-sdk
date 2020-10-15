using Azure;
using Azure.ResourceManager.Network;
using Azure.ResourceManager.Network.Models;
using azure_proto_core;
using azure_proto_core.Resources;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace azure_proto_network
{
    public class NetworkInterfaceContainer : ResourceContainerOperations<NetworkInterfaceOperations, PhNetworkInterface>
    {
        public NetworkInterfaceContainer(ArmClientContext context, PhResourceGroup resourceGroup) : base(context, resourceGroup) { }

        public override ResourceType ResourceType => "Microsoft.Network/networkInterfaces";

        public override ArmResponse<NetworkInterfaceOperations> Create(string name, PhNetworkInterface resourceDetails, CancellationToken cancellationToken = default)
        {
            var operation = Operations.StartCreateOrUpdate(Id.ResourceGroup, name, resourceDetails, cancellationToken);
            return new PhArmResponse<NetworkInterfaceOperations, NetworkInterface>(
                operation.WaitForCompletionAsync(cancellationToken).ConfigureAwait(false).GetAwaiter().GetResult(),
                n => new NetworkInterfaceOperations(ClientContext, new PhNetworkInterface(n)));
        }

        public async override Task<ArmResponse<NetworkInterfaceOperations>> CreateAsync(string name, PhNetworkInterface resourceDetails, CancellationToken cancellationToken = default)
        {
            var operation = await Operations.StartCreateOrUpdateAsync(Id.ResourceGroup, name, resourceDetails, cancellationToken).ConfigureAwait(false);
            return new PhArmResponse<NetworkInterfaceOperations, NetworkInterface>(
                await operation.WaitForCompletionAsync(cancellationToken).ConfigureAwait(false), 
                n => new NetworkInterfaceOperations(ClientContext, new PhNetworkInterface(n)));
        }

        public override ArmOperation<NetworkInterfaceOperations> StartCreate(string name, PhNetworkInterface resourceDetails, CancellationToken cancellationToken = default)
        {
            return new PhArmOperation<NetworkInterfaceOperations, NetworkInterface>(
                Operations.StartCreateOrUpdate(Id.ResourceGroup, name, resourceDetails, cancellationToken),
                n => new NetworkInterfaceOperations(ClientContext, new PhNetworkInterface(n)));
        }

        public async override Task<ArmOperation<NetworkInterfaceOperations>> StartCreateAsync(string name, PhNetworkInterface resourceDetails, CancellationToken cancellationToken = default)
        {
            return new PhArmOperation<NetworkInterfaceOperations, NetworkInterface>(
                await Operations.StartCreateOrUpdateAsync(Id.ResourceGroup, name, resourceDetails, cancellationToken).ConfigureAwait(false),
                n => new NetworkInterfaceOperations(ClientContext, new PhNetworkInterface(n)));
        }

        public ArmBuilder<NetworkInterfaceOperations, PhNetworkInterface> Construct(PhPublicIPAddress ip, string subnetId, Location location = null)
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

            return new ArmBuilder<NetworkInterfaceOperations, PhNetworkInterface>(this, new PhNetworkInterface(nic));
        }

        public Pageable<NetworkInterfaceOperations> List(ArmSubstringFilter filter = null, int? top = null, CancellationToken cancellationToken = default)
        {
            ArmFilterCollection filters = new ArmFilterCollection(PhNetworkInterface.ResourceType);
            filters.SubstringFilter = filter;
            return ResourceListOperations.ListAtContext<NetworkInterfaceOperations, PhNetworkInterface>(ClientContext, Id, filters, top, cancellationToken);
        }

        public AsyncPageable<NetworkInterfaceOperations> ListAsync(ArmSubstringFilter filter = null, int? top = null, CancellationToken cancellationToken = default)
        {
            ArmFilterCollection filters = new ArmFilterCollection(PhNetworkInterface.ResourceType);
            filters.SubstringFilter = filter;
            return ResourceListOperations.ListAtContextAsync<NetworkInterfaceOperations, PhNetworkInterface>(ClientContext, Id, filters, top, cancellationToken);
        }

        internal NetworkInterfacesOperations Operations => GetClient<NetworkManagementClient>((uri, cred) => new NetworkManagementClient(Id.Subscription, uri, cred)).NetworkInterfaces;
    }
}
