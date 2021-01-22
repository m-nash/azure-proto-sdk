// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT License.

using Azure.ResourceManager.Resources;
using Azure.ResourceManager.Resources.Models;
using System.Threading;
using System.Threading.Tasks;

namespace Azure.ResourceManager.Core
{
    /// <summary>
    /// A class representing the operations that can be performed over a specific ArmResource.
    /// </summary>
    public class ArmResourceOperations : ResourceOperationsBase<ArmResource>, ITaggableResource<ArmResource>, IDeletableResource
    {
        private string _apiVersion;

        /// <summary>
        /// Initializes a new instance of the <see cref="ArmResourceOperations"/> class.
        /// </summary>
        /// <param name="operations"> The resource operations to copy the options from. </param>
        /// <param name="id"> The identifier of the resource that is the target of operations. </param>
        internal ArmResourceOperations(ArmResourceOperations operations, ResourceIdentifier id)
            : base(operations, id)
        {
            _apiVersion = "BAD VALUE";
        }

        /// <inheritdoc/>
        protected override ResourceType ValidResourceType => ResourceGroupOperations.ResourceType;

        private ResourcesOperations Operations => new ResourcesManagementClient(
            BaseUri,
            Id.Subscription,
            Credential,
            ClientOptions.Convert<ResourcesManagementClientOptions>()).Resources;

        /// <summary>
        /// Delete the resource.
        /// </summary>
        /// <returns> The status of the delete operation. </returns>
        public ArmResponse<Response> Delete()
        {
            return new ArmResponse(Operations.StartDeleteById(Id, _apiVersion).WaitForCompletionAsync().ConfigureAwait(false).GetAwaiter().GetResult());
        }

        /// <summary>
        /// Delete the resource.
        /// </summary>
        /// <param name="cancellationToken"> A token allowing immediate cancellation of any blocking call performed during the deletion. </param>
        /// <returns> A <see cref="Task"/> that on completion returns the status of the delete operation. </returns>
        public async Task<ArmResponse<Response>> DeleteAsync(CancellationToken cancellationToken = default)
        {
            var operation = await Operations.StartDeleteByIdAsync(Id, _apiVersion, cancellationToken);
            var result = await operation.WaitForCompletionAsync(cancellationToken);
            return new ArmResponse(result);
        }

        /// <summary>
        /// Delete the resource.
        /// </summary>
        /// <param name="cancellationToken"> A token allowing immediate cancellation of any blocking call performed during the deletion. </param>
        /// <returns> A <see cref="ArmOperation{Response}"/> which allows the caller to control polling and waiting for resource deletion.
        /// The operation yields the final http response to the delete request when complete. </returns>
        /// <remarks>
        /// <see href="https://azure.github.io/azure-sdk/dotnet_introduction.html#dotnet-longrunning">Details on long running operation object.</see>
        /// </remarks>
        public ArmOperation<Response> StartDelete(CancellationToken cancellationToken = default)
        {
            return new ArmVoidOperation(Operations.StartDeleteById(Id, _apiVersion, cancellationToken));
        }

        /// <summary>
        /// Delete the resource.  This call returns a Task that blocks until the delete operation is accepted on the service.
        /// </summary>
        /// <param name="cancellationToken"> A token to allow the caller to cancel the call to the service.
        /// The default value is <see cref="P:System.Threading.CancellationToken.None" />. </param>
        /// <returns> A <see cref="Task"/> that on completion returns a <see cref="ArmOperation{Response}"/> which
        /// allows the caller to control polling and waiting for resource deletion.
        /// The operation yields the final http response to the delete request when complete. </returns>
        /// <remarks>
        /// <see href="https://azure.github.io/azure-sdk/dotnet_introduction.html#dotnet-longrunning">Details on long running operation object.</see>
        /// </remarks>
        public async Task<ArmOperation<Response>> StartDeleteAsync(CancellationToken cancellationToken = default)
        {
            var operation = await Operations.StartDeleteByIdAsync(Id, _apiVersion, cancellationToken);
            return new ArmVoidOperation(operation);
        }

        /// <summary>
        /// Add a tag to the resource
        /// </summary>
        /// <param name="key"> The tag key. </param>
        /// <param name="value"> The tag value. </param>
        /// <returns>An <see cref="ArmOperation{TOperations}"/> that allows the user to control polling and waiting for Tag completion.</returns>
        public ArmOperation<ArmResource> StartAddTag(string key, string value)
        {
            ArmResource resource = GetResource();
            UpdateTags(key, value, resource.Data.Tags);
            return new PhArmOperation<ArmResource, GenericResource>(
                Operations.StartUpdateById(Id, _apiVersion, resource.Data).WaitForCompletionAsync().ConfigureAwait(false).GetAwaiter().GetResult(),
                v => new ArmResource(this, new ArmResourceData(v)));
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

        /// <inheritdoc/>
        public override async Task<ArmResponse<ArmResource>> GetAsync(CancellationToken cancellationToken = default)
        {
            return new PhArmResponse<ArmResource, GenericResource>(
                await Operations.GetByIdAsync(Id, _apiVersion, cancellationToken),
                v => new ArmResource(this, new ArmResourceData(v)));
        }

        /// <inheritdoc/>
        public override void Validate(ResourceIdentifier identifier)
        {
            return;
        }

        /// <summary>
        /// Gets the resource associated with this operations object
        /// </summary>
        /// <returns> An instance of a <see cref="ArmResource"/>. </returns>
        private protected virtual ArmResource GetResource()
        {
            return Get().Value;
        }
    }
}
