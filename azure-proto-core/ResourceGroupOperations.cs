using Azure;
using Azure.ResourceManager.Resources;
using Azure.ResourceManager.Resources.Models;
using azure_proto_core.Resources;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace azure_proto_core
{
    public class PhResourceGroupPatchable : IPatchModel
    {
        public IDictionary<string, string> Tags => throw new NotImplementedException();
    }
    public class ResourceGroupOperations : ResourceOperations<PhResourceGroup>
    {
        internal ResourceGroupOperations(ArmOperations parent, ResourceIdentifier context) : base(parent, context)
        {
        }

        internal ResourceGroupOperations(ArmOperations parent, Resource context) : base(parent, context)
        {
        }

        protected override ResourceType ResourceType => "Microsoft.Resources/resourceGroups";

        public override ArmOperation<Response> Delete()
        {
            return new ArmVoidOperation(GetOperationsClient(Context.Subscription).StartDelete(Context.Name));
        }

        public async override Task<ArmOperation<Response>> DeleteAsync(CancellationToken cancellationToken = default)
        {
            return new ArmVoidOperation(await GetOperationsClient(Context.Subscription).StartDeleteAsync(Context.Name, cancellationToken));
        }

        public override Response<PhResourceGroup> Get()
        {
            return new PhResponse<PhResourceGroup, ResourceGroup>(GetOperationsClient(Context.Subscription).Get(Context.Name), g => new PhResourceGroup(g));
        }

        public async override Task<Response<PhResourceGroup>> GetAsync(CancellationToken cancellationToken = default)
        {
            return new PhResponse<PhResourceGroup, ResourceGroup>(await GetOperationsClient(Context.Subscription).GetAsync(Context.Name, cancellationToken), g => new PhResourceGroup(g));
        }

        public override ArmOperation<PhResourceGroup> Update(IPatchModel patchable)
        {
            var patch = new ResourceGroupPatchable();
            foreach (var tuple in patchable.Tags) { patch.Tags.Add(tuple); }
            return new PhArmOperation<PhResourceGroup, ResourceGroup>(GetOperationsClient(Context.Subscription).Update(Context.Name, patch), g => new PhResourceGroup(g));
        }

        public async override Task<ArmOperation<PhResourceGroup>> UpdateAsync(IPatchModel patchable, CancellationToken cancellationToken = default)
        {
            var patch = new ResourceGroupPatchable();
            foreach (var tuple in patchable.Tags) { patch.Tags.Add(tuple); }
            return new PhArmOperation<PhResourceGroup, ResourceGroup>(await GetOperationsClient(Context.Subscription).UpdateAsync(Context.Name, patch, cancellationToken), g => new PhResourceGroup(g));
        }

        public Pageable<ResourceOperations<T>> ListResource<T>(ArmSubstringFilter filter = null, int? top = null, CancellationToken cancellationToken = default) where T : TrackedResource
        {
            ResourceCollectionOperations<T> collection;
            if (!ArmClient.Registry.TryGetColletcion<T>(this, $"/subscriptions/{DefaultSubscription}", out collection))
            {
                throw new InvalidOperationException($"No resource type matching '{typeof(T)}' found.");
            }

            return collection.List(filter, top, cancellationToken);
        }

        public AsyncPageable<ResourceOperations<T>> ListResourceAsync<T>(ArmSubstringFilter filter = null, int? top = null, CancellationToken cancellationToken = default) where T : TrackedResource
        {
            ResourceCollectionOperations<T> collection;
            if (!ArmClient.Registry.TryGetColletcion<T>(this, Context.Id, out collection))
            {
                throw new InvalidOperationException($"No resource type matching '{typeof(T)}' found.");
            }

            return collection.ListAsync(filter, top, cancellationToken);
        }



        internal ResourceGroupsOperations GetOperationsClient(string subscription) => GetClient<ResourcesManagementClient>((uri, creds) => new ResourcesManagementClient(uri, subscription, creds))?.ResourceGroups;
    }
}
