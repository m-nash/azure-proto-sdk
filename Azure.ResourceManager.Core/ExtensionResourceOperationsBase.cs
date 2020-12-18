using System.Threading;
using System.Threading.Tasks;

namespace Azure.ResourceManager.Core
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
        protected ExtensionResourceOperationsBase(ExtensionResourceOperationsBase genericOperations)
            : this(genericOperations.ClientOptions, genericOperations.Id)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ExtensionResourceOperationsBase"/> class.
        /// </summary>
        /// <param name="genericOperations">Generic operations with the identifier for this extention resource</param>
        protected ExtensionResourceOperationsBase(OperationsBase genericOperations)
            : this(genericOperations.ClientOptions, genericOperations.Id)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ExtensionResourceOperationsBase"/> class.
        /// </summary>
        /// <param name="context">Client configuration properties for these operations</param>
        /// <param name="id">The identifier of the extension resource</param>
        /// <param name="options">The client options to sue with these operations.</param>
        protected ExtensionResourceOperationsBase(AzureResourceManagerClientOptions options, ResourceIdentifier id)
            : base(options, id)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ExtensionResourceOperationsBase"/> class.
        /// </summary>
        /// <param name="context">Client configuration properties for these operations</param>
        /// <param name="resource">The extention resource for operatiosn to act upon</param>
        /// <param name="options">The client options to sue with these operations.</param>
        protected ExtensionResourceOperationsBase(AzureResourceManagerClientOptions options, Resource resource)
            : this(options, resource.Id)
        {
        }
    }

    /// <summary>
    /// Separate Extension resources from non-extension resources
    /// </summary>
    /// <typeparam name="TOperations">The typed operatiosn class for a specific resource.</typeparam>
    [System.Diagnostics.CodeAnalysis.SuppressMessage("StyleCop.CSharp.MaintainabilityRules", "SA1402:File may only contain a single type", Justification = "Resource types that differ by Type arguments")]
    public abstract class ExtensionResourceOperationsBase<TOperations> : ExtensionResourceOperationsBase
        where TOperations : ExtensionResourceOperationsBase<TOperations>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ExtensionResourceOperationsBase{TOperations}"/> class.
        /// </summary>
        /// <param name="genericOperations">Operations to create this operations class from</param>
        protected ExtensionResourceOperationsBase(ExtensionResourceOperationsBase genericOperations)
            : this(genericOperations.ClientOptions, genericOperations.Id)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ExtensionResourceOperationsBase{TOperations}"/> class.
        /// </summary>
        /// <param name="genericOperations">Operations to create this operations class from</param>
        protected ExtensionResourceOperationsBase(OperationsBase genericOperations)
            : this(genericOperations.ClientOptions, genericOperations.Id)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ExtensionResourceOperationsBase{TOperations}"/> class.
        /// </summary>
        /// <param name="context">Client configuration properties for these operations</param>
        /// <param name="id">The identifier of the extension resource</param>
        /// <param name="options">The client options to sue with these operations.</param>
        protected ExtensionResourceOperationsBase(AzureResourceManagerClientOptions options, ResourceIdentifier id)
            : base(options, id)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ExtensionResourceOperationsBase{TOperations}"/> class.
        /// </summary>
        /// <param name="context">Client configuration properties for these operations</param>
        /// <param name="resource">The extention resource for operatiosn to act upon</param>
        /// <param name="options">The client options to sue with these operations.</param>
        protected ExtensionResourceOperationsBase(AzureResourceManagerClientOptions options, Resource resource)
            : this(options, resource.Id)
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
