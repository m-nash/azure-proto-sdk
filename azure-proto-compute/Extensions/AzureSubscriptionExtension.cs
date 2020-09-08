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
        public static Pageable<VirtualMachineOperations> ListVms(this SubscriptionOperations subscription, ArmSubstringFilter filter = null, int? top = null, CancellationToken cancellationToken = default)
        {
            var collection = new VirtualMachineCollection(subscription, subscription.Id);
            return new PhWrappingPageable<ResourceClientBase<PhVirtualMachine>, VirtualMachineOperations>(collection.List(filter, top, cancellationToken), vm => new VirtualMachineOperations(vm, vm.Context));
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="subscription"></param>
        /// <param name="filter"></param>
        /// <param name="top"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public static AsyncPageable<VirtualMachineOperations> ListVmsAsync(this SubscriptionOperations subscription, ArmSubstringFilter filter = null, int? top = null, CancellationToken cancellationToken = default)
        {
            var collection = new VirtualMachineCollection(subscription, subscription.Id);
            return new PhWrappingAsyncPageable<ResourceClientBase<PhVirtualMachine>, VirtualMachineOperations>(collection.ListAsync(filter, top, cancellationToken), vm => new VirtualMachineOperations(vm, vm.Context));
        }

        #endregion

        #region AvailabilitySet List Operations

        public static Pageable<AvailabilitySetOperations> ListAvailabilitySets(this SubscriptionOperations subscription, ArmSubstringFilter filter = null, int? top = null, CancellationToken cancellationToken = default)
        {
            var collection = new AvailabilitySetCollection(subscription, subscription.Id);
            return new PhWrappingPageable<ResourceClientBase<PhAvailabilitySet>, AvailabilitySetOperations>(collection.List(filter, top, cancellationToken), a => new AvailabilitySetOperations(a, a.Context));
        }

        public static AsyncPageable<AvailabilitySetOperations> ListAvailabilitySetsAsync(this SubscriptionOperations subscription, ArmSubstringFilter filter = null, int? top = null, CancellationToken cancellationToken = default)
        {
            var collection = new AvailabilitySetCollection(subscription, subscription.Id);
            return new PhWrappingAsyncPageable<ResourceClientBase<PhAvailabilitySet>, AvailabilitySetOperations>(collection.ListAsync(filter, top, cancellationToken), a => new AvailabilitySetOperations(a, a.Context));
        }

        #endregion

    }
}
