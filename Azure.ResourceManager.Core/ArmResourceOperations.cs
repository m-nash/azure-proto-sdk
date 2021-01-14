// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT License.

using Azure.ResourceManager.Resources;
using Azure.ResourceManager.Resources.Models;
using System.Threading;
using System.Threading.Tasks;

namespace Azure.ResourceManager.Core
{
    public class ArmResourceOperations : ResourceOperationsBase<ArmResource>, ITaggableResource<ArmResource>, IDeletableResource
    {
        private string _apiVersion;

        internal ArmResourceOperations(ArmResourceOperations operations, ResourceIdentifier id)
            : base(operations, id)
        {
        }

        protected override ResourceType ValidResourceType => ResourceGroupOperations.ResourceType; 
        
        private ResourcesOperations Operations => new ResourcesManagementClient(
            BaseUri,
            Id.Subscription,
            Credential,
            ClientOptions.Convert<ResourcesManagementClientOptions>()).Resources;

        public ArmResponse<Response> Delete()
        {
            return new ArmResponse(Operations.StartDeleteById(Id, _apiVersion).WaitForCompletionAsync().ConfigureAwait(false).GetAwaiter().GetResult());
        }

        public async Task<ArmResponse<Response>> DeleteAsync(CancellationToken cancellationToken = default)
        {
            var operation = await Operations.StartDeleteByIdAsync(Id, _apiVersion, cancellationToken);
            var result = await operation.WaitForCompletionAsync(cancellationToken);
            return new ArmResponse(result);
        }

        public ArmOperation<Response> StartDelete(CancellationToken cancellationToken = default)
        {
            return new ArmVoidOperation(Operations.StartDeleteById(Id, _apiVersion, cancellationToken));
        }

        public async Task<ArmOperation<Response>> StartDeleteAsync(CancellationToken cancellationToken = default)
        {
            var operation = await Operations.StartDeleteByIdAsync(Id, _apiVersion, cancellationToken);
            return new ArmVoidOperation(operation);
        }

        public ArmOperation<ArmResource> StartAddTag(string key, string value)
        {
            ArmResource resource = GetResource();
            UpdateTags(key, value, resource.Data.Tags);
            return new PhArmOperation<ArmResource, GenericResource>(
                Operations.StartUpdateById(Id, _apiVersion, resource.Data).WaitForCompletionAsync().ConfigureAwait(false).GetAwaiter().GetResult(),
                v => new ArmResource(this, new ArmResourceData(v)));
        }

        public async Task<ArmOperation<ArmResource>> StartAddTagAsync(string key, string value, CancellationToken cancellationToken = default)
        {
            ArmResource resource = GetResource();
            UpdateTags(key, value, resource.Data.Tags);
            var op = await Operations.StartUpdateByIdAsync(Id, _apiVersion, resource.Data, cancellationToken);
            return new PhArmOperation<ArmResource, GenericResource>(
                await op.WaitForCompletionAsync(cancellationToken),
                v => new ArmResource(this, new ArmResourceData(v)));
        }

        /// <inheritdoc/>
        public override ArmResponse<ArmResource> Get()
        {
            return new PhArmResponse<ArmResource, GenericResource>(
                Operations.GetById(Id, _apiVersion),
                v => new ArmResource(this, new ArmResourceData(v)));
        }

        public override async Task<ArmResponse<ArmResource>> GetAsync(CancellationToken cancellationToken = default)
        {
            return new PhArmResponse<ArmResource, GenericResource>(
                await Operations.GetByIdAsync(Id, _apiVersion, cancellationToken),
                v => new ArmResource(this, new ArmResourceData(v)));
        }

        public override void Validate(ResourceIdentifier identifier)
        {
            return;
        }

        private protected virtual ArmResource GetResource()
        {
            return Get().Value;
        }
    }
}
