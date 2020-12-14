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
    public class VirtualNetworkOperations : ResourceOperationsBase<VirtualNetwork>, ITaggable<VirtualNetwork>, IDeletable
    {
        internal VirtualNetworkOperations(ArmResourceOperations genericOperations)
            : base(genericOperations.ClientContext,genericOperations.Id, genericOperations.ClientOptions)
        {
        }

        internal VirtualNetworkOperations(ArmClientContext context, ResourceIdentifier id, ArmClientOptions clientOptions)
            : base(context, id, clientOptions)
        {
        }

        public override ResourceType ResourceType => "Microsoft.Network/virtualNetworks";

        internal VirtualNetworksOperations Operations => GetClient<NetworkManagementClient>((uri, cred) => new NetworkManagementClient(Id.Subscription, uri, cred,
            ArmClientOptions.Convert<NetworkManagementClientOptions>(ClientOptions))).VirtualNetworks;

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
            return new PhArmResponse<VirtualNetwork, Azure.ResourceManager.Network.Models.VirtualNetwork>(Operations.Get(Id.ResourceGroup, Id.Name), 
                n =>
                {
                    Resource = new VirtualNetworkData(n);
                    return new VirtualNetwork(ClientContext, Resource as VirtualNetworkData, ClientOptions);
                });
        }

        public async override Task<ArmResponse<VirtualNetwork>> GetAsync(CancellationToken cancellationToken = default)
        {
            return new PhArmResponse<VirtualNetwork, Azure.ResourceManager.Network.Models.VirtualNetwork>(await Operations.GetAsync(Id.ResourceGroup, Id.Name, null, cancellationToken),
                n =>
                {
                    Resource = new VirtualNetworkData(n);
                    return new VirtualNetwork(ClientContext, Resource as VirtualNetworkData, ClientOptions);
                });
        }

        public ArmOperation<VirtualNetwork> AddTag(string key, string value)
        {
            var patchable = new TagsObject();
            patchable.Tags[key] = value;
            return new PhArmOperation<VirtualNetwork, Azure.ResourceManager.Network.Models.VirtualNetwork>(Operations.UpdateTags(Id.ResourceGroup, Id.Name, patchable),
                n =>
                {
                    Resource = new VirtualNetworkData(n);
                    return new VirtualNetwork(ClientContext, Resource as VirtualNetworkData, ClientOptions);
                });
        }

        public async Task<ArmOperation<VirtualNetwork>> AddTagAsync(string key, string value, CancellationToken cancellationToken = default)
        {
            var patchable = new TagsObject();
            patchable.Tags[key] = value;
            return new PhArmOperation<VirtualNetwork, Azure.ResourceManager.Network.Models.VirtualNetwork>(await Operations.UpdateTagsAsync(Id.ResourceGroup, Id.Name, patchable, cancellationToken),
                n =>
                {
                    Resource = new VirtualNetworkData(n);
                    return new VirtualNetwork(ClientContext, Resource as VirtualNetworkData, ClientOptions);
                });
        }

        public Subnet Subnet(SubnetData subnet)
        {
            return new Subnet(ClientContext, subnet, ClientOptions);
        }

        public SubnetOperations Subnet(ResourceIdentifier subnet)
        {
            return new SubnetOperations(ClientContext, subnet, ClientOptions);
        }

        public SubnetOperations Subnet(string subnet)
        {
            return new SubnetOperations(ClientContext, $"{Id}/subnets/{subnet}", ClientOptions);
        }

        public virtual SubnetContainer Subnets()
        {
            return new SubnetContainer(ClientContext, Id, ClientOptions);
        }
    }
}
