using Azure;
using Azure.ResourceManager.Compute.Models;
using azure_proto_core;
using azure_proto_core.Adapters;
using azure_proto_core.Resources;
using System.Collections.Generic;
using System.Threading;
using Sku = azure_proto_core.Sku;

namespace azure_proto_compute
{
    /// <summary>
    /// Extension methods for convenient access on SubscriptionOperations in a client
    /// </summary>
    public static class AzureSubscriptionExtension
    {
        #region Virtual Machine List Operations
        /// <summary>
        /// List vms at the given subscription context
        /// </summary>
        /// <param name="subscription"></param>
        /// <param name="filter"></param>
        /// <param name="top"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public static Pageable<VmOperations> ListVms(this SubscriptionOperations subscription, ArmSubstringFilter filter = null, int? top = null, CancellationToken cancellationToken = default)
        {
            var collection = new VmCollection(subscription, subscription.DefaultSubscription);
            return new WrappingPageable<ResourceOperations<PhVirtualMachine>, VmOperations>(collection.List(filter, top, cancellationToken), vm => new VmOperations(vm, vm.Context));
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="subscription"></param>
        /// <param name="filter"></param>
        /// <param name="top"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public static AsyncPageable<VmOperations> ListVmsAsync(this SubscriptionOperations subscription, ArmSubstringFilter filter = null, int? top = null, CancellationToken cancellationToken = default)
        {
            var collection = new VmCollection(subscription, subscription.DefaultSubscription);
            return new WrappingAsyncPageable<ResourceOperations<PhVirtualMachine>, VmOperations>(collection.ListAsync(filter, top, cancellationToken), vm => new VmOperations(vm, vm.Context));
        }

        #endregion

        #region AvailabilitySet List Operations

        public static Pageable<AvailabilitySetOperations> ListAvailabilitySets(this SubscriptionOperations subscription, ArmSubstringFilter filter = null, int? top = null, CancellationToken cancellationToken = default)
        {
            var collection = new AvailabilitySetCollection(subscription, subscription.DefaultSubscription);
            return new WrappingPageable<ResourceOperations<PhAvailabilitySet>, AvailabilitySetOperations>(collection.List(filter, top, cancellationToken), a => new AvailabilitySetOperations(a, a.Context));
        }

        public static AsyncPageable<AvailabilitySetOperations> ListAvailabilitySetsAsync(this SubscriptionOperations subscription, ArmSubstringFilter filter = null, int? top = null, CancellationToken cancellationToken = default)
        {
            var collection = new AvailabilitySetCollection(subscription, subscription.DefaultSubscription);
            return new WrappingAsyncPageable<ResourceOperations<PhAvailabilitySet>, AvailabilitySetOperations>(collection.ListAsync(filter, top, cancellationToken), a => new AvailabilitySetOperations(a, a.Context));
        }

        #endregion

    }
}
