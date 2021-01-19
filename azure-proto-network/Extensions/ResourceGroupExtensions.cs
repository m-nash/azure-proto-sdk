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
        /// Gets a <see cref="VirtualNetwork"/> for a given resource under a <see cref="ResourceGroup"/>. 
        /// </summary>
        /// <param name="resourceGroup"> The <see cref="ResourceGroup" /> instance the method will execute against. </param>
        /// <param name="virtualNetwork"> The <see cref="VirtualNetworkData" /> data model. </param>
        /// <returns> An instance of <see cref="VirtualNetwork" />. </returns>
        public static VirtualNetwork VirtualNetwork(this ResourceGroup resourceGroup, VirtualNetworkData virtualNetwork)
        {
            return new VirtualNetwork(resourceGroup.ClientOptions, virtualNetwork);
        }

        /// <summary>
        /// Gets a <see cref="VirtualNetworkOperations"/> for a given resource under a <see cref="ResourceGroup"/>. 
        /// </summary>
        /// <param name="resourceGroup"> The <see cref="ResourceGroupOperations" /> instance the method will execute against. </param>
        /// <param name="virtualNetwork"> The resource id of <see cref="VirtualNetwork" /> data model. </param>
        /// <returns> An instance of <see cref="VirtualNetworkOperations" />. </returns>
        public static VirtualNetworkOperations VirtualNetwork(this ResourceGroupOperations resourceGroup, string virtualNetwork)
        {
            return new VirtualNetworkOperations(resourceGroup.ClientOptions, new ResourceIdentifier($"{resourceGroup.Id}/providers/Microsoft.Network/virtualNetworks/{virtualNetwork}"));
        }

        /// <summary>
        /// Gets a <see cref="VirtualNetworkContainer"/> under a <see cref="ResourceGroup"/>. 
        /// </summary>
        /// <param name="resourceGroup"> The <see cref="ResourceGroup" /> instance the method will execute against. </param>
        /// <returns> An instance of <see cref="VirtualNetworkContainer" />. </returns>
        public static VirtualNetworkContainer VirtualNetworks(this ResourceGroup resourceGroup)
        {
            return new VirtualNetworkContainer(resourceGroup.ClientOptions, resourceGroup.Data);
        }

        /// <summary>
        /// Gets a <see cref="VirtualNetworkContainer"/> under a <see cref="ResourceGroup"/>. 
        /// </summary>
        /// <param name="resourceGroup"> The <see cref="ResourceGroupOperations" /> instance the method will execute against. </param>
        /// <returns> An instance of <see cref="VirtualNetworkContainer" />. </returns>
        public static VirtualNetworkContainer VirtualNetworks(this ResourceGroupOperations resourceGroup)
        {
            return new VirtualNetworkContainer(resourceGroup.ClientOptions, resourceGroup.Id);
        }
        #endregion

        #region Public IP Address Operations

        /// <summary>
        /// Gets a <see cref="PublicIpAddress"/> under a <see cref="ResourceGroup"/>. 
        /// </summary>
        /// <param name="resourceGroup"> The <see cref="ResourceGroupOperations" /> instance the method will execute against. </param>
        /// <param name="publicIpAddress"> The <see cref="PublicIPAddressData" /> data model. </param>
        /// <returns> An instance of <see cref="PublicIpAddress" />. </returns>
        public static PublicIpAddress PublicIpAddress(this ResourceGroupOperations resourceGroup, PublicIPAddressData publicIpAddress)
        {
            return new PublicIpAddress(resourceGroup.ClientOptions, publicIpAddress);
        }

        /// <summary>
        /// Gets a <see cref="PublicIpAddressOperations"/> under a <see cref="ResourceGroup"/>. 
        /// </summary>
        /// <param name="resourceGroup"> The <see cref="ResourceGroupOperations" /> instance the method will execute against. </param>
        /// <param name="publicIpAddress"> The resource id of <see cref="PublicIpAddress" /> data model. </param>
        /// <returns> An instance of <see cref="PublicIpAddressOperations" />. </returns>
        public static PublicIpAddressOperations PublicIpAddress(this ResourceGroupOperations resourceGroup, string publicIpAddress)
        {
            return new PublicIpAddressOperations(resourceGroup.ClientOptions, new ResourceIdentifier($"{resourceGroup.Id}/providers/Microsoft.Network/publicIpAddresses/{publicIpAddress}"));
        }

        /// <summary>
        /// Gets a <see cref="PublicIpAddressContainer"/> under a <see cref="ResourceGroup"/>. 
        /// </summary>
        /// <param name="resourceGroup"> The <see cref="ResourceGroup" /> instance the method will execute against. </param>
        /// <returns> An instance of <see cref="PublicIpAddressContainer" />. </returns>
        public static PublicIpAddressContainer PublicIpAddresses(this ResourceGroup resourceGroup)
        {
            return new PublicIpAddressContainer(resourceGroup.ClientOptions, resourceGroup.Data);
        }

        /// <summary>
        /// Gets a <see cref="PublicIpAddressContainer"/> under a <see cref="ResourceGroup"/>. 
        /// </summary>
        /// <param name="resourceGroup"> The <see cref="ResourceGroupOperations" /> instance the method will execute against. </param>
        /// <returns> An instance of <see cref="PublicIpAddressContainer" />. </returns>
        public static PublicIpAddressContainer PublicIpAddresses(this ResourceGroupOperations resourceGroup)
        {
            return new PublicIpAddressContainer(resourceGroup.ClientOptions, resourceGroup.Id);
        }
        #endregion

        #region Network Interface (NIC) operations

        /// <summary>
        /// Gets a <see cref="NetworkInterface"/> under a <see cref="ResourceGroup"/>. 
        /// </summary>
        /// <param name="resourceGroup"> The <see cref="ResourceGroupOperations" /> instance the method will execute against. </param>
        /// <param name="networkInterface"> The <see cref="NetworkInterfaceData" /> data model. </param>
        /// <returns> An instance of <see cref="NetworkInterface" />. </returns>
        public static NetworkInterface NetworkInterface(this ResourceGroupOperations resourceGroup, NetworkInterfaceData networkInterface)
        {
            return new NetworkInterface(resourceGroup.ClientOptions, networkInterface);
        }

        /// <summary>
        /// Gets a <see cref="NetworkInterfaceOperations"/> under a <see cref="ResourceGroup"/>. 
        /// </summary>
        /// <param name="resourceGroup"> The <see cref="ResourceGroupOperations" /> instance the method will execute against. </param>
        /// <param name="networkInterface"> The resource id of <see cref="NetworkInterface" /> data model. </param>
        /// <returns> An instance of <see cref="NetworkInterfaceOperations" />. </returns>
        public static NetworkInterfaceOperations NetworkInterface(this ResourceGroupOperations resourceGroup, string networkInterface)
        {
            return new NetworkInterfaceOperations(resourceGroup.ClientOptions, new ResourceIdentifier($"{resourceGroup.Id}/providers/Microsoft.Network/networkInterfaces/{networkInterface}"));
        }

        /// <summary>
        /// Gets a <see cref="NetworkInterfaceContainer"/> under a <see cref="ResourceGroup"/>. 
        /// </summary>
        /// <param name="resourceGroup"> The <see cref="ResourceGroup" /> instance the method will execute against. </param>
        /// <returns> An instance of <see cref="NetworkInterfaceContainer" />. </returns>
        public static NetworkInterfaceContainer NetworkInterfaces(this ResourceGroup resourceGroup)
        {
            return new NetworkInterfaceContainer(resourceGroup.ClientOptions, resourceGroup.Data);
        }

        /// <summary>
        /// Gets a <see cref="NetworkInterfaceContainer"/> under a <see cref="ResourceGroup"/>. 
        /// </summary>
        /// <param name="resourceGroup"> The <see cref="ResourceGroupOperations" /> instance the method will execute against. </param>
        /// <returns> An instance of <see cref="NetworkInterfaceContainer" />. </returns>
        public static NetworkInterfaceContainer NetworkInterfaces(this ResourceGroupOperations resourceGroup)
        {
            return new NetworkInterfaceContainer(resourceGroup.ClientOptions, resourceGroup.Id);
        }
        #endregion

        #region Network Security Group operations

        /// <summary>
        /// Gets a <see cref="NetworkSecurityGroup"/> under a <see cref="ResourceGroup"/>.
        /// </summary>
        /// <param name="resourceGroup"> The <see cref="ResourceGroupOperations" /> instance the method will execute against. </param>
        /// <param name="networkSecurityGroup"> The <see cref="NetworkSecurityGroupData" /> data model. </param>
        /// <returns> An instance of <see cref="NetworkSecurityGroup" />. </returns>
        public static NetworkSecurityGroup NetworkSecurityGroup(this ResourceGroupOperations resourceGroup, NetworkSecurityGroupData networkSecurityGroup)
        {
            return new NetworkSecurityGroup(resourceGroup.ClientOptions, networkSecurityGroup);
        }

        /// <summary>
        /// Gets a <see cref="NetworkSecurityGroupOperations"/> under a <see cref="ResourceGroup"/>.
        /// </summary>
        /// <param name="operations"> The <see cref="ResourceGroupOperations" /> instance the method will execute against. </param>
        /// <param name="networkSecurityGroup"> The resource id of <see cref="NetworkSecurityGroup" /> data model. </param>
        /// <returns> An instance of <see cref="NetworkSecurityGroupOperations" />. </returns>
        public static NetworkSecurityGroupOperations NetworkSecurityGroup(this ResourceGroupOperations operations, string networkSecurityGroup)
        {
            return new NetworkSecurityGroupOperations(operations.ClientOptions, new ResourceIdentifier($"{operations.Id}/providers/Microsoft.Network/networkSecurityGroups/{networkSecurityGroup}"));
        }

        /// <summary>
        /// Gets a <see cref="NetworkSecurityGroupContainer"/> under a <see cref="ResourceGroup"/>.
        /// </summary>
        /// <param name="resourceGroup"> The <see cref="ResourceGroup" /> instance the method will execute against. </param>
        /// <returns> An instance of <see cref="NetworkSecurityGroupContainer" />. </returns>
        public static NetworkSecurityGroupContainer NetworkSecurityGroups(this ResourceGroup resourceGroup)
        {
            return new NetworkSecurityGroupContainer(resourceGroup.ClientOptions, resourceGroup.Data);
        }

        /// <summary>
        /// Gets a <see cref="NetworkSecurityGroupContainer"/> under a <see cref="ResourceGroup"/>.
        /// </summary>
        /// <param name="resourceGroup"> The <see cref="ResourceGroupOperations" /> instance the method will execute against. </param>
        /// <returns> An instance of <see cref="NetworkSecurityGroupContainer" />. </returns>
        public static NetworkSecurityGroupContainer NetworkSecurityGroups(this ResourceGroupOperations resourceGroup)
        {
            return new NetworkSecurityGroupContainer(resourceGroup.ClientOptions, resourceGroup.Id);
        }
        #endregion
    }
}
