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
    public abstract class DeletableResourceOperations<TResource, TOperations> : GenericResourcesOperations <TResource, TOperations>
        where TResource:Resource 
        where TOperations: DeletableResourceOperations<TResource, TOperations>
    {
        public DeletableResourceOperations(ArmResourceOperations genericOperations) : this(genericOperations.ClientContext, genericOperations.Id) { }

        public DeletableResourceOperations(ArmClientContext context, ResourceIdentifier id) : this(context, new ArmResource(id)) { }

        public DeletableResourceOperations(ArmClientContext context, Resource resource) : base(context, resource) { }

        public abstract ArmOperation<Response> Delete();
        public abstract Task<ArmOperation<Response>> DeleteAsync(CancellationToken cancellationToken = default);
    }
}
