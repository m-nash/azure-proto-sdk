using Azure;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace azure_proto_core
{
    /// <summary>
    /// Abstract class for long-running or synchronous applications. If we want to add ARM-specific OM, this is where we would add it.
    /// We may need to add ARM-specific OM, as customers have asked for additional configurability over polling stratgies on a per-operation basis
    /// TODO: Remove protected properties, as this should be integrated into the generated client
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public abstract class ArmOperation<T> : Operation<T>
    {
        public ArmOperation(T syncValue)
        {
            CompletedSynchronously = syncValue != null;
            SyncValue = syncValue;
        }

        protected bool CompletedSynchronously { get; }

        protected T SyncValue { get; }
    }

    /// <summary>
    /// Generic ARM long runnign operation class for operatiosn with no returned value
    /// TODO: Reimplement without wrapping
    /// </summary>
    public class ArmVoidOperation : ArmOperation<Response>
    {
        internal class WrappingResponse : ArmResponse<Response>
        {
            Response _wrapped;

            public WrappingResponse(Response wrapped)
            {
                _wrapped = wrapped;
            }

            public override Response Value => _wrapped;

            public override Response GetRawResponse() => _wrapped;
        }

        Operation<Response> _wrapped;
        public ArmVoidOperation(Operation<Response> other) : base(null)
        {
            _wrapped = other;
        }

        public ArmVoidOperation(Response other) : base(other)
        {
        }        
        
        public override string Id => _wrapped?.Id;

        public override Response Value => SyncValue;

        public override bool HasCompleted => CompletedSynchronously || _wrapped.HasCompleted;

        public override bool HasValue => CompletedSynchronously || _wrapped.HasValue;

        public override Response GetRawResponse() => CompletedSynchronously ? SyncValue : _wrapped.GetRawResponse();

        public override Response UpdateStatus(CancellationToken cancellationToken = default) => CompletedSynchronously ? SyncValue : _wrapped.UpdateStatus(cancellationToken);

        public async override ValueTask<Response> UpdateStatusAsync(CancellationToken cancellationToken = default)
            => CompletedSynchronously ? SyncValue : await _wrapped.UpdateStatusAsync(cancellationToken);

        public async override ValueTask<Response<Response>> WaitForCompletionAsync(CancellationToken cancellationToken = default)
            => CompletedSynchronously ? new WrappingResponse(SyncValue) : await _wrapped.WaitForCompletionAsync(cancellationToken);

        public async override ValueTask<Response<Response>> WaitForCompletionAsync(TimeSpan pollingInterval, CancellationToken cancellationToken) 
            => CompletedSynchronously ? new WrappingResponse(SyncValue) : await _wrapped.WaitForCompletionAsync(pollingInterval, cancellationToken);
    }

    /// <summary>
    /// TODO: Reimplement this class without wrapping the underlying Operation
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <typeparam name="U"></typeparam>
    public class PhArmOperation<T, U> : ArmOperation<T> where T: class where U : class
    {
        Operation<U> _wrapped;
        Func<U, T> _converter;
        Response<U> _syncWrapped;

        public PhArmOperation(Operation<U> wrapped, Func<U, T> converter) : base(null)
        {
            _wrapped = wrapped;
            _converter = converter;
        }

        public PhArmOperation(Response<U> wrapped, Func<U, T> converter) : base(converter(wrapped.Value))
        {
            _converter = converter;
            _syncWrapped = wrapped;
        }

        public override string Id => _wrapped?.Id;

        public override T Value => CompletedSynchronously ? SyncValue : _converter(_wrapped.Value);

        public override bool HasCompleted => CompletedSynchronously || _wrapped.HasCompleted;

        public override bool HasValue => CompletedSynchronously || _wrapped.HasValue;

        public override Response GetRawResponse() => CompletedSynchronously ? _syncWrapped.GetRawResponse() : _wrapped.GetRawResponse();

        public override Response UpdateStatus(CancellationToken cancellationToken = default) 
            => CompletedSynchronously ? _syncWrapped.GetRawResponse() : _wrapped.UpdateStatus(cancellationToken);

        public override ValueTask<Response> UpdateStatusAsync(CancellationToken cancellationToken = default) 
            => CompletedSynchronously ? new ValueTask<Response>(_syncWrapped.GetRawResponse()) : _wrapped.UpdateStatusAsync(cancellationToken);

        public async override ValueTask<Response<T>> WaitForCompletionAsync(CancellationToken cancellationToken = default)
            => CompletedSynchronously ? new PhArmResponse<T, U>(_syncWrapped, _converter)
            : new PhArmResponse<T, U>( await _wrapped.WaitForCompletionAsync(cancellationToken), _converter);

        public async override ValueTask<Response<T>> WaitForCompletionAsync(TimeSpan pollingInterval, CancellationToken cancellationToken) 
            => CompletedSynchronously ? new PhArmResponse<T, U>(_syncWrapped, _converter)
            : new PhArmResponse<T, U>(await _wrapped.WaitForCompletionAsync(pollingInterval, cancellationToken), _converter);
    }
}
