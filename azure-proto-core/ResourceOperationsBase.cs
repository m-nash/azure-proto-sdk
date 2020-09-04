using Azure;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace azure_proto_core
{
    /// <summary>
    /// TODO: split this into a base class for all Operations, and a base class for specific operations
    /// </summary>
    public abstract class ResourceOperationsBase : ArmClientBase
    {
        public ResourceOperationsBase(ArmClientBase parent, ResourceIdentifier context) : base(parent)
        {
            Validate(context);
            Context = context;
            DefaultLocation = parent.DefaultLocation;
        }

        public ResourceOperationsBase(ArmClientBase parent, Resource context) : this(parent, context.Id)
        {
            Validate(context?.Id);
            Context = context?.Id;
            DefaultLocation = parent.DefaultLocation;
            var tracked = context as TrackedResource;
            if (tracked != null)
            {
                DefaultLocation = tracked.Location;
            }
            Resource = context;
        }

        protected virtual Resource Resource { get; set; }


        public virtual ResourceIdentifier Context { get; }

        public virtual void Validate(ResourceIdentifier identifier)
        {
            if (identifier?.Type != ResourceType)
            {
                throw new InvalidOperationException($"{identifier} is not a valid resource of type {ResourceType}");
            }
        }
    }


    /// <summary>
    /// Common base type for lifecycle operations over a resource
    /// TODO: Consider whehter to represent POST operations and the acommpanyong actions list call
    /// TODO: Consider whether to provide a Normalized PATCH functionality across RP resources
    /// TODO: Refactor methods beyond the ResourceOperation as extensions [allowing them to appear in generic usage of the type]
    /// </summary>
    /// <typeparam name="Model"></typeparam>
    public abstract class ResourceClientBase<T> : ResourceOperationsBase where T : Resource 
    {
        public ResourceClientBase(ArmClientBase parent, ResourceIdentifier context) : base(parent, context)
        {
            Resource = new ArmResource(context);
        }

        public ResourceClientBase(ArmClientBase parent, Resource context) : base(parent, context)
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

        public virtual T Model
        {
            get
            {
                return Resource as T;
            }
        }

        public async virtual Task<T> GetModelIfNewerAsync(CancellationToken cancellationToken = default)
        {
            if (HasModel)
            {
                return Model;
            }

            return (await GetAsync(cancellationToken)).Value.Model;
        }

        public virtual T GetModelIfNewer()
        {
            if (HasModel)
            {
                return Model;
            }

            return Get().Value.Model;
        }

        public abstract Response<ResourceClientBase<T>> Get();
        public abstract Task<Response<ResourceClientBase<T>>> GetAsync(CancellationToken cancellationToken = default);
        public abstract ArmOperation<ResourceClientBase<T>> AddTag(string key, string value);
        public abstract Task<ArmOperation<ResourceClientBase<T>>> AddTagAsync(string key, string value, CancellationToken cancellationToken = default);        public abstract ArmOperation<Response> Delete();
        public abstract Task<ArmOperation<Response>> DeleteAsync(CancellationToken cancellationToken = default);
    }

}
