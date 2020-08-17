using azure_proto_core;
using System;
using System.Collections.Generic;
using System.Text;

namespace azure_proto_compute
{
    /// <summary>
    /// Naming of these methods
    /// </summary>
    public static class ResourceGroupExtensions
    {
        public static VmContainer Vms(this ResourceGroupContainerOperations operations, ResourceIdentifier resourceGroup)
        {
            return new VmContainer(operations, resourceGroup);
        }

        public static VmContainer Vms(this ResourceGroupContainerOperations operations, Resource resourceGroup)
        {
            return new VmContainer(operations, resourceGroup);
        }

        public static VmContainer Vms(this ResourceGroupContainerOperations operations, string resourceGroupName)
        {
            return new VmContainer(operations, $"/{operations.Context}/resourceGroups/{resourceGroupName}");
        }

        public static VmContainer Vms(this ResourceGroupOperations operations)
        {
            return new VmContainer(operations, operations.Context);
        }

        public static VmOperations Vm(this ResourceGroupOperations operations, string name)
        {
            return new VmOperations(operations, $"{operations.Context}/providers/Microsoft.Compute/virtualMachines/{name}");
        }
        public static VmOperations Vm(this ResourceGroupOperations operations, ResourceIdentifier vm)
        {
            return new VmOperations(operations, vm);
        }

        public static VmOperations Vm(this ResourceGroupOperations operations, TrackedResource vm)
        {
            return new VmOperations(operations, vm);
        }


        public static AvailabilitySetContainer AvailabilitySets(this ResourceGroupContainerOperations operations, ResourceIdentifier resourceGroup)
        {
            return new AvailabilitySetContainer(operations, resourceGroup);
        }

        public static AvailabilitySetContainer AvailabilitySets(this ResourceGroupContainerOperations operations, TrackedResource resourceGroup)
        {
            return new AvailabilitySetContainer(operations, resourceGroup);
        }

        public static AvailabilitySetContainer AvailabilitySets(this ResourceGroupContainerOperations operations, string resourceGroupName)
        {
            return new AvailabilitySetContainer(operations, $"/{operations.Context}/resourceGroups/{resourceGroupName}");
        }

        public static AvailabilitySetContainer AvailabilitySets(this ResourceGroupOperations operations)
        {
            return new AvailabilitySetContainer(operations, operations.Context);
        }

    }
}
