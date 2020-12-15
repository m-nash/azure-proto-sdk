using Azure.ResourceManager.Core;

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

        public static VirtualMachine VirtualMachine(this ResourceGroup resourceGroup, VirtualMachineData vm)
        {
            return new VirtualMachine(resourceGroup.ClientContext, vm, resourceGroup.ClientOptions);
        }

        public static VirtualMachineContainer VirtualMachines(this ResourceGroup resourceGroup)
        {
            return new VirtualMachineContainer(resourceGroup.ClientContext, resourceGroup.Data, resourceGroup.ClientOptions);
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

        public static AvailabilitySet AvailabilitySet(this ResourceGroup resourceGroup, AvailabilitySetData availabilitySet)
        {
            return new AvailabilitySet(resourceGroup.ClientContext, availabilitySet, resourceGroup.ClientOptions);
        }

        public static AvailabilitySetContainer AvailabilitySets(this ResourceGroup resourceGroup)
        {
            return new AvailabilitySetContainer(resourceGroup.ClientContext, resourceGroup.Data, resourceGroup.ClientOptions);
        }

        public static AvailabilitySetContainer AvailabilitySets(this ResourceGroupOperations resourceGroup)
        {
            return new AvailabilitySetContainer(resourceGroup.ClientContext, resourceGroup.Id, resourceGroup.ClientOptions);
        }
        #endregion
    }
}
