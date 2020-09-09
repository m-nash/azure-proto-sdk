using azure_proto_core;

namespace azure_proto_compute
{
    /// <summary>
    /// VM Operations at the subscription level
    /// </summary>
    public class VirtualMachineCollection : ResourceCollectionOperations<PhVirtualMachine>
    {
        public VirtualMachineCollection(ArmClientContext parent, ResourceIdentifier context) : base(parent, context)
        {
        }

        public VirtualMachineCollection(ArmClientContext parent, azure_proto_core.Resource context) : base(parent, context)
        {
        }

        public VirtualMachineCollection(OperationsBase parent, ResourceIdentifier context) : base(parent, context)
        {
        }

        public VirtualMachineCollection(OperationsBase parent, azure_proto_core.Resource context) : base(parent, context)
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

        protected override ResourceOperationsBase<PhVirtualMachine> GetOperations(ResourceIdentifier identifier, azure_proto_core.Location location)
        {
            return new VirtualMachineOperations(this, new ArmResource(identifier, location));
        }

        public override ResourceType ResourceType => "Microsoft.Compute/virtualMachines";
    }
}
