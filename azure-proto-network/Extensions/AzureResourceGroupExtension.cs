using Azure.ResourceManager.Network.Models;
using azure_proto_core;
using System;
using System.Collections.Generic;
using System.Linq;

namespace azure_proto_network
{
    public static class AzureResourceGroupExtension
    {
        private static Dictionary<string, PublicIpAddressCollection> ipCollections = new Dictionary<string, PublicIpAddressCollection>(StringComparer.InvariantCultureIgnoreCase);
        private static readonly object ipLock = new object();

        public static PublicIpAddressCollection IpAddresses(this AzureProviderBase resourceGroup)
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

        public static VnetCollection VNets(this AzureProviderBase resourceGroup)
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

        public static NicCollection Nics(this AzureProviderBase resourceGroup)
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

        public static NetworkSecurityGroupCollection Nsgs(this AzureProviderBase resourceGroup)
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

        public static AzurePublicIpAddress ConstructIPAddress(this AzureProviderBase resourceGroup)
        {
            var ipAddress = new PublicIPAddress()
            {
                PublicIPAddressVersion = Azure.ResourceManager.Network.Models.IPVersion.IPv4.ToString(),
                PublicIPAllocationMethod = IPAllocationMethod.Dynamic,
                Location = resourceGroup.Location,
            };
            return new AzurePublicIpAddress(resourceGroup, new PhPublicIPAddress(ipAddress));
        }

        public static AzureVnet ConstructVnet(this AzureProviderBase resourceGroup, string vnetCidr)
        {
            var vnet = new VirtualNetwork()
            {
                Location = resourceGroup.Location,
                AddressSpace = new AddressSpace() { AddressPrefixes = new List<string>() { vnetCidr } },
            };
            return new AzureVnet(resourceGroup, new PhVirtualNetwork(vnet));
        }

        public static AzureNic ConstructNic(this AzureProviderBase resourceGroup, AzurePublicIpAddress ip, string subnetId)
        {
            var nic = new NetworkInterface()
            {
                Location = resourceGroup.Location,
                IpConfigurations = new List<NetworkInterfaceIPConfiguration>()
                {
                    new NetworkInterfaceIPConfiguration()
                    {
                        Name = "Primary",
                        Primary = true,
                        Subnet = new Subnet() { Id = subnetId },
                        PrivateIPAllocationMethod = IPAllocationMethod.Dynamic,
                        PublicIPAddress = new PublicIPAddress() { Id = ip.Id }
                    }
                }
            };
            return new AzureNic(resourceGroup, new PhNetworkInterface(nic));
        }

        /// <summary>
        /// Create an NSG with the given open TCP ports
        /// </summary>
        /// <param name="openPorts">The set of TCP ports to open</param>
        /// <returns>An NSG, with the given TCP ports open</returns>
        public static AzureNetworkSecurityGroup ConstructNsg(this AzureProviderBase resourceGroup, string nsgName, params int[] openPorts)
        {
            var nsg = new NetworkSecurityGroup { Location = resourceGroup.Location};
            var index = 0;
            nsg.SecurityRules = openPorts.Select(openPort => new SecurityRule
            {
                Name = $"Port{openPort}",
                Priority = 1000 + (++index),
                Protocol = SecurityRuleProtocol.Tcp,
                Access = SecurityRuleAccess.Allow,
                Direction = SecurityRuleDirection.Inbound,
                SourcePortRange = "*",
                SourceAddressPrefix =  "*",
                DestinationPortRange = $"{openPort}",
                DestinationAddressPrefix = "*",
                Description = $"Port_{openPort}"
            }).ToList();
            var result = new AzureNetworkSecurityGroup(resourceGroup, nsg, nsgName);

            return result;
        }

    }
}
