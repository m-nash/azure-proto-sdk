using azure_proto_core;

namespace azure_proto_compute.Extensions
{
    public static class ArmResourceOperationsExtensions
    {
        public static VirtualMachineOperations ToVirtualMachineOperations(this ArmResourceOperations armResourceOperations)
        {
            return new VirtualMachineOperations(armResourceOperations, armResourceOperations.ClientOptions);
        }
    }
}
