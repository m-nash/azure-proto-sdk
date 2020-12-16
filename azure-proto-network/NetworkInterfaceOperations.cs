using Azure;
using Azure.ResourceManager.Network;
using Azure.ResourceManager.Network.Models;
using Azure.ResourceManager.Core;
using System.Threading;
using System.Threading.Tasks;

namespace azure_proto_network
{
    public class NetworkInterfaceOperations : ResourceOperationsBase<NetworkInterface, NetworkInterfaceData>, ITaggable<NetworkInterface, NetworkInterfaceData>, IDeletableResource<NetworkInterface, NetworkInterfaceData>
    {
        internal NetworkInterfaceOperations(ArmResourceOperations genericOperations)
            : base(genericOperations.ClientContext, genericOperations.Id, genericOperations.ClientOptions)
        {
        }

        internal NetworkInterfaceOperations(ArmClientContext context, ResourceIdentifier id, ArmClientOptions clientOptions)
            : base(context, id, clientOptions)
        {
        }

        public override ResourceType ResourceType => "Microsoft.Network/networkInterfaces";

        internal NetworkInterfacesOperations Operations => GetClient<NetworkManagementClient>((uri, cred) => new NetworkManagementClient(Id.Subscription, uri, cred,
            ArmClientOptions.Convert<NetworkManagementClientOptions>(ClientOptions))).NetworkInterfaces;

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

        public override ArmResponse<NetworkInterface> Get()
        {
            return new PhArmResponse<NetworkInterface, Azure.ResourceManager.Network.Models.NetworkInterface>(
                Operations.Get(Id.ResourceGroup, Id.Name),
                n =>
                {
                    Resource = new NetworkInterfaceData(n);
                    return new NetworkInterface(ClientContext, Resource as NetworkInterfaceData, ClientOptions);
                });
        }

        public async override Task<ArmResponse<NetworkInterface>> GetAsync(CancellationToken cancellationToken = default)
        {
            return new PhArmResponse<NetworkInterface, Azure.ResourceManager.Network.Models.NetworkInterface>(
                await Operations.GetAsync(Id.ResourceGroup, Id.Name, null, cancellationToken),
                n =>
                {
                    Resource = new NetworkInterfaceData(n);
                    return new NetworkInterface(ClientContext, Resource as NetworkInterfaceData, ClientOptions);
                });
        }

        public ArmOperation<NetworkInterface> AddTag(string key, string value)
        {
            var patchable = new TagsObject();
            patchable.Tags[key] = value;
            return new PhArmOperation<NetworkInterface, Azure.ResourceManager.Network.Models.NetworkInterface>(Operations.UpdateTags(Id.ResourceGroup, Id.Name, patchable),
                n =>
                {
                    Resource = new NetworkInterfaceData(n);
                    return new NetworkInterface(ClientContext, Resource as NetworkInterfaceData, ClientOptions);
                });
        }

        public async Task<ArmOperation<NetworkInterface>> AddTagAsync(string key, string value, CancellationToken cancellationToken = default)
        {
            var patchable = new TagsObject();
            patchable.Tags[key] = value;
            return new PhArmOperation<NetworkInterface, Azure.ResourceManager.Network.Models.NetworkInterface>(
                await Operations.UpdateTagsAsync(Id.ResourceGroup, Id.Name, patchable, cancellationToken),
                n =>
                {
                    Resource = new NetworkInterfaceData(n);
                    return new NetworkInterface(ClientContext, Resource as NetworkInterfaceData, ClientOptions);
                });
        }
    }
}
