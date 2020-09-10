using Azure;
using azure_proto_core;
using azure_proto_core.Resources;
using System.Threading;

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
        public static Pageable<VirtualMachineOperations> ListVirtualMachines(this SubscriptionOperations subscription, ArmSubstringFilter filter = null, int? top = null, CancellationToken cancellationToken = default)
        {
            //TODO: consider ArmPageable<T> to introduce post network call filtering and avoid breaking changes
            //TODO: validate LINQ/LINQ-Async behavior of requiring entire set before filtering
            //TODO: come up with a way to make sure its known if filtering is service side or client side
            return ResourceListOperations.ListAtContext<VirtualMachineOperations, PhVirtualMachine>(subscription, filter, top, cancellationToken);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="subscription"></param>
        /// <param name="filter"></param>
        /// <param name="top"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public static AsyncPageable<VirtualMachineOperations> ListVirtualMachinesAsync(this SubscriptionOperations subscription, ArmSubstringFilter filter = null, int? top = null, CancellationToken cancellationToken = default)
        {
            return ResourceListOperations.ListAtContextAsync<VirtualMachineOperations, PhVirtualMachine>(subscription, filter, top, cancellationToken);
        }

        #endregion

        #region AvailabilitySet List Operations

        public static Pageable<AvailabilitySetOperations> ListAvailabilitySets(this SubscriptionOperations subscription, ArmSubstringFilter filter = null, int? top = null, CancellationToken cancellationToken = default)
        {
            return ResourceListOperations.ListAtContext<AvailabilitySetOperations, PhAvailabilitySet>(subscription, filter, top, cancellationToken);
        }

        public static AsyncPageable<AvailabilitySetOperations> ListAvailabilitySetsAsync(this SubscriptionOperations subscription, ArmSubstringFilter filter = null, int? top = null, CancellationToken cancellationToken = default)
        {
            return ResourceListOperations.ListAtContextAsync<AvailabilitySetOperations, PhAvailabilitySet>(subscription, filter, top, cancellationToken);
        }

        #endregion

    }
}
