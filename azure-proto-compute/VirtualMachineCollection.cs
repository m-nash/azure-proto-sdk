using azure_proto_core;

namespace azure_proto_compute
{
    /// <summary>
    /// VM Operations at the subscription level
    /// </summary>
    public class VirtualMachineCollection : ResourceCollectionOperations<PhVirtualMachine>
    {
        public VirtualMachineCollection(ArmClientBase parent, ResourceIdentifier context) : base(parent, context)
        {
        }

        public VirtualMachineCollection(ArmClientBase parent, azure_proto_core.Resource context) : base(parent, context)
        {
        }

        public VirtualMachineOperations Vm(string resourceGroupName, string vmName)
        {
            return new VirtualMachineOperations(this, new ResourceIdentifier($"{Context}/resourceGroups/{resourceGroupName}/providers/Microsoft.Compute/virtualMachines/{vmName}"));
        }

        public VirtualMachineOperations Vm(ResourceIdentifier vm)
        {
            return new VirtualMachineOperations(this, vm);
        }

        public VirtualMachineOperations Vm(TrackedResource vm)
        {
            return new VirtualMachineOperations(this, vm);
        }

        protected override ResourceClientBase<PhVirtualMachine> GetOperations(ResourceIdentifier identifier, azure_proto_core.Location location)
        {
            return new VirtualMachineOperations(this, new ArmResource(identifier, location));
        }

        protected override ResourceType ResourceType => "Microsoft.Compute/virtualMachines";
    }
}
