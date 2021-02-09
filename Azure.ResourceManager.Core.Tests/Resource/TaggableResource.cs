using Azure.ResourceManager.Resources;
using Azure.ResourceManager.Resources.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Azure.ResourceManager.Core.Tests
{
    public class TaggableResource : ResourceOperationsBase<GenericResource>, ITaggableResource<GenericResource>
    {
        private readonly string _apiVersion;

        /// <summary>
        /// Initializes a new instance of the <see cref="TaggableResource"/> class.
        /// </summary>
        /// <param name="operations"> The resource operations to copy the options from. </param>
        /// <param name="id"> The identifier of the resource that is the target of operations. </param>
        internal TaggableResource(ResourceOperationsBase operations, ResourceIdentifier id)
            : base(operations, id)
        {
            _apiVersion = "2019-06-01";
        }

        /// <inheritdoc/>
        protected override ResourceType ValidResourceType => ResourceGroupOperations.ResourceType;

        private ResourcesOperations Operations => new ResourcesManagementClient(
            BaseUri,
            Id.Subscription,
            Credential,
            ClientOptions.Convert<ResourcesManagementClientOptions>()).Resources;


        /// <inheritdoc/>
        public override ArmResponse<GenericResource> Get()
        {
            return new PhArmResponse<GenericResource, ResourceManager.Resources.Models.GenericResource>(
                Operations.GetById(Id, _apiVersion),
                v => new GenericResource(this, new GenericResourceData(v)));
        }

        /// <inheritdoc/>
        public override async Task<ArmResponse<GenericResource>> GetAsync(CancellationToken cancellationToken = default)
        {
            return new PhArmResponse<GenericResource, ResourceManager.Resources.Models.GenericResource>(
                await Operations.GetByIdAsync(Id, _apiVersion, cancellationToken),
                v => new GenericResource(this, new GenericResourceData(v)));
        }

        /// <inheritdoc/>
        public override void Validate(ResourceIdentifier identifier)
        {
        }

        /// <summary>
        /// Add a tag to the resource
        /// </summary>
        /// <param name="key"> The tag key. </param>
        /// <param name="value"> The tag value. </param>
        /// <returns>An <see cref="ArmOperation{TOperations}"/> that allows the user to control polling and waiting for Tag completion.</returns>
        public ArmOperation<GenericResource> StartAddTag(string key, string value)
        {
            GenericResource resource = GetResource();
            UpdateTags(key, value, resource.Data.Tags);
            // TODO: Fix cast error
            ResourceManager.Resources.Models.GenericResource casterror = resource.Data;
            casterror.Tags.Add(key, value);
            return new PhArmOperation<GenericResource, ResourceManager.Resources.Models.GenericResource>(
                Operations.StartUpdateById(Id, _apiVersion, casterror).WaitForCompletionAsync().ConfigureAwait(false).GetAwaiter().GetResult(),
                v => new GenericResource(this, new GenericResourceData(v)));
        }

        /// <summary>
        /// Add a tag to the resource
        /// </summary>
        /// <param name="key"> The tag key. </param>
        /// <param name="value"> The tag value. </param>
        /// <param name="cancellationToken"> A token to allow the caller to cancel the call to the service.
        /// The default value is <see cref="P:System.Threading.CancellationToken.None" />. </param>
        /// <returns> A <see cref="Task"/> that performs the Tag operation.  The Task yields an an
        /// <see cref="ArmOperation{TOperations}"/> that allows the user to control polling and waiting for
        /// Tag completion. </returns>
        public async Task<ArmOperation<GenericResource>> StartAddTagAsync(string key, string value, CancellationToken cancellationToken = default)
        {
            GenericResource resource = GetResource();
            UpdateTags(key, value, resource.Data.Tags);
            var op = await Operations.StartUpdateByIdAsync(Id, _apiVersion, resource.Data, cancellationToken);
            return new PhArmOperation<GenericResource, ResourceManager.Resources.Models.GenericResource>(
                await op.WaitForCompletionAsync(cancellationToken),
                v => new GenericResource(this, new GenericResourceData(v)));
        }

        public ArmResponse<GenericResource> SetTags(IDictionary<string, string> tags)
        {
            GenericResource resource = GetResource();
            ReplaceTags(tags, resource.Data.Tags);
            return new PhArmResponse<GenericResource, ResourceManager.Resources.Models.GenericResource>(
                Operations.StartUpdateById(Id, _apiVersion, resource.Data).WaitForCompletionAsync().ConfigureAwait(false).GetAwaiter().GetResult(),
                v => new GenericResource(this, new GenericResourceData(v)));
        }

        public async Task<ArmResponse<GenericResource>> SetTagsAsync(IDictionary<string, string> tags, CancellationToken cancellationToken = default)
        {
            GenericResource resource = GetResource();
            ReplaceTags(tags, resource.Data.Tags);
            var op = await Operations.StartUpdateByIdAsync(Id, _apiVersion, resource.Data, cancellationToken);
            return new PhArmResponse<GenericResource, ResourceManager.Resources.Models.GenericResource>(
                await op.WaitForCompletionAsync(cancellationToken),
                v => new GenericResource(this, new GenericResourceData(v)));
        }

        public ArmOperation<GenericResource> StartSetTags(IDictionary<string, string> tags)
        {
            GenericResource resource = GetResource();
            ReplaceTags(tags, resource.Data.Tags);
            return new PhArmOperation<GenericResource, ResourceManager.Resources.Models.GenericResource>(
                Operations.StartUpdateById(Id, _apiVersion, resource.Data).WaitForCompletionAsync().ConfigureAwait(false).GetAwaiter().GetResult(),
                v => new GenericResource(this, new GenericResourceData(v)));
        }

        public async Task<ArmOperation<GenericResource>> StartSetTagsAsync(IDictionary<string, string> tags, CancellationToken cancellationToken = default)
        {
            GenericResource resource = GetResource();
            ReplaceTags(tags, resource.Data.Tags);
            var op = await Operations.StartUpdateByIdAsync(Id, _apiVersion, resource.Data, cancellationToken);
            return new PhArmOperation<GenericResource, ResourceManager.Resources.Models.GenericResource>(
                await op.WaitForCompletionAsync(cancellationToken),
                v => new GenericResource(this, new GenericResourceData(v)));
        }

        public ArmResponse<GenericResource> RemoveTag(string key)
        {
            GenericResource resource = GetResource();
            DeleteTag(key, resource.Data.Tags);
            return new PhArmResponse<GenericResource, ResourceManager.Resources.Models.GenericResource>(
                Operations.StartUpdateById(Id, _apiVersion, resource.Data).WaitForCompletionAsync().ConfigureAwait(false).GetAwaiter().GetResult(),
                v => new GenericResource(this, new GenericResourceData(v)));
        }

        public async Task<ArmResponse<GenericResource>> RemoveTagAsync(string key, CancellationToken cancellationToken = default)
        {
            GenericResource resource = GetResource();
            DeleteTag(key, resource.Data.Tags);
            var op = await Operations.StartUpdateByIdAsync(Id, _apiVersion, resource.Data, cancellationToken);
            return new PhArmResponse<GenericResource, ResourceManager.Resources.Models.GenericResource>(
                await op.WaitForCompletionAsync(cancellationToken),
                v => new GenericResource(this, new GenericResourceData(v)));
        }

        public ArmOperation<GenericResource> StartRemoveTag(string key)
        {
            GenericResource resource = GetResource();
            DeleteTag(key, resource.Data.Tags);
            return new PhArmOperation<GenericResource, ResourceManager.Resources.Models.GenericResource>(
                Operations.StartUpdateById(Id, _apiVersion, resource.Data).WaitForCompletionAsync().ConfigureAwait(false).GetAwaiter().GetResult(),
                v => new GenericResource(this, new GenericResourceData(v)));
        }

        public async Task<ArmOperation<GenericResource>> StartRemoveTagAsync(string key, CancellationToken cancellationToken = default)
        {
            GenericResource resource = GetResource();
            DeleteTag(key, resource.Data.Tags);
            var op = await Operations.StartUpdateByIdAsync(Id, _apiVersion, resource.Data, cancellationToken);
            return new PhArmOperation<GenericResource, ResourceManager.Resources.Models.GenericResource>(
                await op.WaitForCompletionAsync(cancellationToken),
                v => new GenericResource(this, new GenericResourceData(v)));
        }
    }
}
