using Azure.ResourceManager.Compute.Models;
using azure_proto_core;
using azure_proto_network;
using System.Collections.Generic;
using Sku = azure_proto_core.Sku;

namespace azure_proto_compute
{
    public static class AzureSubscriptionExtension
    {
        /// <summary>
        /// Extensions for VMs
        /// </summary>
        /// <param name="subscriptionOperations"></param>
        /// <returns></returns>
        public static VmCollection Vms(this SubscriptionOperations subscriptionOperations)
        {
            return new VmCollection(subscriptionOperations, subscriptionOperations.DefaultSubscription);
        }

        public static VmCollection Vms(this SubscriptionOperations subscriptionOperations, ResourceIdentifier subscription)
        {
            return new VmCollection(subscriptionOperations, subscription);
        }

        public static VmCollection Vms(this SubscriptionOperations subscriptionOperations, azure_proto_core.Resource subscription)
        {
            return new VmCollection(subscriptionOperations, subscription);
        }

        public static VmCollection Vms(this SubscriptionOperations subscriptionOperations, string subscriptionId)
        {
            return new VmCollection(subscriptionOperations, $"/subscriptions/{subscriptionId}");
        }

        /// <summary>
        /// Extensions for Availability Sets
        /// </summary>
        /// <param name="parent"></param>
        /// <param name="context"></param>
        public static AvailabilitySetCollection AvailabilitySets(this SubscriptionOperations subscriptionOperations)
        {
            return new AvailabilitySetCollection(subscriptionOperations, subscriptionOperations.DefaultSubscription);
        }

        public static AvailabilitySetCollection AvailabilitySets(this SubscriptionOperations subscriptionOperations, ResourceIdentifier subscription)
        {
            return new AvailabilitySetCollection(subscriptionOperations, subscription);
        }

        public static AvailabilitySetCollection AvailabilitySets(this SubscriptionOperations subscriptionOperations, azure_proto_core.Resource subscription)
        {
            return new AvailabilitySetCollection(subscriptionOperations, subscription);
        }

        public static AvailabilitySetCollection AvailabilitySets(this SubscriptionOperations subscriptionOperations, string subscriptionId)
        {
            return new AvailabilitySetCollection(subscriptionOperations, $"/subscriptions/{subscriptionId}");
        }

    }
}
