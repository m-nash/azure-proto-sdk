using azure_proto_core;
using System;
using System.Collections.Generic;
using System.Text;

namespace azure_proto_compute.Extensions
{
    public static class ResourceGroupExtensions
    {
        public static VmContainer Vms(this ResourceGroupContainerOperations rg, ResourceIdentifier context)
        {
            return new VmContainer(rg, context);
        }

        public static VmCollection Vms(this ResourceGroupContainerOperations rg, Resource context)
        {
            return new VmCollection(rg, context);
        }

        public static VmCollection Vms(this ResourceGroupContainerOperations rg, string resourceGroupName)
        {
            return new VmCollection(rg, $"/{rg.Context}/resourceGroups/{resourceGroupName}");
        }
    }
}
