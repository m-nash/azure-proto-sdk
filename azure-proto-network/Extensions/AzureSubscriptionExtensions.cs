using Azure;
using azure_proto_core;
using azure_proto_core.Resources;
using System.Threading;

namespace azure_proto_network
{
    public static class AzureSubscriptionExtensions
    {
        #region Virtual Network Operations

        public static Pageable<XVirtualNetwork> ListVnets(this SubscriptionOperations subscription, ArmSubstringFilter filter = null, int? top = null, CancellationToken cancellationToken = default)
        {
            ArmFilterCollection filters = new ArmFilterCollection(PhVirtualNetwork.ResourceType);
            filters.SubstringFilter = filter;
            return ResourceListOperations.ListAtContext<XVirtualNetwork, PhVirtualNetwork>(subscription, filters, top, cancellationToken);
        }

        public static AsyncPageable<XVirtualNetwork> ListVnetsAsync(this SubscriptionOperations subscription, ArmSubstringFilter filter = null, int? top = null, CancellationToken cancellationToken = default)
        {
            ArmFilterCollection filters = new ArmFilterCollection(PhVirtualNetwork.ResourceType);
            filters.SubstringFilter = filter;
            return ResourceListOperations.ListAtContextAsync<XVirtualNetwork, PhVirtualNetwork>(subscription, filters, top, cancellationToken);
        }

        #endregion

        #region Public IP Address Operations

        public static Pageable<XPublicIpAddress> ListPublicIps(this SubscriptionOperations subscription, ArmSubstringFilter filter = null, int? top = null, CancellationToken cancellationToken = default)
        {
            ArmFilterCollection filters = new ArmFilterCollection(PhPublicIPAddress.ResourceType);
            filters.SubstringFilter = filter;
            return ResourceListOperations.ListAtContext<XPublicIpAddress, PhPublicIPAddress>(subscription, filters, top, cancellationToken);
        }

        public static AsyncPageable<XPublicIpAddress> ListPublicIpsAsync(this SubscriptionOperations subscription, ArmSubstringFilter filter = null, int? top = null, CancellationToken cancellationToken = default)
        {
            ArmFilterCollection filters = new ArmFilterCollection(PhPublicIPAddress.ResourceType);
            filters.SubstringFilter = filter;
            return ResourceListOperations.ListAtContextAsync<XPublicIpAddress, PhPublicIPAddress>(subscription, filters, top, cancellationToken);
        }

        #endregion

        #region Network Interface (NIC) operations

        public static Pageable<XNetworkInterface> ListNics(this SubscriptionOperations subscription, ArmSubstringFilter filter = null, int? top = null, CancellationToken cancellationToken = default)
        {
            ArmFilterCollection filters = new ArmFilterCollection(PhNetworkInterface.ResourceType);
            filters.SubstringFilter = filter;
            return ResourceListOperations.ListAtContext<XNetworkInterface, PhNetworkInterface>(subscription, filters, top, cancellationToken);
        }

        public static AsyncPageable<XNetworkInterface> ListNicsAsync(this SubscriptionOperations subscription, ArmSubstringFilter filter = null, int? top = null, CancellationToken cancellationToken = default)
        {
            ArmFilterCollection filters = new ArmFilterCollection(PhNetworkInterface.ResourceType);
            filters.SubstringFilter = filter;
            return ResourceListOperations.ListAtContextAsync<XNetworkInterface, PhNetworkInterface>(subscription, filters, top, cancellationToken);
        }

        #endregion

        #region Network Security Group operations

        public static Pageable<XNetworkSecurityGroup> ListNsgs(this SubscriptionOperations subscription, ArmSubstringFilter filter = null, int? top = null, CancellationToken cancellationToken = default)
        {
            ArmFilterCollection filters = new ArmFilterCollection(PhNetworkSecurityGroup.ResourceType);
            filters.SubstringFilter = filter;
            return ResourceListOperations.ListAtContext<XNetworkSecurityGroup, PhNetworkSecurityGroup>(subscription, filters, top, cancellationToken);
        }

        public static AsyncPageable<XNetworkSecurityGroup> ListNsgsAsync(this SubscriptionOperations subscription, ArmSubstringFilter filter = null, int? top = null, CancellationToken cancellationToken = default)
        {
            ArmFilterCollection filters = new ArmFilterCollection(PhNetworkSecurityGroup.ResourceType);
            filters.SubstringFilter = filter;
            return ResourceListOperations.ListAtContextAsync<XNetworkSecurityGroup, PhNetworkSecurityGroup>(subscription, filters, top, cancellationToken);
        }

        #endregion
    }
}
