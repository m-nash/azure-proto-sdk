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
    public class VirtualNetworkOperations : ResourceOperationsBase<XVirtualNetwork, PhVirtualNetwork>
    {
        public VirtualNetworkOperations(ArmClientContext context, ResourceIdentifier id) : base(context, id) { }

        public override ResourceType ResourceType => "Microsoft.Network/virtualNetworks";

        public override ArmOperation<Response> Delete()
        {
            return new ArmVoidOperation(Operations.StartDelete(Id.ResourceGroup, Id.Name));
        }

        public async override Task<ArmOperation<Response>> DeleteAsync(CancellationToken cancellationToken = default)
        {
            return new ArmVoidOperation(await Operations.StartDeleteAsync(Id.ResourceGroup, Id.Name, cancellationToken));
        }

        public override ArmResponse<XVirtualNetwork> Get()
        {
            return new PhArmResponse<XVirtualNetwork, VirtualNetwork>(Operations.Get(Id.ResourceGroup, Id.Name), 
                n => { Resource = new PhVirtualNetwork(n); return new XVirtualNetwork(ClientContext, Resource as PhVirtualNetwork); });
        }

        public async override Task<ArmResponse<XVirtualNetwork>> GetAsync(CancellationToken cancellationToken = default)
        {
            return new PhArmResponse<XVirtualNetwork, VirtualNetwork>(await Operations.GetAsync(Id.ResourceGroup, Id.Name, null, cancellationToken),
                n => { Resource = new PhVirtualNetwork(n); return new XVirtualNetwork(ClientContext, Resource as PhVirtualNetwork); });
        }

        public override ArmOperation<XVirtualNetwork> AddTag(string key, string value)
        {
            var patchable = new TagsObject();
            patchable.Tags[key] = value;
            return new PhArmOperation<XVirtualNetwork, VirtualNetwork>(Operations.UpdateTags(Id.ResourceGroup, Id.Name, patchable),
                n => { Resource = new PhVirtualNetwork(n); return new XVirtualNetwork(ClientContext, Resource as PhVirtualNetwork); });
        }

        public async override Task<ArmOperation<XVirtualNetwork>> AddTagAsync(string key, string value, CancellationToken cancellationToken = default)
        {
            var patchable = new TagsObject();
            patchable.Tags[key] = value;
            return new PhArmOperation<XVirtualNetwork, VirtualNetwork>(await Operations.UpdateTagsAsync(Id.ResourceGroup, Id.Name, patchable, cancellationToken),
                n => { Resource = new PhVirtualNetwork(n); return new XVirtualNetwork(ClientContext, Resource as PhVirtualNetwork); });
        }

        public XSubnet Subnet(PhSubnet subnet)
        {
            return new XSubnet(ClientContext, subnet);
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
