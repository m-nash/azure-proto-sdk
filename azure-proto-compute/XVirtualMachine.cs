using azure_proto_core;

namespace azure_proto_compute
{
    public class XVirtualMachine : VirtualMachineOperations
    {
        public XVirtualMachine(ArmClientContext context, PhVirtualMachine resource, ArmClientOptions clientOptions) : base(context, resource.Id, clientOptions)
        {
            Model = resource;
        }

        public PhVirtualMachine Model { get; private set; }
    }
}
