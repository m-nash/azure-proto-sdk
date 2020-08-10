using Azure;
using Azure.ResourceManager.Resources;
using Azure.ResourceManager.Resources.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace azure_proto_core
{
    public class PhResourceGroupPatchable
    {

    }
    public class ResourceGroupOperations : ArmResourceOperations<PhResourceGroup, ResourceGroupPatchable, Response<PhResourceGroup>, PhVoidOperation>
    {
        internal ResourceGroupOperations(ArmOperations parent, ResourceIdentifier context) : base(parent, context)
        {
        }

        internal ResourceGroupOperations(ArmOperations parent, Resource context) : base(parent, context)
        {
        }

        protected override ResourceType ResourceType => "Microsoft.Resources/resourceGroups";

        public override PhVoidOperation Delete()
        {
            return new PhVoidOperation(GetOperationsClient(Context.Subscription).StartDelete(Context.Name));
        }

        public async override Task<PhVoidOperation> DeleteAsync(CancellationToken cancellationToken = default)
        {
            return new PhVoidOperation(await GetOperationsClient(Context.Subscription).StartDeleteAsync(Context.Name, cancellationToken));
        }

        public override Response<PhResourceGroup> Get()
        {
            return new PhResponse<PhResourceGroup, ResourceGroup>(GetOperationsClient(Context.Subscription).Get(Context.Name), g => new PhResourceGroup(g));
        }

        public async override Task<Response<PhResourceGroup>> GetAsync(CancellationToken cancellationToken = default)
        {
            return new PhResponse<PhResourceGroup, ResourceGroup>(await GetOperationsClient(Context.Subscription).GetAsync(Context.Name, cancellationToken), g => new PhResourceGroup(g));
        }

        public override Response<PhResourceGroup> Update(ResourceGroupPatchable patchable)
        {
            return new PhResponse<PhResourceGroup, ResourceGroup>(GetOperationsClient(Context.Subscription).Update(Context.Name, patchable), g => new PhResourceGroup(g));
        }

        public async override Task<Response<PhResourceGroup>> UpdateAsync(ResourceGroupPatchable patchable, CancellationToken cancellationToken = default)
        {
            return new PhResponse<PhResourceGroup, ResourceGroup>(await GetOperationsClient(Context.Subscription).UpdateAsync(Context.Name, patchable, cancellationToken), g => new PhResourceGroup(g));
        }

        internal ResourceGroupsOperations GetOperationsClient(string subscription) => GetClient<ResourcesManagementClient>((uri, creds) => new ResourcesManagementClient(uri, subscription, creds))?.ResourceGroups;
    }
}
