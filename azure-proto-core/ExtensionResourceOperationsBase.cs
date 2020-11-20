using Azure;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace azure_proto_core
{

    public abstract class ExtensionResourceOperationsBase : OperationsBase
    {
        public ExtensionResourceOperationsBase(ExtensionResourceOperationsBase genericOperations) : this(genericOperations.ClientContext, genericOperations.Id) { }
        public ExtensionResourceOperationsBase(OperationsBase genericOperations) : this(genericOperations.ClientContext, genericOperations.Id) { }

        public ExtensionResourceOperationsBase(ArmClientContext context, ResourceIdentifier id) : this(context, new ArmResource(id)) { }

        public ExtensionResourceOperationsBase(ArmClientContext context, Resource resource) : base(context, resource)
        {
        }
    }
    /// <summary>
    /// Implementation for resources that implement the immutable resource pattern
    /// </summary>
    public abstract class ExtensionResourceOperationsBase<TOperations, TResource> : ExtensionResourceOperationsBase
        where TResource:Resource 
        where TOperations: ExtensionResourceOperationsBase<TOperations, TResource>
    {
        public ExtensionResourceOperationsBase(ExtensionResourceOperationsBase genericOperations) : this(genericOperations.ClientContext, genericOperations.Id) { }
        public ExtensionResourceOperationsBase(OperationsBase genericOperations) : this(genericOperations.ClientContext, genericOperations.Id) { }

        public ExtensionResourceOperationsBase(ArmClientContext context, ResourceIdentifier id) : this(context, new ArmResource(id)) { }

        public ExtensionResourceOperationsBase(ArmClientContext context, Resource resource) : base(context, resource)
        {
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
        public abstract ArmOperation<Response> Delete();
        public abstract Task<ArmOperation<Response>> DeleteAsync(CancellationToken cancellationToken = default);

    }
}
