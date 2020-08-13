using azure_proto_core;
using System;
using System.Collections.Generic;
using System.Text;

namespace azure_proto_compute.Extensions
{
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

        public static AvailabilitySetContainer AvailabilitySets(this ResourceGroupContainerOperations operations, ResourceIdentifier resourceGroup)
        {
            return new AvailabilitySetContainer(operations, resourceGroup);
        }

        public static AvailabilitySetContainer AvailabilitySets(this ResourceGroupContainerOperations operations, Resource resourceGroup)
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
