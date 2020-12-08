using System.Threading;
using System.Threading.Tasks;

namespace azure_proto_core
{
    public abstract class ResourceOperationsBase<TOperations, TResource> : OperationsBase 
        where TResource : Resource 
        where TOperations : ResourceOperationsBase<TOperations, TResource>
    {
        public ResourceOperationsBase(ArmResourceOperations genericOperations) : this(genericOperations.ClientContext, genericOperations.Id, genericOperations.ClientOptions) { }

        public ResourceOperationsBase(ArmClientContext context, ResourceIdentifier id, ArmClientOptions clientOptions) : this(context, new ArmResource(id), clientOptions) { }

        public ResourceOperationsBase(ArmClientContext context, Resource resource, ArmClientOptions clientOptions) : base(context, resource, clientOptions) { }

        public abstract ArmResponse<TOperations> Get();

        public abstract Task<ArmResponse<TOperations>> GetAsync(CancellationToken cancellationToken = default);
    }
}
