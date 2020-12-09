using System.Threading;
using System.Threading.Tasks;

namespace azure_proto_core
{
    /// <summary>
    /// Base class for all extensions
    /// </summary>
    public abstract class ExtensionResourceOperationsBase : OperationsBase
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ExtensionResourceOperationsBase"/> class.
        /// </summary>
        /// <param name="genericOperations">Operations to create this operations class from</param>
        public ExtensionResourceOperationsBase(ExtensionResourceOperationsBase genericOperations)
            : this(genericOperations.ClientContext, genericOperations.Id)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ExtensionResourceOperationsBase"/> class.
        /// </summary>
        /// <param name="genericOperations">Generic operations with the identifier for this extention resource</param>
        public ExtensionResourceOperationsBase(OperationsBase genericOperations)
            : this(genericOperations.ClientContext, genericOperations.Id)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ExtensionResourceOperationsBase"/> class.
        /// </summary>
        /// <param name="context">Client configuration properties for these operations</param>
        /// <param name="id">The identifier of the extension resource</param>
        public ExtensionResourceOperationsBase(ArmClientContext context, ResourceIdentifier id)
            : this(context, new ArmResource(id))
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ExtensionResourceOperationsBase"/> class.
        /// </summary>
        /// <param name="context">Client configuration properties for these operations</param>
        /// <param name="resource">The extention resource for operatiosn to act upon</param>
        public ExtensionResourceOperationsBase(ArmClientContext context, Resource resource)
            : base(context, resource)
        {
        }
    }

    /// <summary>
    /// Separate Extension resources from non-extension resources
    /// </summary>
    public abstract class ExtensionResourceOperationsBase<TOperations> : ExtensionResourceOperationsBase
        where TOperations : ExtensionResourceOperationsBase<TOperations>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ExtensionResourceOperationsBase{TOperations}"/> class.
        /// </summary>
        /// <param name="genericOperations">Operations to create this operations class from</param>
        public ExtensionResourceOperationsBase(ExtensionResourceOperationsBase genericOperations)
            : this(genericOperations.ClientContext, genericOperations.Id)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ExtensionResourceOperationsBase{TOperations}"/> class.
        /// </summary>
        /// <param name="genericOperations">Operations to create this operations class from</param>
        public ExtensionResourceOperationsBase(OperationsBase genericOperations)
            : this(genericOperations.ClientContext, genericOperations.Id)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ExtensionResourceOperationsBase{TOperations}"/> class.
        /// </summary>
        /// <param name="context">Client configuration properties for these operations</param>
        /// <param name="id">The identifier of the extension resource</param>
        public ExtensionResourceOperationsBase(ArmClientContext context, ResourceIdentifier id)
            : this(context, new ArmResource(id))
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ExtensionResourceOperationsBase{TOperations, TResource}"/> class.
        /// </summary>
        /// <param name="context">Client configuration properties for these operations</param>
        /// <param name="resource">The extention resource for operatiosn to act upon</param>
        public ExtensionResourceOperationsBase(ArmClientContext context, Resource resource)
            : base(context, resource)
        {
        }


        /// <summary>
        /// Get details and operations for this extension resource.  This call will block the thread until details are returned from the service.
        /// </summary>
        /// <returns>An Http Response containing details and operations for the extension resource</returns>
        public abstract ArmResponse<TOperations> Get();

        /// <summary>
        /// Get details and operations for this extension resource.  This call returns a Task that completes when the details are returned from the service.
        /// </summary>
        /// <param name="cancellationToken">A token allowing cancellation of the Http call in the task</param>
        /// <returns>A Task that retrieves the resource details. When complete, the task will yield an Http Response
        /// containing details and operations for the extension resource</returns>
        public abstract Task<ArmResponse<TOperations>> GetAsync(CancellationToken cancellationToken = default);
    }
}
