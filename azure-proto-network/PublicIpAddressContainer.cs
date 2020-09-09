using Azure.ResourceManager.Network;
using Azure.ResourceManager.Network.Models;
using azure_proto_core;
using System.Threading;
using System.Threading.Tasks;

namespace azure_proto_network
{
    public class PublicIpAddressContainer : ResourceContainerOperations<PhPublicIPAddress>
    {
        public PublicIpAddressContainer(ArmClientContext parent, ResourceIdentifier context) : base(parent, context)
        {
        }

        public PublicIpAddressContainer(ArmClientContext parent, azure_proto_core.Resource context) : base(parent, context)
        {
        }

        public PublicIpAddressContainer(OperationsBase parent, ResourceIdentifier context) : base(parent, context)
        {
        }

        public PublicIpAddressContainer(OperationsBase parent, azure_proto_core.Resource context) : base(parent, context)
        {
        }

        public override ResourceType ResourceType => "Microsoft.Network/publicIpAddresses";

        public override ArmOperation<ResourceOperationsBase<PhPublicIPAddress>> Create(string name, PhPublicIPAddress resourceDetails)
        {
            return new PhArmOperation<ResourceOperationsBase<PhPublicIPAddress>, PublicIPAddress>(Operations.StartCreateOrUpdate(Context.ResourceGroup, name, resourceDetails), 
                n => new PublicIpAddressOperations(this, new PhPublicIPAddress(n)));
        }

        public async override Task<ArmOperation<ResourceOperationsBase<PhPublicIPAddress>>> CreateAsync(string name, PhPublicIPAddress resourceDetails, CancellationToken cancellationToken = default)
        {
            return new PhArmOperation<ResourceOperationsBase<PhPublicIPAddress>, PublicIPAddress>(
                await Operations.StartCreateOrUpdateAsync(Context.ResourceGroup, name, resourceDetails, cancellationToken),
                n => new PublicIpAddressOperations(this, new PhPublicIPAddress(n)));
        }

        internal PublicIPAddressesOperations Operations => GetClient<NetworkManagementClient>((uri, cred) => new NetworkManagementClient(Context.Subscription, uri, cred)).PublicIPAddresses;
    }
}
