using azure_proto_core;
using System;
using System.Collections.Generic;
using System.Text;

namespace azure_proto_compute
{
    public class XVirtualMachine : VirtualMachineOperations
    {
        public XVirtualMachine(ArmClientContext context, PhVirtualMachine resource) : base(context, resource.Id)
        {
            Model = resource;
        }

        public PhVirtualMachine Model { get; private set; }
    }
}
