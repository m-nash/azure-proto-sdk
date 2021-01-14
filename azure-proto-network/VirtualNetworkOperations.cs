using Azure;
using Azure.ResourceManager.Network;
using Azure.ResourceManager.Network.Models;
using Azure.ResourceManager.Core;
using System.Threading;
using System.Threading.Tasks;

namespace azure_proto_network
{
    /// <summary>
    /// Virtual Network Operations
    /// </summary>
    public class VirtualNetworkOperations : ResourceOperationsBase<VirtualNetwork>, ITaggableResource<VirtualNetwork>, IDeletableResource
    {
        internal VirtualNetworkOperations(ArmResourceOperations genericOperations)
            : base(genericOperations)
        {
        }

        protected VirtualNetworkOperations(ResourceOperationsBase operations, ResourceIdentifier id)
            : base(operations, id)
        {
        }

        internal VirtualNetworkOperations(ResourceOperationsBase operations, string vnetName)
            : base(operations, $"{operations}/providers/Microsoft.Network/virtualNetworks/{vnetName}")
        {
        }

        public static readonly ResourceType ResourceType = "Microsoft.Network/virtualNetworks";

        protected override ResourceType ValidResourceType => ResourceType;

        internal VirtualNetworksOperations Operations => new NetworkManagementClient(
            Id.Subscription,
            BaseUri,
            Credential,
            ClientOptions.Convert<NetworkManagementClientOptions>()).VirtualNetworks;

        public ArmResponse<Response> Delete()
        {
            return new ArmResponse(Operations.StartDelete(Id.ResourceGroup, Id.Name).WaitForCompletionAsync().ConfigureAwait(false).GetAwaiter().GetResult());
        }

        public async Task<ArmResponse<Response>> DeleteAsync(CancellationToken cancellationToken = default)
        {
            return new ArmResponse((await Operations.StartDeleteAsync(Id.ResourceGroup, Id.Name, cancellationToken)).WaitForCompletionAsync().ConfigureAwait(false).GetAwaiter().GetResult());
        }

        public ArmOperation<Response> StartDelete(CancellationToken cancellationToken = default)
        {
            return new ArmVoidOperation(Operations.StartDelete(Id.ResourceGroup, Id.Name, cancellationToken));
        }

        public async Task<ArmOperation<Response>> StartDeleteAsync(CancellationToken cancellationToken = default)
        {
            return new ArmVoidOperation(await Operations.StartDeleteAsync(Id.ResourceGroup, Id.Name, cancellationToken));
        }


        public override ArmResponse<VirtualNetwork> Get()
        {
            return new PhArmResponse<VirtualNetwork, Azure.ResourceManager.Network.Models.VirtualNetwork>(Operations.Get(Id.ResourceGroup, Id.Name),
                n => new VirtualNetwork(this, new VirtualNetworkData(n)));
        }

        public async override Task<ArmResponse<VirtualNetwork>> GetAsync(CancellationToken cancellationToken = default)
        {
            return new PhArmResponse<VirtualNetwork, Azure.ResourceManager.Network.Models.VirtualNetwork>(await Operations.GetAsync(Id.ResourceGroup, Id.Name, null, cancellationToken),
                n => new VirtualNetwork(this, new VirtualNetworkData(n)));
        }

        public ArmOperation<VirtualNetwork> StartAddTag(string key, string value)
        {
            var patchable = new TagsObject();
            patchable.Tags[key] = value;
            return new PhArmOperation<VirtualNetwork, Azure.ResourceManager.Network.Models.VirtualNetwork>(Operations.UpdateTags(Id.ResourceGroup, Id.Name, patchable),
                n => new VirtualNetwork(this, new VirtualNetworkData(n)));
        }

        public async Task<ArmOperation<VirtualNetwork>> StartAddTagAsync(string key, string value, CancellationToken cancellationToken = default)
        {
            var patchable = new TagsObject();
            patchable.Tags[key] = value;
            return new PhArmOperation<VirtualNetwork, Azure.ResourceManager.Network.Models.VirtualNetwork>(await Operations.UpdateTagsAsync(Id.ResourceGroup, Id.Name, patchable, cancellationToken),
                n => new VirtualNetwork(this, new VirtualNetworkData(n)));
        }

        public SubnetOperations Subnet(string subnet)
        {
            return new SubnetOperations(this, subnet);
        }

        public SubnetContainer Subnets()
        {
            return new SubnetContainer(this);
        }
    }
}
