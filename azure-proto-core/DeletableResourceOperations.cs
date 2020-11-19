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
    public interface IDeletableResource<TOperations, TResource> 
        where TResource:Resource 
        where TOperations: IDeletableResource<TOperations, TResource>
    {
        ArmOperation<Response> Delete();
        Task<ArmOperation<Response>> DeleteAsync(CancellationToken cancellationToken = default);
    }
}
