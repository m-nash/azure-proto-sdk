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
    public class VnetOperations : ArmResourceOperations<PhVirtualNetwork, TagsObject, ArmOperation<PhVirtualNetwork>, ArmOperation<Response>>
    {
        public VnetOperations(ArmOperations parent, ResourceIdentifier context) : base(parent, context)
        {
        }

        public VnetOperations(ArmOperations parent, azure_proto_core.Resource context) : base(parent, context)
        {
        }

        protected override ResourceType ResourceType => "Microsoft.Network/virtualNetworks";

        public override ArmOperation<Response> Delete()
        {
            return new ArmVoidOperation(Operations.StartDelete(Context.ResourceGroup, Context.Name));
        }

        public async override Task<ArmOperation<Response>> DeleteAsync(CancellationToken cancellationToken = default)
        {
            return new ArmVoidOperation(await Operations.StartDeleteAsync(Context.ResourceGroup, Context.Name, cancellationToken));
        }

        public override Response<PhVirtualNetwork> Get()
        {
            return new PhResponse<PhVirtualNetwork, VirtualNetwork>(Operations.Get(Context.ResourceGroup, Context.Name), n => new PhVirtualNetwork(n));
        }

        public async override Task<Response<PhVirtualNetwork>> GetAsync(CancellationToken cancellationToken = default)
        {
            return new PhResponse<PhVirtualNetwork, VirtualNetwork>(await Operations.GetAsync(Context.ResourceGroup, Context.Name, null, cancellationToken), n => new PhVirtualNetwork(n));
        }

        public override ArmOperation<PhVirtualNetwork> Update(TagsObject patchable)
        {
            return new PhArmOperation<PhVirtualNetwork, VirtualNetwork>(Operations.UpdateTags(Context.ResourceGroup, Context.Name, patchable), n => new PhVirtualNetwork(n));
        }

        public async override Task<ArmOperation<PhVirtualNetwork>> UpdateAsync(TagsObject patchable, CancellationToken cancellationToken = default)
        {
            return new PhArmOperation<PhVirtualNetwork, VirtualNetwork>(await Operations.UpdateTagsAsync(Context.ResourceGroup, Context.Name, patchable, cancellationToken), n => new PhVirtualNetwork(n));
        }

        public SubnetContainer Subnets()
        {
            return new SubnetContainer(this, Context);
        }

        public SubnetOperations Subnet(TrackedResource subnet)
        {
            return new SubnetOperations(this, subnet);
        }

        public SubnetOperations Subnet(ResourceIdentifier subnet)
        {
            return new SubnetOperations(this, subnet);
        }

        public SubnetOperations Subnet(string subnet)
        {
            return new SubnetOperations(this, $"{this.Context}/subnets/{subnet}");
        }



        internal VirtualNetworksOperations Operations => GetClient<NetworkManagementClient>((uri, cred) => new NetworkManagementClient(Context.Subscription, uri, cred)).VirtualNetworks;
    }
}
