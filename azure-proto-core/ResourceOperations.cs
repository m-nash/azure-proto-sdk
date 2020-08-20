using Azure;
using Azure.Core;
using azure_proto_core;
using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Text;
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
        }

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
    /// Common base type for lifecycle operations overs
    /// </summary>
    /// <typeparam name="Model"></typeparam>
    public abstract class ResourceOperations<Model> : ResourceOperations where Model : Resource 
    {
        public ResourceOperations(ArmOperations parent, ResourceIdentifier context) : base(parent, context)
        {
            Resource = new ArmResource(context);
        }

        public ResourceOperations(ArmOperations parent, Resource context) : base(parent, context)
        {
            Resource = context;
        }


        protected virtual Resource Resource { get; }
        public virtual bool TryGetResource(out Model model)
        {
            model = Resource as Model;
            return model != null;
        }

        public abstract Response<Model> Get();
        public abstract Task<Response<Model>> GetAsync(CancellationToken cancellationToken = default);
        public abstract ArmOperation<Model> Update(IPatchModel patchable);
        public abstract Task<ArmOperation<Model>> UpdateAsync(IPatchModel patchable, CancellationToken cancellationToken = default);
        public abstract ArmOperation<Response> Delete();
        public abstract Task<ArmOperation<Response>> DeleteAsync(CancellationToken cancellationToken = default);
    }

}
