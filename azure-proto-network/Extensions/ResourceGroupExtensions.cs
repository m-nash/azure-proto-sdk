// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT License.

using Azure.ResourceManager.Core;

namespace azure_proto_network
{
    /// <summary>
    /// A class to add extension methods to Resource Group.
    /// </summary>
    public static class ResourceGroupExtensions
    {
        #region Virtual Network Operations

        /// <summary>
        /// Gets a <see cref="VirtualNetwork"/> for a given resource under a <see cref="ResourceGroup"/>. 
        /// </summary>
        /// <param name="resourceGroup"> The <see cref="ResourceGroup" /> instance the method will execute against. </param>
        /// <param name="virtualNetwork"> The <see cref="VirtualNetworkData" /> data model. </param>
        /// <returns></returns>
        public static VirtualNetwork VirtualNetwork(this ResourceGroup resourceGroup, VirtualNetworkData virtualNetwork)
        {
            return new VirtualNetwork(resourceGroup.ClientOptions, virtualNetwork);
        }

        /// <summary>
        /// Gets a <see cref="VirtualNetworkOperations"/> for a given resource id under a <see cref="ResourceGroup"/>. 
        /// </summary>
        /// <param name="resourceGroup"> The <see cref="ResourceGroup" /> instance the method will execute against. </param>
        /// <param name="virtualNetwork"> The resource id of <see cref="VirtualNetwork" /> data model. </param>
        /// <returns></returns>
        public static VirtualNetworkOperations VirtualNetwork(this ResourceGroupOperations resourceGroup, string virtualNetwork)
        {
            return new VirtualNetworkOperations(resourceGroup.ClientOptions, new ResourceIdentifier($"{resourceGroup.Id}/providers/Microsoft.Network/virtualNetworks/{virtualNetwork}"));
        }

        /// <summary>
        /// Gets a <see cref="VirtualNetworkContainer"/> under a <see cref="ResourceGroup"/>. 
        /// </summary>
        /// <param name="resourceGroup"> The <see cref="ResourceGroup" /> instance the method will execute against. </param>
        /// <returns></returns>
        public static VirtualNetworkContainer VirtualNetworks(this ResourceGroup resourceGroup)
        {
            return new VirtualNetworkContainer(resourceGroup.ClientOptions, resourceGroup.Data);
        }

        public static VirtualNetworkContainer VirtualNetworks(this ResourceGroupOperations resourceGroup)
        {
            return new VirtualNetworkContainer(resourceGroup.ClientOptions, resourceGroup.Id);
        }

        #endregion

        #region Public IP Address Operations

        public static PublicIpAddress PublicIpAddress(this ResourceGroupOperations resourceGroup, PublicIPAddressData publicIpAddress)
        {
            return new PublicIpAddress(resourceGroup.ClientOptions, publicIpAddress);
        }

        public static PublicIpAddressOperations PublicIpAddress(this ResourceGroupOperations resourceGroup, string publicIpAddress)
        {
            return new PublicIpAddressOperations(resourceGroup.ClientOptions, new ResourceIdentifier($"{resourceGroup.Id}/providers/Microsoft.Network/publicIpAddresses/{publicIpAddress}"));
        }

        public static PublicIpAddressContainer PublicIpAddresses(this ResourceGroup resourceGroup)
        {
            return new PublicIpAddressContainer(resourceGroup.ClientOptions, resourceGroup.Data);
        }

        public static PublicIpAddressContainer PublicIpAddresses(this ResourceGroupOperations resourceGroup)
        {
            return new PublicIpAddressContainer(resourceGroup.ClientOptions, resourceGroup.Id);
        }

        #endregion

        #region Network Interface (NIC) operations

        public static NetworkInterface NetworkInterface(this ResourceGroupOperations resourceGroup, NetworkInterfaceData networkInterface)
        {
            return new NetworkInterface(resourceGroup.ClientOptions, networkInterface);
        }

        public static NetworkInterfaceOperations NetworkInterface(this ResourceGroupOperations resourceGroup, string networkInterface)
        {
            return new NetworkInterfaceOperations(resourceGroup.ClientOptions, new ResourceIdentifier($"{resourceGroup.Id}/providers/Microsoft.Network/networkInterfaces/{networkInterface}"));
        }

        public static NetworkInterfaceContainer NetworkInterfaces(this ResourceGroup resourceGroup)
        {
            return new NetworkInterfaceContainer(resourceGroup.ClientOptions, resourceGroup.Data);
        }

        public static NetworkInterfaceContainer NetworkInterfaces(this ResourceGroupOperations resourceGroup)
        {
            return new NetworkInterfaceContainer(resourceGroup.ClientOptions, resourceGroup.Id);
        }

        #endregion

        #region Network Security Group operations

        /// <summary>
        /// Create an NSG with the given open TCP ports
        /// </summary>
        /// <param name="openPorts">The set of TCP ports to open</param>
        /// <returns>An NSG, with the given TCP ports open</returns>
        public static NetworkSecurityGroup NetworkSecurityGroup(this ResourceGroupOperations resourceGroup, NetworkSecurityGroupData networkSecurityGroup)
        {
            return new NetworkSecurityGroup(resourceGroup.ClientOptions, networkSecurityGroup);
        }

        public static NetworkSecurityGroupOperations NetworkSecurityGroup(this ResourceGroupOperations operations, string networkSecurityGroup)
        {
            return new NetworkSecurityGroupOperations(operations.ClientOptions, new ResourceIdentifier($"{operations.Id}/providers/Microsoft.Network/networkSecurityGroups/{networkSecurityGroup}"));
        }

        public static NetworkSecurityGroupContainer NetworkSecurityGroups(this ResourceGroup resourceGroup)
        {
            return new NetworkSecurityGroupContainer(resourceGroup.ClientOptions, resourceGroup.Data);
        }

        public static NetworkSecurityGroupContainer NetworkSecurityGroups(this ResourceGroupOperations resourceGroup)
        {
            return new NetworkSecurityGroupContainer(resourceGroup.ClientOptions, resourceGroup.Id);
        }

        #endregion
    }
}
