using Azure;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace azure_proto_core
{
    /// <summary>
    /// TODO: split this into a base class for all Operations, and a base class for specific operations
    /// </summary>
    public abstract class ResourceOperations : ArmOperations
    {
        public ResourceOperations(ArmOperations parent, ResourceIdentifier context) : base(parent)
        {
            Validate(context);
            Context = context;
            DefaultLocation = parent.DefaultLocation;
        }

        public ResourceOperations(ArmOperations parent, Resource context) : this(parent, context.Id)
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
    /// <typeparam name="T"></typeparam>
    public abstract class ResourceOperations<T> : ResourceOperations where T : Resource 
    {
        public ResourceOperations(ArmOperations parent, ResourceIdentifier context) : base(parent, context)
        {
            Resource = new ArmResource(context);
        }

        public ResourceOperations(ArmOperations parent, Resource context) : base(parent, context)
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


        public abstract Response<ResourceOperations<T>> Get();
        public abstract Task<Response<ResourceOperations<T>>> GetAsync(CancellationToken cancellationToken = default);
        public abstract ArmOperation<ResourceOperations<T>> AddTag(string key, string value);
        public abstract Task<ArmOperation<ResourceOperations<T>>> AddTagAsync(string key, string value, CancellationToken cancellationToken = default);
        public abstract ArmOperation<Response> Delete();
        public abstract Task<ArmOperation<Response>> DeleteAsync(CancellationToken cancellationToken = default);
    }

}
