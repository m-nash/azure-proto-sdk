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
            return new VirtualMachineOperations(resourceGroup.ClientContext, $"{resourceGroup.Id}/providers/Microsoft.Compute/virtualMachines/{name}", resourceGroup.ClientOptions);
        }

        public static VirtualMachineOperations VirtualMachine(this ResourceGroupOperations resourceGroup, ResourceIdentifier vmId)
        {
            return new VirtualMachineOperations(resourceGroup.ClientContext, vmId, resourceGroup.ClientOptions);
        }

        public static XVirtualMachine VirtualMachine(this XResourceGroup resourceGroup, PhVirtualMachine vm)
        {
            return new XVirtualMachine(resourceGroup.ClientContext, vm, resourceGroup.ClientOptions);
        }

        public static VirtualMachineContainer VirtualMachines(this XResourceGroup resourceGroup)
        {
            return new VirtualMachineContainer(resourceGroup.ClientContext, resourceGroup.Model, resourceGroup.ClientOptions);
        }

        public static VirtualMachineContainer VirtualMachines(this ResourceGroupOperations resourceGroup)
        {
            return new VirtualMachineContainer(resourceGroup.ClientContext, resourceGroup.Id, resourceGroup.ClientOptions);
        }
        #endregion

        #region AvailabilitySets
        public static AvailabilitySetOperations AvailabilitySet(this ResourceGroupOperations resourceGroup, string name)
        {
            return new AvailabilitySetOperations(resourceGroup.ClientContext, $"{resourceGroup.Id}/providers/Microsoft.Compute/availabilitySets/{name}", resourceGroup.ClientOptions);
        }

        public static AvailabilitySetOperations AvailabilitySet(this ResourceGroupOperations resourceGroup, ResourceIdentifier availabilitySetId)
        {
            return new AvailabilitySetOperations(resourceGroup.ClientContext, availabilitySetId, resourceGroup.ClientOptions);
        }

        public static XAvailabilitySet AvailabilitySet(this XResourceGroup resourceGroup, PhAvailabilitySet availabilitySet)
        {
            return new XAvailabilitySet(resourceGroup.ClientContext, availabilitySet, resourceGroup.ClientOptions);
        }

        public static AvailabilitySetContainer AvailabilitySets(this XResourceGroup resourceGroup)
        {
            return new AvailabilitySetContainer(resourceGroup.ClientContext, resourceGroup.Model, resourceGroup.ClientOptions);
        }

        public static AvailabilitySetContainer AvailabilitySets(this ResourceGroupOperations resourceGroup)
        {
            return new AvailabilitySetContainer(resourceGroup.ClientContext, resourceGroup.Id, resourceGroup.ClientOptions);
        }
        #endregion
    }
}
