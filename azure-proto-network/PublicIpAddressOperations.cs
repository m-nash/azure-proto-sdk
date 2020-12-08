using Azure;
using Azure.ResourceManager.Network;
using Azure.ResourceManager.Network.Models;
using azure_proto_core;
using System.Threading;
using System.Threading.Tasks;

namespace azure_proto_network
{
    public class PublicIpAddressOperations : ResourceOperationsBase<PublicIpAddress, PublicIPAddressData>, ITaggable<PublicIpAddress, PublicIPAddressData>, IDeletableResource<PublicIpAddress, PublicIPAddressData>
    {
        public PublicIpAddressOperations(ArmClientContext context, ResourceIdentifier id) : base(context, id) { }

        public override ResourceType ResourceType => "Microsoft.Network/publicIpAddresses";

        public ArmOperation<Response> Delete()
        {
            return new ArmVoidOperation(Operations.StartDelete(Id.ResourceGroup, Id.Name));
        }

        public async Task<ArmOperation<Response>> DeleteAsync(CancellationToken cancellationToken = default)
        {
            return new ArmVoidOperation(await Operations.StartDeleteAsync (Id.ResourceGroup, Id.Name, cancellationToken));
        }

        public override ArmResponse<PublicIpAddress> Get()
        {
            return new PhArmResponse<PublicIpAddress, PublicIPAddress>(Operations.Get(Id.ResourceGroup, Id.Name), 
                n => { Resource = new PublicIPAddressData(n); return new PublicIpAddress(ClientContext, Resource as PublicIPAddressData); });
        }

        public async override Task<ArmResponse<PublicIpAddress>> GetAsync(CancellationToken cancellationToken = default)
        {
            return new PhArmResponse<PublicIpAddress, PublicIPAddress>(await Operations.GetAsync(Id.ResourceGroup, Id.Name, null, cancellationToken),
               n => { Resource = new PublicIPAddressData(n); return new PublicIpAddress(ClientContext, Resource as PublicIPAddressData); });
        }

        public ArmOperation<PublicIpAddress> AddTag(string key, string value)
        {
            var patchable = new TagsObject();
            patchable.Tags[key] = value;
            return new PhArmOperation<PublicIpAddress, PublicIPAddress>(Operations.UpdateTags(Id.ResourceGroup, Id.Name, patchable),
                n => { Resource = new PublicIPAddressData(n); return new PublicIpAddress(ClientContext, Resource as PublicIPAddressData); });
        }

        public async Task<ArmOperation<PublicIpAddress>> AddTagAsync(string key, string value, CancellationToken cancellationToken = default)
        {
            var patchable = new TagsObject();
            patchable.Tags[key] = value;
            return new PhArmOperation<PublicIpAddress, PublicIPAddress>(await Operations.UpdateTagsAsync(Id.ResourceGroup, Id.Name, patchable, cancellationToken),
                n => { Resource = new PublicIPAddressData(n); return new PublicIpAddress(ClientContext, Resource as PublicIPAddressData); });
        }

        internal PublicIPAddressesOperations Operations => GetClient<NetworkManagementClient>((uri, cred) => new NetworkManagementClient(Id.Subscription, uri, cred)).PublicIPAddresses;
    }
}
