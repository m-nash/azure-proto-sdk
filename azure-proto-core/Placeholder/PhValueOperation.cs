using Azure;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace azure_proto_core
{
    /// <summary>
    /// General transforming Long-Running poller.  Wraps an existing poller and transforms the return value
    /// </summary>
    /// <typeparam name="T">Desired return typr</typeparam>
    /// <typeparam name="U">return type fo the wrapped poller</typeparam>
    public class PhValueOperation<T, U> : Operation<T> where T : TrackedResource<U> where U : class
    {
        Operation<U> _wrapped;
        Func<U, T> _converter;

        public PhValueOperation(Operation<U> wrapped, Func<U, T> converter)
        {
            _wrapped = wrapped;
            _converter = converter;
        }

        public override string Id => _wrapped.Id;

        public override T Value => _converter(_wrapped.Value);

        public override bool HasCompleted => _wrapped.HasCompleted;

        public override bool HasValue => _wrapped.HasValue;

        public override Response GetRawResponse() => _wrapped.GetRawResponse();

        public override Response UpdateStatus(CancellationToken cancellationToken = default) => _wrapped.UpdateStatus(cancellationToken);

        public override ValueTask<Response> UpdateStatusAsync(CancellationToken cancellationToken = default) => _wrapped.UpdateStatusAsync(cancellationToken);

        public async override ValueTask<Response<T>> WaitForCompletionAsync(CancellationToken cancellationToken = default)
            => new PhArmResponse<T, U>(await _wrapped.WaitForCompletionAsync(cancellationToken), _converter);

        public async override ValueTask<Response<T>> WaitForCompletionAsync(TimeSpan pollingInterval, CancellationToken cancellationToken)
            => new PhArmResponse<T, U>(await _wrapped.WaitForCompletionAsync(pollingInterval, cancellationToken), _converter);
    }
}
