using azure_proto_core;

namespace azure_proto_network
{
    public static class ResourceGroupExtensions
    {
        #region Virtual Network Operations
        public static VirtualNetworkOperations VirtualNetwork(this ResourceGroupOperations resourceGroup, TrackedResource virtualNetwork)
        {
            return new VirtualNetworkOperations(resourceGroup.ClientContext, virtualNetwork, resourceGroup.ClientOptions);
        }

        public static VirtualNetworkOperations VirtualNetwork(this ResourceGroupOperations resourceGroup, string virtualNetwork)
        {
            return new VirtualNetworkOperations(resourceGroup.ClientContext, new ResourceIdentifier($"{resourceGroup.Id}/providers/Microsoft.Network/virtualNetworks/{virtualNetwork}"), resourceGroup.ClientOptions);
        }

        public static VirtualNetworkContainer VirtualNetworks(this ResourceGroupOperations resourceGroup)
        {
            return new VirtualNetworkContainer(resourceGroup.ClientContext, resourceGroup.Model, resourceGroup.ClientOptions);
        }
        #endregion

        #region Public IP Address Operations
        public static PublicIpAddressOperations PublicIpAddress(this ResourceGroupOperations resourceGroup, TrackedResource publicIpAddress)
        {
            return new PublicIpAddressOperations(resourceGroup.ClientContext, publicIpAddress, resourceGroup.ClientOptions);
        }

        public static PublicIpAddressOperations PublicIpAddress(this ResourceGroupOperations resourceGroup, string publicIpAddress)
        {
            return new PublicIpAddressOperations(resourceGroup.ClientContext, new ResourceIdentifier($"{resourceGroup.Id}/providers/Microsoft.Network/publicIpAddresses/{publicIpAddress}"), resourceGroup.ClientOptions);
        }

        public static PublicIpAddressContainer PublicIpAddresses(this ResourceGroupOperations resourceGroup)
        {
            return new PublicIpAddressContainer(resourceGroup.ClientContext, resourceGroup.Model, resourceGroup.ClientOptions);
        }
        #endregion

        #region Network Interface (NIC) operations
        public static NetworkInterfaceOperations NetworkInterface(this ResourceGroupOperations resourceGroup, TrackedResource networkInterface)
        {
            return new NetworkInterfaceOperations(resourceGroup.ClientContext, networkInterface, resourceGroup.ClientOptions);
        }

        public static NetworkInterfaceOperations NetworkInterface(this ResourceGroupOperations resourceGroup, string networkInterface)
        {
            return new NetworkInterfaceOperations(resourceGroup.ClientContext, new ResourceIdentifier($"{resourceGroup.Id}/providers/Microsoft.Network/networkInterfaces/{networkInterface}"), resourceGroup.ClientOptions);
        }

        public static NetworkInterfaceContainer NetworkInterfaces(this ResourceGroupOperations resourceGroup)
        {
            return new NetworkInterfaceContainer(resourceGroup.ClientContext, resourceGroup.Model, resourceGroup.ClientOptions);
        }
        #endregion

        #region Network Security Group operations
        /// <summary>
        /// Create an NSG with the given open TCP ports
        /// </summary>
        /// <param name="openPorts">The set of TCP ports to open</param>
        /// <returns>An NSG, with the given TCP ports open</returns>
        public static NetworkSecurityGroupOperations NetworkSecurityGroup(this ResourceGroupOperations resourceGroup, TrackedResource networkSecurityGroup)
        {
            return new NetworkSecurityGroupOperations(resourceGroup.ClientContext, networkSecurityGroup, resourceGroup.ClientOptions);
        }

        public static NetworkSecurityGroupOperations NetworkSecurityGroup(this ResourceGroupOperations operations, string networkSecurityGroup)
        {
            return new NetworkSecurityGroupOperations(operations.ClientContext, new ResourceIdentifier($"{operations.Id}/providers/Microsoft.Network/networkSecurityGroups/{networkSecurityGroup}"), operations.ClientOptions);
        }

        public static NetworkSecurityGroupContainer NetworkSecurityGroups(this ResourceGroupOperations resourceGroup)
        {
            return new NetworkSecurityGroupContainer(resourceGroup.ClientContext, resourceGroup.Model, resourceGroup.ClientOptions);
        }
        #endregion
    }
}
