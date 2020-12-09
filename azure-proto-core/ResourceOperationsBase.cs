// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT License.

using System.Threading;
using System.Threading.Tasks;

namespace azure_proto_core
{
    /// <summary>
    /// Base class for all operations over a resource.
    /// </summary>
    public abstract class ResourceOperationsBase : OperationsBase
    {

        /// <summary>
        /// Initializes a new instance of the <see cref="ResourceOperationsBase"/> class.
        /// </summary>
        /// <param name="context">The client parameters to use in these operations.</param>
        /// <param name="id">The identifier of the resource that is the target of operations.</param>
        public ResourceOperationsBase(ArmClientContext context, ResourceIdentifier id)
            : this(context, new ArmResource(id))
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ResourceOperationsBase"/> class.
        /// </summary>
        /// <param name="context">The client parameters to use in these operations.</param>
        /// <param name="resource">The resource that is the target of operations.</param>
        public ResourceOperationsBase(ArmClientContext context, Resource resource)
            : base(context, resource)
        {
        }

    }

    public abstract class ResourceOperationsBase<TOperations, TResource> : ResourceOperationsBase
        where TResource : Resource
        where TOperations : ResourceOperationsBase<TOperations, TResource>
    {
        public ResourceOperationsBase(ArmResourceOperations genericOperations)
            : this(genericOperations.ClientContext, genericOperations.Id, genericOperations.ClientOptions)
        {
        }

        public ResourceOperationsBase(ArmClientContext context, ResourceIdentifier id, ArmClientOptions clientOptions)
            : this(context, new ArmResource(id), clientOptions)
        {
        }

        public ResourceOperationsBase(ArmClientContext context, Resource resource, ArmClientOptions clientOptions)
            : base(context, resource, clientOptions)
        {
        }

        public abstract ArmResponse<TOperations> Get();

        public abstract Task<ArmResponse<TOperations>> GetAsync(CancellationToken cancellationToken = default);
    }
}
