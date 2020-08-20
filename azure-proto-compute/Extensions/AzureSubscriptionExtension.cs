using Azure;
using Azure.ResourceManager.Compute.Models;
using azure_proto_core;
using azure_proto_core.Adapters;
using azure_proto_core.Resources;
using azure_proto_network;
using System.Collections.Generic;
using System.Threading;
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

        public static Pageable<VmOperations> ListVms(this SubscriptionOperations subscription, ArmSubstringFilter filter = null, int? top = null, CancellationToken cancellationToken = default)
        {
            var collection = new VmCollection(subscription, subscription.DefaultSubscription);

            return new WrappingPageable<ResourceOperations<PhVirtualMachine>, VmOperations>(collection.List(filter, top, cancellationToken), vm => new VmOperations(vm, vm.Context));
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
