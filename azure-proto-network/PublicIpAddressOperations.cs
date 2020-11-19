using Azure;
using Azure.ResourceManager.Network;
using Azure.ResourceManager.Network.Models;
using azure_proto_core;
using System.Threading;
using System.Threading.Tasks;

namespace azure_proto_network
{
    public class PublicIpAddressOperations : GenericResourcesOperations<PublicIpAddressOperations, PhPublicIPAddress>, ITagable<PublicIpAddressOperations, PhPublicIPAddress>, IDeletableResource<PublicIpAddressOperations, PhPublicIPAddress>
    {
        public PublicIpAddressOperations(ArmClientContext context, ResourceIdentifier id) : base(context, id) { }

        public PublicIpAddressOperations(ArmClientContext context, azure_proto_core.Resource resource) : base(context, resource) { }

        public override ResourceType ResourceType => "Microsoft.Network/publicIpAddresses";

        public ArmOperation<Response> Delete()
        {
            return new ArmVoidOperation(Operations.StartDelete(Id.ResourceGroup, Id.Name));
        }

        public async Task<ArmOperation<Response>> DeleteAsync(CancellationToken cancellationToken = default)
        {
            return new ArmVoidOperation(await Operations.StartDeleteAsync (Id.ResourceGroup, Id.Name, cancellationToken));
        }

        public override ArmResponse<PublicIpAddressOperations> Get()
        {
            return new PhArmResponse<PublicIpAddressOperations, PublicIPAddress>(Operations.Get(Id.ResourceGroup, Id.Name), 
                n => { Resource = new PhPublicIPAddress(n); return this; });
        }

        public async override Task<ArmResponse<PublicIpAddressOperations>> GetAsync(CancellationToken cancellationToken = default)
        {
            return new PhArmResponse<PublicIpAddressOperations, PublicIPAddress>(await Operations.GetAsync(Id.ResourceGroup, Id.Name, null, cancellationToken),
               n => { Resource = new PhPublicIPAddress(n); return this; });
        }

        public ArmOperation<PublicIpAddressOperations> AddTag(string key, string value)
        {
            var patchable = new TagsObject();
            patchable.Tags[key] = value;
            return new PhArmOperation<PublicIpAddressOperations, PublicIPAddress>(Operations.UpdateTags(Id.ResourceGroup, Id.Name, patchable),
                n => { Resource = new PhPublicIPAddress(n); return this; });
        }

        public async Task<ArmOperation<PublicIpAddressOperations>> AddTagAsync(string key, string value, CancellationToken cancellationToken = default)
        {
            var patchable = new TagsObject();
            patchable.Tags[key] = value;
            return new PhArmOperation<PublicIpAddressOperations, PublicIPAddress>(await Operations.UpdateTagsAsync(Id.ResourceGroup, Id.Name, patchable, cancellationToken),
                n => { Resource = new PhPublicIPAddress(n); return this; });
        }

        internal PublicIPAddressesOperations Operations => GetClient<NetworkManagementClient>((uri, cred) => new NetworkManagementClient(Id.Subscription, uri, cred)).PublicIPAddresses;
    }
}
