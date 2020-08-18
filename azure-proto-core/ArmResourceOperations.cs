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
            DefaultLocation = parent.DefaultLocation;
        }

        public ArmResourceOperations(ArmOperations parent, Resource context) : this(parent, context.Id)
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

    public abstract class ArmResourceOperations<Model, PatchModel, PatchResult, DeleteResult> : ArmResourceOperations
        where Model : Resource where PatchModel : class where PatchResult : class where DeleteResult : class
    {
        public ArmResourceOperations(ArmOperations parent, ResourceIdentifier context) : base(parent, context)
        {
        }

        public ArmResourceOperations(ArmOperations parent, Resource context) : base(parent, context)
        {
        }

        public abstract ArmResponse<Model> GetModel()




        public abstract Response<Model> Get();
        public abstract Task<Response<Model>> GetAsync(CancellationToken cancellationToken = default);
        public abstract PatchResult Update(PatchModel patchable);
        public abstract Task<PatchResult> UpdateAsync(PatchModel patchable, CancellationToken cancellationToken = default);
        public abstract DeleteResult Delete();
        public abstract Task<DeleteResult> DeleteAsync(CancellationToken cancellationToken = default);
    }

    public abstract class RpOperations<T> : ArmResourceOperations<T, T, T, T>
        {
        private T _model;
        public T Model
        {
            get
            {
                //if(_model == null)
                //{
                //    _model = Get();
                //}
                return _model;
            }
        }

        public class ArmResponse<T> where T : Resource
        {
            T _value;
            Func<T> _call;
            public T Value
            {
                get
                {
                    if (_value != null) return _value;
                    return _call();
                }
            }
            public bool CompletedSynchronously { get { return _value == null; }
        }
        //        {
        //            _model = GetAsync();
        //        }
        //        return _model;
        //    }
        //}
    }
}
