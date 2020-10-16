using Azure;
using azure_proto_core;
using azure_proto_core.Resources;
using System.Threading;

namespace azure_proto_network
{
    public static class AzureSubscriptionExtensions
    {
        #region Virtual Network Operations

        public static Pageable<VirtualNetworkOperations> ListVnets(this SubscriptionOperations subscription, ArmSubstringFilter filter = null, int? top = null, CancellationToken cancellationToken = default)
        {
            ArmFilterCollection filters = new ArmFilterCollection(PhVirtualNetwork.ResourceType);
            filters.SubstringFilter = filter;
            return ResourceListOperations.ListAtContext<VirtualNetworkOperations, PhVirtualNetwork>(subscription, filters, top, cancellationToken);
        }

        public static AsyncPageable<VirtualNetworkOperations> ListVnetsAsync(this SubscriptionOperations subscription, ArmSubstringFilter filter = null, int? top = null, CancellationToken cancellationToken = default)
        {
            ArmFilterCollection filters = new ArmFilterCollection(PhVirtualNetwork.ResourceType);
            filters.SubstringFilter = filter;
            return ResourceListOperations.ListAtContextAsync<VirtualNetworkOperations, PhVirtualNetwork>(subscription, filters, top, cancellationToken);
        }

        #endregion

        #region Public IP Address Operations

        public static Pageable<PublicIpAddressOperations> ListPublicIps(this SubscriptionOperations subscription, ArmSubstringFilter filter = null, int? top = null, CancellationToken cancellationToken = default)
        {
            ArmFilterCollection filters = new ArmFilterCollection(PhPublicIPAddress.ResourceType);
            filters.SubstringFilter = filter;
            return ResourceListOperations.ListAtContext<PublicIpAddressOperations, PhPublicIPAddress>(subscription, filters, top, cancellationToken);
        }

        public static AsyncPageable<PublicIpAddressOperations> ListPublicIpsAsync(this SubscriptionOperations subscription, ArmSubstringFilter filter = null, int? top = null, CancellationToken cancellationToken = default)
        {
            ArmFilterCollection filters = new ArmFilterCollection(PhPublicIPAddress.ResourceType);
            filters.SubstringFilter = filter;
            return ResourceListOperations.ListAtContextAsync<PublicIpAddressOperations, PhPublicIPAddress>(subscription, filters, top, cancellationToken);
        }

        #endregion

        #region Network Interface (NIC) operations

        public static Pageable<NetworkInterfaceOperations> ListNics(this SubscriptionOperations subscription, ArmSubstringFilter filter = null, int? top = null, CancellationToken cancellationToken = default)
        {
            ArmFilterCollection filters = new ArmFilterCollection(PhNetworkInterface.ResourceType);
            filters.SubstringFilter = filter;
            return ResourceListOperations.ListAtContext<NetworkInterfaceOperations, PhNetworkInterface>(subscription, filters, top, cancellationToken);
        }

        public static AsyncPageable<NetworkInterfaceOperations> ListNicsAsync(this SubscriptionOperations subscription, ArmSubstringFilter filter = null, int? top = null, CancellationToken cancellationToken = default)
        {
            ArmFilterCollection filters = new ArmFilterCollection(PhNetworkInterface.ResourceType);
            filters.SubstringFilter = filter;
            return ResourceListOperations.ListAtContextAsync<NetworkInterfaceOperations, PhNetworkInterface>(subscription, filters, top, cancellationToken);
        }

        #endregion

        #region Network Security Group operations

        public static Pageable<NetworkSecurityGroupOperations> ListNsgs(this SubscriptionOperations subscription, ArmSubstringFilter filter = null, int? top = null, CancellationToken cancellationToken = default)
        {
            ArmFilterCollection filters = new ArmFilterCollection(PhNetworkSecurityGroup.ResourceType);
            filters.SubstringFilter = filter;
            return ResourceListOperations.ListAtContext<NetworkSecurityGroupOperations, PhNetworkSecurityGroup>(subscription, filters, top, cancellationToken);
        }

        public static AsyncPageable<NetworkSecurityGroupOperations> ListNsgsAsync(this SubscriptionOperations subscription, ArmSubstringFilter filter = null, int? top = null, CancellationToken cancellationToken = default)
        {
            ArmFilterCollection filters = new ArmFilterCollection(PhNetworkSecurityGroup.ResourceType);
            filters.SubstringFilter = filter;
            return ResourceListOperations.ListAtContextAsync<NetworkSecurityGroupOperations, PhNetworkSecurityGroup>(subscription, filters, top, cancellationToken);
        }

        #endregion
    }
}
