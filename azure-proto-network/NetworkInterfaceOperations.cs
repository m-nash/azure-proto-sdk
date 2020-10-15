using Azure;
using Azure.ResourceManager.Network;
using Azure.ResourceManager.Network.Models;
using azure_proto_core;
using System.Threading;
using System.Threading.Tasks;

namespace azure_proto_network
{
    public class NetworkInterfaceOperations : ResourceOperationsBase<NetworkInterfaceOperations, PhNetworkInterface>
    {
        public NetworkInterfaceOperations(ArmClientContext context, ResourceIdentifier id) : base(context, id) { }

        public NetworkInterfaceOperations(ArmClientContext context, azure_proto_core.Resource resource) : base(context, resource) { }

        public override ResourceType ResourceType => "Microsoft.Network/networkInterfaces";

        public override ArmOperation<Response> Delete()
        {
            return new ArmVoidOperation(Operations.StartDelete(Id.ResourceGroup, Id.Name));
        }

        public async override Task<ArmOperation<Response>> DeleteAsync(CancellationToken cancellationToken = default)
        {
            return new ArmVoidOperation(await Operations.StartDeleteAsync(Id.ResourceGroup, Id.Name, cancellationToken));
        }

        public override ArmResponse<NetworkInterfaceOperations> Get()
        {
            return new PhArmResponse<NetworkInterfaceOperations, NetworkInterface>(
                Operations.Get(Id.ResourceGroup, Id.Name),
                n => { Resource = new PhNetworkInterface(n); return this; });
        }

        public async override Task<ArmResponse<NetworkInterfaceOperations>> GetAsync(CancellationToken cancellationToken = default)
        {
            return new PhArmResponse<NetworkInterfaceOperations, NetworkInterface>(
                await Operations.GetAsync(Id.ResourceGroup, Id.Name, null, cancellationToken),
                n => { Resource = new PhNetworkInterface(n); return this; });
        }

        public override ArmOperation<NetworkInterfaceOperations> AddTag(string key, string value)
        {
            var patchable = new TagsObject();
            patchable.Tags[key] = value;
            return new PhArmOperation<NetworkInterfaceOperations, NetworkInterface>(Operations.UpdateTags(Id.ResourceGroup, Id.Name, patchable),
                n => { Resource = new PhNetworkInterface(n); return this; });
        }

        public async override Task<ArmOperation<NetworkInterfaceOperations>> AddTagAsync(string key, string value, CancellationToken cancellationToken = default)
        {
            var patchable = new TagsObject();
            patchable.Tags[key] = value;
            return new PhArmOperation<NetworkInterfaceOperations, NetworkInterface>(
                await Operations.UpdateTagsAsync(Id.ResourceGroup, Id.Name, patchable, cancellationToken), 
                n => { Resource = new PhNetworkInterface(n); return this; });
        }

        internal NetworkInterfacesOperations Operations => GetClient<NetworkManagementClient>((uri, cred) => new NetworkManagementClient(Id.Subscription, uri, cred)).NetworkInterfaces;
    }
}
