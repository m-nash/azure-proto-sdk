using azure_proto_core;

namespace azure_proto_network
{
    public static class ResourceGroupExtensions
    {
        #region Virtual Network Operations
        public static XVirtualNetwork VirtualNetwork(this XResourceGroup resourceGroup, PhVirtualNetwork virtualNetwork)
        {
            return new XVirtualNetwork(resourceGroup.ClientContext, virtualNetwork);
        }

        public static VirtualNetworkOperations VirtualNetwork(this ResourceGroupOperations resourceGroup, string virtualNetwork)
        {
            return new VirtualNetworkOperations(resourceGroup.ClientContext, new ResourceIdentifier($"{resourceGroup.Id}/providers/Microsoft.Network/virtualNetworks/{virtualNetwork}"));
        }

        public static VirtualNetworkContainer VirtualNetworks(this XResourceGroup resourceGroup)
        {
            return new VirtualNetworkContainer(resourceGroup.ClientContext, resourceGroup.Model);
        }

        public static VirtualNetworkContainer VirtualNetworks(this ResourceGroupOperations resourceGroup)
        {
            return new VirtualNetworkContainer(resourceGroup.ClientContext, resourceGroup.Id);
        }
        #endregion

        #region Public IP Address Operations
        public static XPublicIpAddress PublicIpAddress(this ResourceGroupOperations resourceGroup, PhPublicIPAddress publicIpAddress)
        {
            return new XPublicIpAddress(resourceGroup.ClientContext, publicIpAddress);
        }

        public static PublicIpAddressOperations PublicIpAddress(this ResourceGroupOperations resourceGroup, string publicIpAddress)
        {
            return new PublicIpAddressOperations(resourceGroup.ClientContext, new ResourceIdentifier($"{resourceGroup.Id}/providers/Microsoft.Network/publicIpAddresses/{publicIpAddress}"));
        }

        public static PublicIpAddressContainer PublicIpAddresses(this XResourceGroup resourceGroup)
        {
            return new PublicIpAddressContainer(resourceGroup.ClientContext, resourceGroup.Model);
        }

        public static PublicIpAddressContainer PublicIpAddresses(this ResourceGroupOperations resourceGroup)
        {
            return new PublicIpAddressContainer(resourceGroup.ClientContext, resourceGroup.Id);
        }
        #endregion

        #region Network Interface (NIC) operations
        public static XNetworkInterface NetworkInterface(this ResourceGroupOperations resourceGroup, PhNetworkInterface networkInterface)
        {
            return new XNetworkInterface(resourceGroup.ClientContext, networkInterface);
        }

        public static NetworkInterfaceOperations NetworkInterface(this ResourceGroupOperations resourceGroup, string networkInterface)
        {
            return new NetworkInterfaceOperations(resourceGroup.ClientContext, new ResourceIdentifier($"{resourceGroup.Id}/providers/Microsoft.Network/networkInterfaces/{networkInterface}"));
        }

        public static NetworkInterfaceContainer NetworkInterfaces(this XResourceGroup resourceGroup)
        {
            return new NetworkInterfaceContainer(resourceGroup.ClientContext, resourceGroup.Model);
        }

        public static NetworkInterfaceContainer NetworkInterfaces(this ResourceGroupOperations resourceGroup)
        {
            return new NetworkInterfaceContainer(resourceGroup.ClientContext, resourceGroup.Id);
        }
        #endregion

        #region Network Security Group operations
        /// <summary>
        /// Create an NSG with the given open TCP ports
        /// </summary>
        /// <param name="openPorts">The set of TCP ports to open</param>
        /// <returns>An NSG, with the given TCP ports open</returns>
        public static XNetworkSecurityGroup NetworkSecurityGroup(this ResourceGroupOperations resourceGroup, PhNetworkSecurityGroup networkSecurityGroup)
        {
            return new XNetworkSecurityGroup(resourceGroup.ClientContext, networkSecurityGroup);
        }

        public static NetworkSecurityGroupOperations NetworkSecurityGroup(this ResourceGroupOperations operations, string networkSecurityGroup)
        {
            return new NetworkSecurityGroupOperations(operations.ClientContext, new ResourceIdentifier($"{operations.Id}/providers/Microsoft.Network/networkSecurityGroups/{networkSecurityGroup}"));
        }

        public static NetworkSecurityGroupContainer NetworkSecurityGroups(this XResourceGroup resourceGroup)
        {
            return new NetworkSecurityGroupContainer(resourceGroup.ClientContext, resourceGroup.Model);
        }

        public static NetworkSecurityGroupContainer NetworkSecurityGroups(this ResourceGroupOperations resourceGroup)
        {
            return new NetworkSecurityGroupContainer(resourceGroup.ClientContext, resourceGroup.Id);
        }
        #endregion
    }
}
