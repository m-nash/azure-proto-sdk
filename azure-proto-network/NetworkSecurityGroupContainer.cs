using Azure;
using Azure.ResourceManager.Network;
using Azure.ResourceManager.Network.Models;
using azure_proto_core;
using azure_proto_core.Resources;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System;
using azure_proto_core.Adapters;

namespace azure_proto_network
{
    public class NetworkSecurityGroupContainer : ResourceContainerOperations<XNetworkSecurityGroup, PhNetworkSecurityGroup>
    {
        public NetworkSecurityGroupContainer(ArmClientContext context, PhResourceGroup resourceGroup, ArmClientOptions clientOptions) : base(context, resourceGroup, clientOptions) { }

        internal NetworkSecurityGroupContainer(ArmClientContext context, ResourceIdentifier id, ArmClientOptions clientOptions) : base(context, id, clientOptions) { }

        public override ResourceType ResourceType => "Microsoft.Network/networkSecurityGroups";

        public override ArmResponse<XNetworkSecurityGroup> Create(string name, PhNetworkSecurityGroup resourceDetails, CancellationToken cancellationToken = default)
        {
            var operation = Operations.StartCreateOrUpdate(Id.ResourceGroup, name, resourceDetails.Model, cancellationToken);
            return new PhArmResponse<XNetworkSecurityGroup, NetworkSecurityGroup>(
                operation.WaitForCompletionAsync(cancellationToken).ConfigureAwait(false).GetAwaiter().GetResult(),
                n => new XNetworkSecurityGroup(ClientContext, new PhNetworkSecurityGroup(n), this.ClientOptions));
        }

        public async override Task<ArmResponse<XNetworkSecurityGroup>> CreateAsync(string name, PhNetworkSecurityGroup resourceDetails, CancellationToken cancellationToken = default)
        {
            var operation = await Operations.StartCreateOrUpdateAsync(Id.ResourceGroup, name, resourceDetails.Model, cancellationToken).ConfigureAwait(false);
            return new PhArmResponse<XNetworkSecurityGroup, NetworkSecurityGroup>(
                await operation.WaitForCompletionAsync(cancellationToken).ConfigureAwait(false),
                n => new XNetworkSecurityGroup(ClientContext, new PhNetworkSecurityGroup(n), this.ClientOptions));
        }

        public override ArmOperation<XNetworkSecurityGroup> StartCreate(string name, PhNetworkSecurityGroup resourceDetails, CancellationToken cancellationToken = default)
        {
            return new PhArmOperation<XNetworkSecurityGroup, NetworkSecurityGroup>(
                Operations.StartCreateOrUpdate(Id.ResourceGroup, name, resourceDetails.Model, cancellationToken),
                n => new XNetworkSecurityGroup(ClientContext, new PhNetworkSecurityGroup(n), this.ClientOptions));
        }

        public async override Task<ArmOperation<XNetworkSecurityGroup>> StartCreateAsync(string name, PhNetworkSecurityGroup resourceDetails, CancellationToken cancellationToken = default)
        {
            return new PhArmOperation<XNetworkSecurityGroup, NetworkSecurityGroup>(
                await Operations.StartCreateOrUpdateAsync(Id.ResourceGroup, name, resourceDetails.Model, cancellationToken).ConfigureAwait(false),
                n => new XNetworkSecurityGroup(ClientContext, new PhNetworkSecurityGroup(n), this.ClientOptions));
        }

        public ArmBuilder<XNetworkSecurityGroup, PhNetworkSecurityGroup> Construct(string nsgName, Location location = null, params int[] openPorts)
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

            return new ArmBuilder<XNetworkSecurityGroup, PhNetworkSecurityGroup>(this, new PhNetworkSecurityGroup(nsg));
        }

        public ArmBuilder<XNetworkSecurityGroup, PhNetworkSecurityGroup> Construct(string nsgName, params int[] openPorts)
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

            return new ArmBuilder<XNetworkSecurityGroup, PhNetworkSecurityGroup>(this, new PhNetworkSecurityGroup(nsg));
        }
        public Pageable<XNetworkSecurityGroup> List(CancellationToken cancellationToken = default)
        {
            return new PhWrappingPageable<NetworkSecurityGroup, XNetworkSecurityGroup>(
                Operations.List(Id.Name, cancellationToken),
                this.convertor());
        }
        public AsyncPageable<XNetworkSecurityGroup> ListAsync(CancellationToken cancellationToken = default)
        {
            return new PhWrappingAsyncPageable<NetworkSecurityGroup, XNetworkSecurityGroup>(
                Operations.ListAsync(Id.Name, cancellationToken),
                this.convertor());
        }

        public Pageable<ArmResourceOperations> ListByName(ArmSubstringFilter filter, int? top = null, CancellationToken cancellationToken = default)
        {
            ArmFilterCollection filters = new ArmFilterCollection(PhNetworkSecurityGroup.ResourceType);
            filters.SubstringFilter = filter;
            return ResourceListOperations.ListAtContext<ArmResourceOperations, ArmResource>(ClientContext, Id, filters, top, cancellationToken);
        }

        public AsyncPageable<ArmResourceOperations> ListByNameAsync(ArmSubstringFilter filter, int? top = null, CancellationToken cancellationToken = default)
        {
            ArmFilterCollection filters = new ArmFilterCollection(PhNetworkSecurityGroup.ResourceType);
            filters.SubstringFilter = filter;
            return ResourceListOperations.ListAtContextAsync<ArmResourceOperations, ArmResource>(ClientContext, Id, filters, top, cancellationToken);
        }
        private Func<NetworkSecurityGroup, XNetworkSecurityGroup> convertor()
        {
            return s => new XNetworkSecurityGroup(ClientContext, new PhNetworkSecurityGroup(s), this.ClientOptions);
        }


        internal NetworkSecurityGroupsOperations Operations => GetClient<NetworkManagementClient>((uri, cred) => new NetworkManagementClient(Id.Subscription, uri, cred,
                    ArmClientOptions.convert<NetworkManagementClientOptions>(this.ClientOptions))).NetworkSecurityGroups;
    }
}
