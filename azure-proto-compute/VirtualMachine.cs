using azure_proto_core;

namespace azure_proto_compute
{
    public class VirtualMachine : VirtualMachineOperations
    {
        public VirtualMachine(ArmClientContext context, VirtualMachineData resource, ArmClientOptions options)
            : base(context, resource.Id, options)
        {
            Data = resource;
        }

        public VirtualMachineData Data { get; private set; }
    }
}
