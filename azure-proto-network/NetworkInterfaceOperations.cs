using Azure;
using Azure.ResourceManager.Network;
using Azure.ResourceManager.Network.Models;
using Azure.ResourceManager.Core;
using System.Threading;
using System.Threading.Tasks;

namespace azure_proto_network
{
    public class NetworkInterfaceOperations : ResourceOperationsBase<NetworkInterface>, ITaggableResource<NetworkInterface>, IDeletableResource
    {
        internal NetworkInterfaceOperations(ArmResourceOperations genericOperations)
            : base(genericOperations)
        {
        }

        protected NetworkInterfaceOperations(ResourceOperationsBase options, ResourceIdentifier id)
            : base(options, id)
        {
        }

        internal NetworkInterfaceOperations(ResourceOperationsBase options, string nicName)
            : base(options, $"{options.Id}/providers/Microsoft.Network/networkInterfaces/{nicName}")
        {
        }

        public static readonly ResourceType ResourceType = "Microsoft.Network/networkInterfaces";

        protected override ResourceType ValidResourceType => ResourceType;

        internal NetworkInterfacesOperations Operations => new NetworkManagementClient(
            Id.Subscription,
            BaseUri,
            Credential,
            ClientOptions.Convert<NetworkManagementClientOptions>()).NetworkInterfaces;

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

        public override ArmResponse<NetworkInterface> Get()
        {
            return new PhArmResponse<NetworkInterface, Azure.ResourceManager.Network.Models.NetworkInterface>(
                Operations.Get(Id.ResourceGroup, Id.Name),
                n => new NetworkInterface(this, new NetworkInterfaceData(n)));
        }

        public async override Task<ArmResponse<NetworkInterface>> GetAsync(CancellationToken cancellationToken = default)
        {
            return new PhArmResponse<NetworkInterface, Azure.ResourceManager.Network.Models.NetworkInterface>(
                await Operations.GetAsync(Id.ResourceGroup, Id.Name, null, cancellationToken),
                n => new NetworkInterface(this, new NetworkInterfaceData(n)));
        }

        public ArmOperation<NetworkInterface> StartAddTag(string key, string value)
        {
            var patchable = new TagsObject();
            patchable.Tags[key] = value;
            return new PhArmOperation<NetworkInterface, Azure.ResourceManager.Network.Models.NetworkInterface>(Operations.UpdateTags(Id.ResourceGroup, Id.Name, patchable),
                n => new NetworkInterface(this, new NetworkInterfaceData(n)));
        }

        public async Task<ArmOperation<NetworkInterface>> StartAddTagAsync(string key, string value, CancellationToken cancellationToken = default)
        {
            var patchable = new TagsObject();
            patchable.Tags[key] = value;
            return new PhArmOperation<NetworkInterface, Azure.ResourceManager.Network.Models.NetworkInterface>(
                await Operations.UpdateTagsAsync(Id.ResourceGroup, Id.Name, patchable, cancellationToken),
                n => new NetworkInterface(this, new NetworkInterfaceData(n)));
        }
    }
}
