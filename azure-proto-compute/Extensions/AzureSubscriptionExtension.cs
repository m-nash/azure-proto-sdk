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
        
        public static Pageable<XVirtualMachine> ListVirtualMachines(this SubscriptionOperations subscription, ArmSubstringFilter filter = null, int? top = null, CancellationToken cancellationToken = default)
        {
            //TODO: consider ArmPageable<T> to introduce post network call filtering and avoid breaking changes
            ArmFilterCollection filters = new ArmFilterCollection(PhVirtualMachine.ResourceType);
            filters.SubstringFilter = filter;
            return ResourceListOperations.ListAtContext<XVirtualMachine, PhVirtualMachine>(subscription, filters, top, cancellationToken);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="subscription"></param>
        /// <param name="filter"></param>
        /// <param name="top"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public static AsyncPageable<XVirtualMachine> ListVirtualMachinesAsync(this SubscriptionOperations subscription, ArmSubstringFilter filter = null, int? top = null, CancellationToken cancellationToken = default)
        {
            ArmFilterCollection filters = new ArmFilterCollection(PhVirtualMachine.ResourceType);
            filters.SubstringFilter = filter;
            return ResourceListOperations.ListAtContextAsync<XVirtualMachine, PhVirtualMachine>(subscription, filters, top, cancellationToken);
        }
        #endregion

        #region AvailabilitySet List Operations
        public static Pageable<XAvailabilitySet> ListAvailabilitySets(this SubscriptionOperations subscription, ArmSubstringFilter filter = null, int? top = null, CancellationToken cancellationToken = default)
        {
            ArmFilterCollection filters = new ArmFilterCollection(PhAvailabilitySet.ResourceType);
            filters.SubstringFilter = filter;
            return ResourceListOperations.ListAtContext<XAvailabilitySet, PhAvailabilitySet>(subscription, filters, top, cancellationToken);
        }

        public static AsyncPageable<XAvailabilitySet> ListAvailabilitySetsAsync(this SubscriptionOperations subscription, ArmSubstringFilter filter = null, int? top = null, CancellationToken cancellationToken = default)
        {
            ArmFilterCollection filters = new ArmFilterCollection(PhAvailabilitySet.ResourceType);
            filters.SubstringFilter = filter;
            return ResourceListOperations.ListAtContextAsync<XAvailabilitySet, PhAvailabilitySet>(subscription, filters, top, cancellationToken);
        }
        #endregion
    }
}
