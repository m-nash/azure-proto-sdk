using Azure;
using System.Threading;
using System.Threading.Tasks;

namespace azure_proto_core
{
    /// <summary>
    /// Common base type for lifecycle operations over a resource
    /// TODO: Split into ResourceOperations/TrackedResourceOperations
    /// </summary>
    /// <typeparam name="Model"></typeparam>
    public interface ITagable<TOperations, TResource>
        where TResource:Resource 
        where TOperations: ITagable<TOperations, TResource>
    {
        ArmOperation<TOperations> AddTag(string key, string value);
        Task<ArmOperation<TOperations>> AddTagAsync(string key, string value, CancellationToken cancellationToken = default);
    }
}
