using Azure.ResourceManager.Network;
using Azure.ResourceManager.Network.Models;
using azure_proto_core;
using System.Threading;
using System.Threading.Tasks;

namespace azure_proto_network
{
    public class NetworkSecurityGroupContainer : ResourceContainerOperations<PhNetworkSecurityGroup>
    {
        public NetworkSecurityGroupContainer(ArmClientContext parent, ResourceIdentifier context) : base(parent, context)
        {
        }

        public NetworkSecurityGroupContainer(ArmClientContext parent, azure_proto_core.Resource context) : base(parent, context)
        {
        }
        public NetworkSecurityGroupContainer(OperationsBase parent, ResourceIdentifier context) : base(parent, context)
        {
        }

        public NetworkSecurityGroupContainer(OperationsBase parent, azure_proto_core.Resource context) : base(parent, context)
        {
        }

        public override ResourceType ResourceType => "Microsoft.Network/networkSecurityGroups";

        internal NetworkSecurityGroupsOperations Operations => GetClient<NetworkManagementClient>((uri, cred) => new NetworkManagementClient(Id.Subscription, uri, cred)).NetworkSecurityGroups;

        public override ArmOperation<ResourceOperationsBase<PhNetworkSecurityGroup>> Create(string name, PhNetworkSecurityGroup resourceDetails)
        {
            var operation = Operations.StartCreateOrUpdate(Id.ResourceGroup, name, resourceDetails.Model);
            return new PhArmOperation<ResourceOperationsBase<PhNetworkSecurityGroup>, NetworkSecurityGroup>(operation.WaitForCompletionAsync().ConfigureAwait(false).GetAwaiter().GetResult(), 
                n => new NetworkSecurityGroupOperations(this, new PhNetworkSecurityGroup(n)));
        }

        public async override Task<ArmOperation<ResourceOperationsBase<PhNetworkSecurityGroup>>> CreateAsync(string name, PhNetworkSecurityGroup resourceDetails, CancellationToken cancellationToken = default)
        {
            return new PhArmOperation<ResourceOperationsBase<PhNetworkSecurityGroup>, NetworkSecurityGroup>(
                await Operations.StartCreateOrUpdateAsync(Id.ResourceGroup, name, resourceDetails.Model, cancellationToken),
                n => new NetworkSecurityGroupOperations(this, new PhNetworkSecurityGroup(n)));
        }
    }
}
