using Azure;
using Azure.ResourceManager.Network;
using Azure.ResourceManager.Network.Models;
using Azure.ResourceManager.Core;
using System.Threading;
using System.Threading.Tasks;

namespace azure_proto_network
{
    public class PublicIpAddressOperations : ResourceOperationsBase<PublicIpAddress>, ITaggableResource<PublicIpAddress>, IDeletableResource
    {
        internal PublicIpAddressOperations(ArmResourceOperations genericOperations)
            : base(genericOperations.ClientOptions, genericOperations.Id)
        {
        }

        internal PublicIpAddressOperations(AzureResourceManagerClientOptions options, ResourceIdentifier id)
            : base(options, id)
        {
        }

        public static readonly ResourceType ResourceType = "Microsoft.Network/publicIpAddresses";

        internal PublicIPAddressesOperations Operations => GetClient<NetworkManagementClient>((uri, cred) => new NetworkManagementClient(Id.Subscription, uri, cred,
            ClientOptions.Convert<NetworkManagementClientOptions>())).PublicIPAddresses;

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


        public override ArmResponse<PublicIpAddress> Get()
        {
            return new PhArmResponse<PublicIpAddress, PublicIPAddress>(Operations.Get(Id.ResourceGroup, Id.Name),
                n =>
                {
                    Resource = new PublicIPAddressData(n);
                    return new PublicIpAddress(ClientOptions, Resource as PublicIPAddressData);
                });
        }

        public async override Task<ArmResponse<PublicIpAddress>> GetAsync(CancellationToken cancellationToken = default)
        {
            return new PhArmResponse<PublicIpAddress, PublicIPAddress>(await Operations.GetAsync(Id.ResourceGroup, Id.Name, null, cancellationToken),
               n =>
               {
                   Resource = new PublicIPAddressData(n);
                   return new PublicIpAddress(ClientOptions, Resource as PublicIPAddressData);
               });
        }

        public ArmOperation<PublicIpAddress> AddTag(string key, string value)
        {
            var patchable = new TagsObject();
            patchable.Tags[key] = value;
            return new PhArmOperation<PublicIpAddress, PublicIPAddress>(Operations.UpdateTags(Id.ResourceGroup, Id.Name, patchable),
                n =>
                {
                    Resource = new PublicIPAddressData(n);
                    return new PublicIpAddress(ClientOptions, Resource as PublicIPAddressData);
                });
        }

        public async Task<ArmOperation<PublicIpAddress>> AddTagAsync(string key, string value, CancellationToken cancellationToken = default)
        {
            var patchable = new TagsObject();
            patchable.Tags[key] = value;
            return new PhArmOperation<PublicIpAddress, PublicIPAddress>(await Operations.UpdateTagsAsync(Id.ResourceGroup, Id.Name, patchable, cancellationToken),
                n =>
                {
                    Resource = new PublicIPAddressData(n);
                    return new PublicIpAddress(ClientOptions, Resource as PublicIPAddressData);
                });
        }

        protected override ResourceType GetValidResourceType()
        {
            return ResourceType;
        }
    }
}
