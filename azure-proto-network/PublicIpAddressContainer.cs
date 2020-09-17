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

        public override ArmResponse<ResourceOperationsBase<PhPublicIPAddress>> Create(string name, PhPublicIPAddress resourceDetails, CancellationToken cancellationToken = default)
        {
            var operation = Operations.StartCreateOrUpdate(Id.ResourceGroup, name, resourceDetails, cancellationToken);
            return new PhArmResponse<ResourceOperationsBase<PhPublicIPAddress>, PublicIPAddress>(
                operation.WaitForCompletionAsync(cancellationToken).ConfigureAwait(false).GetAwaiter().GetResult(), 
                n => new PublicIpAddressOperations(this, new PhPublicIPAddress(n)));
        }

        public async override Task<ArmResponse<ResourceOperationsBase<PhPublicIPAddress>>> CreateAsync(string name, PhPublicIPAddress resourceDetails, CancellationToken cancellationToken = default)
        {
            var operation = await Operations.StartCreateOrUpdateAsync(Id.ResourceGroup, name, resourceDetails, cancellationToken).ConfigureAwait(false);
            return new PhArmResponse<ResourceOperationsBase<PhPublicIPAddress>, PublicIPAddress>(
                await operation.WaitForCompletionAsync(cancellationToken).ConfigureAwait(false),
                n => new PublicIpAddressOperations(this, new PhPublicIPAddress(n)));
        }

        public override ArmOperation<ResourceOperationsBase<PhPublicIPAddress>> StartCreate(string name, PhPublicIPAddress resourceDetails, CancellationToken cancellationToken = default)
        {
            return new PhArmOperation<ResourceOperationsBase<PhPublicIPAddress>, PublicIPAddress>(
                Operations.StartCreateOrUpdate(Id.ResourceGroup, name, resourceDetails, cancellationToken),
                n => new PublicIpAddressOperations(this, new PhPublicIPAddress(n)));
        }

        public async override Task<ArmOperation<ResourceOperationsBase<PhPublicIPAddress>>> StartCreateAsync(string name, PhPublicIPAddress resourceDetails, CancellationToken cancellationToken = default)
        {
            return new PhArmOperation<ResourceOperationsBase<PhPublicIPAddress>, PublicIPAddress>(
                await Operations.StartCreateOrUpdateAsync(Id.ResourceGroup, name, resourceDetails, cancellationToken).ConfigureAwait(false),
                n => new PublicIpAddressOperations(this, new PhPublicIPAddress(n)));
        }

        internal PublicIPAddressesOperations Operations => GetClient<NetworkManagementClient>((uri, cred) => new NetworkManagementClient(Id.Subscription, uri, cred)).PublicIPAddresses;
    }
}
