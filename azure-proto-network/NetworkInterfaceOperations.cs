using Azure;
using Azure.ResourceManager.Network;
using Azure.ResourceManager.Network.Models;
using azure_proto_core;
using System.Threading;
using System.Threading.Tasks;

namespace azure_proto_network
{
    public class NetworkInterfaceOperations : ResourceOperationsBase<NetworkInterface, NetworkInterfaceData>, ITaggable<NetworkInterface, NetworkInterfaceData>, IDeletableResource<NetworkInterface, NetworkInterfaceData>
    {
        public NetworkInterfaceOperations(ArmClientContext context, ResourceIdentifier id) : base(context, id) { }

        public override ResourceType ResourceType => "Microsoft.Network/networkInterfaces";

        public ArmOperation<Response> Delete()
        {
            return new ArmVoidOperation(Operations.StartDelete(Id.ResourceGroup, Id.Name));
        }

        public async Task<ArmOperation<Response>> DeleteAsync(CancellationToken cancellationToken = default)
        {
            return new ArmVoidOperation(await Operations.StartDeleteAsync(Id.ResourceGroup, Id.Name, cancellationToken));
        }

        public override ArmResponse<NetworkInterface> Get()
        {
            return new PhArmResponse<NetworkInterface, Azure.ResourceManager.Network.Models.NetworkInterface>(
                Operations.Get(base.Id.ResourceGroup, base.Id.Name),
                n => { base.Resource = new NetworkInterfaceData(n); return new NetworkInterface(base.ClientContext, base.Resource as NetworkInterfaceData); });
        }

        public async override Task<ArmResponse<NetworkInterface>> GetAsync(CancellationToken cancellationToken = default)
        {
            return new PhArmResponse<NetworkInterface, Azure.ResourceManager.Network.Models.NetworkInterface>(
                await Operations.GetAsync(base.Id.ResourceGroup, base.Id.Name, null, cancellationToken),
                n => { base.Resource = new NetworkInterfaceData(n); return new NetworkInterface(base.ClientContext, base.Resource as NetworkInterfaceData); });
        }

        public ArmOperation<NetworkInterface> AddTag(string key, string value)
        {
            var patchable = new TagsObject();
            patchable.Tags[key] = value;
            return new PhArmOperation<NetworkInterface, Azure.ResourceManager.Network.Models.NetworkInterface>(Operations.UpdateTags(base.Id.ResourceGroup, base.Id.Name, patchable),
                n => { base.Resource = new NetworkInterfaceData(n); return new NetworkInterface(base.ClientContext, base.Resource as NetworkInterfaceData); });
        }

        public async Task<ArmOperation<NetworkInterface>> AddTagAsync(string key, string value, CancellationToken cancellationToken = default)
        {
            var patchable = new TagsObject();
            patchable.Tags[key] = value;
            return new PhArmOperation<NetworkInterface, Azure.ResourceManager.Network.Models.NetworkInterface>(
                await Operations.UpdateTagsAsync(base.Id.ResourceGroup, base.Id.Name, patchable, cancellationToken), 
                n => { base.Resource = new NetworkInterfaceData(n); return new NetworkInterface(base.ClientContext, base.Resource as NetworkInterfaceData); });
        }

        internal NetworkInterfacesOperations Operations => GetClient<NetworkManagementClient>((uri, cred) => new NetworkManagementClient(Id.Subscription, uri, cred)).NetworkInterfaces;
    }
}
