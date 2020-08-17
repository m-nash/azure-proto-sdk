using Azure;
using Azure.ResourceManager.Network;
using Azure.ResourceManager.Network.Models;
using azure_proto_core;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace azure_proto_network
{
    public class PublicIpOperations : ArmResourceOperations<PhPublicIPAddress, TagsObject, ArmOperation<PhPublicIPAddress>, ArmOperation<Response>>
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

        public override Response<PhPublicIPAddress> Get()
        {
            return new PhResponse<PhPublicIPAddress, PublicIPAddress>(Operations.Get(Context.ResourceGroup, Context.Name), n => new PhPublicIPAddress(n));
        }

        public async override Task<Response<PhPublicIPAddress>> GetAsync(CancellationToken cancellationToken = default)
        {
            return new PhResponse<PhPublicIPAddress, PublicIPAddress>(await Operations.GetAsync(Context.ResourceGroup, Context.Name, null, cancellationToken), n => new PhPublicIPAddress(n));
        }

        public override ArmOperation<PhPublicIPAddress> Update(TagsObject patchable)
        {
            return new PhArmOperation<PhPublicIPAddress, PublicIPAddress>(Operations.UpdateTags(Context.ResourceGroup, Context.Name, patchable), n => new PhPublicIPAddress(n));
        }

        public async override Task<ArmOperation<PhPublicIPAddress>> UpdateAsync(TagsObject patchable, CancellationToken cancellationToken = default)
        {
            return new PhArmOperation<PhPublicIPAddress, PublicIPAddress>(await Operations.UpdateTagsAsync(Context.ResourceGroup, Context.Name, patchable, cancellationToken), n => new PhPublicIPAddress(n));
        }
        internal PublicIPAddressesOperations Operations => GetClient<NetworkManagementClient>((uri, cred) => new NetworkManagementClient(Context.Subscription, uri, cred)).PublicIPAddresses;
    }
}
