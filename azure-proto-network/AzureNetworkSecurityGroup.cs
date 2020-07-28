using Azure.ResourceManager.Network;
using Azure.ResourceManager.Network.Models;
using azure_proto_core;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;

namespace azure_proto_network
{
    public class AzureNetworkSecurityGroup : AzureEntity<PhNetworkSecurityGroup>
    {
        public class RuleIdEqualityComparer : IEqualityComparer<SecurityRule>
        {
            public bool Equals([AllowNull] SecurityRule x, [AllowNull] SecurityRule y)
            {
                return ResourceIdentifier.Equals(x?.Id, y?.Id);
            }

            public int GetHashCode([DisallowNull] SecurityRule obj)
            {
                return string.GetHashCode(obj.Id, StringComparison.InvariantCultureIgnoreCase);
            }
        }

        private NetworkManagementClient Client => ClientFactory.Instance.GetNetworkClient(Id.Subscription);

        public AzureNetworkSecurityGroup(TrackedResource parent, PhNetworkSecurityGroup nsg) : base(parent, nsg)
        {
        }

        /// <summary>
        /// TODO: Make use of the entity tags on the resource - we may need to add to the generated management client
        /// </summary>
        /// <param name="rules">The new set of network security rules</param>
        /// <returns></returns>
        public AzureNetworkSecurityGroup Update(params SecurityRule[] rules )
        {
            foreach (var rule in rules)
            {
                var matchingRule = Model.SecurityRules.FirstOrDefault(r => r.Id == rule.Id);
                if (matchingRule != null)
                {
                    matchingRule.Access = rule.Access;
                    matchingRule.Description = rule.Description;
                    matchingRule.DestinationAddressPrefix = rule.DestinationAddressPrefix;
                    matchingRule.DestinationAddressPrefixes = rule.DestinationAddressPrefixes;
                    matchingRule.DestinationPortRange = rule.DestinationPortRange;
                    matchingRule.DestinationPortRanges = rule.DestinationPortRanges;
                    matchingRule.Direction = rule.Direction;
                    matchingRule.Priority = rule.Priority;
                    matchingRule.Protocol = rule.Protocol;
                    matchingRule.SourceAddressPrefix = rule.SourceAddressPrefix;
                    matchingRule.SourceAddressPrefixes = rule.SourceAddressPrefixes;
                    matchingRule.SourcePortRange = rule.SourcePortRange;
                    matchingRule.SourcePortRanges = rule.SourcePortRanges;
                }
                else
                {
                    Model.SecurityRules.Add(rule);
                }
            }

            Client.NetworkSecurityGroups.StartCreateOrUpdate(Id.ResourceGroup, Name, Model);
            return this;
        }
    }
}
