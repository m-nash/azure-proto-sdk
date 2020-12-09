using Azure;
using azure_proto_core;
using azure_proto_core.Resources;
using System.Threading;

namespace azure_proto_network
{
    public static class AzureSubscriptionExtensions
    {
        #region Virtual Network Operations

        public static Pageable<VirtualNetwork> ListVnets(this SubscriptionOperations subscription, ArmSubstringFilter filter = null, int? top = null, CancellationToken cancellationToken = default)
        {
            ArmFilterCollection filters = new ArmFilterCollection(VirtualNetworkData.ResourceType);
            filters.SubstringFilter = filter;
            return ResourceListOperations.ListAtContext<VirtualNetwork, VirtualNetworkData>(subscription, filters, top, cancellationToken);
        }

        public static AsyncPageable<VirtualNetwork> ListVnetsAsync(this SubscriptionOperations subscription, ArmSubstringFilter filter = null, int? top = null, CancellationToken cancellationToken = default)
        {
            ArmFilterCollection filters = new ArmFilterCollection(VirtualNetworkData.ResourceType);
            filters.SubstringFilter = filter;
            return ResourceListOperations.ListAtContextAsync<VirtualNetwork, VirtualNetworkData>(subscription, filters, top, cancellationToken);
        }

        #endregion

        #region Public IP Address Operations

        public static Pageable<PublicIpAddress> ListPublicIps(this SubscriptionOperations subscription, ArmSubstringFilter filter = null, int? top = null, CancellationToken cancellationToken = default)
        {
            ArmFilterCollection filters = new ArmFilterCollection(PublicIPAddressData.ResourceType);
            filters.SubstringFilter = filter;
            return ResourceListOperations.ListAtContext<PublicIpAddress, PublicIPAddressData>(subscription, filters, top, cancellationToken);
        }

        public static AsyncPageable<PublicIpAddress> ListPublicIpsAsync(this SubscriptionOperations subscription, ArmSubstringFilter filter = null, int? top = null, CancellationToken cancellationToken = default)
        {
            ArmFilterCollection filters = new ArmFilterCollection(PublicIPAddressData.ResourceType);
            filters.SubstringFilter = filter;
            return ResourceListOperations.ListAtContextAsync<PublicIpAddress, PublicIPAddressData>(subscription, filters, top, cancellationToken);
        }

        #endregion

        #region Network Interface (NIC) operations

        public static Pageable<NetworkInterface> ListNics(this SubscriptionOperations subscription, ArmSubstringFilter filter = null, int? top = null, CancellationToken cancellationToken = default)
        {
            ArmFilterCollection filters = new ArmFilterCollection(NetworkInterfaceData.ResourceType);
            filters.SubstringFilter = filter;
            return ResourceListOperations.ListAtContext<NetworkInterface, NetworkInterfaceData>(subscription, filters, top, cancellationToken);
        }

        public static AsyncPageable<NetworkInterface> ListNicsAsync(this SubscriptionOperations subscription, ArmSubstringFilter filter = null, int? top = null, CancellationToken cancellationToken = default)
        {
            ArmFilterCollection filters = new ArmFilterCollection(NetworkInterfaceData.ResourceType);
            filters.SubstringFilter = filter;
            return ResourceListOperations.ListAtContextAsync<NetworkInterface, NetworkInterfaceData>(subscription, filters, top, cancellationToken);
        }

        #endregion

        #region Network Security Group operations

        public static Pageable<NetworkSecurityGroup> ListNsgs(this SubscriptionOperations subscription, ArmSubstringFilter filter = null, int? top = null, CancellationToken cancellationToken = default)
        {
            ArmFilterCollection filters = new ArmFilterCollection(NetworkSecurityGroupData.ResourceType);
            filters.SubstringFilter = filter;
            return ResourceListOperations.ListAtContext<NetworkSecurityGroup, NetworkSecurityGroupData>(subscription, filters, top, cancellationToken);
        }

        public static AsyncPageable<NetworkSecurityGroup> ListNsgsAsync(this SubscriptionOperations subscription, ArmSubstringFilter filter = null, int? top = null, CancellationToken cancellationToken = default)
        {
            ArmFilterCollection filters = new ArmFilterCollection(NetworkSecurityGroupData.ResourceType);
            filters.SubstringFilter = filter;
            return ResourceListOperations.ListAtContextAsync<NetworkSecurityGroup, NetworkSecurityGroupData>(subscription, filters, top, cancellationToken);
        }

        #endregion
    }
}
