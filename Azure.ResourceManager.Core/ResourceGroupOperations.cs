// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT License.

using Azure.ResourceManager.Resources;
using Azure.ResourceManager.Resources.Models;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Azure.ResourceManager.Core
{
    public class ResourceGroupOperations : ResourceOperationsBase<ResourceGroup>,
        ITaggableResource<ResourceGroup>, IDeletableResource
    {
        public static readonly ResourceType ResourceType = "Microsoft.Resources/resourceGroups";

        internal ResourceGroupOperations(AzureResourceManagerClientOptions options, ResourceIdentifier id)
            : base(options, id) { }

        internal ResourceGroupOperations(AzureResourceManagerClientOptions options, Resource resource)
            : base(options, resource) { }

        internal ResourceGroupsOperations Operations => GetClient<ResourcesManagementClient>((uri, creds) => new ResourcesManagementClient(
            uri,
            Id.Subscription,
            creds,
            ClientOptions.Convert<ResourcesManagementClientOptions>()))?.ResourceGroups;

        public ArmResponse<Response> Delete()
        {
            return new ArmResponse(Operations.StartDelete(Id.Name).WaitForCompletionAsync().ConfigureAwait(false).GetAwaiter().GetResult());
        }

        public async Task<ArmResponse<Response>> DeleteAsync(CancellationToken cancellationToken = default)
        {
            return new ArmResponse(await Operations.StartDelete(Id.Name).WaitForCompletionAsync());
        }

        public ArmOperation<Response> StartDelete(CancellationToken cancellationToken = default)
        {
            return new ArmVoidOperation(Operations.StartDelete(Id.Name, cancellationToken));
        }

        public async Task<ArmOperation<Response>> StartDeleteAsync(CancellationToken cancellationToken = default)
        {
            return new ArmVoidOperation(await Operations.StartDeleteAsync(Id.Name, cancellationToken));
        }

        public override ArmResponse<ResourceGroup> Get()
        {
            return new PhArmResponse<ResourceGroup, Azure.ResourceManager.Resources.Models.ResourceGroup>(Operations.Get(Id.Name), g =>
            {
                Resource = new ResourceGroupData(g);
                return new ResourceGroup(ClientOptions, Resource as ResourceGroupData);
            });
        }

        public async override Task<ArmResponse<ResourceGroup>> GetAsync(CancellationToken cancellationToken = default)
        {
            return new PhArmResponse<ResourceGroup, Azure.ResourceManager.Resources.Models.ResourceGroup>(await Operations.GetAsync(Id.Name, cancellationToken), g =>
            {
                Resource = new ResourceGroupData(g);
                return new ResourceGroup(ClientOptions, Resource as ResourceGroupData);
            });
        }

        public ArmOperation<ResourceGroup> AddTag(string name, string value)
        {
            var patch = new ResourceGroupPatchable();
            patch.Tags[name] = value;
            return new PhArmOperation<ResourceGroup, Azure.ResourceManager.Resources.Models.ResourceGroup>(Operations.Update(Id.Name, patch), g =>
            {
                Resource = new ResourceGroupData(g);
                return new ResourceGroup(ClientOptions, Resource as ResourceGroupData);
            });
        }

        public async Task<ArmOperation<ResourceGroup>> AddTagAsync(string name, string value, CancellationToken cancellationToken = default)
        {
            var patch = new ResourceGroupPatchable();
            patch.Tags[name] = value;
            return new PhArmOperation<ResourceGroup, Azure.ResourceManager.Resources.Models.ResourceGroup>(await Operations.UpdateAsync(Id.Name, patch, cancellationToken), g =>
            {
                Resource = new ResourceGroupData(g);
                return new ResourceGroup(ClientOptions, Resource as ResourceGroupData);
            });
        }

        public ArmResponse<TOperations> CreateResource<TContainer, TOperations, TResource>(string name, TResource model, Location location = default)
            where TResource : TrackedResource
            where TOperations : ResourceOperationsBase<TOperations>
            where TContainer : ResourceContainerBase<TOperations, TResource>
        {
            var myResource = Resource as TrackedResource;

            if (myResource == null)
            {
                myResource = new ArmResourceData(Id);
            }

            if (location != null)
            {
                myResource = new ArmResourceData(Id, location);
            }

            TContainer container = Activator.CreateInstance(typeof(TContainer), ClientOptions, myResource) as TContainer;

            return container.Create(name, model);
        }

        public Task<ArmResponse<TOperations>> CreateResourceAsync<TContainer, TOperations, TResource>(string name, TResource model, Location location = default, CancellationToken token = default)
            where TResource : TrackedResource
            where TOperations : ResourceOperationsBase<TOperations>
            where TContainer : ResourceContainerBase<TOperations, TResource>
        {
            var myResource = Resource as TrackedResource;

            if (myResource == null)
            {
                myResource = new ArmResourceData(Id);
            }

            if (location != null)
            {
                myResource = new ArmResourceData(Id, location);
            }

            TContainer container = Activator.CreateInstance(typeof(TContainer), ClientOptions, myResource) as TContainer;

            return container.CreateAsync(name, model, token);
        }

        protected internal override ResourceType GetValidResourceType()
        {
            return ResourceType;
        }
    }
}