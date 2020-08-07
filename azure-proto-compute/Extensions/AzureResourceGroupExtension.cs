using azure_proto_core;
using Sku = azure_proto_core.Sku;
using azure_proto_compute.Convenience;
using Azure.ResourceManager.Compute.Models;

namespace azure_proto_compute
{
    public static class AzureResourceGroupExtension
    {
        public static VmCollection Vms(this AzureResourceGroupBase resourceGroup)
        {
            return resourceGroup.GetCollection<VmCollection, AzureVm>(() => { return new VmCollection(resourceGroup); });
        }

        public static AvailabilitySetCollection AvailabilitySets(this AzureResourceGroupBase resourceGroup)
        {
            return resourceGroup.GetCollection<AvailabilitySetCollection, AzureAvailabilitySet>(() => { return new AvailabilitySetCollection(resourceGroup); });
        }
    }
}
