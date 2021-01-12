using Azure;
using Azure.Core;
using Azure.ResourceManager.Compute;
using Azure.ResourceManager.Core;
using Azure.ResourceManager.Core.Adapters;
using Azure.ResourceManager.Core.Resources;
using System;
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
        /// <returns></returns>
        public static Pageable<VirtualMachine> ListVirtualMachines(this SubscriptionOperations subscription)
        {
            ComputeManagementClient computeClient = GetComputeClient(subscription);
            var vmOperations = computeClient.VirtualMachines;
            var result = vmOperations.ListAll();
            return new PhWrappingPageable<Azure.ResourceManager.Compute.Models.VirtualMachine, VirtualMachine>(
                result,
                s => new VirtualMachine(subscription.ClientOptions, new VirtualMachineData(s)));
        }

        private static ComputeManagementClient GetComputeClient(SubscriptionOperations subscription)
        {
            Func<Uri, TokenCredential, ComputeManagementClient> ctor = (baseUri, cred) => new ComputeManagementClient(
                                baseUri,
                                subscription.Id.Subscription,
                                cred,
                                subscription.ClientOptions.Convert<ComputeManagementClientOptions>());
            var computeClient = subscription.GetClient(ctor);
            return computeClient;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="subscription"></param>
        /// <returns></returns>
        public static AsyncPageable<VirtualMachine> ListVirtualMachinesAsync(this SubscriptionOperations subscription)
        {
            var vmOperations = subscription.GetClient((baseUri, cred) => new ComputeManagementClient(baseUri, subscription.Id.Subscription, cred,
                    subscription.ClientOptions.Convert<ComputeManagementClientOptions>())).VirtualMachines;
            var result = vmOperations.ListAllAsync();
            return new PhWrappingAsyncPageable<Azure.ResourceManager.Compute.Models.VirtualMachine, VirtualMachine>(
                result,
                s => new VirtualMachine(subscription.ClientOptions, new VirtualMachineData(s)));
        }

        public static Pageable<ArmResource> ListVirtualMachinesByName(this SubscriptionOperations subscription, ArmSubstringFilter filter, int? top = null, CancellationToken cancellationToken = default)
        {
            ArmFilterCollection filters = new ArmFilterCollection(VirtualMachineOperations.ResourceType);
            filters.SubstringFilter = filter;
            return ResourceListOperations.ListAtContext(subscription.ClientOptions, subscription.Id, filters, top, cancellationToken);
        }

        public static AsyncPageable<ArmResource> ListVirtualMachinesByNameAsync(this SubscriptionOperations subscription, ArmSubstringFilter filter, int? top = null, CancellationToken cancellationToken = default)
        {
            ArmFilterCollection filters = new ArmFilterCollection(VirtualMachineOperations.ResourceType);
            filters.SubstringFilter = filter;
            return ResourceListOperations.ListAtContextAsync(subscription.ClientOptions, subscription.Id, filters, top, cancellationToken);
        }
        #endregion

        #region AvailabilitySet List Operations
        public static Pageable<AvailabilitySet> ListAvailabilitySets(this SubscriptionOperations subscription)
        {
            ComputeManagementClient computeClient = GetComputeClient(subscription);
            var availabilitySetOperations = computeClient.AvailabilitySets;
            var result = availabilitySetOperations.ListBySubscription();
            return new PhWrappingPageable<Azure.ResourceManager.Compute.Models.AvailabilitySet, AvailabilitySet>(
                result,
                s => new AvailabilitySet(subscription.ClientOptions, new AvailabilitySetData(s)));
        }

        public static AsyncPageable<AvailabilitySet> ListAvailabilitySetsAsync(this SubscriptionOperations subscription)
        {
            ComputeManagementClient computeClient = GetComputeClient(subscription);
            var availabilitySetOperations = computeClient.AvailabilitySets;
            var result = availabilitySetOperations.ListBySubscriptionAsync();
            return new PhWrappingAsyncPageable<Azure.ResourceManager.Compute.Models.AvailabilitySet, AvailabilitySet>(
                result,
                s => new AvailabilitySet(subscription.ClientOptions, new AvailabilitySetData(s)));
        }
        #endregion
    }
}
