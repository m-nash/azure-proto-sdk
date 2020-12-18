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
            return new VirtualMachineOperations(resourceGroup.ClientOptions, $"{resourceGroup.Id}/providers/Microsoft.Compute/virtualMachines/{name}");
        }

        public static VirtualMachineOperations VirtualMachine(this ResourceGroupOperations resourceGroup, ResourceIdentifier vmId)
        {
            return new VirtualMachineOperations(resourceGroup.ClientOptions, vmId);
        }

        public static VirtualMachine VirtualMachine(this ResourceGroup resourceGroup, VirtualMachineData vm)
        {
            return new VirtualMachine(resourceGroup.ClientOptions, vm);
        }

        public static VirtualMachineContainer VirtualMachines(this ResourceGroup resourceGroup)
        {
            return new VirtualMachineContainer(resourceGroup.ClientOptions, resourceGroup.Data);
        }

        public static VirtualMachineContainer VirtualMachines(this ResourceGroupOperations resourceGroup)
        {
            return new VirtualMachineContainer(resourceGroup.ClientOptions, resourceGroup.Id);
        }
        #endregion

        #region AvailabilitySets
        public static AvailabilitySetOperations AvailabilitySet(this ResourceGroupOperations resourceGroup, string name)
        {
            return new AvailabilitySetOperations(resourceGroup.ClientOptions, $"{resourceGroup.Id}/providers/Microsoft.Compute/availabilitySets/{name}");
        }

        public static AvailabilitySetOperations AvailabilitySet(this ResourceGroupOperations resourceGroup, ResourceIdentifier availabilitySetId)
        {
            return new AvailabilitySetOperations(resourceGroup.ClientOptions, availabilitySetId);
        }

        public static AvailabilitySet AvailabilitySet(this ResourceGroup resourceGroup, AvailabilitySetData availabilitySet)
        {
            return new AvailabilitySet(resourceGroup.ClientOptions, availabilitySet);
        }

        public static AvailabilitySetContainer AvailabilitySets(this ResourceGroup resourceGroup)
        {
            return new AvailabilitySetContainer(resourceGroup.ClientOptions, resourceGroup.Data);
        }

        public static AvailabilitySetContainer AvailabilitySets(this ResourceGroupOperations resourceGroup)
        {
            return new AvailabilitySetContainer(resourceGroup.ClientOptions, resourceGroup.Id);
        }
        #endregion
    }
}
