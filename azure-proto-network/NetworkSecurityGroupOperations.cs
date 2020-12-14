using Azure;
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
    /// An operations + Data class for NSGs
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

        internal NetworkSecurityGroupOperations(ArmResourceOperations genericOperations)
            : base(genericOperations.ClientContext, genericOperations.Id, genericOperations.ClientOptions)
        {
        }

        internal NetworkSecurityGroupOperations(ArmClientContext parent, ResourceIdentifier context, ArmClientOptions clientOptions)
            : base(parent, context, clientOptions)
        {
        }

        public NetworkSecurityGroupOperations(ArmClientContext parent, TrackedResource context, ArmClientOptions clientOptions)
            : base(parent, context, clientOptions)
        {
        }

        public override ResourceType ResourceType => "Microsoft.Network/networkSecurityGroups";

        internal NetworkSecurityGroupsOperations Operations => GetClient<NetworkManagementClient>((uri, cred) => new NetworkManagementClient(Id.Subscription, uri, cred,
                     ArmClientOptions.Convert<NetworkManagementClientOptions>(ClientOptions))).NetworkSecurityGroups;

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

            return new PhArmOperation<NetworkSecurityGroup, Azure.ResourceManager.Network.Models.NetworkSecurityGroup>(Operations.StartCreateOrUpdate(Id.ResourceGroup, Id.Name, model.Model),
                n =>
                {
                    Resource = new NetworkSecurityGroupData(n);
                    return new NetworkSecurityGroup(ClientContext, Resource as NetworkSecurityGroupData, ClientOptions);
                });
        }

        public override ArmResponse<NetworkSecurityGroup> Get()
        {
            return new PhArmResponse<NetworkSecurityGroup, Azure.ResourceManager.Network.Models.NetworkSecurityGroup>(Operations.Get(Id.ResourceGroup, Id.Name),
                n =>
                {
                    Resource = new NetworkSecurityGroupData(n);
                    return new NetworkSecurityGroup(ClientContext, Resource as NetworkSecurityGroupData, ClientOptions);
                });
        }

        public async override Task<ArmResponse<NetworkSecurityGroup>> GetAsync(CancellationToken cancellationToken = default)
        {
            return new PhArmResponse<NetworkSecurityGroup, Azure.ResourceManager.Network.Models.NetworkSecurityGroup>(await Operations.GetAsync(Id.ResourceGroup, Id.Name, null, cancellationToken),
                n =>
                {
                    Resource = new NetworkSecurityGroupData(n);
                    return new NetworkSecurityGroup(ClientContext, Resource as NetworkSecurityGroupData, ClientOptions);
                });
        }

        public ArmOperation<NetworkSecurityGroup> AddTag(string key, string value)
        {
            var patchable = new TagsObject();
            patchable.Tags[key] = value;
            return new PhArmOperation<NetworkSecurityGroup, Azure.ResourceManager.Network.Models.NetworkSecurityGroup>(Operations.UpdateTags(Id.ResourceGroup, Id.Name, patchable),
                n =>
                {
                    Resource = new NetworkSecurityGroupData(n);
                    return new NetworkSecurityGroup(ClientContext, Resource as NetworkSecurityGroupData, ClientOptions);
                });
        }

        public async Task<ArmOperation<NetworkSecurityGroup>> AddTagAsync(string key, string value, CancellationToken cancellationToken = default)
        {
            var patchable = new TagsObject();
            patchable.Tags[key] = value;
            return new PhArmOperation<NetworkSecurityGroup, Azure.ResourceManager.Network.Models.NetworkSecurityGroup>(await Operations.UpdateTagsAsync(Id.ResourceGroup, Id.Name, patchable, cancellationToken),
                n =>
                {
                    Resource = new NetworkSecurityGroupData(n);
                    return new NetworkSecurityGroup(ClientContext, Resource as NetworkSecurityGroupData, ClientOptions);
                });
        }

        public ArmResponse<Response> Delete()
        {
            return new ArmVoidResponse(Operations.StartDelete(Id.ResourceGroup, Id.Name).WaitForCompletionAsync().ConfigureAwait(false).GetAwaiter().GetResult());
        }

        public async Task<ArmResponse<Response>> DeleteAsync(CancellationToken cancellationToken = default)
        {
            return new ArmVoidResponse((await Operations.StartDeleteAsync(Id.ResourceGroup, Id.Name, cancellationToken)).WaitForCompletionAsync().ConfigureAwait(false).GetAwaiter().GetResult());
        }

        public ArmOperation<Response> StartDelete()
        {
            return new ArmVoidOperation(Operations.StartDelete(Id.ResourceGroup, Id.Name));
        }

        public async Task<ArmOperation<Response>> StartDeleteAsync(CancellationToken cancellationToken = default)
        {
            return new ArmVoidOperation(await Operations.StartDeleteAsync(Id.ResourceGroup, Id.Name, cancellationToken));
        }
    }
}
