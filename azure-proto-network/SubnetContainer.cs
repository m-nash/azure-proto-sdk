using Azure.ResourceManager.Network;
using Azure.ResourceManager.Network.Models;
using azure_proto_core;
using System.Threading;
using System.Threading.Tasks;

namespace azure_proto_network
{
    public class SubnetContainer : ResourceContainerOperations<PhSubnet>
    {
        public SubnetContainer(ArmClientContext parent, ResourceIdentifier context) : base(parent, context)
        {
        }

        public SubnetContainer(ArmClientContext parent, azure_proto_core.Resource context) : base(parent, context)
        {
        }

        public SubnetContainer(OperationsBase parent, ResourceIdentifier context) : base(parent, context)
        {
        }

        public SubnetContainer(OperationsBase parent, azure_proto_core.Resource context) : base(parent, context)
        {
        }

        public override ResourceType ResourceType => "Microsoft.Network/virtualNetworks/subnets";


        internal SubnetsOperations Operations => GetClient<NetworkManagementClient>((uri, cred) => new NetworkManagementClient(Id.Subscription, uri, cred)).Subnets;

        public override ArmOperation<ResourceOperationsBase<PhSubnet>> Create(string name, PhSubnet resourceDetails, CancellationToken cancellationToken = default)
        {
            var operation = Operations.StartCreateOrUpdate(Id.ResourceGroup, Id.Name, name, resourceDetails.Model, cancellationToken);
            return new PhArmOperation<ResourceOperationsBase<PhSubnet>, Subnet>(
                operation.WaitForCompletionAsync().ConfigureAwait(false).GetAwaiter().GetResult(), 
                s => Subnet(new PhSubnet(s, Location.Default)));
        }

        public async override Task<ArmOperation<ResourceOperationsBase<PhSubnet>>> CreateAsync(string name, PhSubnet resourceDetails, CancellationToken cancellationToken = default)
        {
            var operation = await Operations.StartCreateOrUpdateAsync(Id.ResourceGroup, name, name, resourceDetails.Model, cancellationToken);
            return new PhArmOperation<ResourceOperationsBase<PhSubnet>, Subnet>(
                operation.WaitForCompletionAsync().ConfigureAwait(false).GetAwaiter().GetResult(),
                s => Subnet(new PhSubnet(s, Location.Default)));
        }

        public override ArmOperation<ResourceOperationsBase<PhSubnet>> StartCreate(string name, PhSubnet resourceDetails, CancellationToken cancellationToken = default)
        {
            return new PhArmOperation<ResourceOperationsBase<PhSubnet>, Subnet>(
                Operations.StartCreateOrUpdate(Id.ResourceGroup, Id.Name, name, resourceDetails.Model, cancellationToken),
                s => Subnet(new PhSubnet(s, Location.Default)));
        }

        public async override Task<ArmOperation<ResourceOperationsBase<PhSubnet>>> StartCreateAsync(string name, PhSubnet resourceDetails, CancellationToken cancellationToken = default)
        {
            return new PhArmOperation<ResourceOperationsBase<PhSubnet>, Subnet>(
                await Operations.StartCreateOrUpdateAsync(Id.ResourceGroup, Id.Name, name, resourceDetails.Model, cancellationToken),
                s => Subnet(new PhSubnet(s, Location.Default)));
        }

        internal SubnetOperations Subnet(PhSubnet subnet)
        {
            return new SubnetOperations(this, subnet);
        }
    }
}
