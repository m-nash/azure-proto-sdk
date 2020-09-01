using azure_proto_core;

namespace azure_proto_compute
{
    /// <summary>
    /// VM Operations at the subscription level
    /// </summary>
    public class VmCollection : ResourceCollectionOperations<PhVirtualMachine>
    {
        public VmCollection(ArmOperations parent, ResourceIdentifier context) : base(parent, context)
        {
        }

        public VmCollection(ArmOperations parent, azure_proto_core.Resource context) : base(parent, context)
        {
        }

        public VmOperations Vm(string resourceGroupName, string vmName)
        {
            return new VmOperations(this, new ResourceIdentifier($"{Context}/resourceGroups/{resourceGroupName}/providers/Microsoft.Compute/virtualMachines/{vmName}"));
        }

        public VmOperations Vm(ResourceIdentifier vm)
        {
            return new VmOperations(this, vm);
        }

        public VmOperations Vm(TrackedResource vm)
        {
            return new VmOperations(this, vm);
        }

        protected override ResourceOperations<PhVirtualMachine> GetOperations(ResourceIdentifier identifier, azure_proto_core.Location location)
        {
            return new VmOperations(this, new ArmResource(identifier, location));
        }

        protected override ResourceType ResourceType => "Microsoft.Compute/virtualMachines";
    }
}
