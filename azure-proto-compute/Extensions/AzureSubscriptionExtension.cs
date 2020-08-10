using Azure.ResourceManager.Compute.Models;
using azure_proto_core;
using azure_proto_network;
using System.Collections.Generic;
using Sku = azure_proto_core.Sku;

namespace azure_proto_compute
{
    public static class AzureSubscriptionExtension
    {
        public static VmCollection Vms(this SubscriptionCollectionOperations subscription)
        {
            return new VmCollection(subscription, subscription.DefaultSubscription);
        }

        public static VmCollection Vms(this SubscriptionCollectionOperations subscription, ResourceIdentifier context)
        {
            return new VmCollection(subscription, context);
        }

        public static VmCollection Vms(this SubscriptionCollectionOperations subscription, azure_proto_core.Resource context)
        {
            return new VmCollection(subscription, context);
        }

        public static VmCollection Vms(this SubscriptionCollectionOperations subscription, string subscriptionId)
        {
            return new VmCollection(subscription, $"/subscriptions/{subscriptionId}");
        }
    }
}
