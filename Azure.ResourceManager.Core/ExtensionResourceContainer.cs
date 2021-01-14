using System.Threading;
using System.Threading.Tasks;

namespace Azure.ResourceManager.Core
{
    /// <summary>
    /// Container for extension resources.  Because there is no CreateOrUpdate, there is a difference in the input and output model
    /// </summary>
    /// <typeparam name="TOperations">Operations class returned</typeparam>
    /// <typeparam name="TInput">Input Model</typeparam>
    public abstract class ExtensionResourceContainer<TOperations, TInput> : ExtensionResourceOperationsBase
        where TOperations : ExtensionResourceOperationsBase<TOperations>
        where TInput : class
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ExtensionResourceContainer{TOperations, TInput}"/> class.
        /// Create an ResourceContainer from an operations class or client
        /// </summary>
        /// <param name="operations"> The client or operations class to create this container from. </param>
        protected ExtensionResourceContainer(OperationsBase operations)
            : base(operations)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ExtensionResourceContainer{TOperations, TInput}"/> class.
        /// </summary>
        /// <param name="options"> The client options with http client details for these operations. </param>
        /// <param name="parentId"> The resource Id of the parent resource. </param>
        protected ExtensionResourceContainer(OperationsBase operations, ResourceIdentifier parentId)
            : base(operations, parentId)
        {
        }

        /// <summary>
        /// Validate that the given resource Id represents a valid parent for thsi resource
        /// </summary>
        /// <param name="identifier">The resource Id of the parent resource</param>
        public override void Validate(ResourceIdentifier identifier)
        {
        }

        /// <summary>
        /// Create a new extension resource at the given scope.  Block further execution on the current thread until creation is complete.
        /// </summary>
        /// <param name="name">The name of the created extention resource</param>
        /// <param name="resourceDetails">The properties of the extension resource</param>
        /// <param name="cancellationToken">The cancellation token clients can use to cancel any blocking calls made by this method</param>
        /// <returns>An Http envelope containing the operations for the given extension</returns>
        public abstract ArmResponse<TOperations> Create(string name, TInput resourceDetails, CancellationToken cancellationToken = default);

        /// <summary>
        /// Create a new extension resource at the given scope without blocking the current thread.
        /// Returns a Task that allows control over when or if the thread is blocked.
        /// </summary>
        /// <param name="name">The name of the created extention resource</param>
        /// <param name="resourceDetails">The properties of the extension resource</param>
        /// <param name="cancellationToken">The cancellation token clients can use to cancel any blocking calls made by this method</param>
        /// <returns>A Task that creates the extension resource.</returns>
        public abstract Task<ArmResponse<TOperations>> CreateAsync(string name, TInput resourceDetails, CancellationToken cancellationToken = default);

        /// <summary>
        /// Begin Creation of a new extension resource. Block until the creation is accepted by the service.
        /// The returned object allows fine-grained control over waiting for creation to complete.
        /// </summary>
        /// <param name="name">The name of the created extention resource</param>
        /// <param name="resourceDetails">The properties of the extension resource</param>
        /// <param name="cancellationToken">The cancellation token clients can use to cancel any blocking calls made by this method</param>
        /// <returns>An instance of <see cref="ArmOperation{TOperation}"/>, allowing fine grained control over waiting for creation to complete.</returns>
        public abstract ArmOperation<TOperations> StartCreate(string name, TInput resourceDetails, CancellationToken cancellationToken = default);

        /// <summary>
        /// Begin Creation of a new extension resource in a background task.
        /// When creation has successfully begin, the object returned from the completed task allows fine-grained control over waiting for creation to complete.
        /// </summary>
        /// <param name="name">The name of the created extention resource</param>
        /// <param name="resourceDetails">The properties of the extension resource</param>
        /// <param name="cancellationToken">The cancellation token clients can use to cancel any blocking calls made by this method</param>
        /// <returns>A <see cref="Task<typeparamref name="TInput"/>"/>  that starts creation of an extension resource.
        /// Once creation has completed, the Task yields an instance of <see cref="ArmOperation{TOperation}"/>, allowing fine grained control
        /// over waiting for creation to complete.</returns>
        public abstract Task<ArmOperation<TOperations>> StartCreateAsync(string name, TInput resourceDetails, CancellationToken cancellationToken = default);

        /// <summary>
        /// Lists the extension resources at the current scope. Blocks until the first page of results is returned.
        /// </summary>
        /// <param name="cancellationToken">The cancellation token clients can use to cancel any blocking HTTP requests made by this method, including
        /// any Http requests that result from enumerating pages of results.</param>
        /// <returns>An instance of <see cref="Azure.Pageable{TResource}"/> allowing paged or unpaged enumeration of results</returns>
        public abstract Pageable<TOperations> ListAtScope(CancellationToken cancellationToken = default);

        /// <summary>
        /// Lists the extension resources at the current scope asynchronously. The returned task completes when the first page of results is returrned.
        /// </summary>
        /// <param name="cancellationToken">The cancellation token clients can use to cancel any blocking HTTP requests made by this method, including
        /// any Http requests that result from enumerating pages of results.</param>
        /// <returns>An instance of <see cref="Azure.AsyncPageable{TResource}"/> allowing asynchronous paged or unpaged enumeration of results.</returns>
        public abstract AsyncPageable<TOperations> ListAtScopeAsync(CancellationToken cancellationToken = default);
    }
}
