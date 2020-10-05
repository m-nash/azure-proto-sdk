using Azure;
using azure_proto_core;
using azure_proto_core.Resources;
using System.Threading;

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
            return new VirtualMachineOperations(resourceGroup, $"{resourceGroup.Id}/providers/Microsoft.Compute/virtualMachines/{name}");
        }

        public static VirtualMachineOperations VirtualMachine(this ResourceGroupOperations resourceGroup, ResourceIdentifier vm)
        {
            return new VirtualMachineOperations(resourceGroup, vm);
        }

        public static VirtualMachineOperations VirtualMachine(this ResourceGroupOperations resourceGroup, TrackedResource vm)
        {
            return new VirtualMachineOperations(resourceGroup, vm);
        }

        public static VirtualMachineContainer VirtualMachines(this ResourceGroupOperations resourceGroup)
        {
            return new VirtualMachineContainer(resourceGroup.ClientContext, resourceGroup.Id);
        }
        #endregion

        #region AvailabilitySets
        public static AvailabilitySetOperations AvailabilitySet(this ResourceGroupOperations resourceGroup, string name)
        {
            return new AvailabilitySetOperations(resourceGroup, $"{resourceGroup.Id}/providers/Microsoft.Compute/virtualMachines/{name}");
        }
        public static AvailabilitySetOperations AvailabilitySet(this ResourceGroupOperations resourceGroup, ResourceIdentifier set)
        {
            return new AvailabilitySetOperations(resourceGroup, set);
        }

        public static AvailabilitySetOperations AvailabilitySet(this ResourceGroupOperations resourceGroup, TrackedResource set)
        {
            return new AvailabilitySetOperations(resourceGroup, set);
        }

        public static AvailabilitySetContainer AvailabilitySets(this ResourceGroupOperations resourceGroup)
        {
            return new AvailabilitySetContainer(resourceGroup.ClientContext, resourceGroup.Id);
        }
        #endregion
    }
}
