using Azure;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace azure_proto_core
{
    public abstract class ResourceOperationsBase<TOperations, TResource> : OperationsBase 
        where TResource:Resource 
        where TOperations: ResourceOperationsBase<TOperations, TResource>
    {
        public ResourceOperationsBase(ArmResourceOperations genericOperations) : this(genericOperations.ClientContext, genericOperations.Id) { }

        public ResourceOperationsBase(ArmClientContext context, ResourceIdentifier id) : this(context, new ArmResource(id)) { }

        public ResourceOperationsBase(ArmClientContext context, Resource resource) : base(context, resource)
        {
            Resource = resource;
        }

        protected override Resource Resource { get; set; }

        public override ResourceIdentifier Id => Resource.Id;

        public virtual bool HasModel
        {
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

    }
}
