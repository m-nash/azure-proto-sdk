using Azure.ResourceManager.Network.Models;
using azure_proto_core;
using System.Collections.Generic;

namespace azure_proto_network
{
    public static class AzureResourceGroupExtension
    {
        private static Dictionary<string, PublicIpAddressCollection> ipCollections = new Dictionary<string, PublicIpAddressCollection>();

        public static PublicIpAddressCollection IpAddresses(this AzureResourceGroupBase resourceGroup)
        {
            PublicIpAddressCollection result;
            if (!ipCollections.TryGetValue(resourceGroup.Id, out result))
            {
                result = new PublicIpAddressCollection(resourceGroup);
                ipCollections.Add(resourceGroup.Id, result);
            }
            return result;
        }

        private static Dictionary<string, VnetCollection> vnetCollections = new Dictionary<string, VnetCollection>();

        public static VnetCollection VNets(this AzureResourceGroupBase resourceGroup)
        {
            VnetCollection result;
            if (!vnetCollections.TryGetValue(resourceGroup.Id, out result))
            {
                result = new VnetCollection(resourceGroup);
                vnetCollections.Add(resourceGroup.Id, result);
            }
            return result;
        }

        private static Dictionary<string, NicCollection> nicCollections = new Dictionary<string, NicCollection>();

        public static NicCollection Nics(this AzureResourceGroupBase resourceGroup)
        {
            NicCollection result;
            if (!nicCollections.TryGetValue(resourceGroup.Id, out result))
            {
                result = new NicCollection(resourceGroup);
                nicCollections.Add(resourceGroup.Id, result);
            }
            return result;
        }

        public static AzurePublicIpAddress ConstructIPAddress(this AzureResourceGroupBase resourceGroup)
        {
            var ipAddress = new PublicIPAddress()
            {
                PublicIPAddressVersion = Azure.ResourceManager.Network.Models.IPVersion.IPv4.ToString(),
                PublicIPAllocationMethod = IPAllocationMethod.Dynamic,
                Location = resourceGroup.Parent.Name,
            };
            return new AzurePublicIpAddress(resourceGroup, new PhPublicIPAddress(ipAddress));
        }

        public static AzureVnet ConstructVnet(this AzureResourceGroupBase resourceGroup, string vnetCidr)
        {
            var vnet = new VirtualNetwork()
            {
                Location = resourceGroup.Parent.Name,
                AddressSpace = new AddressSpace() { AddressPrefixes = new List<string>() { vnetCidr } },
            };
            return new AzureVnet(resourceGroup, new PhVirtualNetwork(vnet));
        }

        public static AzureNic ConstructNic(this AzureResourceGroupBase resourceGroup, AzurePublicIpAddress ip, string subnetId)
        {
            var nic = new NetworkInterface()
            {
                Location = resourceGroup.Parent.Name,
                IpConfigurations = new List<NetworkInterfaceIPConfiguration>()
                {
                    new NetworkInterfaceIPConfiguration()
                    {
                        Name = "Primary",
                        Primary = true,
                        Subnet = new Subnet() { Id = subnetId },
                        PrivateIPAllocationMethod = IPAllocationMethod.Dynamic,
                        PublicIPAddress = new PublicIPAddress() { Id = ip.Model.Id }
                    }
                }
            };
            return new AzureNic(resourceGroup, new PhNetworkInterface(nic));
        }
    }
}
