using Azure.ResourceManager.Network;
using Azure.ResourceManager.Network.Models;
using azure_proto_core;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace azure_proto_network
{
    public class NetworkSecurityGroupContainer : ResourceContainerOperations<NetworkSecurityGroupOperations, PhNetworkSecurityGroup>
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

        public override ArmResponse<NetworkSecurityGroupOperations> Create(string name, PhNetworkSecurityGroup resourceDetails, CancellationToken cancellationToken = default)
        {
            var operation = Operations.StartCreateOrUpdate(Id.ResourceGroup, name, resourceDetails.Model, cancellationToken);
            return new PhArmResponse<NetworkSecurityGroupOperations, NetworkSecurityGroup>(
                operation.WaitForCompletionAsync(cancellationToken).ConfigureAwait(false).GetAwaiter().GetResult(), 
                n => new NetworkSecurityGroupOperations(this, new PhNetworkSecurityGroup(n)));
        }

        public async override Task<ArmResponse<NetworkSecurityGroupOperations>> CreateAsync(string name, PhNetworkSecurityGroup resourceDetails, CancellationToken cancellationToken = default)
        {
            var operation = await Operations.StartCreateOrUpdateAsync(Id.ResourceGroup, name, resourceDetails.Model, cancellationToken).ConfigureAwait(false);
            return new PhArmResponse<NetworkSecurityGroupOperations, NetworkSecurityGroup>(
                await operation.WaitForCompletionAsync(cancellationToken).ConfigureAwait(false),
                n => new NetworkSecurityGroupOperations(this, new PhNetworkSecurityGroup(n)));
        }

        public override ArmOperation<NetworkSecurityGroupOperations> StartCreate(string name, PhNetworkSecurityGroup resourceDetails, CancellationToken cancellationToken = default)
        {
            return new PhArmOperation<NetworkSecurityGroupOperations, NetworkSecurityGroup>(
                Operations.StartCreateOrUpdate(Id.ResourceGroup, name, resourceDetails.Model, cancellationToken),
                n => new NetworkSecurityGroupOperations(this, new PhNetworkSecurityGroup(n)));
        }

        public async override Task<ArmOperation<NetworkSecurityGroupOperations>> StartCreateAsync(string name, PhNetworkSecurityGroup resourceDetails, CancellationToken cancellationToken = default)
        {
            return new PhArmOperation<NetworkSecurityGroupOperations, NetworkSecurityGroup>(
                await Operations.StartCreateOrUpdateAsync(Id.ResourceGroup, name, resourceDetails.Model, cancellationToken).ConfigureAwait(false),
                n => new NetworkSecurityGroupOperations(this, new PhNetworkSecurityGroup(n)));
        }

        public ArmBuilder<NetworkSecurityGroupOperations, PhNetworkSecurityGroup> Construct(string nsgName, Location location = null, params int[] openPorts)
        {
            var nsg = new NetworkSecurityGroup { Location = location ?? DefaultLocation };
            var index = 0;
            nsg.SecurityRules = openPorts.Select(openPort => new SecurityRule
            {
                Name = $"Port{openPort}",
                Priority = 1000 + (++index),
                Protocol = SecurityRuleProtocol.Tcp,
                Access = SecurityRuleAccess.Allow,
                Direction = SecurityRuleDirection.Inbound,
                SourcePortRange = "*",
                SourceAddressPrefix = "*",
                DestinationPortRange = $"{openPort}",
                DestinationAddressPrefix = "*",
                Description = $"Port_{openPort}"
            }).ToList();

            return new ArmBuilder<NetworkSecurityGroupOperations, PhNetworkSecurityGroup>(new NetworkSecurityGroupContainer(this, Id), new PhNetworkSecurityGroup(nsg));
        }

        public ArmBuilder<NetworkSecurityGroupOperations, PhNetworkSecurityGroup> Construct(string nsgName, params int[] openPorts)
        {
            var nsg = new NetworkSecurityGroup { Location = DefaultLocation };
            var index = 0;
            nsg.SecurityRules = openPorts.Select(openPort => new SecurityRule
            {
                Name = $"Port{openPort}",
                Priority = 1000 + (++index),
                Protocol = SecurityRuleProtocol.Tcp,
                Access = SecurityRuleAccess.Allow,
                Direction = SecurityRuleDirection.Inbound,
                SourcePortRange = "*",
                SourceAddressPrefix = "*",
                DestinationPortRange = $"{openPort}",
                DestinationAddressPrefix = "*",
                Description = $"Port_{openPort}"
            }).ToList();

            return new ArmBuilder<NetworkSecurityGroupOperations, PhNetworkSecurityGroup>(new NetworkSecurityGroupContainer(this, Id), new PhNetworkSecurityGroup(nsg));
        }

        internal NetworkSecurityGroupsOperations Operations => GetClient<NetworkManagementClient>((uri, cred) => new NetworkManagementClient(Id.Subscription, uri, cred)).NetworkSecurityGroups;
    }
}
