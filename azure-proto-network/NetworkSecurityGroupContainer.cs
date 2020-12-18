using Azure;
using Azure.ResourceManager.Network;
using Azure.ResourceManager.Network.Models;
using Azure.ResourceManager.Core;
using Azure.ResourceManager.Core.Adapters;
using Azure.ResourceManager.Core.Resources;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System;

namespace azure_proto_network
{
    public class NetworkSecurityGroupContainer : ResourceContainerBase<NetworkSecurityGroup, NetworkSecurityGroupData>
    {
        internal NetworkSecurityGroupContainer(ArmResourceOperations genericOperations)
            : base(genericOperations.ClientOptions, genericOperations.Id)
        {
        }

        internal NetworkSecurityGroupContainer(AzureResourceManagerClientOptions options, ResourceGroupData resourceGroup)
            : base(options, resourceGroup)
        {
        }

        internal NetworkSecurityGroupContainer(AzureResourceManagerClientOptions options, ResourceIdentifier id)
            : base(options, id)
        {
        }

        public override ResourceType ResourceType => "Microsoft.Network/networkSecurityGroups";

        internal NetworkSecurityGroupsOperations Operations => GetClient<NetworkManagementClient>((uri, cred) => new NetworkManagementClient(Id.Subscription, uri, cred,
                    ClientOptions.Convert<NetworkManagementClientOptions>())).NetworkSecurityGroups;

        public override ArmResponse<NetworkSecurityGroup> Create(string name, NetworkSecurityGroupData resourceDetails, CancellationToken cancellationToken = default)
        {
            var operation = Operations.StartCreateOrUpdate(Id.ResourceGroup, name, resourceDetails.Model, cancellationToken);
            return new PhArmResponse<NetworkSecurityGroup, Azure.ResourceManager.Network.Models.NetworkSecurityGroup>(
                operation.WaitForCompletionAsync(cancellationToken).ConfigureAwait(false).GetAwaiter().GetResult(),
                n => new NetworkSecurityGroup(ClientOptions, new NetworkSecurityGroupData(n)));
        }

        public async override Task<ArmResponse<NetworkSecurityGroup>> CreateAsync(string name, NetworkSecurityGroupData resourceDetails, CancellationToken cancellationToken = default)
        {
            var operation = await Operations.StartCreateOrUpdateAsync(Id.ResourceGroup, name, resourceDetails.Model, cancellationToken).ConfigureAwait(false);
            return new PhArmResponse<NetworkSecurityGroup, Azure.ResourceManager.Network.Models.NetworkSecurityGroup>(
                await operation.WaitForCompletionAsync(cancellationToken).ConfigureAwait(false),
                n => new NetworkSecurityGroup(ClientOptions, new NetworkSecurityGroupData(n)));
        }

        public override ArmOperation<NetworkSecurityGroup> StartCreate(string name, NetworkSecurityGroupData resourceDetails, CancellationToken cancellationToken = default)
        {
            return new PhArmOperation<NetworkSecurityGroup, Azure.ResourceManager.Network.Models.NetworkSecurityGroup>(
                Operations.StartCreateOrUpdate(Id.ResourceGroup, name, resourceDetails.Model, cancellationToken),
                n => new NetworkSecurityGroup(ClientOptions, new NetworkSecurityGroupData(n)));
        }

        public async override Task<ArmOperation<NetworkSecurityGroup>> StartCreateAsync(string name, NetworkSecurityGroupData resourceDetails, CancellationToken cancellationToken = default)
        {
            return new PhArmOperation<NetworkSecurityGroup, Azure.ResourceManager.Network.Models.NetworkSecurityGroup>(
                await Operations.StartCreateOrUpdateAsync(Id.ResourceGroup, name, resourceDetails.Model, cancellationToken).ConfigureAwait(false),
                n => new NetworkSecurityGroup(ClientOptions, new NetworkSecurityGroupData(n)));
        }

        public ArmBuilder<NetworkSecurityGroup, NetworkSecurityGroupData> Construct(string nsgName, Location location = null, params int[] openPorts)
        {
            var nsg = new Azure.ResourceManager.Network.Models.NetworkSecurityGroup { Location = location ?? DefaultLocation };
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

            return new ArmBuilder<NetworkSecurityGroup, NetworkSecurityGroupData>(this, new NetworkSecurityGroupData(nsg));
        }

        public ArmBuilder<NetworkSecurityGroup, NetworkSecurityGroupData> Construct(string nsgName, params int[] openPorts)
        {
            var nsg = new Azure.ResourceManager.Network.Models.NetworkSecurityGroup { Location = DefaultLocation };
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

            return new ArmBuilder<NetworkSecurityGroup, NetworkSecurityGroupData>(this, new NetworkSecurityGroupData(nsg));
        }

        public Pageable<NetworkSecurityGroup> List(CancellationToken cancellationToken = default)
        {
            return new PhWrappingPageable<Azure.ResourceManager.Network.Models.NetworkSecurityGroup, NetworkSecurityGroup>(
                Operations.List(Id.Name, cancellationToken),
                this.convertor());
        }

        public AsyncPageable<NetworkSecurityGroup> ListAsync(CancellationToken cancellationToken = default)
        {
            return new PhWrappingAsyncPageable<Azure.ResourceManager.Network.Models.NetworkSecurityGroup, NetworkSecurityGroup>(
                Operations.ListAsync(Id.Name, cancellationToken),
                this.convertor());
        }

        public Pageable<ArmResourceOperations> ListByName(ArmSubstringFilter filter, int? top = null, CancellationToken cancellationToken = default)
        {
            ArmFilterCollection filters = new ArmFilterCollection(NetworkSecurityGroupData.ResourceType);
            filters.SubstringFilter = filter;
            return ResourceListOperations.ListAtContext<ArmResourceOperations, ArmResource>(ClientOptions, Id, filters, top, cancellationToken);
        }

        public AsyncPageable<ArmResourceOperations> ListByNameAsync(ArmSubstringFilter filter, int? top = null, CancellationToken cancellationToken = default)
        {
            ArmFilterCollection filters = new ArmFilterCollection(NetworkSecurityGroupData.ResourceType);
            filters.SubstringFilter = filter;
            return ResourceListOperations.ListAtContextAsync<ArmResourceOperations, ArmResource>(ClientOptions, Id, filters, top, cancellationToken);
        }

        public Pageable<NetworkSecurityGroup> ListByNameExpanded(ArmSubstringFilter filter, int? top = null, CancellationToken cancellationToken = default)
        {
            var results = ListByName(filter, top, cancellationToken);
            return new PhWrappingPageable<ArmResourceOperations, NetworkSecurityGroup>(results, s => new NetworkSecurityGroupOperations(s).Get().Value);
        }

        public AsyncPageable<NetworkSecurityGroup> ListByNameExpandedAsync(ArmSubstringFilter filter, int? top = null, CancellationToken cancellationToken = default)
        {
            var results = ListByNameAsync(filter, top, cancellationToken);
            return new PhWrappingAsyncPageable<ArmResourceOperations, NetworkSecurityGroup>(results, s => new NetworkSecurityGroupOperations(s).Get().Value);
        }

        private Func<Azure.ResourceManager.Network.Models.NetworkSecurityGroup, NetworkSecurityGroup> convertor()
        {
            return s => new NetworkSecurityGroup(ClientOptions, new NetworkSecurityGroupData(s));
        }
    }
}
