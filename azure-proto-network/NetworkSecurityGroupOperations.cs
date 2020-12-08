﻿using Azure;
using Azure.ResourceManager.Network;
using Azure.ResourceManager.Network.Models;
using azure_proto_core;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace azure_proto_network
{
    /// <summary>
    /// An operations + Model class for NSGs
    /// TODO: How does the operation signature change for resources that support Etags?
    /// </summary>
    public class NetworkSecurityGroupOperations : ResourceOperationsBase<NetworkSecurityGroup, NetworkSecurityGroupData>, ITaggable<NetworkSecurityGroup, NetworkSecurityGroupData>, IDeletableResource<NetworkSecurityGroup, NetworkSecurityGroupData>
    {
        class RuleIdEqualityComparer : IEqualityComparer<SecurityRule>
        {
            public bool Equals(SecurityRule x, SecurityRule y)
            {
                return ResourceIdentifier.Equals(x?.Id, y?.Id);
            }

            public int GetHashCode(SecurityRule obj)
            {
                return obj.Id.ToLower().GetHashCode();
            }
        }

        public NetworkSecurityGroupOperations(ArmClientContext parent, ResourceIdentifier context) : base(parent, context) { }

        /// <summary>
        /// TODO: GENERATOR Make use of the entity tags on the resource - we may need to add to the generated management client
        /// </summary>
        /// <param name="rules">The new set of network security rules</param>
        /// <returns>A network security group with the given set of rules merged with thsi one</returns>
        public ArmOperation<NetworkSecurityGroup> UpdateRules(NetworkSecurityGroupData model, CancellationToken cancellationToken = default, params SecurityRule[] rules)
        {
            foreach (var rule in rules)
            {
                // Note that this makes use of the 
                var matchingRule = model.Model.SecurityRules.FirstOrDefault(r => ResourceIdentifier.Equals(r.Id, rule.Id));
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
                    model.Model.SecurityRules.Add(rule);
                }
            }

            return new PhArmOperation<NetworkSecurityGroup, Azure.ResourceManager.Network.Models.NetworkSecurityGroup>(Operations.StartCreateOrUpdate(base.Id.ResourceGroup, base.Id.Name, model.Model), 
                n => { base.Resource = new NetworkSecurityGroupData(n); return new NetworkSecurityGroup(base.ClientContext, base.Resource as NetworkSecurityGroupData); });
        }

        public override ArmResponse<NetworkSecurityGroup> Get()
        {
            return new PhArmResponse<NetworkSecurityGroup, Azure.ResourceManager.Network.Models.NetworkSecurityGroup>(Operations.Get(base.Id.ResourceGroup, base.Id.Name),
                n => { base.Resource = new NetworkSecurityGroupData(n); return new NetworkSecurityGroup(base.ClientContext, base.Resource as NetworkSecurityGroupData); });
        }

        public async override Task<ArmResponse<NetworkSecurityGroup>> GetAsync(CancellationToken cancellationToken = default)
        {
            return new PhArmResponse<NetworkSecurityGroup, Azure.ResourceManager.Network.Models.NetworkSecurityGroup>(await Operations.GetAsync(base.Id.ResourceGroup, base.Id.Name, null, cancellationToken),
                n => { base.Resource = new NetworkSecurityGroupData(n); return new NetworkSecurityGroup(base.ClientContext, base.Resource as NetworkSecurityGroupData); });
        }

        public ArmOperation<NetworkSecurityGroup> AddTag(string key, string value)
        {
            var patchable = new TagsObject();
            patchable.Tags[key] = value;
            return new PhArmOperation<NetworkSecurityGroup, Azure.ResourceManager.Network.Models.NetworkSecurityGroup>(Operations.UpdateTags(base.Id.ResourceGroup, base.Id.Name, patchable),
                n => { base.Resource = new NetworkSecurityGroupData(n); return new NetworkSecurityGroup(base.ClientContext, base.Resource as NetworkSecurityGroupData); });
        }

        public async Task<ArmOperation<NetworkSecurityGroup>> AddTagAsync(string key, string value, CancellationToken cancellationToken = default)
        {
            var patchable = new TagsObject();
            patchable.Tags[key] = value;
            return new PhArmOperation<NetworkSecurityGroup, Azure.ResourceManager.Network.Models.NetworkSecurityGroup>(await Operations.UpdateTagsAsync(base.Id.ResourceGroup, base.Id.Name, patchable, cancellationToken),
                n => { base.Resource = new NetworkSecurityGroupData(n); return new NetworkSecurityGroup(base.ClientContext, base.Resource as NetworkSecurityGroupData); });
        }

        public ArmOperation<Response> Delete()
        {
            return new ArmVoidOperation(Operations.StartDelete(Id.ResourceGroup, Id.Name));
        }

        public async Task<ArmOperation<Response>> DeleteAsync(CancellationToken cancellationToken = default)
        {
            return new ArmVoidOperation(await Operations.StartDeleteAsync(Id.ResourceGroup, Id.Name, cancellationToken));
        }

        public override ResourceType ResourceType => "Microsoft.Network/networkSecurityGroups";

        internal NetworkSecurityGroupsOperations Operations => GetClient<NetworkManagementClient>((uri, cred) => new NetworkManagementClient(Id.Subscription, uri, cred)).NetworkSecurityGroups;
    }
}
