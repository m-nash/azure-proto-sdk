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
        /// List VMs at the given subscription.
        /// </summary>
        /// <param name="subscription"> The id of the Azure subscription. </param>
        /// <returns> A response with the <see cref="Pageable{VirtualMachine}"/> operation for this resource. </returns>
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
        /// List VMs at the given subscription.
        /// </summary>
        /// <param name="subscription"> The id of the Azure subscription. </param>
        /// <returns> A response with the <see cref="AsyncPageable{VirtualMachine}"/> operation for this resource. </returns>
        public static AsyncPageable<VirtualMachine> ListVirtualMachinesAsync(this SubscriptionOperations subscription)
        {
            var vmOperations = subscription.GetClient((baseUri, cred) => new ComputeManagementClient(baseUri, subscription.Id.Subscription, cred,
                    subscription.ClientOptions.Convert<ComputeManagementClientOptions>())).VirtualMachines;
            var result = vmOperations.ListAllAsync();
            return new PhWrappingAsyncPageable<Azure.ResourceManager.Compute.Models.VirtualMachine, VirtualMachine>(
                result,
                s => new VirtualMachine(subscription.ClientOptions, new VirtualMachineData(s)));
        }

        /// <summary>
        /// List VMs by name at the given subscription.
        /// </summary>
        /// <param name="subscription"> The id of the Azure subscription. </param>
        /// <param name="filter"> ArmSubstringFilter to filter on. </param>
        /// <param name="cancellationToken"> A token to allow the caller to cancel the call to the service. The default value is <see cref="P:System.Threading.CancellationToken.None" />. </param>
        /// <returns> A response with the <see cref="Pageable{ArmResource}"/> operation for this resource. </returns>
        public static Pageable<ArmResource> ListVirtualMachinesByName(this SubscriptionOperations subscription, ArmSubstringFilter filter, int? top = null, CancellationToken cancellationToken = default)
        {
            ArmFilterCollection filters = new ArmFilterCollection(VirtualMachineOperations.ResourceType);
            filters.SubstringFilter = filter;
            return ResourceListOperations.ListAtContext(subscription.ClientOptions, subscription.Id, filters, top, cancellationToken);
        }

        /// <summary>
        /// List VMs by name at the given subscription.
        /// </summary>
        /// <param name="subscription"> The id of the Azure subscription. </param>
        /// <param name="filter"> ArmSubstringFilter to filter on. </param>
        /// <param name="cancellationToken"> A token to allow the caller to cancel the call to the service. The default value is <see cref="P:System.Threading.CancellationToken.None" />. </param>
        /// <returns> A <see cref="Task"/> that on completion returns a response with the <see cref="AsyncPageable{ArmResource}"/> operation for this resource. </returns>
        public static AsyncPageable<ArmResource> ListVirtualMachinesByNameAsync(this SubscriptionOperations subscription, ArmSubstringFilter filter, int? top = null, CancellationToken cancellationToken = default)
        {
            ArmFilterCollection filters = new ArmFilterCollection(VirtualMachineOperations.ResourceType);
            filters.SubstringFilter = filter;
            return ResourceListOperations.ListAtContextAsync(subscription.ClientOptions, subscription.Id, filters, top, cancellationToken);
        }
        #endregion

        #region AvailabilitySet List Operations
        /// <summary>
        /// List AvailabilitySets at the given subscription.
        /// </summary>
        /// <param name="subscription"> The id of the Azure subscription. </param>
        /// <returns> A response with the <see cref="Pageable{AvailabilitySet}"/> operation for this resource. </returns>
        public static Pageable<AvailabilitySet> ListAvailabilitySets(this SubscriptionOperations subscription)
        {
            ComputeManagementClient computeClient = GetComputeClient(subscription);
            var availabilitySetOperations = computeClient.AvailabilitySets;
            var result = availabilitySetOperations.ListBySubscription();
            return new PhWrappingPageable<Azure.ResourceManager.Compute.Models.AvailabilitySet, AvailabilitySet>(
                result,
                s => new AvailabilitySet(subscription.ClientOptions, new AvailabilitySetData(s)));
        }

        /// <summary>
        /// List AvailabilitySets at the given subscription.
        /// </summary>
        /// <param name="subscription"> The id of the Azure subscription. </param>
        /// <returns> A response with the <see cref="AsyncPageable{AvailabilitySet}"/> operation for this resource. </returns>
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
