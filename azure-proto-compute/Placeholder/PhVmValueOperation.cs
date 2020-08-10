using Azure;
using Azure.ResourceManager.Compute;
using Azure.ResourceManager.Compute.Models;
using Azure.ResourceManager.Network.Models;
using azure_proto_core;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace azure_proto_compute.Placeholder
{
    /// <summary>
    /// Placeholder class - operations that return PHVirtualMachine, makes use of the underlying operationa s a wrapper
    /// </summary>
    public class PhVmValueOperation : Operation<PhVirtualMachine>
    {
        Operation<VirtualMachine> _wrapped;
        public PhVmValueOperation(Operation<VirtualMachine> wrapped)
        {
            _wrapped = wrapped;
        }

        public override string Id => _wrapped.Id;

        public override PhVirtualMachine Value => new PhVirtualMachine(_wrapped.Value);

        public override bool HasCompleted => _wrapped.HasCompleted;

        public override bool HasValue => _wrapped.HasValue;

        public override Response GetRawResponse() => _wrapped.GetRawResponse();

        public override Response UpdateStatus(CancellationToken cancellationToken = default) => _wrapped.UpdateStatus(cancellationToken);

        public override ValueTask<Response> UpdateStatusAsync(CancellationToken cancellationToken = default) => _wrapped.UpdateStatusAsync(cancellationToken);

        public async override ValueTask<Response<PhVirtualMachine>> WaitForCompletionAsync(CancellationToken cancellationToken = default) 
            => new PhResponse<PhVirtualMachine, VirtualMachine>( await _wrapped.WaitForCompletionAsync(cancellationToken), v => new PhVirtualMachine(v));

        public async override ValueTask<Response<PhVirtualMachine>> WaitForCompletionAsync(TimeSpan pollingInterval, CancellationToken cancellationToken)
            => new PhResponse<PhVirtualMachine, VirtualMachine>(await _wrapped.WaitForCompletionAsync(pollingInterval, cancellationToken), v => new PhVirtualMachine(v));
    }
}
