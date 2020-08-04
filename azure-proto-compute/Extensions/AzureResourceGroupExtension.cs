using azure_proto_core;

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
