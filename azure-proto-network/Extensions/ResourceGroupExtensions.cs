// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT License.

using Azure.ResourceManager.Core;

namespace azure_proto_network
{
    /// <summary>
    /// A class to add extension methods to resource group.
    /// </summary>
    public static class ResourceGroupExtensions
    {
        #region Virtual Network Operations
        /// <summary>
        /// Gets a <see cref="VirtualNetworkOperations"/> for a given resource under a <see cref="ResourceGroup"/>. 
        /// </summary>
        /// <param name="resourceGroup"> The <see cref="ResourceGroupOperations" /> instance the method will execute against. </param>
        /// <param name="virtualNetwork"> The resource id of <see cref="VirtualNetwork" /> data model. </param>
        /// <returns> An instance of <see cref="VirtualNetworkOperations" />. </returns>
        public static VirtualNetworkOperations VirtualNetwork(this ResourceGroupOperations resourceGroup, string virtualNetwork)
        {
            return new VirtualNetworkOperations(resourceGroup, virtualNetwork);
        }

        /// <summary>
        /// Gets a <see cref="VirtualNetworkContainer"/> under a <see cref="ResourceGroup"/>. 
        /// </summary>
        /// <param name="resourceGroup"> The <see cref="ResourceGroupOperations" /> instance the method will execute against. </param>
        /// <returns> An instance of <see cref="VirtualNetworkContainer" />. </returns>
        public static VirtualNetworkContainer VirtualNetworks(this ResourceGroupOperations resourceGroup)
        {
            return new VirtualNetworkContainer(resourceGroup);
        }
        #endregion

        #region Public IP Address Operations
        /// <summary>
        /// Gets a <see cref="PublicIpAddressOperations"/> under a <see cref="ResourceGroup"/>. 
        /// </summary>
        /// <param name="resourceGroup"> The <see cref="ResourceGroupOperations" /> instance the method will execute against. </param>
        /// <param name="publicIpAddress"> The resource id of <see cref="PublicIpAddress" /> data model. </param>
        /// <returns> An instance of <see cref="PublicIpAddressOperations" />. </returns>
        public static PublicIpAddressOperations PublicIpAddress(this ResourceGroupOperations resourceGroup, string publicIpAddress)
        {
            return new PublicIpAddressOperations(resourceGroup, publicIpAddress);
        }

        /// <summary>
        /// Gets a <see cref="PublicIpAddressContainer"/> under a <see cref="ResourceGroup"/>. 
        /// </summary>
        /// <param name="resourceGroup"> The <see cref="ResourceGroup" /> instance the method will execute against. </param>
        /// <returns> An instance of <see cref="PublicIpAddressContainer" />. </returns>
        public static PublicIpAddressContainer PublicIpAddresses(this ResourceGroup resourceGroup)
        {
            return new PublicIpAddressContainer(resourceGroup);
        }
        #endregion

        #region Network Interface (NIC) operations
        /// <summary>
        /// Gets the operations over a specific <see cref="NetworkInterface>"/>
        /// </summary>
        /// <param name="resourceGroup"> The operations over a specific resource group. </param>
        /// <param name="networkInterface"> The name of the network interface to target for operations. </param>
        /// <returns> A <see cref="NetworkInterface"/> including the operations that can be peformed on it. </returns>
        public static NetworkInterfaceOperations NetworkInterface(this ResourceGroupOperations resourceGroup, string networkInterface)
        {
            return new NetworkInterfaceOperations(resourceGroup, networkInterface);
        }

        /// <summary>
        /// Gets the operations over the collection of <see cref="NetworkInterface"/> contained in the resource group.
        /// </summary>
        /// <param name="resourceGroup"> The <see cref="ResourceGroup"/> that contains the network interfaces. </param>
        /// <returns> A <see cref="NetworkInterfaceContainer"/> representing the collection of <see cref="NetworkInterface"/> 
        /// in the resource group. </returns>
        public static NetworkInterfaceContainer NetworkInterfaces(this ResourceGroup resourceGroup)
        {
            return new NetworkInterfaceContainer(resourceGroup);
        }
        #endregion

        #region Network Security Group operations
        /// <summary>
        /// Gets a <see cref="NetworkSecurityGroupOperations"/> under a <see cref="ResourceGroup"/>.
        /// </summary>
        /// <param name="operations"> The <see cref="ResourceGroupOperations" /> instance the method will execute against. </param>
        /// <param name="networkSecurityGroup"> The resource id of <see cref="NetworkSecurityGroup" /> data model. </param>
        /// <returns> An instance of <see cref="NetworkSecurityGroupOperations" />. </returns>
        public static NetworkSecurityGroupOperations NetworkSecurityGroup(this ResourceGroupOperations operations, string networkSecurityGroup)
        {
            return new NetworkSecurityGroupOperations(operations, networkSecurityGroup);
        }

        /// <summary>
        /// Gets a <see cref="NetworkSecurityGroupContainer"/> under a <see cref="ResourceGroup"/>.
        /// </summary>
        /// <param name="resourceGroup"> The <see cref="ResourceGroupOperations" /> instance the method will execute against. </param>
        /// <returns> An instance of <see cref="NetworkSecurityGroupContainer" />. </returns>
        public static NetworkSecurityGroupContainer NetworkSecurityGroups(this ResourceGroupOperations resourceGroup)
        {
            return new NetworkSecurityGroupContainer(resourceGroup);
        }
        #endregion
    }
}
