using azure_proto_core;

namespace azure_proto_compute
{
    /// <summary>
    /// Naming of these methods
    /// </summary>
    public static class ResourceGroupExtensions
    {
        #region VirtualMachines
        public static VirtualMachineOperations VirtualMachine(this ResourceGroupOperations resourceGroup, string name)
        {
            return new VirtualMachineOperations(resourceGroup.ClientContext, $"{resourceGroup.Id}/providers/Microsoft.Compute/virtualMachines/{name}");
        }

        public static VirtualMachineOperations VirtualMachine(this ResourceGroupOperations resourceGroup, ResourceIdentifier vmId)
        {
            return new VirtualMachineOperations(resourceGroup.ClientContext, vmId);
        }

        public static VirtualMachine VirtualMachine(this ResourceGroup resourceGroup, VirtualMachineData vm)
        {
            return new VirtualMachine(resourceGroup.ClientContext, vm);
        }

        public static VirtualMachineContainer VirtualMachines(this ResourceGroup resourceGroup)
        {
            return new VirtualMachineContainer(resourceGroup.ClientContext, resourceGroup.Model);
        }

        public static VirtualMachineContainer VirtualMachines(this ResourceGroupOperations resourceGroup)
        {
            return new VirtualMachineContainer(resourceGroup.ClientContext, resourceGroup.Id);
        }
        #endregion

        #region AvailabilitySets
        public static AvailabilitySetOperations AvailabilitySet(this ResourceGroupOperations resourceGroup, string name)
        {
            return new AvailabilitySetOperations(resourceGroup.ClientContext, $"{resourceGroup.Id}/providers/Microsoft.Compute/availabilitySets/{name}");
        }

        public static AvailabilitySetOperations AvailabilitySet(this ResourceGroupOperations resourceGroup, ResourceIdentifier availabilitySetId)
        {
            return new AvailabilitySetOperations(resourceGroup.ClientContext, availabilitySetId);
        }

        public static AvailabilitySet AvailabilitySet(this ResourceGroup resourceGroup, AvailabilitySetData availabilitySet)
        {
            return new AvailabilitySet(resourceGroup.ClientContext, availabilitySet);
        }

        public static AvailabilitySetContainer AvailabilitySets(this ResourceGroup resourceGroup)
        {
            return new AvailabilitySetContainer(resourceGroup.ClientContext, resourceGroup.Model);
        }

        public static AvailabilitySetContainer AvailabilitySets(this ResourceGroupOperations resourceGroup)
        {
            return new AvailabilitySetContainer(resourceGroup.ClientContext, resourceGroup.Id);
        }
        #endregion
    }
}
