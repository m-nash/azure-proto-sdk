using Azure.ResourceManager.Compute.Models;
using azure_proto_core;
using azure_proto_network;
using System.Collections.Generic;
using Sku = azure_proto_core.Sku;

namespace azure_proto_compute
{
    public static class AzureSubscriptionExtension
    {
        public static VmCollection Vms(this SubscriptionCollectionOperations subscriptionOperations)
        {
            return new VmCollection(subscriptionOperations, subscriptionOperations.DefaultSubscription);
        }

        public static VmCollection Vms(this SubscriptionCollectionOperations subscriptionOperations, ResourceIdentifier subscription)
        {
            return new VmCollection(subscriptionOperations, subscription);
        }

        public static VmCollection Vms(this SubscriptionCollectionOperations subscriptionOperations, azure_proto_core.Resource subscription)
        {
            return new VmCollection(subscriptionOperations, subscription);
        }

        public static VmCollection Vms(this SubscriptionCollectionOperations subscriptionOperations, string subscriptionId)
        {
            return new VmCollection(subscriptionOperations, $"/subscriptions/{subscriptionId}");
        }
    }
}
