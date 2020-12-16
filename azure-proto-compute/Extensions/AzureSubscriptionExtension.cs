using Azure;
using Azure.ResourceManager.Core;
using Azure.ResourceManager.Core.Resources;
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
        public static Pageable<VirtualMachine> ListVirtualMachines(this SubscriptionOperations subscription, ArmSubstringFilter filter = null, int? top = null, CancellationToken cancellationToken = default)
        {
            //TODO: consider ArmPageable<T> to introduce post network call filtering and avoid breaking changes
            ArmFilterCollection filters = new ArmFilterCollection(VirtualMachineData.ResourceType);
            filters.SubstringFilter = filter;
            return ResourceListOperations.ListAtContext<VirtualMachine, VirtualMachineData>(subscription, filters, top, cancellationToken);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="subscription"></param>
        /// <param name="filter"></param>
        /// <param name="top"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public static AsyncPageable<VirtualMachine> ListVirtualMachinesAsync(this SubscriptionOperations subscription, ArmSubstringFilter filter = null, int? top = null, CancellationToken cancellationToken = default)
        {
            ArmFilterCollection filters = new ArmFilterCollection(VirtualMachineData.ResourceType);
            filters.SubstringFilter = filter;
            return ResourceListOperations.ListAtContextAsync<VirtualMachine, VirtualMachineData>(subscription, filters, top, cancellationToken);
        }
        #endregion

        #region AvailabilitySet List Operations
        public static Pageable<AvailabilitySet> ListAvailabilitySets(this SubscriptionOperations subscription, ArmSubstringFilter filter = null, int? top = null, CancellationToken cancellationToken = default)
        {
            ArmFilterCollection filters = new ArmFilterCollection(AvailabilitySetData.ResourceType);
            filters.SubstringFilter = filter;
            return ResourceListOperations.ListAtContext<AvailabilitySet, AvailabilitySetData>(subscription, filters, top, cancellationToken);
        }

        public static AsyncPageable<AvailabilitySet> ListAvailabilitySetsAsync(this SubscriptionOperations subscription, ArmSubstringFilter filter = null, int? top = null, CancellationToken cancellationToken = default)
        {
            ArmFilterCollection filters = new ArmFilterCollection(AvailabilitySetData.ResourceType);
            filters.SubstringFilter = filter;
            return ResourceListOperations.ListAtContextAsync<AvailabilitySet, AvailabilitySetData>(subscription, filters, top, cancellationToken);
        }
        #endregion
    }
}
