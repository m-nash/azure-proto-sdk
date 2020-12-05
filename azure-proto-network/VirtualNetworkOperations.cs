using Azure;
using Azure.ResourceManager.Network;
using Azure.ResourceManager.Network.Models;
using azure_proto_core;
using System.Threading;
using System.Threading.Tasks;

namespace azure_proto_network
{
    /// <summary>
    /// Virtual Network Operations
    /// </summary>
    public class VirtualNetworkOperations : ResourceOperationsBase<VirtualNetworkOperations, PhVirtualNetwork>, ITaggable<VirtualNetworkOperations, PhVirtualNetwork>, IDeletableResource<VirtualNetworkOperations, PhVirtualNetwork>
    {
        public VirtualNetworkOperations(ArmClientContext context, ResourceIdentifier id, ArmClientOptions clientOptions) : base(context, id, clientOptions) { }

        public VirtualNetworkOperations(ArmClientContext context, azure_proto_core.Resource resource, ArmClientOptions clientOptions) : base(context, resource, clientOptions) { }

        public override ResourceType ResourceType => "Microsoft.Network/virtualNetworks";

        public ArmOperation<Response> Delete()
        {
            return new ArmVoidOperation(Operations.StartDelete(Id.ResourceGroup, Id.Name));
        }

        public async Task<ArmOperation<Response>> DeleteAsync(CancellationToken cancellationToken = default)
        {
            return new ArmVoidOperation(await Operations.StartDeleteAsync(Id.ResourceGroup, Id.Name, cancellationToken));
        }

        public override ArmResponse<VirtualNetworkOperations> Get()
        {
            return new PhArmResponse<VirtualNetworkOperations, VirtualNetwork>(Operations.Get(Id.ResourceGroup, Id.Name),
                n => { Resource = new PhVirtualNetwork(n); return this; });
        }

        public async override Task<ArmResponse<VirtualNetworkOperations>> GetAsync(CancellationToken cancellationToken = default)
        {
            return new PhArmResponse<VirtualNetworkOperations, VirtualNetwork>(await Operations.GetAsync(Id.ResourceGroup, Id.Name, null, cancellationToken),
                n => { Resource = new PhVirtualNetwork(n); return this; });
        }

        public ArmOperation<VirtualNetworkOperations> AddTag(string key, string value)
        {
            var patchable = new TagsObject();
            patchable.Tags[key] = value;
            return new PhArmOperation<VirtualNetworkOperations, VirtualNetwork>(Operations.UpdateTags(Id.ResourceGroup, Id.Name, patchable),
                n => { Resource = new PhVirtualNetwork(n); return this; });
        }

        public async Task<ArmOperation<VirtualNetworkOperations>> AddTagAsync(string key, string value, CancellationToken cancellationToken = default)
        {
            var patchable = new TagsObject();
            patchable.Tags[key] = value;
            return new PhArmOperation<VirtualNetworkOperations, VirtualNetwork>(await Operations.UpdateTagsAsync(Id.ResourceGroup, Id.Name, patchable, cancellationToken),
                n => { Resource = new PhVirtualNetwork(n); return this; });
        }

        public SubnetOperations Subnet(TrackedResource subnet)
        {
            return new SubnetOperations(ClientContext, subnet, this.ClientOptions);
        }

        public SubnetOperations Subnet(ResourceIdentifier subnet)
        {
            return new SubnetOperations(ClientContext, subnet, this.ClientOptions);
        }

        public SubnetOperations Subnet(string subnet)
        {
            return new SubnetOperations(ClientContext, $"{Id}/subnets/{subnet}", this.ClientOptions);
        }

        public SubnetContainer Subnets()
        {
            return new SubnetContainer(ClientContext, Model, this.ClientOptions);
        }

        internal VirtualNetworksOperations Operations => GetClient<NetworkManagementClient>((uri, cred) => new NetworkManagementClient(Id.Subscription, uri, cred,
                    ArmClientOptions.convert<NetworkManagementClientOptions>(this.ClientOptions))).VirtualNetworks;
    }
}
