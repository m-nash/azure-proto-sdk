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
    public abstract class ArmResourceOperations : ArmOperations
    {
        public ArmResourceOperations(ArmOperations parent, ResourceIdentifier context) : base(parent)
        {
            Validate(context);
            Context = context;
        }

        public ArmResourceOperations(ArmOperations parent, Resource context) : this(parent, context.Id)
        {
            Validate(context?.Id);
            Context = context?.Id;
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

    public abstract class ArmResourceOperations<Model, PatchModel, PatchResult, DeleteResult> : ArmResourceOperations 
        where Model: Resource where PatchModel : class where PatchResult: class where DeleteResult : class
    {
        public ArmResourceOperations(ArmOperations parent, ResourceIdentifier context) : base(parent, context)
        {
        }

        public ArmResourceOperations(ArmOperations parent, Resource context) : base(parent, context)
        {
        }

        public abstract Response<Model> Get();
        public abstract Task<Response<Model>> GetAsync(CancellationToken cancellationToken = default);
        public abstract PatchResult Update(PatchModel patchable);
        public abstract Task<PatchResult> UpdateAsync(PatchModel patchable, CancellationToken cancellationToken = default);
        public abstract DeleteResult Delete();
        public abstract Task<DeleteResult> DeleteAsync(CancellationToken cancellationToken = default);
    }

    public abstract class Patchable<T> where T: Resource
    {
        IDictionary<string, string> Tag { get; }
    }

    public abstract class ArmSyncResourceOperations<T> : ArmResourceOperations<T, Patchable<T>, Response<T>, Response> where T: Resource
    {
        public ArmSyncResourceOperations(ArmOperations parent, ResourceIdentifier context) : base(parent, context)
        {
        }
        public ArmSyncResourceOperations(ArmOperations parent, Resource context) : base(parent, context)
        {
        }
    }

    public abstract class ArmAsyncResourceOperations<T> : ArmResourceOperations<T, Patchable<T>, Operation<T>, Operation<Response>> where T : Resource
    {
        public ArmAsyncResourceOperations(ArmOperations parent, ResourceIdentifier context) : base(parent, context)
        {
        }
        public ArmAsyncResourceOperations(ArmOperations parent, Resource context) : base(parent, context)
        {
        }
    }

}
