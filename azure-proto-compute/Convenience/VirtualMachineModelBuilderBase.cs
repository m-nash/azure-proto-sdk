using azure_proto_core;

namespace azure_proto_compute.Convenience
{
    public abstract class VirtualMachineModelBuilderBase : ArmBuilder<VirtualMachine, VirtualMachineData>
    {
        protected VirtualMachineModelBuilderBase(VirtualMachineContainer containerOperations, VirtualMachineData vm): base(containerOperations, vm){ }

        public abstract VirtualMachineModelBuilderBase UseWindowsImage(string adminUser, string password);

        public abstract VirtualMachineModelBuilderBase UseLinuxImage(string adminUser, string password);

        public abstract VirtualMachineModelBuilderBase RequiredNetworkInterface(ResourceIdentifier nicResourceId);

        public abstract VirtualMachineModelBuilderBase RequiredAvalabilitySet(ResourceIdentifier asetResourceId);
    }
}
