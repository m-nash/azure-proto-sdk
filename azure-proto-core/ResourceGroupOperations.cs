using Azure;
using Azure.ResourceManager.Resources;
using Azure.ResourceManager.Resources.Models;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace azure_proto_core
{
    public class ResourceGroupOperations : ResourceOperationsBase<PhResourceGroup>
    {
        internal ResourceGroupOperations(OperationsBase parent, ResourceIdentifier context) : base(parent, context) { }

        internal ResourceGroupOperations(OperationsBase parent, Resource context) : base(parent, context) { }

        internal ResourceGroupOperations(ArmClientContext parent, ResourceIdentifier context) : base(parent, context) { }

        internal ResourceGroupOperations(ArmClientContext parent, Resource context) : base(parent, context) { }

        public override ResourceType ResourceType => "Microsoft.Resources/resourceGroups";

        public override ArmOperation<Response> Delete()
        {
            return new ArmVoidOperation(Operations.StartDelete(Id.Name));
        }

        public async override Task<ArmOperation<Response>> DeleteAsync(CancellationToken cancellationToken = default)
        {
            return new ArmVoidOperation(await Operations.StartDeleteAsync(Id.Name, cancellationToken));
        }

        public override ArmResponse<ResourceOperationsBase<PhResourceGroup>> Get()
        {
            return new PhArmResponse<ResourceOperationsBase<PhResourceGroup>, ResourceGroup>(Operations.Get(Id.Name), g => { this.Resource = new PhResourceGroup(g); return this; });
        }

        public async override Task<ArmResponse<ResourceOperationsBase<PhResourceGroup>>> GetAsync(CancellationToken cancellationToken = default)
        {
            return new PhArmResponse<ResourceOperationsBase<PhResourceGroup>, ResourceGroup>(await Operations.GetAsync(Id.Name, cancellationToken), g => { this.Resource = new PhResourceGroup(g); return this; });
        }

        public override ArmOperation<ResourceOperationsBase<PhResourceGroup>> AddTag(string name, string value)
        {
            var patch = new ResourceGroupPatchable();
            patch.Tags[name] = value;
            return new PhArmOperation<ResourceOperationsBase<PhResourceGroup>, ResourceGroup>(Operations.Update(Id.Name, patch), g => { this.Resource = new PhResourceGroup(g); return this; });
        }

        public async override Task<ArmOperation<ResourceOperationsBase<PhResourceGroup>>> AddTagAsync(string name, string value, CancellationToken cancellationToken = default)
        {
            var patch = new ResourceGroupPatchable();
            patch.Tags[name] = value;
            return new PhArmOperation<ResourceOperationsBase<PhResourceGroup>, ResourceGroup>(await Operations.UpdateAsync(Id.Name, patch, cancellationToken), g => { this.Resource = new PhResourceGroup(g); return this; });
        }

        public ArmResponse<ResourceOperationsBase<T>> CreateResource<T>(string name, T model, azure_proto_core.Location location = default) where T : TrackedResource
        {

            var myResource = Resource as TrackedResource;

            if (myResource == null)
            {
                myResource = new ArmResource(Id);
            }

            if (location != null)
            {
                myResource = new ArmResource(Id, location);
            }

            ResourceContainerOperations<T> container;
            if (!ArmClient.Registry.TryGetContainer<T>(this.ClientContext, myResource, out container))
            {
                throw new InvalidOperationException($"No resource type matching '{typeof(T)}' found.");
            }

            return container.Create(name, model);
        }

        public Task<ArmResponse<ResourceOperationsBase<T>>> CreateResourceAsync<T>(string name, T model, azure_proto_core.Location location = default, CancellationToken token = default) where T : TrackedResource
        {

            var myResource = Resource as TrackedResource;

            if (myResource == null)
            {
                myResource = new ArmResource(Id);
            }

            if (location != null)
            {
                myResource = new ArmResource(Id, location);
            }

            ResourceContainerOperations<T> container;
            if (!ArmClient.Registry.TryGetContainer<T>(this.ClientContext, myResource, out container))
            {
                throw new InvalidOperationException($"No resource type matching '{typeof(T)}' found.");
            }

            return container.CreateAsync(name, model, token);
        }

        internal ResourceGroupsOperations Operations => GetClient<ResourcesManagementClient>((uri, creds) => new ResourcesManagementClient(uri, Id.Subscription, creds))?.ResourceGroups;
    }
}
