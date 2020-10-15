using Azure;
using System.Threading;
using System.Threading.Tasks;

namespace azure_proto_core
{
    /// <summary>
    /// Common base type for lifecycle operations over a resource
    /// TODO: Refactor methods beyond the ResourceOperation as extensions [allowing them to appear in generic usage of the type]
    /// TODO: Split into ResourceOperations/TrackedResourceOperations
    /// </summary>
    /// <typeparam name="Model"></typeparam>
    public abstract class ResourceOperationsBase<TOperations, TResource> : OperationsBase
        where TResource : Resource
        where TOperations : ResourceOperationsBase<TOperations, TResource>
    {
        public ResourceOperationsBase(ArmResourceOperations genericOperations) : this(genericOperations.ClientContext, genericOperations.Id) { }

        public ResourceOperationsBase(ArmClientContext context, ResourceIdentifier id) : this(context, new ArmResource(id)) { }

        public ResourceOperationsBase(ArmClientContext context, Resource resource) : base(context, resource)
        {
            Resource = resource;
        }

        protected override Resource Resource { get;  set; }

        public override ResourceIdentifier Id => Resource.Id;

        public virtual bool HasModel { 
            get 
            {
                var model = Resource as TResource;
                return model != null;
            }
        }

        public TResource Model 
        { 
            get
            {
                return Resource as TResource;
            } 
        }

        public TResource GetModelIfNewer()
        {
            if (HasModel)
            {
                return Model;
            }

            return Get().Value.Model;
        }

        public abstract ArmResponse<TOperations> Get();
        public abstract Task<ArmResponse<TOperations>> GetAsync(CancellationToken cancellationToken = default);
        public abstract ArmOperation<TOperations> AddTag(string key, string value);
        public abstract Task<ArmOperation<TOperations>> AddTagAsync(string key, string value, CancellationToken cancellationToken = default);
        public abstract ArmOperation<Response> Delete();
        public abstract Task<ArmOperation<Response>> DeleteAsync(CancellationToken cancellationToken = default);
    }
}
