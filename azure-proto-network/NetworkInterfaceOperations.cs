using Azure;
using Azure.ResourceManager.Network;
using Azure.ResourceManager.Network.Models;
using azure_proto_core;
using System.Threading;
using System.Threading.Tasks;

namespace azure_proto_network
{
    public class NetworkInterfaceOperations : ResourceOperationsBase<XNetworkInterface, PhNetworkInterface>, ITaggable<XNetworkInterface, PhNetworkInterface>, IDeletableResource<XNetworkInterface, PhNetworkInterface>
    {
        public NetworkInterfaceOperations(ArmClientContext context, ResourceIdentifier id, ArmClientOptions clientOptions) : base(context, id, clientOptions) { }

        public override ResourceType ResourceType => "Microsoft.Network/networkInterfaces";

        public ArmOperation<Response> Delete()
        {
            return new ArmVoidOperation(Operations.StartDelete(Id.ResourceGroup, Id.Name));
        }

        public async Task<ArmOperation<Response>> DeleteAsync(CancellationToken cancellationToken = default)
        {
            return new ArmVoidOperation(await Operations.StartDeleteAsync(Id.ResourceGroup, Id.Name, cancellationToken));
        }

        public override ArmResponse<XNetworkInterface> Get()
        {
            return new PhArmResponse<XNetworkInterface, NetworkInterface>(
                Operations.Get(Id.ResourceGroup, Id.Name),
                n => { Resource = new PhNetworkInterface(n); return new XNetworkInterface(ClientContext, Resource as PhNetworkInterface, this.ClientOptions); });
        }

        public async override Task<ArmResponse<XNetworkInterface>> GetAsync(CancellationToken cancellationToken = default)
        {
            return new PhArmResponse<XNetworkInterface, NetworkInterface>(
                await Operations.GetAsync(Id.ResourceGroup, Id.Name, null, cancellationToken),
                n => { Resource = new PhNetworkInterface(n); return new XNetworkInterface(ClientContext, Resource as PhNetworkInterface, this.ClientOptions); });
        }

        public ArmOperation<XNetworkInterface> AddTag(string key, string value)
        {
            var patchable = new TagsObject();
            patchable.Tags[key] = value;
            return new PhArmOperation<XNetworkInterface, NetworkInterface>(Operations.UpdateTags(Id.ResourceGroup, Id.Name, patchable),
                n => { Resource = new PhNetworkInterface(n); return new XNetworkInterface(ClientContext, Resource as PhNetworkInterface, this.ClientOptions); });
        }

        public async Task<ArmOperation<XNetworkInterface>> AddTagAsync(string key, string value, CancellationToken cancellationToken = default)
        {
            var patchable = new TagsObject();
            patchable.Tags[key] = value;
            return new PhArmOperation<XNetworkInterface, NetworkInterface>(
                await Operations.UpdateTagsAsync(Id.ResourceGroup, Id.Name, patchable, cancellationToken), 
                n => { Resource = new PhNetworkInterface(n); return new XNetworkInterface(ClientContext, Resource as PhNetworkInterface, this.ClientOptions); });
        }

        internal NetworkInterfacesOperations Operations => GetClient<NetworkManagementClient>((uri, cred) => new NetworkManagementClient(Id.Subscription, uri, cred, 
                    ArmClientOptions.convert<NetworkManagementClientOptions>(this.ClientOptions))).NetworkInterfaces;
    }
}
