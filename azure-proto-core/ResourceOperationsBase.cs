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
    public abstract class ResourceClientBase<Model> : ResourceOperationsBase where Model : Resource 
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
        public virtual bool TryGetModel(out Model model)
        {
            model = Resource as Model;
            return model != null;
        }

        public virtual Model SafeGet()
        {
            Model model = null;
            if (!TryGetModel(out model))
            {
                try
                {
                    Get().Value.TryGetModel(out model);
                }
                catch { }
            }

            return model;
        }

        public abstract Response<ResourceClientBase<Model>> Get();
        public abstract Task<Response<ResourceClientBase<Model>>> GetAsync(CancellationToken cancellationToken = default);
        public abstract ArmOperation<ResourceClientBase<Model>> AddTag(string key, string value);
        public abstract Task<ArmOperation<ResourceClientBase<Model>>> AddTagAsync(string key, string value, CancellationToken cancellationToken = default);
        public abstract ArmOperation<Response> Delete();
        public abstract Task<ArmOperation<Response>> DeleteAsync(CancellationToken cancellationToken = default);
    }

}
