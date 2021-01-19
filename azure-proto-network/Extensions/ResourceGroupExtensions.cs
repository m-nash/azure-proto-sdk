using Azure.ResourceManager.Core;

namespace azure_proto_network
{
    public static class ResourceGroupExtensions
    {
        #region Virtual Network Operations
        public static VirtualNetwork VirtualNetwork(this ResourceGroup resourceGroup, VirtualNetworkData virtualNetwork)
        {
            return new VirtualNetwork(resourceGroup.ClientOptions, virtualNetwork);
        }

        public static VirtualNetworkOperations VirtualNetwork(this ResourceGroupOperations resourceGroup, string virtualNetwork)
        {
            return new VirtualNetworkOperations(resourceGroup.ClientOptions, new ResourceIdentifier($"{resourceGroup.Id}/providers/Microsoft.Network/virtualNetworks/{virtualNetwork}"));
        }

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

        /// <summary>
        /// Gets the operations over a specific <see cref="NetworkInterface>"/>
        /// </summary>
        /// <param name="resourceGroup"> The operations over a specific resource group. </param>
        /// <param name="networkInterface"> The network interface to target for operations. </param>
        /// <returns> A <see cref="NetworkInterface"/> including the operations that can be pefromed on it. </returns>
        public static NetworkInterface NetworkInterface(this ResourceGroupOperations resourceGroup, NetworkInterfaceData networkInterface)
        {
            return new NetworkInterface(resourceGroup.ClientOptions, networkInterface);
        }

        /// <summary>
        /// Gets the operations over a specific <see cref="NetworkInterface>"/>
        /// </summary>
        /// <param name="resourceGroup"> The operations over a specific resource group. </param>
        /// <param name="networkInterface"> The name of the network interface to target for operations. </param>
        /// <returns> A <see cref="NetworkInterface"/> including the operations that can be pefromed on it. </returns>
        public static NetworkInterfaceOperations NetworkInterface(this ResourceGroupOperations resourceGroup, string networkInterface)
        {
            return new NetworkInterfaceOperations(resourceGroup.ClientOptions, new ResourceIdentifier($"{resourceGroup.Id}/providers/Microsoft.Network/networkInterfaces/{networkInterface}"));
        }

        /// <summary>
        /// Gets the operations over the collection of <see cref="NetworkInterface"/> contained in the resource group.
        /// </summary>
        /// <param name="resourceGroup"> The <see cref="ResourceGroup"/> that contains the network interfaces. </param>
        /// <returns> A <see cref="NetworkInterfaceContainer"/> representing the collection of <see cref="NetworkInterface"/> 
        /// in the resource group. </returns>
        public static NetworkInterfaceContainer NetworkInterfaces(this ResourceGroup resourceGroup)
        {
            return new NetworkInterfaceContainer(resourceGroup.ClientOptions, resourceGroup.Data);
        }

        /// <summary>
        /// Gets the operations over the collection of <see cref="NetworkInterface"/> contained in the resource group.
        /// </summary>
        /// <param name="resourceGroup"> The name of the <see cref="ResourceGroup"/> that contains the network interfaces. </param>
        /// <returns> A <see cref="NetworkInterfaceContainer"/> representing the collection of <see cref="NetworkInterface"/> 
        /// in the resource group. </returns>
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
