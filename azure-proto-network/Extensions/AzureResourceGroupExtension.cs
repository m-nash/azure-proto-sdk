using azure_proto_core;
using System;
using System.Collections.Generic;

namespace azure_proto_network
{
    public static class AzureResourceGroupExtension
    {
        private static Dictionary<string, PublicIpAddressCollection> ipCollections = new Dictionary<string, PublicIpAddressCollection>(StringComparer.InvariantCultureIgnoreCase);
        private static readonly object ipLock = new object();

        public static PublicIpAddressCollection IpAddresses(this AzureResourceGroupBase resourceGroup)
        {
            PublicIpAddressCollection result;
            if (!ipCollections.TryGetValue(resourceGroup.Id, out result))
            {
                lock (ipLock)
                {
                    if (!ipCollections.TryGetValue(resourceGroup.Id, out result))
                    {
                        result = new PublicIpAddressCollection(resourceGroup);
                        ipCollections.Add(resourceGroup.Id, result);
                    }
                }
            }

            return result;
        }

        private static Dictionary<string, VnetCollection> vnetCollections = new Dictionary<string, VnetCollection>(StringComparer.InvariantCultureIgnoreCase);
        private static readonly object vnetLock = new object();

        public static VnetCollection VNets(this AzureResourceGroupBase resourceGroup)
        {
            VnetCollection result;
            if (!vnetCollections.TryGetValue(resourceGroup.Id, out result))
            {
                lock (vnetLock)
                {
                    if (!vnetCollections.TryGetValue(resourceGroup.Id, out result))
                    {
                        result = new VnetCollection(resourceGroup);
                        vnetCollections.Add(resourceGroup.Id, result);
                    }
                }
            }

            return result;
        }

        private static Dictionary<string, NicCollection> nicCollections = new Dictionary<string, NicCollection>(StringComparer.InvariantCultureIgnoreCase);
        private static readonly object nicLock = new object();

        public static NicCollection Nics(this AzureResourceGroupBase resourceGroup)
        {
            NicCollection result;
            if (!nicCollections.TryGetValue(resourceGroup.Id, out result))
            {
                lock (nicLock)
                {
                    if (!nicCollections.TryGetValue(resourceGroup.Id, out result))
                    {
                        result = new NicCollection(resourceGroup);
                        nicCollections.Add(resourceGroup.Id, result);
                    }
                }
            }

            return result;
        }

        private static Dictionary<string, NetworkSecurityGroupCollection> nsgCollections = new Dictionary<string, NetworkSecurityGroupCollection>(StringComparer.InvariantCultureIgnoreCase);
        private static readonly object nsgLock = new object();

        public static NetworkSecurityGroupCollection Nsgs(this AzureResourceGroupBase resourceGroup)
        {
            NetworkSecurityGroupCollection result;
            lock(nsgLock)
            {
                if (!nsgCollections.TryGetValue(resourceGroup.Id, out result))
                {
                    result = new NetworkSecurityGroupCollection(resourceGroup);
                    nsgCollections.Add(resourceGroup.Id, result);
                }
            }

            return result;
        }
    }
}
