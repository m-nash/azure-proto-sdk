using Azure;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace azure_proto_core
{
    /// <summary>
    /// Implementation for resources that implement the immutable resource pattern
    /// </summary>
    public abstract class GenericResourcesOperations<TResource, TOperations> : OperationsBase 
        where TResource:Resource 
        where TOperations: GenericResourcesOperations<TResource, TOperations>
    {
        public GenericResourcesOperations(ArmResourceOperations genericOperations) : this(genericOperations.ClientContext, genericOperations.Id) { }

        public GenericResourcesOperations(ArmClientContext context, ResourceIdentifier id) : this(context, new ArmResource(id)) { }

        public GenericResourcesOperations(ArmClientContext context, Resource resource) : base(context, resource)
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


        public abstract ArmResponse<TOperations> Get();
        public abstract Task<ArmResponse<TOperations>> GetAsync(CancellationToken cancellationToken = default);

    }
}