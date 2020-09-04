using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using Azure;
using azure_proto_core;
using azure_proto_core.Adapters;
using azure_proto_core.Resources;

namespace azure_proto_network
{
    public static class AzureSubscriptionExtensions
    {
        #region Virtual Network Operations

        public static Pageable<VirtualNetworkOperations> ListVnets(this SubscriptionOperations subscription, ArmSubstringFilter filter = null, int? top = null, CancellationToken cancellationToken = default)
        {
            var collection = new VirtualNetworkCollection( subscription, subscription.DefaultSubscription);
            return new PhWrappingPageable<ResourceClientBase<PhVirtualNetwork>, VirtualNetworkOperations>(collection.List(filter, top, cancellationToken), vnet => new VirtualNetworkOperations(vnet, vnet.Context));
        }

        public static AsyncPageable<VirtualNetworkOperations> ListVnetsAsync(this SubscriptionOperations subscription, ArmSubstringFilter filter = null, int? top = null, CancellationToken cancellationToken = default)
        {
            var collection = new VirtualNetworkCollection(subscription, subscription.DefaultSubscription);
            return new PhWrappingAsyncPageable<ResourceClientBase<PhVirtualNetwork>, VirtualNetworkOperations>(collection.ListAsync(filter, top, cancellationToken), vnet => new VirtualNetworkOperations(vnet, vnet.Context));
        }


        #endregion

        #region Public IP Address Operations

        public static Pageable<ResourceClientBase<PhPublicIPAddress>> ListPublicIps(this SubscriptionOperations subscription, ArmSubstringFilter filter = null, int? top = null, CancellationToken cancellationToken = default)
        {
            var collection = new PublicIpAddressCollection(subscription, subscription.DefaultSubscription);
            return collection.List(filter, top, cancellationToken);
        }

        public static AsyncPageable<ResourceClientBase<PhPublicIPAddress>> ListPublicIpsAsync(this SubscriptionOperations subscription, ArmSubstringFilter filter = null, int? top = null, CancellationToken cancellationToken = default)
        {
            var collection = new PublicIpAddressCollection(subscription, subscription.DefaultSubscription);
            return collection.ListAsync(filter, top, cancellationToken);
        }

        #endregion

        #region Network Interface (NIC) operations

        public static Pageable<ResourceClientBase<PhNetworkInterface>> ListNics(this SubscriptionOperations subscription, ArmSubstringFilter filter = null, int? top = null, CancellationToken cancellationToken = default)
        {
            var collection = new NetworkInterfaceCollection(subscription, subscription.DefaultSubscription);
            return collection.List(filter, top, cancellationToken);
        }

        public static AsyncPageable<ResourceClientBase<PhNetworkInterface>> ListNicsAsync(this SubscriptionOperations subscription, ArmSubstringFilter filter = null, int? top = null, CancellationToken cancellationToken = default)
        {
            var collection = new NetworkInterfaceCollection(subscription, subscription.DefaultSubscription);
            return collection.ListAsync(filter, top, cancellationToken);
        }


        #endregion

        #region Network Security Group operations
        public static Pageable<ResourceClientBase<PhNetworkSecurityGroup>> ListNsgs(this SubscriptionOperations subscription, ArmSubstringFilter filter = null, int? top = null, CancellationToken cancellationToken = default)
        {
            var collection = new NetworkSecurityGroupCollection(subscription, subscription.DefaultSubscription);
            return collection.List(filter, top, cancellationToken);
        }

        public static AsyncPageable<ResourceClientBase<PhNetworkSecurityGroup>> ListNsgsAsync(this SubscriptionOperations subscription, ArmSubstringFilter filter = null, int? top = null, CancellationToken cancellationToken = default)
        {
            var collection = new NetworkSecurityGroupCollection(subscription, subscription.DefaultSubscription);
            return collection.ListAsync(filter, top, cancellationToken);
        }

        #endregion
    }
}
