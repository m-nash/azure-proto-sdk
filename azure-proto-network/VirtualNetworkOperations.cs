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
    public class VirtualNetworkOperations : ResourceOperationsBase<VirtualNetwork, VirtualNetworkData>, ITaggable<VirtualNetwork, VirtualNetworkData>, IDeletableResource<VirtualNetwork, VirtualNetworkData>
    {
        public VirtualNetworkOperations(ArmClientContext context, ResourceIdentifier id) : base(context, id) { }

        public override ResourceType ResourceType => "Microsoft.Network/virtualNetworks";

        public ArmOperation<Response> Delete()
        {
            return new ArmVoidOperation(Operations.StartDelete(Id.ResourceGroup, Id.Name));
        }

        public async Task<ArmOperation<Response>> DeleteAsync(CancellationToken cancellationToken = default)
        {
            return new ArmVoidOperation(await Operations.StartDeleteAsync(Id.ResourceGroup, Id.Name, cancellationToken));
        }

        public override ArmResponse<VirtualNetwork> Get()
        {
            return new PhArmResponse<VirtualNetwork, Azure.ResourceManager.Network.Models.VirtualNetwork>(Operations.Get(base.Id.ResourceGroup, base.Id.Name), 
                n => { base.Resource = new VirtualNetworkData(n); return new VirtualNetwork(base.ClientContext, base.Resource as VirtualNetworkData); });
        }

        public async override Task<ArmResponse<VirtualNetwork>> GetAsync(CancellationToken cancellationToken = default)
        {
            return new PhArmResponse<VirtualNetwork, Azure.ResourceManager.Network.Models.VirtualNetwork>(await Operations.GetAsync(base.Id.ResourceGroup, base.Id.Name, null, cancellationToken),
                n => { base.Resource = new VirtualNetworkData(n); return new VirtualNetwork(base.ClientContext, base.Resource as VirtualNetworkData); });
        }

        public ArmOperation<VirtualNetwork> AddTag(string key, string value)
        {
            var patchable = new TagsObject();
            patchable.Tags[key] = value;
            return new PhArmOperation<VirtualNetwork, Azure.ResourceManager.Network.Models.VirtualNetwork>(Operations.UpdateTags(base.Id.ResourceGroup, base.Id.Name, patchable),
                n => { base.Resource = new VirtualNetworkData(n); return new VirtualNetwork(base.ClientContext, base.Resource as VirtualNetworkData); });
        }

        public async Task<ArmOperation<VirtualNetwork>> AddTagAsync(string key, string value, CancellationToken cancellationToken = default)
        {
            var patchable = new TagsObject();
            patchable.Tags[key] = value;
            return new PhArmOperation<VirtualNetwork, Azure.ResourceManager.Network.Models.VirtualNetwork>(await Operations.UpdateTagsAsync(base.Id.ResourceGroup, base.Id.Name, patchable, cancellationToken),
                n => { base.Resource = new VirtualNetworkData(n); return new VirtualNetwork(base.ClientContext, base.Resource as VirtualNetworkData); });
        }

        public Subnet Subnet(SubnetData subnet)
        {
            return new Subnet(ClientContext, subnet);
        }

        public SubnetOperations Subnet(ResourceIdentifier subnet)
        {
            return new SubnetOperations(ClientContext, subnet);
        }

        public SubnetOperations Subnet(string subnet)
        {
            return new SubnetOperations(ClientContext, $"{Id}/subnets/{subnet}");
        }

        public virtual SubnetContainer Subnets()
        {
            return new SubnetContainer(ClientContext, Id);
        }

        internal VirtualNetworksOperations Operations => GetClient<NetworkManagementClient>((uri, cred) => new NetworkManagementClient(Id.Subscription, uri, cred)).VirtualNetworks;
    }
}
