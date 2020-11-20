using Azure;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace azure_proto_core
{
    /// <summary>
    /// Container for extension resources.  Because there is no CreateOrUpdate, there is a difference in the input and output model
    /// </summary>
    /// <typeparam name="TOperations">Operations class returned</typeparam>
    /// <typeparam name="TInput">Input Model</typeparam>
    /// <typeparam name="TResource">Output Model</typeparam>
    public abstract class ExtensionResourceContainer<TOperations, TInput, TResource> : ExtensionResourceOperationsBase
        where TOperations : ExtensionResourceOperationsBase<TOperations, TResource>
        where TResource : Resource
        where TInput : class
    {
        protected ExtensionResourceContainer(OperationsBase operations) : this(operations.ClientContext, operations.Id)
        {

        }
        protected ExtensionResourceContainer(ArmClientContext parent, ResourceIdentifier contexts) : base(parent, contexts)
        {
        }

        protected ExtensionResourceContainer(ArmClientContext parent, TrackedResource contexts) : base(parent, contexts)
        {
        }

        public override void Validate(ResourceIdentifier identifier)
        {
            if (identifier.Type != ResourceGroupOperations.AzureResourceType && identifier.Type != SubscriptionOperations.AzureResourceType && identifier.Type != ResourceType.Parent)
            {
                throw new InvalidOperationException($"{identifier.Type} is not a valid container for {ResourceType}");
            }
        }

        public abstract ArmResponse<TOperations> Create(string name, TInput resourceDetails, CancellationToken cancellationToken = default);
        public abstract Task<ArmResponse<TOperations>> CreateAsync(string name, TInput resourceDetails, CancellationToken cancellationToken = default);
        public abstract ArmOperation<TOperations> StartCreate(string name, TInput resourceDetails, CancellationToken cancellationToken = default);
        public abstract Task<ArmOperation<TOperations>> StartCreateAsync(string name, TInput resourceDetails, CancellationToken cancellationToken = default);
        public abstract Pageable<TResource> ListAtScope(CancellationToken cancellationToken = default);
        public abstract AsyncPageable<TResource> ListAtScopeAsync(CancellationToken cancellationToken = default);

    }
}
