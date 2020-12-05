using Azure;
using Azure.ResourceManager.Network;
using Azure.ResourceManager.Network.Models;
using azure_proto_core;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace azure_proto_network
{
    /// <summary>
    /// An operations + Model class for NSGs
    /// TODO: How does the operation signature change for resources that support Etags?
    /// </summary>
    public class NetworkSecurityGroupOperations : ResourceOperationsBase<NetworkSecurityGroupOperations, PhNetworkSecurityGroup>, ITaggable<NetworkSecurityGroupOperations, PhNetworkSecurityGroup>, IDeletableResource<NetworkSecurityGroupOperations, PhNetworkSecurityGroup>
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

        public NetworkSecurityGroupOperations(ArmClientContext parent, ResourceIdentifier context, ArmClientOptions clientOptions) : base(parent, context, clientOptions) { }

        public NetworkSecurityGroupOperations(ArmClientContext parent, TrackedResource context, ArmClientOptions clientOptions) : base(parent, context, clientOptions) { }

        /// <summary>
        /// TODO: GENERATOR Make use of the entity tags on the resource - we may need to add to the generated management client
        /// </summary>
        /// <param name="rules">The new set of network security rules</param>
        /// <returns>A network security group with the given set of rules merged with thsi one</returns>
        public ArmOperation<NetworkSecurityGroupOperations> UpdateRules(CancellationToken cancellationToken = default, params SecurityRule[] rules)
        {
            var model = Model;
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

            return new PhArmOperation<NetworkSecurityGroupOperations, NetworkSecurityGroup>(Operations.StartCreateOrUpdate(Id.ResourceGroup, Id.Name, model.Model),
                n => { Resource = new PhNetworkSecurityGroup(n); return this; });
        }

        public override ArmResponse<NetworkSecurityGroupOperations> Get()
        {
            return new PhArmResponse<NetworkSecurityGroupOperations, NetworkSecurityGroup>(Operations.Get(Id.ResourceGroup, Id.Name),
                n => { Resource = new PhNetworkSecurityGroup(n); return this; });
        }

        public async override Task<ArmResponse<NetworkSecurityGroupOperations>> GetAsync(CancellationToken cancellationToken = default)
        {
            return new PhArmResponse<NetworkSecurityGroupOperations, NetworkSecurityGroup>(await Operations.GetAsync(Id.ResourceGroup, Id.Name, null, cancellationToken),
                n => { Resource = new PhNetworkSecurityGroup(n); return this; });
        }

        public ArmOperation<NetworkSecurityGroupOperations> AddTag(string key, string value)
        {
            var patchable = new TagsObject();
            patchable.Tags[key] = value;
            return new PhArmOperation<NetworkSecurityGroupOperations, NetworkSecurityGroup>(Operations.UpdateTags(Id.ResourceGroup, Id.Name, patchable),
                n => { Resource = new PhNetworkSecurityGroup(n); return this; });
        }

        public async Task<ArmOperation<NetworkSecurityGroupOperations>> AddTagAsync(string key, string value, CancellationToken cancellationToken = default)
        {
            var patchable = new TagsObject();
            patchable.Tags[key] = value;
            return new PhArmOperation<NetworkSecurityGroupOperations, NetworkSecurityGroup>(await Operations.UpdateTagsAsync(Id.ResourceGroup, Id.Name, patchable, cancellationToken),
                n => { Resource = new PhNetworkSecurityGroup(n); return this; });
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

        internal NetworkSecurityGroupsOperations Operations => GetClient<NetworkManagementClient>((uri, cred) => new NetworkManagementClient(Id.Subscription, uri, cred,
                     ArmClientOptions.convert<NetworkManagementClientOptions>(this.ClientOptions))).NetworkSecurityGroups;
    }
}
