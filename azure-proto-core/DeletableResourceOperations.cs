using Azure;
using System.Threading;
using System.Threading.Tasks;

namespace azure_proto_core
{
    public interface IDeletableResource<TOperations, TResource> 
        where TResource:Resource 
        where TOperations: IDeletableResource<TOperations, TResource>
    {
        ArmOperation<Response> Delete();
        Task<ArmOperation<Response>> DeleteAsync(CancellationToken cancellationToken = default);
    }
}
