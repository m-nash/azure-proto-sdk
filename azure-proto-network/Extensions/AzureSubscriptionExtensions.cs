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

        public static Pageable<VnetOperations> ListVnets(this SubscriptionOperations subscription, ArmSubstringFilter filter = null, int? top = null, CancellationToken cancellationToken = default)
        {
            var collection = new VnetCollection( subscription, subscription.DefaultSubscription);
            return new WrappingPageable<ResourceOperations<PhVirtualNetwork>, VnetOperations>(collection.List(filter, top, cancellationToken), vnet => new VnetOperations(vnet, vnet.Context));
        }

        public static AsyncPageable<VnetOperations> ListVnetsAsync(this SubscriptionOperations subscription, ArmSubstringFilter filter = null, int? top = null, CancellationToken cancellationToken = default)
        {
            var collection = new VnetCollection(subscription, subscription.DefaultSubscription);
            return new WrappingAsyncPageable<ResourceOperations<PhVirtualNetwork>, VnetOperations>(collection.ListAsync(filter, top, cancellationToken), vnet => new VnetOperations(vnet, vnet.Context));
        }


        #endregion

        #region Public IP Address Operations

        public static Pageable<ResourceOperations<PhPublicIPAddress>> ListPublicIps(this SubscriptionOperations subscription, ArmSubstringFilter filter = null, int? top = null, CancellationToken cancellationToken = default)
        {
            var collection = new PublicIpCollection(subscription, subscription.DefaultSubscription);
            return collection.List(filter, top, cancellationToken);
        }

        public static AsyncPageable<ResourceOperations<PhPublicIPAddress>> ListPublicIpsAsync(this SubscriptionOperations subscription, ArmSubstringFilter filter = null, int? top = null, CancellationToken cancellationToken = default)
        {
            var collection = new PublicIpCollection(subscription, subscription.DefaultSubscription);
            return collection.ListAsync(filter, top, cancellationToken);
        }

        #endregion

        #region Network Interface (NIC) operations

        public static Pageable<ResourceOperations<PhNetworkInterface>> ListNics(this SubscriptionOperations subscription, ArmSubstringFilter filter = null, int? top = null, CancellationToken cancellationToken = default)
        {
            var collection = new NicCollection(subscription, subscription.DefaultSubscription);
            return collection.List(filter, top, cancellationToken);
        }

        public static AsyncPageable<ResourceOperations<PhNetworkInterface>> ListNicsAsync(this SubscriptionOperations subscription, ArmSubstringFilter filter = null, int? top = null, CancellationToken cancellationToken = default)
        {
            var collection = new NicCollection(subscription, subscription.DefaultSubscription);
            return collection.ListAsync(filter, top, cancellationToken);
        }


        #endregion

        #region Network Security Group operations
        public static Pageable<ResourceOperations<PhNetworkSecurityGroup>> ListNsgs(this SubscriptionOperations subscription, ArmSubstringFilter filter = null, int? top = null, CancellationToken cancellationToken = default)
        {
            var collection = new NsgCollection(subscription, subscription.DefaultSubscription);
            return collection.List(filter, top, cancellationToken);
        }

        public static AsyncPageable<ResourceOperations<PhNetworkSecurityGroup>> ListNsgsAsync(this SubscriptionOperations subscription, ArmSubstringFilter filter = null, int? top = null, CancellationToken cancellationToken = default)
        {
            var collection = new NsgCollection(subscription, subscription.DefaultSubscription);
            return collection.ListAsync(filter, top, cancellationToken);
        }

        #endregion
    }
}
