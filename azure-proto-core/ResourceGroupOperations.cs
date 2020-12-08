// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT License.

using System;
using System.Threading;
using System.Threading.Tasks;
using Azure;
using Azure.ResourceManager.Resources;
using Azure.ResourceManager.Resources.Models;

namespace azure_proto_core
{
    public class ResourceGroupOperations : ResourceOperationsBase<ResourceGroup, ResourceGroupData>,
        ITaggable<ResourceGroup, ResourceGroupData>, IDeletableResource<ResourceGroup, ResourceGroupData>
    {
        public static readonly string AzureResourceType = "Microsoft.Resources/resourceGroups";

        internal ResourceGroupOperations(ArmClientContext context, ResourceIdentifier id, ArmClientOptions clientOptions)
            : base(context, id, clientOptions) { }

        internal ResourceGroupOperations(ArmClientContext context, Resource resource, ArmClientOptions clientOptions)
            : base(context, resource, clientOptions) { }

        public override ResourceType ResourceType => AzureResourceType;

        internal ResourceGroupsOperations Operations => GetClient<ResourcesManagementClient>((uri, creds) => new ResourcesManagementClient(uri, Id.Subscription, creds,
            ArmClientOptions.Convert<ResourcesManagementClientOptions>(ClientOptions)))?.ResourceGroups;

        public ArmOperation<Response> Delete()
        {
            return new ArmVoidOperation(Operations.StartDelete(Id.Name));
        }

        public async Task<ArmOperation<Response>> DeleteAsync(CancellationToken cancellationToken = default)
        {
            return new ArmVoidOperation(await Operations.StartDeleteAsync(Id.Name, cancellationToken));
        }

        public override ArmResponse<ResourceGroup> Get()
        {
            return new PhArmResponse<ResourceGroup, Azure.ResourceManager.Resources.Models.ResourceGroup>(Operations.Get(Id.Name), g =>
            {
                this.Resource = new ResourceGroupData(g); return new ResourceGroup(ClientContext, Resource as (, ClientOptions);
            });
        }

        public async override Task<ArmResponse<ResourceGroup>> GetAsync(CancellationToken cancellationToken = default)
        {
            return new PhArmResponse<ResourceGroup, Azure.ResourceManager.Resources.Models.ResourceGroup>(await Operations.GetAsync(Id.Name, cancellationToken), g =>
            {
                this.Resource = new ResourceGroupData(g); return new ResourceGroup(ClientContext, Resource as ResourceGroupData, ClientOptions);
            });
        }

        public ArmOperation<ResourceGroup> AddTag(string name, string value)
        {
            var patch = new ResourceGroupPatchable();
            patch.Tags[name] = value;
            return new PhArmOperation<ResourceGroup, Azure.ResourceManager.Resources.Models.ResourceGroup>(Operations.Update(Id.Name, patch), g =>
            {
                this.Resource = new ResourceGroupData(g); return new ResourceGroup(ClientContext, Resource as ResourceGroupData, ClientOptions);
            });
        }

        public async Task<ArmOperation<ResourceGroup>> AddTagAsync(string name, string value, CancellationToken cancellationToken = default)
        {
            var patch = new ResourceGroupPatchable();
            patch.Tags[name] = value;
            return new PhArmOperation<ResourceGroup, Azure.ResourceManager.Resources.Models.ResourceGroup>(await Operations.UpdateAsync(Id.Name, patch, cancellationToken), g =>
            {
                this.Resource = new ResourceGroupData(g); return new ResourceGroup(ClientContext, Resource as ResourceGroupData, ClientOptions);
            });
        }

        public ArmResponse<TOperations> CreateResource<TContainer, TOperations, TResource>(string name, TResource model, azure_proto_core.Location location = default)
            where TResource : TrackedResource
            where TOperations : ResourceOperationsBase<TOperations, TResource>
            where TContainer : ResourceContainerOperations<TOperations, TResource>
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

            TContainer container = Activator.CreateInstance(typeof(TContainer), ClientContext, myResource) as TContainer;

            return container.Create(name, model);
        }

        public Task<ArmResponse<TOperations>> CreateResourceAsync<TContainer, TOperations, TResource>(
            string name, TResource model,
            azure_proto_core.Location location = default,
            CancellationToken token = default)
            where TResource : TrackedResource
            where TOperations : ResourceOperationsBase<TOperations, TResource>
            where TContainer : ResourceContainerOperations<TOperations, TResource>
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

            TContainer container = Activator.CreateInstance(typeof(TContainer), ClientContext, myResource) as TContainer;

            return container.CreateAsync(name, model, token);
        }
    }
}