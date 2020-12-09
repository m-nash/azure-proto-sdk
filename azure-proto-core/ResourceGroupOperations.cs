﻿using Azure;
using Azure.ResourceManager.Resources;
using Azure.ResourceManager.Resources.Models;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace azure_proto_core
{
    public class ResourceGroupOperations : ResourceOperationsBase<XResourceGroup, PhResourceGroup>, ITaggable<XResourceGroup, PhResourceGroup>, IDeletableResource<XResourceGroup, PhResourceGroup>
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

        public override ArmResponse<XResourceGroup> Get()
        {
            return new PhArmResponse<XResourceGroup, ResourceGroup>(Operations.Get(Id.Name), g =>
            {
                this.Resource = new PhResourceGroup(g); return new XResourceGroup(ClientContext, Resource as PhResourceGroup, ClientOptions);
            });
        }

        public async override Task<ArmResponse<XResourceGroup>> GetAsync(CancellationToken cancellationToken = default)
        {
            return new PhArmResponse<XResourceGroup, ResourceGroup>(await Operations.GetAsync(Id.Name, cancellationToken), g =>
            {
                this.Resource = new PhResourceGroup(g); return new XResourceGroup(ClientContext, Resource as PhResourceGroup, ClientOptions);
            });
        }

        public ArmOperation<XResourceGroup> AddTag(string name, string value)
        {
            var patch = new ResourceGroupPatchable();
            patch.Tags[name] = value;
            return new PhArmOperation<XResourceGroup, ResourceGroup>(Operations.Update(Id.Name, patch), g =>
            {
                this.Resource = new PhResourceGroup(g); return new XResourceGroup(ClientContext, Resource as PhResourceGroup, ClientOptions);
            });
        }

        public async Task<ArmOperation<XResourceGroup>> AddTagAsync(string name, string value, CancellationToken cancellationToken = default)
        {
            var patch = new ResourceGroupPatchable();
            patch.Tags[name] = value;
            return new PhArmOperation<XResourceGroup, ResourceGroup>(await Operations.UpdateAsync(Id.Name, patch, cancellationToken), g =>
            {
                this.Resource = new PhResourceGroup(g); return new XResourceGroup(ClientContext, Resource as PhResourceGroup, ClientOptions);
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