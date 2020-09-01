using Azure;
using Azure.ResourceManager.Network;
using Azure.ResourceManager.Network.Models;
using azure_proto_core;
using System.Threading;
using System.Threading.Tasks;

namespace azure_proto_network
{
    public class PublicIpOperations : ResourceOperations<PhPublicIPAddress>
    {
        public PublicIpOperations(ArmOperations parent, ResourceIdentifier context) : base(parent, context)
        {
        }

        public PublicIpOperations(ArmOperations parent, azure_proto_core.Resource context) : base(parent, context)
        {
        }

        protected override ResourceType ResourceType => "Microsoft.Network/publicIpAddresses";

        public override ArmOperation<Response> Delete()
        {
            return new ArmVoidOperation(Operations.StartDelete(Context.ResourceGroup, Context.Name));
        }

        public async override Task<ArmOperation<Response>> DeleteAsync(CancellationToken cancellationToken = default)
        {
            return new ArmVoidOperation(await Operations.StartDeleteAsync (Context.ResourceGroup, Context.Name, cancellationToken));
        }

        public override Response<ResourceOperations<PhPublicIPAddress>> Get()
        {
            return new PhArmResponse<ResourceOperations<PhPublicIPAddress>, PublicIPAddress>(Operations.Get(Context.ResourceGroup, Context.Name), 
                n => { Resource = new PhPublicIPAddress(n); return this; });
        }

        public async override Task<Response<ResourceOperations<PhPublicIPAddress>>> GetAsync(CancellationToken cancellationToken = default)
        {
            return new PhArmResponse<ResourceOperations<PhPublicIPAddress>, PublicIPAddress>(await Operations.GetAsync(Context.ResourceGroup, Context.Name, null, cancellationToken),
               n => { Resource = new PhPublicIPAddress(n); return this; });
        }

        public override ArmOperation<ResourceOperations<PhPublicIPAddress>> AddTag(string key, string value)
        {
            var patchable = new TagsObject();
            patchable.Tags[key] = value;
            return new PhArmOperation<ResourceOperations<PhPublicIPAddress>, PublicIPAddress>(Operations.UpdateTags(Context.ResourceGroup, Context.Name, patchable),
                n => { Resource = new PhPublicIPAddress(n); return this; });
        }

        public async override Task<ArmOperation<ResourceOperations<PhPublicIPAddress>>> AddTagAsync(string key, string value, CancellationToken cancellationToken = default)
        {
            var patchable = new TagsObject();
            patchable.Tags[key] = value;
            return new PhArmOperation<ResourceOperations<PhPublicIPAddress>, PublicIPAddress>(await Operations.UpdateTagsAsync(Context.ResourceGroup, Context.Name, patchable, cancellationToken),
                n => { Resource = new PhPublicIPAddress(n); return this; });
        }
        internal PublicIPAddressesOperations Operations => GetClient<NetworkManagementClient>((uri, cred) => new NetworkManagementClient(Context.Subscription, uri, cred)).PublicIPAddresses;
    }
}
