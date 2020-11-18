using Azure;
using System.Threading;
using System.Threading.Tasks;

namespace azure_proto_core
{
    /// <summary>
    /// Common base type for lifecycle operations over a resource
    /// TODO: Split into ResourceOperations/TrackedResourceOperations
    /// </summary>
    /// <typeparam name="Model"></typeparam>
    public abstract class ResourceOperationsBase<TOperations, TResource> : DeletableResourceOperations <TResource, TOperations>
        where TResource:Resource 
        where TOperations: DeletableResourceOperations<TResource, TOperations>
    {
        public ResourceOperationsBase(ArmResourceOperations genericOperations) : this(genericOperations.ClientContext, genericOperations.Id) { }

        public ResourceOperationsBase(ArmClientContext context, ResourceIdentifier id) : this(context, new ArmResource(id)) { }

        public ResourceOperationsBase(ArmClientContext context, Resource resource) : base(context, resource)
        {
            Resource = resource;
        }

        public abstract ArmOperation<TOperations> AddTag(string key, string value);
        public abstract Task<ArmOperation<TOperations>> AddTagAsync(string key, string value, CancellationToken cancellationToken = default);

    }
}
