using azure_proto_core;
using System;
using System.Collections.Generic;
using System.Text;

namespace azure_proto_compute.Extensions
{
    public static class ProviderResourcesExtensions
    {
        public static VmContainer Vms(this ProviderResources providers, ResourceIdentifier resourceGroup)
        {
            return new VmContainer(providers, resourceGroup);
        }

        public static VmContainer Vms(this ProviderResources providers, PhSubscriptionModel resourceGroup)
        {
            return new VmContainer(providers, resourceGroup);
        }

        public static VmContainer Vms(this ProviderResources providers, string subscription, string resourceGroup)
        {
            return new VmContainer(providers, $"/subscriptions/{subscription}/resourceGroups/{resourceGroup}");
        }


        public static VmOperations Vm(this ProviderResources providers, string subscription, string resourceGroup, string vm)
        {
            return new VmOperations(providers, $"/subscriptions/{subscription}/resourceGroups/{resourceGroup}/providers/Microsoft.Compute/virtualMachines/{vm}");
        }

        public static VmOperations Vm(this ProviderResources providers, ResourceIdentifier vm)
        {
            return new VmOperations(providers, vm);
        }

        public static VmOperations Vm(this ProviderResources providers, PhVirtualMachine vm)
        {
            return new VmOperations(providers, vm);
        }

    }
}
