using Azure.ResourceManager.Network;
using Azure.ResourceManager.Network.Models;
using azure_proto_core;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace azure_proto_network
{
    public class NetworkSecurityGroupCollection : AzureCollection<AzureNetworkSecurityGroup>
    {
        public NetworkSecurityGroupCollection(TrackedResource parent) : base(parent) { }

        private NetworkManagementClient Client => ClientFactory.Instance.GetNetworkClient(Parent.Id.Subscription);

        protected override AzureNetworkSecurityGroup Get(string name)
        {
            var model = Client.NetworkSecurityGroups.Get(Parent.Id.ResourceGroup, name).Value;
            return new AzureNetworkSecurityGroup(Parent, model, name);
        }

        /// <summary>
        /// Make this a yield return, so enumeration can be short-circuited
        /// TODO: We have separate methods for List by resource group and by subscription for this item, how to represent?
        /// </summary>
        /// <returns></returns>
        protected override IEnumerable<AzureNetworkSecurityGroup> GetItems()
        {
            foreach (var nsg in Client.NetworkSecurityGroups.List(Parent.Id.ResourceGroup))
            {
                yield return new AzureNetworkSecurityGroup(Parent, nsg, nsg?.Name);
            }
        }

        public AzureNetworkSecurityGroup CreateOrUpdateNsgs(AzureNetworkSecurityGroup nsg)
        {
            var result = Client.NetworkSecurityGroups.StartCreateOrUpdate(Parent.Id.ResourceGroup, nsg.Name, nsg.Model).WaitForCompletionAsync().Result.Value;
            return new AzureNetworkSecurityGroup(Parent, result, result?.Name);
        }

        public async Task<AzureNetworkSecurityGroup> CreateOrUpdateNsgsAsync(AzureNetworkSecurityGroup nsg, CancellationToken cancellationToken = default)
        {
            var result = await Client.NetworkSecurityGroups.StartCreateOrUpdateAsync(Parent.Id.ResourceGroup, nsg.Name, nsg.Model, cancellationToken);
            return new AzureNetworkSecurityGroup(Parent, result.Value, result?.Value.Name);
        }

        /// <summary>
        /// Create an NSG with the given open TCP ports
        /// </summary>
        /// <param name="openPorts">The set of TCP ports to open</param>
        /// <returns>An NSG, with the given TCP ports open</returns>
        public AzureNetworkSecurityGroup ConstructNsg(string nsgName, params int[] openPorts)
        {
            var nsg = new NetworkSecurityGroup { Location = Parent.Location };
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
            var result = new AzureNetworkSecurityGroup(Parent, nsg, nsgName);

            return result;
        }
    }
}
