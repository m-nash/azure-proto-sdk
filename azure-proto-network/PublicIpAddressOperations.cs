using Azure;
using Azure.ResourceManager.Network;
using Azure.ResourceManager.Network.Models;
using azure_proto_core;
using System.Threading;
using System.Threading.Tasks;

namespace azure_proto_network
{
    public class PublicIpAddressOperations : ResourceOperationsBase<PhPublicIPAddress>
    {
        public PublicIpAddressOperations(ArmClientContext parent, ResourceIdentifier context) : base(parent, context)
        {
        }

        public PublicIpAddressOperations(ArmClientContext parent, azure_proto_core.Resource context) : base(parent, context)
        {
        }

        public PublicIpAddressOperations(OperationsBase parent, ResourceIdentifier context) : base(parent, context)
        {
        }

        public PublicIpAddressOperations(OperationsBase parent, azure_proto_core.Resource context) : base(parent, context)
        {
        }

        public override ResourceType ResourceType => "Microsoft.Network/publicIpAddresses";

        public override ArmOperation<Response> Delete()
        {
            return new ArmVoidOperation(Operations.StartDelete(Id.ResourceGroup, Id.Name));
        }

        public async override Task<ArmOperation<Response>> DeleteAsync(CancellationToken cancellationToken = default)
        {
            return new ArmVoidOperation(await Operations.StartDeleteAsync (Id.ResourceGroup, Id.Name, cancellationToken));
        }

        public override ArmResponse<ResourceOperationsBase<PhPublicIPAddress>> Get()
        {
            return new PhArmResponse<ResourceOperationsBase<PhPublicIPAddress>, PublicIPAddress>(Operations.Get(Id.ResourceGroup, Id.Name), 
                n => { Resource = new PhPublicIPAddress(n); return this; });
        }

        public async override Task<ArmResponse<ResourceOperationsBase<PhPublicIPAddress>>> GetAsync(CancellationToken cancellationToken = default)
        {
            return new PhArmResponse<ResourceOperationsBase<PhPublicIPAddress>, PublicIPAddress>(await Operations.GetAsync(Id.ResourceGroup, Id.Name, null, cancellationToken),
               n => { Resource = new PhPublicIPAddress(n); return this; });
        }

        public override ArmOperation<ResourceOperationsBase<PhPublicIPAddress>> AddTag(string key, string value)
        {
            var patchable = new TagsObject();
            patchable.Tags[key] = value;
            return new PhArmOperation<ResourceOperationsBase<PhPublicIPAddress>, PublicIPAddress>(Operations.UpdateTags(Id.ResourceGroup, Id.Name, patchable),
                n => { Resource = new PhPublicIPAddress(n); return this; });
        }

        public async override Task<ArmOperation<ResourceOperationsBase<PhPublicIPAddress>>> AddTagAsync(string key, string value, CancellationToken cancellationToken = default)
        {
            var patchable = new TagsObject();
            patchable.Tags[key] = value;
            return new PhArmOperation<ResourceOperationsBase<PhPublicIPAddress>, PublicIPAddress>(await Operations.UpdateTagsAsync(Id.ResourceGroup, Id.Name, patchable, cancellationToken),
                n => { Resource = new PhPublicIPAddress(n); return this; });
        }

        internal PublicIPAddressesOperations Operations => GetClient<NetworkManagementClient>((uri, cred) => new NetworkManagementClient(Id.Subscription, uri, cred)).PublicIPAddresses;
    }
}
