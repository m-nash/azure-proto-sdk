using Azure;
using Azure.ResourceManager.Resources;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace azure_proto_core
{
    public class PhVoidOperation : Operation<Response>
    {
        Operation<Response> _wrapped;

        public PhVoidOperation(Operation<Response> wrapped)
        {
            _wrapped = wrapped;
        }

        public override string Id => _wrapped.Id;

        public override Response Value => _wrapped.Value;

        public override bool HasCompleted => _wrapped.HasCompleted;

        public override bool HasValue => _wrapped.HasValue;

        public override Response GetRawResponse()
        {
            return _wrapped.GetRawResponse();
        }

        public override Response UpdateStatus(CancellationToken cancellationToken = default)
        {
            return _wrapped.UpdateStatus(cancellationToken);
        }

        public override ValueTask<Response> UpdateStatusAsync(CancellationToken cancellationToken = default)
        {
            return _wrapped.UpdateStatusAsync(cancellationToken);
        }

        public override ValueTask<Response<Response>> WaitForCompletionAsync(CancellationToken cancellationToken = default)
        {
            return _wrapped.WaitForCompletionAsync(cancellationToken);
        }

        public override ValueTask<Response<Response>> WaitForCompletionAsync(TimeSpan pollingInterval, CancellationToken cancellationToken)
        {
            return _wrapped.WaitForCompletionAsync(pollingInterval, cancellationToken);
        }
    }
}
