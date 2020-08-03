using azure_proto_core;
using System.Collections.Generic;

namespace azure_proto_compute
{
    public static class AzureResourceGroupExtension
    {
        public static VmCollection Vms(this AzureResourceGroupBase resourceGroup)
        {
            return resourceGroup.GetCollection<VmCollection, AzureVm>("vms", () => { return new VmCollection(resourceGroup); });
        }

        private static Dictionary<string, AvailabilitySetCollection> asetCollections = new Dictionary<string, AvailabilitySetCollection>();
        private static readonly object asetLock = new object();

        public static AvailabilitySetCollection AvailabilitySets(this AzureResourceGroupBase resourceGroup)
        {
            AvailabilitySetCollection result;
            if (!asetCollections.TryGetValue(resourceGroup.Id, out result))
            {
                lock (asetLock)
                {
                    if (!asetCollections.TryGetValue(resourceGroup.Id, out result))
                    {
                        result = new AvailabilitySetCollection(resourceGroup);
                        asetCollections.Add(resourceGroup.Id, result);
                    }
                }
            }
            return result;
        }
    }
}
