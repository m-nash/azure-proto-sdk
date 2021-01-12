// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT License.

using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Azure.ResourceManager.Core
{
    /// <summary>
    /// A class representing the operations that can be performed over a specific resource.
    /// </summary>
    public abstract class ResourceOperationsBase : OperationsBase
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ResourceOperationsBase"/> class.
        /// </summary>
        /// <param name="options"> The client parameters to use in these operations. </param>
        /// <param name="id"> The identifier of the resource that is the target of operations. </param>
        protected ResourceOperationsBase(AzureResourceManagerClientOptions options, ResourceIdentifier id)
            : base(options, id)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ResourceOperationsBase"/> class.
        /// </summary>
        /// <param name="options"> The client parameters to use in these operations. </param>
        /// <param name="resource"> The resource that is the target of operations. </param>
        protected ResourceOperationsBase(AzureResourceManagerClientOptions options, Resource resource)
            : base(options, resource)
        {
        }
    }

    /// <summary>
    /// Base class for all operations over a resource
    /// </summary>
    /// <typeparam name="TOperations"> The type implementing operations over the resource. </typeparam>
    [System.Diagnostics.CodeAnalysis.SuppressMessage("StyleCop.CSharp.MaintainabilityRules", "SA1402:File may only contain a single type", Justification = "Types differ by type argument only")]
    public abstract class ResourceOperationsBase<TOperations> : ResourceOperationsBase
        where TOperations : ResourceOperationsBase<TOperations>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ResourceOperationsBase{TOperations}"/> class.
        /// </summary>
        /// <param name="genericOperations"> Generic ARMResourceOperations for this resource type </param>
        protected ResourceOperationsBase(ArmResourceOperations genericOperations)
            : this(genericOperations.ClientOptions, genericOperations.Id)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ResourceOperationsBase{TOperations}"/> class.
        /// </summary>
        /// <param name="options"> The http client options for these operations. </param>
        /// <param name="id"> The resource Id of this resource. </param>
        protected ResourceOperationsBase(AzureResourceManagerClientOptions options, ResourceIdentifier id)
            : base(options, id)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ResourceOperationsBase{TOperations}"/> class.
        /// </summary>
        /// <param name="options"> The http client options for these operations. </param>
        /// <param name="resource"> The object corresponding to this resource. </param>
        protected ResourceOperationsBase(AzureResourceManagerClientOptions options, Resource resource)
            : base(options, resource)
        {
        }

        /// <summary>
        /// Gets details for this resource from the service.
        /// </summary>
        /// <returns> A response with the <see cref="ArmResponse{TOperations}"/> operation for this resource. </returns>
        public abstract ArmResponse<TOperations> Get();

        /// <summary>
        /// Gets details for this resource from the service.
        /// </summary>
        /// <param name="cancellationToken"> A token to allow the caller to cancel the call to the service. The default value is <see cref="P:System.Threading.CancellationToken.None" />. </param> 
        /// <returns> A <see cref="Task"/> that on completion returns a response with the <see cref="ArmResponse{TOperations}"/> operation for this resource. </returns>
        public abstract Task<ArmResponse<TOperations>> GetAsync(CancellationToken cancellationToken = default);

        /// <summary>
        /// Gets new dictionary of tags after adding the key value pair or updating the existing key value pair
        /// </summary>
        /// <param name="key"> The key to update. </param>
        /// <param name="value"> The value to update. </param>
        /// <param name="existingTags"> Existing tag dictionary to update. </param>
        protected void UpdateTags(string key, string value, IDictionary<string, string> existingTags)
        {
            if (existingTags.ContainsKey(key))
            {
                existingTags[key] = value;
            }
            else
            {
                existingTags.Add(key, value);
            }
        }
    }
}
