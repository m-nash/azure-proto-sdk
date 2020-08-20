using Azure;
using Azure.ResourceManager.Network;
using Azure.ResourceManager.Network.Models;
using azure_proto_core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace azure_proto_network
{
    public class NsgContainer : ResourceContainerOperations<PhNetworkSecurityGroup, Operation<PhNetworkSecurityGroup>>
    {
        public NsgContainer(ArmOperations parent, ResourceIdentifier context) : base(parent, context)
        {
        }

        public NsgContainer(ArmOperations parent, azure_proto_core.Resource context) : base(parent, context)
        {
        }

        protected override ResourceType ResourceType => "Microsoft.Network/networkSecurityGroups";

        internal NetworkSecurityGroupsOperations Operations => GetClient<NetworkManagementClient>((uri, cred) => new NetworkManagementClient(Context.Subscription, uri, cred)).NetworkSecurityGroups;

        public override Operation<PhNetworkSecurityGroup> Create(string name, PhNetworkSecurityGroup resourceDetails)
        {
            return new PhValueOperation<PhNetworkSecurityGroup, NetworkSecurityGroup>(Operations.StartCreateOrUpdate(Context.ResourceGroup, Context.Name, resourceDetails.Model), n => new PhNetworkSecurityGroup(n));
        }

        public async override Task<Operation<PhNetworkSecurityGroup>> CreateAsync(string name, PhNetworkSecurityGroup resourceDetails, CancellationToken cancellationToken = default)
        {
            return new PhValueOperation<PhNetworkSecurityGroup, NetworkSecurityGroup>(await Operations.StartCreateOrUpdateAsync(Context.ResourceGroup, Context.Name, resourceDetails.Model, cancellationToken), n => new PhNetworkSecurityGroup(n));
        }

        /// <summary>
        /// Create an NSG with the given open TCP ports
        /// </summary>
        /// <param name="openPorts">The set of TCP ports to open</param>
        /// <returns>An NSG, with the given TCP ports open</returns>
        public PhNetworkSecurityGroup ConstructNsg(string nsgName, Location location = null, params int[] openPorts)
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

            return new PhNetworkSecurityGroup(nsg); 
        }

        public PhNetworkSecurityGroup ConstructNsg(string nsgName, params int[] openPorts)
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

            return new PhNetworkSecurityGroup(nsg);
        }

    }
}
