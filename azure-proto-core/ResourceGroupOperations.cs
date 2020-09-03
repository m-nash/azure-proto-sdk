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
        internal ResourceGroupOperations(ArmClientBase parent, ResourceIdentifier context) : base(parent, context)
        {
        }

        internal ResourceGroupOperations(ArmClientBase parent, Resource context) : base(parent, context)
        {
        }

        protected override ResourceType ResourceType => "Microsoft.Resources/resourceGroups";

        public override ArmOperation<Response> Delete()
        {
            return new ArmVoidOperation(Operations.StartDelete(Context.Name));
        }

        public async override Task<ArmOperation<Response>> DeleteAsync(CancellationToken cancellationToken = default)
        {
            return new ArmVoidOperation(await Operations.StartDeleteAsync(Context.Name, cancellationToken));
        }

        public override Response<ResourceOperations<PhResourceGroup>> Get()
        {
            return new PhArmResponse<ResourceOperations<PhResourceGroup>, ResourceGroup>(Operations.Get(Context.Name), g => { this.Resource = new PhResourceGroup(g); return this; });
        }

        public async override Task<Response<ResourceOperations<PhResourceGroup>>> GetAsync(CancellationToken cancellationToken = default)
        {
            return new PhArmResponse<ResourceOperations<PhResourceGroup>, ResourceGroup>(await Operations.GetAsync(Context.Name, cancellationToken), g => { this.Resource = new PhResourceGroup(g); return this; });
        }

        public override ArmOperation<ResourceOperations<PhResourceGroup>> AddTag(string name, string value)
        {
            var patch = new ResourceGroupPatchable();
            patch.Tags[name] = value;
            return new PhArmOperation<ResourceOperations<PhResourceGroup>, ResourceGroup>(Operations.Update(Context.Name, patch), g => { this.Resource = new PhResourceGroup(g); return this; });
        }

        public async override Task<ArmOperation<ResourceOperations<PhResourceGroup>>> AddTagAsync(string name, string value, CancellationToken cancellationToken = default)
        {
            var patch = new ResourceGroupPatchable();
            patch.Tags[name] = value;
            return new PhArmOperation<ResourceOperations<PhResourceGroup>, ResourceGroup>(await Operations.UpdateAsync(Context.Name, patch, cancellationToken), g => { this.Resource = new PhResourceGroup(g); return this; });
        }

        public Pageable<ResourceOperations<T>> ListResource<T>(ArmSubstringFilter filter = null, int? top = null, CancellationToken cancellationToken = default) where T : TrackedResource
        {
            ResourceCollectionOperations<T> collection;
            if (!ArmClient.Registry.TryGetColletcion<T>(this, Context, out collection))
            {
                throw new InvalidOperationException($"No resource type matching '{typeof(T)}' found.");
            }

            return collection.List(filter, top, cancellationToken);
        }

        public AsyncPageable<ResourceOperations<T>> ListResourceAsync<T>(ArmSubstringFilter filter = null, int? top = null, CancellationToken cancellationToken = default) where T : TrackedResource
        {
            ResourceCollectionOperations<T> collection;
            if (!ArmClient.Registry.TryGetColletcion<T>(this, Context, out collection))
            {
                throw new InvalidOperationException($"No resource type matching '{typeof(T)}' found.");
            }

            return collection.ListAsync(filter, top, cancellationToken);
        }

        public ArmOperation<ResourceOperations<T>> CreateResource<T>(string name, T model, azure_proto_core.Location location = default) where T : TrackedResource
        {

            var myResource = Resource as TrackedResource;

            if (myResource == null)
            {
                myResource = new ArmResource(Context);
            }

            if (location != null)
            {
                myResource = new ArmResource(Context, location);
            }

            ResourceContainerOperations<T> container;
            if (!ArmClient.Registry.TryGetContainer<T>(this, myResource, out container))
            {
                throw new InvalidOperationException($"No resource type matching '{typeof(T)}' found.");
            }

            return container.Create(name, model);
        }

        public Task<ArmOperation<ResourceOperations<T>>> CreateResourceAsync<T>(string name, T model, azure_proto_core.Location location = default, CancellationToken token = default) where T : TrackedResource
        {

            var myResource = Resource as TrackedResource;

            if (myResource == null)
            {
                myResource = new ArmResource(Context);
            }

            if (location != null)
            {
                myResource = new ArmResource(Context, location);
            }

            ResourceContainerOperations<T> container;
            if (!ArmClient.Registry.TryGetContainer<T>(this, myResource, out container))
            {
                throw new InvalidOperationException($"No resource type matching '{typeof(T)}' found.");
            }

            return container.CreateAsync(name, model, token);
        }

        internal ResourceGroupsOperations Operations => GetClient<ResourcesManagementClient>((uri, creds) => new ResourcesManagementClient(uri, Context.Subscription, creds))?.ResourceGroups;
    }
}
