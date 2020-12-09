using System;
using System.Threading;
using System.Threading.Tasks;

namespace azure_proto_core
{
    /// <summary>
    /// Create known Container and Leaf scopes for ARM Containers
    /// Think about how to extend known scope types in an extensible fashion (is it just adding them to the default, or is it having scopes for all provider or consumer services?
    /// For example, INetworkConsumer, IDatabaseConsumer, IEncryptionConsumer, IControlConsumer, ITriggerConsumer which also allows you to attach at that scope? [AttachDatabase]
    /// </summary>
    /// <typeparam name="T">The type of the resource model</typeparam>
    /// <typeparam name="U">The return type of the Creation methods, this can be Response<typeparamref name="T"/> or a long-running response</typeparam>
    public abstract class ResourceContainerOperations<TOperations, TResource> : OperationsBase
        where TOperations :  ResourceOperationsBase<TOperations, TResource>
        where TResource : Resource
    {
        protected TrackedResource Parent { get; private set; }

        protected ResourceContainerOperations(ArmClientContext context, ResourceIdentifier id) : base(context, id)
        {
        }

        protected ResourceContainerOperations(ArmClientContext context, TrackedResource resource) : base(context, resource)
        {
            Parent = resource;
        }

        public override void Validate(ResourceIdentifier identifier)
        {
            if (identifier.Type != ResourceGroupOperations.AzureResourceType && identifier.Type != SubscriptionOperations.AzureResourceType && identifier.Type != ResourceType.Parent)
            {
                throw new InvalidOperationException($"{identifier.Type} is not a valid container for {ResourceType}");
            }
        }

        public abstract ArmResponse<TOperations> Create(string name, TResource resourceDetails, CancellationToken cancellationToken = default);

        public abstract Task<ArmResponse<TOperations>> CreateAsync(string name, TResource resourceDetails, CancellationToken cancellationToken = default);

        public abstract ArmOperation<TOperations> StartCreate(string name, TResource resourceDetails, CancellationToken cancellationToken = default);

        public abstract Task<ArmOperation<TOperations>> StartCreateAsync(string name, TResource resourceDetails, CancellationToken cancellationToken = default);
    }
}
