using Azure.ResourceManager.Resources;
using Azure.ResourceManager.Resources.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Azure.ResourceManager.Core.Tests
{
    public class TaggableResource : ResourceOperationsBase<ArmResource>, ITaggableResource<ArmResource>
    {
        private string _apiVersion = "2019-06-01";

        public TaggableResource(AzureResourceManagerClientOptions options, ResourceIdentifier id)
            : base(options, id)
        {
        }

        public TaggableResource(AzureResourceManagerClientOptions options, ArmResourceData resource)
            : base(options, resource)
        {
        }

        protected override ResourceType ValidResourceType => ResourceGroupOperations.ResourceType;

        private ResourcesOperations Operations => GetClient<ResourcesManagementClient>((uri, creds) => new ResourcesManagementClient(
            uri,
            Id.Subscription,
            creds,
            ClientOptions.Convert<ResourcesManagementClientOptions>()))?.Resources;

        public override ArmResponse<ArmResource> Get()
        {
            return new PhArmResponse<ArmResource, GenericResource>(
                Operations.GetById(Id, _apiVersion),
                v =>
                {
                    Resource = new ArmResourceData(v);
                    return new ArmResource(ClientOptions, Resource as ArmResourceData);
                });
        }

        public override async Task<ArmResponse<ArmResource>> GetAsync(CancellationToken cancellationToken = default)
        {
            return new PhArmResponse<ArmResource, GenericResource>(
                await Operations.GetByIdAsync(Id, _apiVersion, cancellationToken),
                v =>
                {
                    Resource = new ArmResourceData(v);
                    return new ArmResource(ClientOptions, Resource as ArmResourceData);
                });
        }

        public override void Validate(ResourceIdentifier identifier)
        {
            return;
        }

        public ArmOperation<ArmResource> StartAddTag(string key, string value)
        {
            ArmResource resource = GetResource();
            UpdateTags(key, value, resource.Data.Tags);
            return new PhArmOperation<ArmResource, GenericResource>(
                Operations.StartUpdateById(Id, _apiVersion, resource.Data).WaitForCompletionAsync().ConfigureAwait(false).GetAwaiter().GetResult(),
                v =>
                {
                    Resource = new ArmResourceData(v);
                    return new ArmResource(ClientOptions, Resource as ArmResourceData);
                });
        }

        public async Task<ArmOperation<ArmResource>> StartAddTagAsync(string key, string value, CancellationToken cancellationToken = default)
        {
            ArmResource resource = GetResource();
            UpdateTags(key, value, resource.Data.Tags);
            var op = await Operations.StartUpdateByIdAsync(Id, _apiVersion, resource.Data, cancellationToken);
            return new PhArmOperation<ArmResource, GenericResource>(
                await op.WaitForCompletionAsync(cancellationToken),
                v =>
                {
                    Resource = new ArmResourceData(v);
                    return new ArmResource(ClientOptions, Resource as ArmResourceData);
                });
        }

        public ArmResponse<ArmResource> SetTags(IDictionary<string, string> tags)
        {
            ArmResource resource = GetResource();
            ReplaceTags(tags, resource.Data.Tags);
            return new PhArmResponse<ArmResource, GenericResource>(
                Operations.StartUpdateById(Id, _apiVersion, resource.Data).WaitForCompletionAsync().ConfigureAwait(false).GetAwaiter().GetResult(),
                v =>
                {
                    Resource = new ArmResourceData(v);
                    return new ArmResource(ClientOptions, Resource as ArmResourceData);
                });
        }

        public async Task<ArmResponse<ArmResource>> SetTagsAsync(IDictionary<string, string> tags, CancellationToken cancellationToken = default)
        {
            ArmResource resource = GetResource();
            ReplaceTags(tags, resource.Data.Tags);
            var op = await Operations.StartUpdateByIdAsync(Id, _apiVersion, resource.Data, cancellationToken);
            return new PhArmResponse<ArmResource, GenericResource>(
                await op.WaitForCompletionAsync(cancellationToken),
                v =>
                {
                    Resource = new ArmResourceData(v);
                    return new ArmResource(ClientOptions, Resource as ArmResourceData);
                });
        }

        public ArmOperation<ArmResource> StartSetTags(IDictionary<string, string> tags)
        {
            ArmResource resource = GetResource();
            ReplaceTags(tags, resource.Data.Tags);
            return new PhArmOperation<ArmResource, GenericResource>(
                Operations.StartUpdateById(Id, _apiVersion, resource.Data).WaitForCompletionAsync().ConfigureAwait(false).GetAwaiter().GetResult(),
                v =>
                {
                    Resource = new ArmResourceData(v);
                    return new ArmResource(ClientOptions, Resource as ArmResourceData);
                });
        }

        public async Task<ArmOperation<ArmResource>> StartSetTagsAsync(IDictionary<string, string> tags, CancellationToken cancellationToken = default)
        {
            ArmResource resource = GetResource();
            ReplaceTags(tags, resource.Data.Tags);
            var op = await Operations.StartUpdateByIdAsync(Id, _apiVersion, resource.Data, cancellationToken);
            return new PhArmOperation<ArmResource, GenericResource>(
                await op.WaitForCompletionAsync(cancellationToken),
                v =>
                {
                    Resource = new ArmResourceData(v);
                    return new ArmResource(ClientOptions, Resource as ArmResourceData);
                });
        }

        public ArmResponse<ArmResource> RemoveTag(string key)
        {
            ArmResource resource = GetResource();
            DeleteTag(key, resource.Data.Tags);
            return new PhArmResponse<ArmResource, GenericResource>(
                Operations.StartUpdateById(Id, _apiVersion, resource.Data).WaitForCompletionAsync().ConfigureAwait(false).GetAwaiter().GetResult(),
                v =>
                {
                    Resource = new ArmResourceData(v);
                    return new ArmResource(ClientOptions, Resource as ArmResourceData);
                });
        }

        public async Task<ArmResponse<ArmResource>> RemoveTagAsync(string key, CancellationToken cancellationToken = default)
        {
            ArmResource resource = GetResource();
            DeleteTag(key, resource.Data.Tags);
            var op = await Operations.StartUpdateByIdAsync(Id, _apiVersion, resource.Data, cancellationToken);
            return new PhArmResponse<ArmResource, GenericResource>(
                await op.WaitForCompletionAsync(cancellationToken),
                v =>
                {
                    Resource = new ArmResourceData(v);
                    return new ArmResource(ClientOptions, Resource as ArmResourceData);
                });
        }

        public ArmOperation<ArmResource> StartRemoveTag(string key)
        {
            ArmResource resource = GetResource();
            DeleteTag(key, resource.Data.Tags);
            return new PhArmOperation<ArmResource, GenericResource>(
                Operations.StartUpdateById(Id, _apiVersion, resource.Data).WaitForCompletionAsync().ConfigureAwait(false).GetAwaiter().GetResult(),
                v =>
                {
                    Resource = new ArmResourceData(v);
                    return new ArmResource(ClientOptions, Resource as ArmResourceData);
                });
        }

        public async Task<ArmOperation<ArmResource>> StartRemoveTagAsync(string key, CancellationToken cancellationToken = default)
        {
            ArmResource resource = GetResource();
            DeleteTag(key, resource.Data.Tags);
            var op = await Operations.StartUpdateByIdAsync(Id, _apiVersion, resource.Data, cancellationToken);
            return new PhArmOperation<ArmResource, GenericResource>(
                await op.WaitForCompletionAsync(cancellationToken),
                v =>
                {
                    Resource = new ArmResourceData(v);
                    return new ArmResource(ClientOptions, Resource as ArmResourceData);
                });
        }

        private protected virtual ArmResource GetResource()
        {
            return Get().Value;
        }
    }
}
