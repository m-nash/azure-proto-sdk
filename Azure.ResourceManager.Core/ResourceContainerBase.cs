// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT License.

using System;
using System.Globalization;
using System.Threading;
using System.Threading.Tasks;

namespace Azure.ResourceManager.Core
{
    /// <summary>
    /// A class representing collection of resources and their operations over their parent.
    /// </summary>
    /// <typeparam name="TOperations"> The type of the class containing operations for the underlying resource. </typeparam>
    /// <typeparam name="TResource"> The type of the class containing properties for the underlying resource. </typeparam>
    public abstract class ResourceContainerBase<TOperations, TResource> : OperationsBase
        where TOperations : ResourceOperationsBase<TOperations>
        where TResource : Resource
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ResourceContainerBase{TOperations, TData}"/> class.
        /// </summary>
        /// <param name="parent"> The resource representing the parent resource. </param>
        protected ResourceContainerBase(ResourceOperationsBase parent)
            : base(parent.ClientOptions, parent.Id, parent.Credential, parent.BaseUri)
        {
            Parent = parent;
        }

        /// <summary>
        /// Gets the parent resource of this resource
        /// </summary>
        protected ResourceOperationsBase Parent { get; }

        /// <summary>
        /// Verify that the input resource Id is a valid container for this type.
        /// </summary>
        /// <param name="identifier"> The input resource Id to check. </param>
        /// <exception cref="InvalidOperationException"> Resource identifier is not a valid type for this container. </exception>
        public override void Validate(ResourceIdentifier identifier)
        {
            if (identifier.Type != ValidResourceType)
                throw new InvalidOperationException($"{identifier.Type} is not a valid container for {Id.Type}");
        }

        /// <summary>
        /// Creates a new resource.
        /// </summary>
        /// <param name="name"> The name of the resource. </param>
        /// <param name="resourceDetails"> The desired resource configuration. </param>
        /// <returns> A response with the <see cref="ArmResponse{TOperations}"/> operation for this resource. </returns>
        public abstract ArmResponse<TOperations> Create(
            string name,
            TResource resourceDetails);

        /// <summary>
        /// Creates a new resource.
        /// </summary>
        /// <param name="name"> The name of the resource. </param>
        /// <param name="resourceDetails"> The desired resource configuration. </param>
        /// <param name="cancellationToken"> A token to allow the caller to cancel the call to the service. The default value is <see cref="P:System.Threading.CancellationToken.None" />. </param>
        /// <returns> A <see cref="Task"/> that on completion returns a response with the <see cref="ArmResponse{TOperations}"/> operation for this resource. </returns>
        public abstract Task<ArmResponse<TOperations>> CreateAsync(
            string name,
            TResource resourceDetails,
            CancellationToken cancellationToken = default);

        /// <summary>
        /// Creates a new resource.
        /// </summary>
        /// <param name="name"> The name of the resource. </param>
        /// <param name="resourceDetails"> The desired resource configuration. </param>
        /// <param name="cancellationToken"> A token to allow the caller to cancel the call to the service. The default value is <see cref="P:System.Threading.CancellationToken.None" />. </param>
        /// <returns> An <see cref="ArmOperation{TOperations}"/> that allows polling for completion of the operation. </returns>
        /// <remarks>
        /// <see href="https://azure.github.io/azure-sdk/dotnet_introduction.html#dotnet-longrunning">Details on long running operation object.</see>
        /// </remarks>
        public abstract ArmOperation<TOperations> StartCreate(
            string name,
            TResource resourceDetails,
            CancellationToken cancellationToken = default);

        /// <summary>
        /// Creates a new resource.
        /// </summary>
        /// <param name="name"> The name of the resource. </param>
        /// <param name="resourceDetails"> The desired resource configuration. </param>
        /// <param name="cancellationToken"> A token to allow the caller to cancel the call to the service. The default value is <see cref="P:System.Threading.CancellationToken.None" />. </param>
        /// <returns> A <see cref="Task"/> that on completion returns an <see cref="ArmOperation{TOperations}"/> that allows polling for completion of the operation. </returns>
        /// <remarks>
        /// <see href="https://azure.github.io/azure-sdk/dotnet_introduction.html#dotnet-longrunning">Details on long running operation object.</see>
        /// </remarks>
        public abstract Task<ArmOperation<TOperations>> StartCreateAsync(
            string name,
            TResource resourceDetails,
            CancellationToken cancellationToken = default);

        /// <summary>
        /// Returns the resource from Azure if it exists
        /// </summary>
        /// <param name="resourceName"> The name of the resource you want to get. </param>
        /// <param name="resource"> The resource if it existed, null otherwise. </param>
        /// <returns> Whether or not the resource existed. </returns>
        public virtual bool TryGetValue(string resourceName, out ArmResponse<TOperations> resource)
        {
            var op = GetOperation(resourceName);

            try
            {
                resource = op.Get();
                return true;
            }
            catch
            {
                resource = null;
                return false;
            }
        }

        /// <summary>
        /// Determines whether or not the azure resource exists in this container
        /// </summary>
        /// <param name="resourceName"> The name of the resource you want to check. </param>
        /// <returns> Whether or not the resource existed. </returns>
        public virtual bool DoesExist(string resourceName)
        {
            ArmResponse<TOperations> output;
            return TryGetValue(resourceName, out output);
        }

        private ResourceOperationsBase<TOperations> GetOperation(string resourceName)
        {
            Type opType = typeof(TOperations).BaseType;
            return Activator.CreateInstance(
                typeof(TOperations).BaseType,
                System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance,
                null,
                new object[] { Parent, resourceName },
                CultureInfo.InvariantCulture) as ResourceOperationsBase<TOperations>;
        }
    }
}
