using Azure;
using Azure.ResourceManager.Core;
using Azure.ResourceManager.Core.Adapters;
using Azure.ResourceManager.Network;

namespace azure_proto_network
{
    public static class AzureSubscriptionExtensions
    {
        #region Virtual Network Operations

        private static NetworkManagementClient GetNetworkClient(SubscriptionOperations subscription)
        {
            return new NetworkManagementClient(
                subscription.Id.Subscription,
                subscription.BaseUri,
                subscription.Credential,
                subscription.ClientOptions.Convert<NetworkManagementClientOptions>());
        }

        public static Pageable<VirtualNetwork> ListVnets(this SubscriptionOperations subscription)
        {
            NetworkManagementClient networkClient = GetNetworkClient(subscription);
            var vmOperations = networkClient.VirtualNetworks;
            var result = vmOperations.ListAll();
            return new PhWrappingPageable<Azure.ResourceManager.Network.Models.VirtualNetwork, VirtualNetwork>(
                result,
                s => new VirtualNetwork(subscription, new VirtualNetworkData(s)));
        }

        public static AsyncPageable<VirtualNetwork> ListVnetsAsync(this SubscriptionOperations subscription)
        {
            NetworkManagementClient networkClient = GetNetworkClient(subscription);
            var vmOperations = networkClient.VirtualNetworks;
            var result = vmOperations.ListAllAsync();
            return new PhWrappingAsyncPageable<Azure.ResourceManager.Network.Models.VirtualNetwork, VirtualNetwork>(
                result,
                s => new VirtualNetwork(subscription, new VirtualNetworkData(s)));
        }

        #endregion

        #region Public IP Address Operations

        public static Pageable<PublicIpAddress> ListPublicIps(this SubscriptionOperations subscription)
        {
            NetworkManagementClient networkClient = GetNetworkClient(subscription);
            var publicIPAddressesOperations = networkClient.PublicIPAddresses;
            var result = publicIPAddressesOperations.ListAll();
            return new PhWrappingPageable<Azure.ResourceManager.Network.Models.PublicIPAddress, PublicIpAddress>(
                result,
                s => new PublicIpAddress(subscription, new PublicIPAddressData(s)));
        }

        public static AsyncPageable<PublicIpAddress> ListPublicIpsAsync(this SubscriptionOperations subscription)
        {
            NetworkManagementClient networkClient = GetNetworkClient(subscription);
            var publicIPAddressesOperations = networkClient.PublicIPAddresses;
            var result = publicIPAddressesOperations.ListAllAsync();
            return new PhWrappingAsyncPageable<Azure.ResourceManager.Network.Models.PublicIPAddress, PublicIpAddress>(
                result,
                s => new PublicIpAddress(subscription, new PublicIPAddressData(s)));
        }

        #endregion

        #region Network Interface (NIC) operations

        public static Pageable<NetworkInterface> ListNics(this SubscriptionOperations subscription)
        {
            NetworkManagementClient networkClient = GetNetworkClient(subscription);
            var networkInterfacesOperations = networkClient.NetworkInterfaces;
            var result = networkInterfacesOperations.ListAll();
            return new PhWrappingPageable<Azure.ResourceManager.Network.Models.NetworkInterface, NetworkInterface>(
                result,
                s => new NetworkInterface(subscription, new NetworkInterfaceData(s)));
        }

        public static AsyncPageable<NetworkInterface> ListNicsAsync(this SubscriptionOperations subscription)
        {
            NetworkManagementClient networkClient = GetNetworkClient(subscription);
            var networkInterfacesOperations = networkClient.NetworkInterfaces;
            var result = networkInterfacesOperations.ListAllAsync();
            return new PhWrappingAsyncPageable<Azure.ResourceManager.Network.Models.NetworkInterface, NetworkInterface>(
                result,
                s => new NetworkInterface(subscription, new NetworkInterfaceData(s)));
        }

        #endregion

        #region Network Security Group operations

        public static Pageable<NetworkSecurityGroup> ListNsgs(this SubscriptionOperations subscription)
        {
            NetworkManagementClient networkClient = GetNetworkClient(subscription);
            var networkSecurityGroupsOperations = networkClient.NetworkSecurityGroups;
            var result = networkSecurityGroupsOperations.ListAll();
            return new PhWrappingPageable<Azure.ResourceManager.Network.Models.NetworkSecurityGroup, NetworkSecurityGroup>(
                result,
                s => new NetworkSecurityGroup(subscription, new NetworkSecurityGroupData(s)));
        }

        public static AsyncPageable<NetworkSecurityGroup> ListNsgsAsync(this SubscriptionOperations subscription)
        {
            NetworkManagementClient networkClient = GetNetworkClient(subscription);
            var networkSecurityGroupsOperations = networkClient.NetworkSecurityGroups;
            var result = networkSecurityGroupsOperations.ListAllAsync();
            return new PhWrappingAsyncPageable<Azure.ResourceManager.Network.Models.NetworkSecurityGroup, NetworkSecurityGroup>(
                result,
                s => new NetworkSecurityGroup(subscription, new NetworkSecurityGroupData(s)));
        }

        #endregion
    }
}
