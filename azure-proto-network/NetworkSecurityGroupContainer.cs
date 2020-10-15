using Azure;
using Azure.ResourceManager.Network;
using Azure.ResourceManager.Network.Models;
using azure_proto_core;
using azure_proto_core.Resources;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace azure_proto_network
{
    public class NetworkSecurityGroupContainer : ResourceContainerOperations<NetworkSecurityGroupOperations, PhNetworkSecurityGroup>
    {
        public NetworkSecurityGroupContainer(ArmClientContext context, PhResourceGroup resourceGroup) : base(context, resourceGroup) { }

        public override ResourceType ResourceType => "Microsoft.Network/networkSecurityGroups";

        public override ArmResponse<NetworkSecurityGroupOperations> Create(string name, PhNetworkSecurityGroup resourceDetails, CancellationToken cancellationToken = default)
        {
            var operation = Operations.StartCreateOrUpdate(Id.ResourceGroup, name, resourceDetails.Model, cancellationToken);
            return new PhArmResponse<NetworkSecurityGroupOperations, NetworkSecurityGroup>(
                operation.WaitForCompletionAsync(cancellationToken).ConfigureAwait(false).GetAwaiter().GetResult(), 
                n => new NetworkSecurityGroupOperations(ClientContext, new PhNetworkSecurityGroup(n)));
        }

        public async override Task<ArmResponse<NetworkSecurityGroupOperations>> CreateAsync(string name, PhNetworkSecurityGroup resourceDetails, CancellationToken cancellationToken = default)
        {
            var operation = await Operations.StartCreateOrUpdateAsync(Id.ResourceGroup, name, resourceDetails.Model, cancellationToken).ConfigureAwait(false);
            return new PhArmResponse<NetworkSecurityGroupOperations, NetworkSecurityGroup>(
                await operation.WaitForCompletionAsync(cancellationToken).ConfigureAwait(false),
                n => new NetworkSecurityGroupOperations(ClientContext, new PhNetworkSecurityGroup(n)));
        }

        public override ArmOperation<NetworkSecurityGroupOperations> StartCreate(string name, PhNetworkSecurityGroup resourceDetails, CancellationToken cancellationToken = default)
        {
            return new PhArmOperation<NetworkSecurityGroupOperations, NetworkSecurityGroup>(
                Operations.StartCreateOrUpdate(Id.ResourceGroup, name, resourceDetails.Model, cancellationToken),
                n => new NetworkSecurityGroupOperations(ClientContext, new PhNetworkSecurityGroup(n)));
        }

        public async override Task<ArmOperation<NetworkSecurityGroupOperations>> StartCreateAsync(string name, PhNetworkSecurityGroup resourceDetails, CancellationToken cancellationToken = default)
        {
            return new PhArmOperation<NetworkSecurityGroupOperations, NetworkSecurityGroup>(
                await Operations.StartCreateOrUpdateAsync(Id.ResourceGroup, name, resourceDetails.Model, cancellationToken).ConfigureAwait(false),
                n => new NetworkSecurityGroupOperations(ClientContext, new PhNetworkSecurityGroup(n)));
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

            return new ArmBuilder<NetworkSecurityGroupOperations, PhNetworkSecurityGroup>(this, new PhNetworkSecurityGroup(nsg));
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

            return new ArmBuilder<NetworkSecurityGroupOperations, PhNetworkSecurityGroup>(this, new PhNetworkSecurityGroup(nsg));
        }

        public Pageable<NetworkSecurityGroupOperations> List(ArmSubstringFilter filter = null, int? top = null, CancellationToken cancellationToken = default)
        {
            ArmFilterCollection filters = new ArmFilterCollection(PhNetworkSecurityGroup.ResourceType);
            filters.SubstringFilter = filter;
            return ResourceListOperations.ListAtContext<NetworkSecurityGroupOperations, PhNetworkSecurityGroup>(ClientContext, Id, filters, top, cancellationToken);
        }

        public AsyncPageable<NetworkSecurityGroupOperations> ListAsync(ArmSubstringFilter filter = null, int? top = null, CancellationToken cancellationToken = default)
        {
            ArmFilterCollection filters = new ArmFilterCollection(PhNetworkSecurityGroup.ResourceType);
            filters.SubstringFilter = filter;
            return ResourceListOperations.ListAtContextAsync<NetworkSecurityGroupOperations, PhNetworkSecurityGroup>(ClientContext, Id, filters, top, cancellationToken);
        }

        internal NetworkSecurityGroupsOperations Operations => GetClient<NetworkManagementClient>((uri, cred) => new NetworkManagementClient(Id.Subscription, uri, cred)).NetworkSecurityGroups;
    }
}
