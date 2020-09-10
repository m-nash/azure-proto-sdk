using Azure.ResourceManager.Network;
using azure_proto_core;
using System.Threading;
using System.Threading.Tasks;

namespace azure_proto_network
{
    public class NetworkInterfaceContainer : ResourceContainerOperations<PhNetworkInterface>
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

        public override ArmOperation<ResourceOperationsBase<PhNetworkInterface>> Create(string name, PhNetworkInterface resourceDetails)
        {
            var operation = Operations.StartCreateOrUpdate(Id.ResourceGroup, Id.Name, resourceDetails);
            return new PhArmOperation<ResourceOperationsBase<PhNetworkInterface>, Azure.ResourceManager.Network.Models.NetworkInterface>(
                operation.WaitForCompletionAsync().ConfigureAwait(false).GetAwaiter().GetResult(),
                n => new NetworkInterfaceOperations(this, new PhNetworkInterface(n)));
        }

        public async override Task<ArmOperation<ResourceOperationsBase<PhNetworkInterface>>> CreateAsync(string name, PhNetworkInterface resourceDetails, CancellationToken cancellationToken = default)
        {
            return new PhArmOperation<ResourceOperationsBase<PhNetworkInterface>, Azure.ResourceManager.Network.Models.NetworkInterface>(
                await Operations.StartCreateOrUpdateAsync(Id.ResourceGroup, Id.Name, resourceDetails, cancellationToken), 
                n => new NetworkInterfaceOperations(this, new PhNetworkInterface(n)));
        }

        internal NetworkInterfacesOperations Operations => GetClient<NetworkManagementClient>((uri, cred) => new NetworkManagementClient(Id.Subscription, uri, cred)).NetworkInterfaces;
    }
}
