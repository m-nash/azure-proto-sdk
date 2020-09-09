using Azure;
using System.Threading;
using System.Threading.Tasks;

namespace azure_proto_core
{
    /// <summary>
    /// Common base type for lifecycle operations over a resource
    /// TODO: Consider whehter to represent POST operations and the acommpanyong actions list call
    /// TODO: Consider whether to provide a Normalized PATCH functionality across RP resources
    /// TODO: Refactor methods beyond the ResourceOperation as extensions [allowing them to appear in generic usage of the type]
    /// </summary>
    /// <typeparam name="Model"></typeparam>
    public abstract class ResourceOperationsBase<T> : OperationsBase where T : Resource 
    {
        public ResourceOperationsBase(OperationsBase parent, ResourceIdentifier context) : base(parent, context)
        {
            Resource = new ArmResource(context);
        }

        public ResourceOperationsBase(OperationsBase parent, Resource context) : base(parent, context)
        {
            Resource = context;
        }

        public ResourceOperationsBase(ArmClientContext parent, ResourceIdentifier context) : base(parent, context)
        {
            Resource = new ArmResource(context);
        }

        public ResourceOperationsBase(ArmClientContext parent, Resource context) : base(parent, context)
        {
            Resource = context;
        }



        protected override Resource Resource { get;  set; }
        public override ResourceIdentifier Context => Resource.Id;

        public virtual bool HasModel { 
            get 
            {
                var model = Resource as T;
                return model != null;
            }
        }

        protected virtual Resource Resource { get; set; }

        public virtual ResourceIdentifier Context { get; }
        public virtual Resource Resource { get; }

        public abstract Response<ResourceOperationsBase<T>> Get();
        public abstract Task<Response<ResourceOperationsBase<T>>> GetAsync(CancellationToken cancellationToken = default);
        public abstract ArmOperation<ResourceOperationsBase<T>> AddTag(string key, string value);
        public abstract Task<ArmOperation<ResourceOperationsBase<T>>> AddTagAsync(string key, string value, CancellationToken cancellationToken = default);
        public abstract ArmOperation<Response> Delete();
        public abstract Task<ArmOperation<Response>> DeleteAsync(CancellationToken cancellationToken = default);

    }

}
