using azure_proto_core;
using System.Collections.Generic;

namespace azure_proto_compute
{
    public static class AzureResourceGroupExtension
    {
        private static Dictionary<string, VmCollection> vmCollections = new Dictionary<string, VmCollection>();
        private static readonly object vmLock = new object();

        public static VmCollection Vms(this AzureResourceGroupBase resourceGroup)
        {
            VmCollection result;
            if(!vmCollections.TryGetValue(resourceGroup.Id, out result))
            {
                lock (vmLock)
                {
                    if (!vmCollections.TryGetValue(resourceGroup.Id, out result))
                    {
                        result = new VmCollection(resourceGroup);
                        vmCollections.Add(resourceGroup.Id, result);
                    }
                }
            }
            return result;
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
