using Azure;
using Azure.Core;
using Azure.ResourceManager.Core;
using Azure.ResourceManager.Core.Adapters;
using Azure.ResourceManager.Core.Resources;
using Azure.ResourceManager.Network;
using System;
using System.Threading;

namespace azure_proto_network
{
    public static class AzureSubscriptionExtensions
    {
        #region Virtual Network Operations

        private static NetworkManagementClient GetNetworkClient(SubscriptionOperations subscription)
        {
            Func<Uri, TokenCredential, NetworkManagementClient> ctor = (baseUri, cred) => new NetworkManagementClient(
                                subscription.Id.Subscription,
                                baseUri,
                                cred,
                                subscription.ClientOptions.Convert<NetworkManagementClientOptions>());
            var networkClient = subscription.GetClient(ctor);
            return networkClient;
        }

        public static Pageable<VirtualNetwork> ListVnets(this SubscriptionOperations subscription)
        {
            NetworkManagementClient networkClient = GetNetworkClient(subscription);
            var vmOperations = networkClient.VirtualNetworks;
            var result = vmOperations.ListAll();
            return new PhWrappingPageable<Azure.ResourceManager.Network.Models.VirtualNetwork, VirtualNetwork>(
                result,
                s => new VirtualNetwork(subscription.ClientOptions, new VirtualNetworkData(s)));
        }

        public static AsyncPageable<VirtualNetwork> ListVnetsAsync(this SubscriptionOperations subscription)
        {
            NetworkManagementClient networkClient = GetNetworkClient(subscription);
            var vmOperations = networkClient.VirtualNetworks;
            var result = vmOperations.ListAllAsync();
            return new PhWrappingAsyncPageable<Azure.ResourceManager.Network.Models.VirtualNetwork, VirtualNetwork>(
                result,
                s => new VirtualNetwork(subscription.ClientOptions, new VirtualNetworkData(s)));
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
                s => new PublicIpAddress(subscription.ClientOptions, new PublicIPAddressData(s)));
        }

        public static AsyncPageable<PublicIpAddress> ListPublicIpsAsync(this SubscriptionOperations subscription)
        {
            NetworkManagementClient networkClient = GetNetworkClient(subscription);
            var publicIPAddressesOperations = networkClient.PublicIPAddresses;
            var result = publicIPAddressesOperations.ListAllAsync();
            return new PhWrappingAsyncPageable<Azure.ResourceManager.Network.Models.PublicIPAddress, PublicIpAddress>(
                result,
                s => new PublicIpAddress(subscription.ClientOptions, new PublicIPAddressData(s)));
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
                s => new NetworkInterface(subscription.ClientOptions, new NetworkInterfaceData(s)));
        }

        public static AsyncPageable<NetworkInterface> ListNicsAsync(this SubscriptionOperations subscription)
        {
            NetworkManagementClient networkClient = GetNetworkClient(subscription);
            var networkInterfacesOperations = networkClient.NetworkInterfaces;
            var result = networkInterfacesOperations.ListAllAsync();
            return new PhWrappingAsyncPageable<Azure.ResourceManager.Network.Models.NetworkInterface, NetworkInterface>(
                result,
                s => new NetworkInterface(subscription.ClientOptions, new NetworkInterfaceData(s)));
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
                s => new NetworkSecurityGroup(subscription.ClientOptions, new NetworkSecurityGroupData(s)));
        }

        public static AsyncPageable<NetworkSecurityGroup> ListNsgsAsync(this SubscriptionOperations subscription)
        {
            NetworkManagementClient networkClient = GetNetworkClient(subscription);
            var networkSecurityGroupsOperations = networkClient.NetworkSecurityGroups;
            var result = networkSecurityGroupsOperations.ListAllAsync();
            return new PhWrappingAsyncPageable<Azure.ResourceManager.Network.Models.NetworkSecurityGroup, NetworkSecurityGroup>(
                result,
                s => new NetworkSecurityGroup(subscription.ClientOptions, new NetworkSecurityGroupData(s)));
        }

        #endregion
    }
}
