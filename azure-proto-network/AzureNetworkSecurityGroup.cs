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
    /// <summary>
    /// An operations + Model class for NSGs
    /// TODO: How does the operation signature change for resources that support Etags?
    /// </summary>
    public class AzureNetworkSecurityGroup : AzureEntity<PhNetworkSecurityGroup>
    {
        string _name;
        class RuleIdEqualityComparer : IEqualityComparer<SecurityRule>
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

        // TODO: Can we do a better job of client sharing with the parent?
        private NetworkManagementClient Client => ClientFactory.Instance.GetNetworkClient(Id.Subscription);

        public AzureNetworkSecurityGroup(TrackedResource parent, PhNetworkSecurityGroup nsg, string name) : base(parent, nsg)
        {
            _name = name;
        }

        public override string Name => Id?.Name ?? _name;
        /// <summary>
        /// TODO: Make use of the entity tags on the resource - we may need to add to the generated management client
        /// TODO: Look for PATCH update methods in the swagger
        /// TODO: How to represent PATCH where the patch model has properties that are collections (replace/merge)?
        /// </summary>
        /// <param name="rules">The new set of network security rules</param>
        /// <returns>A network security group with the given set of rules merged with thsi one</returns>
        public AzureNetworkSecurityGroup UpdateRules(params SecurityRule[] rules )
        {
            foreach (var rule in rules)
            {
                // Note that this makes use of the 
                var matchingRule = Model.SecurityRules.FirstOrDefault(r => ResourceIdentifier.Equals(r.Id, rule.Id));
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

            this.Model = Client.NetworkSecurityGroups.StartCreateOrUpdate(Id.ResourceGroup, Name, Model).WaitForCompletionAsync().Result.Value;
            return this;
        }
    }
}
