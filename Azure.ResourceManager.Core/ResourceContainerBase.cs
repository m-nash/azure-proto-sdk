// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT License.

using System;
using System.Threading;
using System.Threading.Tasks;

namespace Azure.ResourceManager.Core
{
    /// <summary>
    ///     Create known Container and Leaf scopes for ARM Containers
    ///     Think about how to extend known scope types in an extensible fashion (is it just adding them to the default, or is
    ///     it having scopes for all provider or consumer services?
    ///     For example, INetworkConsumer, IDatabaseConsumer, IEncryptionConsumer, IControlConsumer, ITriggerConsumer which
    ///     also allows you to attach at that scope? [AttachDatabase]
    /// </summary>
    /// <typeparam name="TOperations">The class containing operations for the underlying resource</typeparam>
    /// <typeparam name="TResource">The class containing properties for the underlying resource</typeparam>
    public abstract class ResourceContainerBase<TOperations, TResource> : OperationsBase
        where TOperations : ResourceOperationsBase<TOperations>
        where TResource : Resource
    {

        /// <summary>
        /// Initializes a new instance of the <see cref="ResourceContainerBase{TOperations, TData}"/> class.
        /// <param name="options">The http client options for these operations</param>
        /// <param name="parentId">The resource Id of the parent resource.</param>
        protected ResourceContainerBase(AzureResourceManagerClientOptions options, ResourceIdentifier id)
           : base(options, id)
        {
        }

        protected ResourceContainerBase(AzureResourceManagerClientOptions options, TrackedResource resource)
            : base(options, resource)
        /// <summary>
        /// Initializes a new instance of the <see cref="ResourceContainerBase{TOperations, TData}"/> class.
        /// <param name="options">The http client options for these operations</param>
        /// <param name="resource">The resource representing the parent resource.</param>
        {
            Parent = resource;
        }

        /// <summary>
        /// Gets the parent scope for this resource
        /// </summary>
        protected TrackedResource Parent { get; }

        /// <summary>
        /// Verify that the input resource Id is a valid container for this type.
        /// </summary>
        /// <param name="identifier">The input resource Id to check.</param>
        public override void Validate(ResourceIdentifier identifier)
        {
            if (identifier.Type != ResourceGroupOperations.AzureResourceType &&
                identifier.Type != SubscriptionOperations.AzureResourceType &&
                identifier.Type != ResourceType.Parent)
            {
                throw new InvalidOperationException($"{identifier.Type} is not a valid container for {ResourceType}");
            }
        }

        /// <summary>
        /// Creates a new resource.  This call will block until the resource is created.
        /// </summary>
        /// <param name="name">The name of the resource.</param>
        /// <param name="resourceDetails">The desired resource configuration.</param>
        /// <param name="cancellationToken">A token that allows the caller to cancel the call before
        /// it is completed. Note that cancellation cancels requests, but does not cancel the operation
        /// on the client side.</param>
        /// <returns>An http response containing the operations over the newly created resource.</returns>
        public abstract ArmResponse<TOperations> Create(
            string name,
            TResource resourceDetails,
            CancellationToken cancellationToken = default);

        /// <summary>
        /// Creates a new resource. This method returns a Taks which will complete when the resource is created.
        /// </summary>
        /// <param name="name">The name of the resource.</param>
        /// <param name="resourceDetails">The desired resource configuration.</param>
        /// <param name="cancellationToken">A token that allows the caller to cancel the call before
        /// it is completed. Note that cancellation cancels requests, but does not cancel the operation
        /// on the client side.</param>
        /// <returns>A Task that will complete when the new resource is created.  The caller can use the task
        /// to control when or if to wait for completion of the Create operation.</returns>
        public abstract Task<ArmResponse<TOperations>> CreateAsync(
            string name,
            TResource resourceDetails,
            CancellationToken cancellationToken = default);

        /// <summary>
        /// Begins an Operation on the service to create a new resource as specified. The call blocks until the
        /// service accepts the operation.  The returned Operation allows the caller to control polling for
        /// completion of e Create operation.
        /// </summary>
        /// <param name="name">The name of the resource.</param>
        /// <param name="resourceDetails">The desired resource configuration.</param>
        /// <param name="cancellationToken">A token that allows the caller to cancel the call before
        /// it is completed. Note that cancellation cancels requests, but does not cancel the operation
        /// on the client side.</param>
        /// <returns>An <see cref="ArmOperation{TOperations}"/> that allows polling for completion
        /// of the Create operation.</returns>
        public abstract ArmOperation<TOperations> StartCreate(
            string name,
            TResource resourceDetails,
            CancellationToken cancellationToken = default);

        /// <summary>
        /// Begins an Operation on the service to create a new resource as specified. The call returns a Task
        /// that completes when the service accepts the operation.  The 
        /// <see cref="System.Threading.Tasks.Task"/> yields an
        /// <see cref="ArmOperation{TOperations}"/> which allows the caller to control polling for completion 
        /// of the Create operation.
        /// </summary>
        /// <param name="name">The name of the resource.</param>
        /// <param name="resourceDetails">The desired resource configuration.</param>
        /// <param name="cancellationToken">A token that allows the caller to cancel the call before
        /// it is completed. Note that cancellation cancels requests, but does not cancel the operation
        /// on the client side.</param>
        /// <returns></returns>
        public abstract Task<ArmOperation<TOperations>> StartCreateAsync(
            string name,
            TResource resourceDetails,
            CancellationToken cancellationToken = default);
    }
}
