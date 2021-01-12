using Azure;
using Azure.ResourceManager.Compute;
using Azure.ResourceManager.Compute.Models;
using Azure.ResourceManager.Core;
using System.Threading;
using System.Threading.Tasks;

namespace azure_proto_compute
{
    /// <summary>
    /// A class representing the operations that can be performed over a specific availability set.
    /// </summary>
    public class AvailabilitySetOperations : ResourceOperationsBase<AvailabilitySet>, ITaggableResource<AvailabilitySet>, IDeletableResource
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ArmResourceOperations"/> class.
        /// </summary>
        /// <param name="genericOperations"> An instance of <see cref="ArmResourceOperations"/> that has an id for an availability set. </param>
        internal AvailabilitySetOperations(ArmResourceOperations genericOperations)
            : base(genericOperations)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ArmResourceOperations"/> class.
        /// </summary>
        /// <param name="options"> The client parameters to use in these operations. </param>
        /// <param name="resource"> The resource that is the target of operations. </param>
        internal AvailabilitySetOperations(AzureResourceManagerClientOptions options, TrackedResource resource)
            : base(options, resource)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ArmResourceOperations"/> class.
        /// </summary>
        /// <param name="options"> The client parameters to use in these operations. </param>
        /// <param name="id"> The identifier of the resource that is the target of operations. </param>
        internal AvailabilitySetOperations(AzureResourceManagerClientOptions options, ResourceIdentifier id)
            : base(options, id)
        {
        }

        /// <summary>
        /// Gets the resource type definition for an availability set.
        /// </summary>
        public static readonly ResourceType ResourceType = "Microsoft.Compute/availabilitySets";

        /// <inheritdoc/>
        protected override ResourceType ValidResourceType => ResourceType;

        private AvailabilitySetsOperations Operations => GetClient((uri, cred) =>
            new ComputeManagementClient(
                uri,
                Id.Subscription,
                cred,
                ClientOptions.Convert<ComputeManagementClientOptions>())).AvailabilitySets;

        /// <summary>
        /// The operation to delete an availability set. 
        /// </summary>
        /// <returns> A response with the <see cref="ArmResponse{Response}"/> operation for this resource. </returns>
        public ArmResponse<Response> Delete()
        {
            return new ArmResponse(Operations.Delete(Id.ResourceGroup, Id.Name));
        }

        /// <summary>
        /// The operation to delete an availability set. 
        /// </summary>
        /// <param name="cancellationToken"> A token to allow the caller to cancel the call to the service. The default value is <see cref="P:System.Threading.CancellationToken.None" />. </param>
        /// <returns> A <see cref="Task"/> that on completion returns a response with the <see cref="ArmResponse{Response}"/> operation for this resource. </returns>
        public async Task<ArmResponse<Response>> DeleteAsync(CancellationToken cancellationToken = default)
        {
            return new ArmResponse(await Operations.DeleteAsync(Id.ResourceGroup, Id.Name, cancellationToken));
        }

        /// <summary>
        /// The operation to delete an availability set. 
        /// </summary>
        /// <param name="cancellationToken"> A token to allow the caller to cancel the call to the service. The default value is <see cref="P:System.Threading.CancellationToken.None" />. </param>
        /// <remarks>
        /// <see href="https://azure.github.io/azure-sdk/dotnet_introduction.html#dotnet-longrunning"> Details on long running operation object. </see>
        /// </remarks>
        /// <returns> An <see cref="ArmOperation{Response}"/> that allows polling for completion of the operation. </returns>
        public ArmOperation<Response> StartDelete(CancellationToken cancellationToken = default)
        {
            return new ArmVoidOperation(Operations.Delete(Id.ResourceGroup, Id.Name, cancellationToken));
        }

        /// <summary>
        /// The operation to delete an availability set. 
        /// </summary>
        /// <param name="cancellationToken"> A token to allow the caller to cancel the call to the service. The default value is <see cref="P:System.Threading.CancellationToken.None" />. </param>
        /// <remarks>
        /// <see href="https://azure.github.io/azure-sdk/dotnet_introduction.html#dotnet-longrunning"> Details on long running operation object. </see>
        /// </remarks>
        /// <returns> A <see cref="Task"/> that on completion returns an <see cref="ArmOperation{Response}"/> that allows polling for completion of the operation. </returns>
        public async Task<ArmOperation<Response>> StartDeleteAsync(CancellationToken cancellationToken = default)
        {
            return new ArmVoidOperation(await Operations.DeleteAsync(Id.ResourceGroup, Id.Name, cancellationToken));
        }

        /// <inheritdoc/>
        public override ArmResponse<AvailabilitySet> Get()
        {
            return new PhArmResponse<AvailabilitySet, Azure.ResourceManager.Compute.Models.AvailabilitySet>(
                Operations.Get(Id.ResourceGroup, Id.Name),
                a =>
                {
                    Resource = new AvailabilitySetData(a);
                    return new AvailabilitySet(ClientOptions, Resource as AvailabilitySetData);
                });
        }

        /// <inheritdoc/>
        public async override Task<ArmResponse<AvailabilitySet>> GetAsync(CancellationToken cancellationToken = default)
        {
            return new PhArmResponse<AvailabilitySet, Azure.ResourceManager.Compute.Models.AvailabilitySet>(
                await Operations.GetAsync(Id.ResourceGroup, Id.Name, cancellationToken),
                a =>
                {
                    Resource = new AvailabilitySetData(a);
                    return new AvailabilitySet(ClientOptions, Resource as AvailabilitySetData);
                });
        }

        /// <summary>
        /// The operation to update an availability set. 
        /// </summary>
        /// <param name="patchable"> The parameters to update. </param>
        /// <returns> The operation of the updated resource. </returns>
        public ArmOperation<AvailabilitySet> Update(AvailabilitySetUpdate patchable)
        {
            return new PhArmOperation<AvailabilitySet, Azure.ResourceManager.Compute.Models.AvailabilitySet>(
                Operations.Update(Id.ResourceGroup, Id.Name, patchable),
                a =>
                {
                    Resource = new AvailabilitySetData(a);
                    return new AvailabilitySet(ClientOptions, Resource as AvailabilitySetData);
                });
        }

        /// <summary>
        /// The operation to update an availability set.
        /// </summary>
        /// <param name="patchable">  The parameters to update. </param>
        /// <param name="cancellationToken"> A token to allow the caller to cancel the call to the service. The default value is <see cref="P:System.Threading.CancellationToken.None" />. </param>
        /// <returns> A <see cref="Task"/> that on completion returns the operation of the updated resource. </returns>
        public async Task<ArmOperation<AvailabilitySet>> UpdateAsync(AvailabilitySetUpdate patchable, CancellationToken cancellationToken = default)
        {
            return new PhArmOperation<AvailabilitySet, Azure.ResourceManager.Compute.Models.AvailabilitySet>(
                await Operations.UpdateAsync(Id.ResourceGroup, Id.Name, patchable, cancellationToken),
                a =>
                {
                    Resource = new AvailabilitySetData(a);
                    return new AvailabilitySet(ClientOptions, Resource as AvailabilitySetData);
                });
        }

        /// <summary>
        /// The operation to add a tag to an availability set.
        /// </summary>
        /// <param name="key"> The key for the tag. </param>
        /// <param name="value"> The value for the tag. </param>
        /// <remarks>
        /// <see href="https://azure.github.io/azure-sdk/dotnet_introduction.html#dotnet-longrunning"> Details on long running operation object. </see>
        /// </remarks>
        /// <returns> An <see cref="ArmOperation{AvailabilitySet}"/> that allows polling for completion of the operation. </returns>
        public ArmOperation<AvailabilitySet> StartAddTag(string key, string value)
        {
            var patchable = new AvailabilitySetUpdate();
            patchable.Tags[key] = value;
            return Update(patchable);
        }

        /// <summary>
        /// The operation to add a tag to an availability set.
        /// </summary>
        /// <param name="key"> The key for the tag. </param>
        /// <param name="value"> The value for the tag. </param>
        /// <remarks>
        /// <see href="https://azure.github.io/azure-sdk/dotnet_introduction.html#dotnet-longrunning"> Details on long running operation object. </see>
        /// </remarks>
        /// <returns> A <see cref="Task"/> that on completion returns an <see cref="ArmOperation{AvailabilitySet}"/> that allows polling for completion of the operation. </returns>
        public Task<ArmOperation<AvailabilitySet>> StartAddTagAsync(string key, string value, CancellationToken cancellationToken = default)
        {
            var patchable = new AvailabilitySetUpdate();
            patchable.Tags[key] = value;
            return UpdateAsync(patchable);
        }
    }
}
