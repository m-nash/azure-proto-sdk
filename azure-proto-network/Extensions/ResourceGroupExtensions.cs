using Azure;
using Azure.ResourceManager.Network.Models;
using azure_proto_core;
using azure_proto_core.Resources;
using System.Collections.Generic;
using System.Linq;
using System.Threading;

namespace azure_proto_network
{
    public static class ResourceGroupExtensions
    {
        #region Virtual Network Operations
        public static VirtualNetworkOperations VirtualNetwork(this ResourceGroupOperations resourceGroup, TrackedResource vnet)
        {
            return new VirtualNetworkOperations(resourceGroup, vnet);
        }

        public static VirtualNetworkOperations VirtualNetwork(this ResourceGroupOperations resourceGroup, string vnet)
        {
            return new VirtualNetworkOperations(resourceGroup, new ResourceIdentifier($"{resourceGroup.Id}/providers/Microsoft.Network/virtualNetworks/{vnet}"));
        }

        public static VirtualNetworkContainer VirtualNetworks(this ResourceGroupOperations resourceGroup)
        {
            return new VirtualNetworkContainer(resourceGroup.ClientContext, resourceGroup.Id);
        }

        public static Pageable<VirtualNetworkOperations> ListVirtualNetworks(this ResourceGroupOperations resourceGroup, ArmSubstringFilter filter = null, int? top = null, CancellationToken cancellationToken = default)
        {
            return ResourceListOperations.ListAtContext<VirtualNetworkOperations, PhVirtualNetwork>(resourceGroup, filter, top, cancellationToken);
        }

        public static AsyncPageable<VirtualNetworkOperations> ListVvirtualNetworksAsync(this ResourceGroupOperations resourceGroup, ArmSubstringFilter filter = null, int? top = null, CancellationToken cancellationToken = default)
        {
            return ResourceListOperations.ListAtContextAsync<VirtualNetworkOperations, PhVirtualNetwork>(resourceGroup, filter, top, cancellationToken);
        }
        #endregion

        #region Public IP Address Operations
        public static PublicIpAddressOperations PublicIpAddress(this ResourceGroupOperations resourceGroup, TrackedResource vnet)
        {
            return new PublicIpAddressOperations(resourceGroup, vnet);
        }

        public static PublicIpAddressOperations PublicIpAddress(this ResourceGroupOperations resourceGroup, string vnet)
        {
            return new PublicIpAddressOperations(resourceGroup, new ResourceIdentifier($"{resourceGroup.Id}/providers/Microsoft.Network/virtualNetworks/{vnet}"));
        }

        public static PublicIpAddressContainer PublicIpAddresses(this ResourceGroupOperations resourceGroup)
        {
            return new PublicIpAddressContainer(resourceGroup.ClientContext, resourceGroup.Id);
        }

        public static Pageable<PublicIpAddressOperations> ListPublicIpAddresses(this ResourceGroupOperations resourceGroup, ArmSubstringFilter filter = null, int? top = null, CancellationToken cancellationToken = default)
        {
            return ResourceListOperations.ListAtContext<PublicIpAddressOperations, PhPublicIPAddress>(resourceGroup, filter, top, cancellationToken);
        }

        public static AsyncPageable<PublicIpAddressOperations> ListPublicIpAddressesAsync(this ResourceGroupOperations resourceGroup, ArmSubstringFilter filter = null, int? top = null, CancellationToken cancellationToken = default)
        {
            return ResourceListOperations.ListAtContextAsync<PublicIpAddressOperations, PhPublicIPAddress>(resourceGroup, filter, top, cancellationToken);
        }
        #endregion

        #region Network Interface (NIC) operations
        public static NetworkInterfaceOperations NetworkInterface(this ResourceGroupOperations resourceGroup, TrackedResource vnet)
        {
            return new NetworkInterfaceOperations(resourceGroup, vnet);
        }

        public static NetworkInterfaceOperations NetworkInterface(this ResourceGroupOperations resourceGroup, string vnet)
        {
            return new NetworkInterfaceOperations(resourceGroup, new ResourceIdentifier($"{resourceGroup.Id}/providers/Microsoft.Network/virtualNetworks/{vnet}"));
        }

        public static NetworkInterfaceContainer NetworkInterfaces(this ResourceGroupOperations resourceGroup)
        {
            return new NetworkInterfaceContainer(resourceGroup.ClientContext, resourceGroup.Id);
        }

        public static Pageable<NetworkInterfaceOperations> ListNetworkInterfaces(this ResourceGroupOperations resourceGroup, ArmSubstringFilter filter = null, int? top = null, CancellationToken cancellationToken = default)
        {
            return ResourceListOperations.ListAtContext<NetworkInterfaceOperations, PhNetworkInterface>(resourceGroup, filter, top, cancellationToken);
        }

        public static AsyncPageable<NetworkInterfaceOperations> ListNetworkInterfacesAsync(this ResourceGroupOperations resourceGroup, ArmSubstringFilter filter = null, int? top = null, CancellationToken cancellationToken = default)
        {
            return ResourceListOperations.ListAtContextAsync<NetworkInterfaceOperations, PhNetworkInterface>(resourceGroup, filter, top, cancellationToken);
        }
        #endregion

        #region Network Security Group operations
        /// <summary>
        /// Create an NSG with the given open TCP ports
        /// </summary>
        /// <param name="openPorts">The set of TCP ports to open</param>
        /// <returns>An NSG, with the given TCP ports open</returns>
        public static NetworkSecurityGroupOperations NetworkSecurityGroup(this ResourceGroupOperations resourceGroup, TrackedResource vnet)
        {
            return new NetworkSecurityGroupOperations(resourceGroup, vnet);
        }

        public static NetworkSecurityGroupOperations NetworkSecurityGroup(this ResourceGroupOperations operations, string vnet)
        {
            return new NetworkSecurityGroupOperations(operations, new ResourceIdentifier($"{operations.Id}/providers/Microsoft.Network/virtualNetworks/{vnet}"));
        }

        public static NetworkSecurityGroupContainer NetworkSecurityGroups(this ResourceGroupOperations resourceGroup)
        {
            return new NetworkSecurityGroupContainer(resourceGroup.ClientContext, resourceGroup.Id);
        }

        public static Pageable<NetworkSecurityGroupOperations> ListNetworkSecurityGroups(this ResourceGroupOperations resourceGroup, ArmSubstringFilter filter = null, int? top = null, CancellationToken cancellationToken = default)
        {
            return ResourceListOperations.ListAtContext<NetworkSecurityGroupOperations, PhNetworkSecurityGroup>(resourceGroup, filter, top, cancellationToken);
        }

        public static AsyncPageable<NetworkSecurityGroupOperations> ListNetworkSecurityGroupsAsync(this ResourceGroupOperations resourceGroup, ArmSubstringFilter filter = null, int? top = null, CancellationToken cancellationToken = default)
        {
            return ResourceListOperations.ListAtContextAsync<NetworkSecurityGroupOperations, PhNetworkSecurityGroup>(resourceGroup, filter, top, cancellationToken);
        }
        #endregion
    }
}
