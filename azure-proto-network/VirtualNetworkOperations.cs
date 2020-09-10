using Azure;
using Azure.ResourceManager.Network;
using Azure.ResourceManager.Network.Models;
using azure_proto_core;
using azure_proto_core.Adapters;
using System.Threading;
using System.Threading.Tasks;

namespace azure_proto_network
{
    /// <summary>
    /// Virtual Network Operations
    /// </summary>
    public class VirtualNetworkOperations : ResourceOperationsBase<PhVirtualNetwork>
    {
        public VirtualNetworkOperations(ArmClientContext parent, ResourceIdentifier context) : base(parent, context)
        {
        }

        public VirtualNetworkOperations(ArmClientContext parent, azure_proto_core.Resource context) : base(parent, context)
        {
        }

        public VirtualNetworkOperations(OperationsBase parent, ResourceIdentifier context) : base(parent, context)
        {
        }

        public VirtualNetworkOperations(OperationsBase parent, azure_proto_core.Resource context) : base(parent, context)
        {
        }

        public override ResourceType ResourceType => "Microsoft.Network/virtualNetworks";

        public override ArmOperation<Response> Delete()
        {
            return new ArmVoidOperation(Operations.StartDelete(Id.ResourceGroup, Id.Name));
        }

        public async override Task<ArmOperation<Response>> DeleteAsync(CancellationToken cancellationToken = default)
        {
            return new ArmVoidOperation(await Operations.StartDeleteAsync(Id.ResourceGroup, Id.Name, cancellationToken));
        }

        public override Response<ResourceOperationsBase<PhVirtualNetwork>> Get()
        {
            return new PhArmResponse<ResourceOperationsBase<PhVirtualNetwork>, VirtualNetwork>(Operations.Get(Id.ResourceGroup, Id.Name), 
                n => { Resource = new PhVirtualNetwork(n); return this; });
        }

        public async override Task<Response<ResourceOperationsBase<PhVirtualNetwork>>> GetAsync(CancellationToken cancellationToken = default)
        {
            return new PhArmResponse<ResourceOperationsBase<PhVirtualNetwork>, VirtualNetwork>(await Operations.GetAsync(Id.ResourceGroup, Id.Name, null, cancellationToken),
                n => { Resource = new PhVirtualNetwork(n); return this;});
        }

        public override ArmOperation<ResourceOperationsBase<PhVirtualNetwork>> AddTag(string key, string value)
        {
            var patchable = new TagsObject();
            patchable.Tags[key] = value;
            return new PhArmOperation<ResourceOperationsBase<PhVirtualNetwork>, VirtualNetwork>(Operations.UpdateTags(Id.ResourceGroup, Id.Name, patchable),
                n => { Resource = new PhVirtualNetwork(n); return this; });
        }

        public async override Task<ArmOperation<ResourceOperationsBase<PhVirtualNetwork>>> AddTagAsync(string key, string value, CancellationToken cancellationToken = default)
        {
            var patchable = new TagsObject();
            patchable.Tags[key] = value;
            return new PhArmOperation<ResourceOperationsBase<PhVirtualNetwork>, VirtualNetwork>(await Operations.UpdateTagsAsync(Id.ResourceGroup, Id.Name, patchable, cancellationToken),
                n => { Resource = new PhVirtualNetwork(n); return this; });
        }

        public Pageable<ResourceOperationsBase<PhSubnet>> ListSubnets(CancellationToken cancellationToken = default)
        {
            var subnetClient = GetClient<NetworkManagementClient>((uri, cred) => new NetworkManagementClient(Id.Subscription, uri, cred)).Subnets;
            return new PhWrappingPageable<Subnet, ResourceOperationsBase<PhSubnet>>(subnetClient.List(Id.ResourceGroup, Id.Name, cancellationToken), s => new SubnetOperations(this, new PhSubnet(s, DefaultLocation)));
        }

        public AsyncPageable<ResourceOperationsBase<PhSubnet>> ListSubnetsAsync(CancellationToken cancellationToken = default)
        {
            var subnetClient = GetClient<NetworkManagementClient>((uri, cred) => new NetworkManagementClient(Id.Subscription, uri, cred)).Subnets;
            return new PhWrappingAsyncPageable<Subnet, ResourceOperationsBase<PhSubnet>>(subnetClient.ListAsync(Id.ResourceGroup, Id.Name, cancellationToken), s => new SubnetOperations(this, new PhSubnet(s, DefaultLocation)));
        }

        internal VirtualNetworksOperations Operations => GetClient<NetworkManagementClient>((uri, cred) => new NetworkManagementClient(Id.Subscription, uri, cred)).VirtualNetworks;
    }
}
