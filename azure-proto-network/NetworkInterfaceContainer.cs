using Azure.ResourceManager.Network;
using Azure.ResourceManager.Network.Models;
using azure_proto_core;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace azure_proto_network
{
    public class NetworkInterfaceContainer : ResourceContainerOperations<NetworkInterfaceOperations, PhNetworkInterface>
    {
        public NetworkInterfaceContainer(ArmClientContext parent, ResourceIdentifier context) : base(parent, context)
        {
        }

        public NetworkInterfaceContainer(ArmClientContext parent, TrackedResource context) : base(parent, context)
        {
        }

        public NetworkInterfaceContainer(OperationsBase parent, ResourceIdentifier context) : base(parent, context)
        {
        }

        public NetworkInterfaceContainer(OperationsBase parent, TrackedResource context) : base(parent, context)
        {
        }

        public override ResourceType ResourceType => "Microsoft.Network/networkInterfaces";

        public override ArmResponse<NetworkInterfaceOperations> Create(string name, PhNetworkInterface resourceDetails, CancellationToken cancellationToken = default)
        {
            var operation = Operations.StartCreateOrUpdate(Id.ResourceGroup, name, resourceDetails, cancellationToken);
            return new PhArmResponse<NetworkInterfaceOperations, NetworkInterface>(
                operation.WaitForCompletionAsync(cancellationToken).ConfigureAwait(false).GetAwaiter().GetResult(),
                n => new NetworkInterfaceOperations(this, new PhNetworkInterface(n)));
        }

        public async override Task<ArmResponse<NetworkInterfaceOperations>> CreateAsync(string name, PhNetworkInterface resourceDetails, CancellationToken cancellationToken = default)
        {
            var operation = await Operations.StartCreateOrUpdateAsync(Id.ResourceGroup, name, resourceDetails, cancellationToken).ConfigureAwait(false);
            return new PhArmResponse<NetworkInterfaceOperations, NetworkInterface>(
                await operation.WaitForCompletionAsync(cancellationToken).ConfigureAwait(false), 
                n => new NetworkInterfaceOperations(this, new PhNetworkInterface(n)));
        }

        public override ArmOperation<NetworkInterfaceOperations> StartCreate(string name, PhNetworkInterface resourceDetails, CancellationToken cancellationToken = default)
        {
            return new PhArmOperation<NetworkInterfaceOperations, NetworkInterface>(
                Operations.StartCreateOrUpdate(Id.ResourceGroup, name, resourceDetails, cancellationToken),
                n => new NetworkInterfaceOperations(this, new PhNetworkInterface(n)));
        }

        public async override Task<ArmOperation<NetworkInterfaceOperations>> StartCreateAsync(string name, PhNetworkInterface resourceDetails, CancellationToken cancellationToken = default)
        {
            return new PhArmOperation<NetworkInterfaceOperations, NetworkInterface>(
                await Operations.StartCreateOrUpdateAsync(Id.ResourceGroup, name, resourceDetails, cancellationToken).ConfigureAwait(false),
                n => new NetworkInterfaceOperations(this, new PhNetworkInterface(n)));
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

            return new ArmBuilder<NetworkInterfaceOperations, PhNetworkInterface>(new NetworkInterfaceContainer(this, Id), new PhNetworkInterface(nic));
        }

        internal NetworkInterfacesOperations Operations => GetClient<NetworkManagementClient>((uri, cred) => new NetworkManagementClient(Id.Subscription, uri, cred)).NetworkInterfaces;
    }
}
