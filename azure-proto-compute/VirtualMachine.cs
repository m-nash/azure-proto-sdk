using azure_proto_core;

namespace azure_proto_compute
{
    public class VirtualMachine : VirtualMachineOperations
    {
        public VirtualMachine(ArmClientContext context, VirtualMachineData resource) : base(context, resource.Id)
        {
            Model = resource;
        }

        public VirtualMachineData Model { get; private set; }
    }
}
