// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT License.

using System;
using System.Threading;
using System.Threading.Tasks;

namespace Azure.ResourceManager.Core
{
    /// <summary>
    /// Base class for resource container
    /// TODO:    Create known Container and Leaf scopes for ARM Containers
    ///     Think about how to extend known scope types in an extensible fashion (is it just adding them to the default, or is
    ///     it having scopes for all provider or consumer services?
    ///     For example, INetworkConsumer, IDatabaseConsumer, IEncryptionConsumer, IControlConsumer, ITriggerConsumer which
    ///     also allows you to attach at that scope? [AttachDatabase]
    /// </summary>
    /// <typeparam name="TOperations">The type of the class containing operations for the underlying resource</typeparam>
    /// <typeparam name="TResource">The type of the class containing properties for the underlying resource</typeparam>
    public abstract class ResourceContainerBase<TOperations, TResource> : OperationsBase
        where TOperations : ResourceOperationsBase<TOperations>
        where TResource : Resource
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ResourceContainerBase{TOperations, TData}"/> class.
        /// </summary>
        /// <param name="options">The client parameters to use in these operations.</param>
        /// <param name="parentId">The resource Id of the parent resource.</param>
        protected ResourceContainerBase(AzureResourceManagerClientOptions options, ResourceIdentifier parentId)
           : base(options, parentId)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ResourceContainerBase{TOperations, TData}"/> class.
        /// </summary>
        /// <param name="options">The client parameters to use in these operations.</param>
        /// <param name="parentResource">The resource representing the parent resource.</param>
        protected ResourceContainerBase(AzureResourceManagerClientOptions options, TrackedResource parentResource)
            : base(options, parentResource)
        {
            Parent = parentResource;
        }

        /// <summary>
        /// Gets the parent resource of this resource
        /// </summary>
        protected TrackedResource Parent { get; }

        /// <summary>
        /// Verify that the input resource Id is a valid container for this type.
        /// </summary>
        /// <param name="identifier">The input resource Id to check.</param>
        /// <exception cref="InvalidOperationException">Resource identifier is not a valid type for this container.</exception>
        public override void Validate(ResourceIdentifier identifier)
        {
            if (identifier.Type != ResourceGroupOperations.ResourceType &&
                identifier.Type != SubscriptionOperations.ResourceType &&
                identifier.Type != Id.Type.Parent)
            {
                throw new InvalidOperationException($"{identifier.Type} is not a valid container for {Id.Type}");
            }
        }

        /// <summary>
        /// Creates a new resource synchronously.
        /// </summary>
        /// <param name="name">The name of the resource.</param>
        /// <param name="resourceDetails">The desired resource configuration.</param>
        /// <param name="cancellationToken">A token that allows the caller to cancel the call before
        /// it is completed. Note that cancellation cancels requests, but does not cancel the operation
        /// on the client side.</param>
        /// <returns>A response with the <see cref="ArmResponse{TOperations}"/> operation for the newly created resource.</returns>
        public abstract ArmResponse<TOperations> Create(
            string name,
            TResource resourceDetails,
            CancellationToken cancellationToken = default);

        /// <summary>
        /// Creates a new resource asynchronously.
        /// </summary>
        /// <param name="name">The name of the resource.</param>
        /// <param name="resourceDetails">The desired resource configuration.</param>
        /// <param name="cancellationToken">A token that allows the caller to cancel the call before
        /// it is completed. Note that cancellation cancels requests, but does not cancel the operation
        /// on the client side.</param>
        /// <returns>A <see cref="Task"/> that on completion returns a response with the
        /// <see cref="ArmResponse{TOperations}"/> operation for the newly created resource.</returns>
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
        /// <remarks>
        /// <see href="https://azure.github.io/azure-sdk/dotnet_introduction.html#dotnet-longrunning">Details on long running operation object.</see>
        /// </remarks>
        public abstract ArmOperation<TOperations> StartCreate(
            string name,
            TResource resourceDetails,
            CancellationToken cancellationToken = default);

        /// <summary>
        /// Begins an Operation on the service to create a new resource as specified. The call returns a Task
        /// that completes when the service accepts the operation.  The
        /// <see cref="System.Threading.Tasks.Task"/> yields an
        /// <see cref="ArmOperation{TOperations}"/> which allows the caller to control polling for completion
        ///  of the Create operation.
        /// </summary>
        /// <param name="name">The name of the resource.</param>
        /// <param name="resourceDetails">The desired resource configuration.</param>
        /// <param name="cancellationToken">A token that allows the caller to cancel the call before
        /// it is completed. Note that cancellation cancels requests, but does not cancel the operation
        /// on the client side.</param>
        /// <returns>
        /// A <see cref="Task"/> that on completion returns an <see cref="ArmOperation{TOperations}"/>
        ///  that allows polling for completion of the Create operation.
        /// </returns>
        /// <remarks>
        /// <see href="https://azure.github.io/azure-sdk/dotnet_introduction.html#dotnet-longrunning">Details on long running operation object.</see>
        /// </remarks>
        public abstract Task<ArmOperation<TOperations>> StartCreateAsync(
            string name,
            TResource resourceDetails,
            CancellationToken cancellationToken = default);
    }
}
