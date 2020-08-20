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
    public class PhVmValueOperation : PhArmOperation<PhVirtualMachine, VirtualMachine>
    {
        public PhVmValueOperation(Operation<VirtualMachine> wrapped) : base(wrapped, v => new PhVirtualMachine(v))
        {
        }
    }
}
