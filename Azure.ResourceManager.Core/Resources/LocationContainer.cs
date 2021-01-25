// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT License.

using Azure.ResourceManager.Core.Adapters;
using Azure.ResourceManager.Resources;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Azure.ResourceManager.Core
{
    /// <summary>
    /// Represents an Azure geography region where supported resource providers live.
    /// </summary>
    public class LocationContainer : OperationsBase
    {
        public LocationContainer(AzureResourceManagerClientOptions clientOptions, SubscriptionOperations subscriptionOperations)
            : base(clientOptions, subscriptionOperations.Id)
        {
        }

        protected override ResourceType ValidResourceType => SubscriptionOperations.ResourceType;

        /// <summary>
        /// Gets the resource client.
        /// </summary>
        internal ResourcesManagementClient GetResourcesClient(string subscription) => ClientOptions.GetClient((uri, credential) => new ResourcesManagementClient(uri, subscription, credential));

        /// <summary>
        /// Gets the subscription client.
        /// </summary>
        internal SubscriptionsOperations SubscriptionsClient => GetResourcesClient(Guid.NewGuid().ToString()).Subscriptions;

        /// <summary>
        /// Gets the Azure subscriptions.
        /// </summary>
        /// <returns> Subscription container. </returns>
        public SubscriptionContainer GetSubscriptionContainer()
        {
            return new SubscriptionContainer(ClientOptions);
        }

        /// <summary>
        /// Gets the default Azure subscription.
        /// </summary>
        public SubscriptionOperations DefaultSubscription { get; private set; }

        /// <summary>
        /// Gets default subscription.
        /// </summary>
        /// <param name="cancellationToken"> A token to allow the caller to cancel the call to the service. The default value is <see cref="P:System.Threading.CancellationToken.None" />. </param>
        /// <returns> A <see cref="Task"/> that on completion returns the subscription id. </returns>
        internal async Task<string> GetDefaultSubscription(CancellationToken cancellationToken = default)
        {
            string sub = DefaultSubscription?.Id?.Subscription;
            if (null == sub)
            {
                sub = await GetSubscriptionContainer().GetDefaultSubscriptionAsync(cancellationToken);
            }

            return sub;
        }

        /// <summary>
        /// Lists all geo-locations.
        /// </summary>
        /// <param name="subscriptionId"> The Id of the target subscription. </param>
        /// <param name="cancellationToken"> A token to allow the caller to cancel the call to the service. The default value is <see cref="P:System.Threading.CancellationToken.None" />. </param>
        /// <returns> A collection of location data that may take multiple service requests to iterate over. </returns>
        /// <exception cref="InvalidOperationException"> <paramref name="subscriptionId"/> is null. </exception>
        public Pageable<LocationData> List(string subscriptionId = null, CancellationToken cancellationToken = default(CancellationToken))
        {
            if (string.IsNullOrWhiteSpace(subscriptionId))
            {
                subscriptionId = GetDefaultSubscription(cancellationToken).ConfigureAwait(false).GetAwaiter().GetResult();
                if (subscriptionId == null)
                {
                    throw new InvalidOperationException("Please select a default subscription");
                }
            }

            return new PhWrappingPageable<Azure.ResourceManager.Resources.Models.Location, LocationData>(SubscriptionsClient.ListLocations(subscriptionId, cancellationToken), s => s.DisplayName);
        }

        /// <summary>
        /// Lists all geo-locations.
        /// </summary>
        /// <param name="subscriptionId"> The Id of the target subscription. </param>
        /// <param name="token"> A token to allow the caller to cancel the call to the service. The default value is <see cref="P:System.Threading.CancellationToken.None" />. </param>
        /// <returns> An async collection of location data that may take multiple service requests to iterate over. </returns>
        /// <exception cref="InvalidOperationException"> <paramref name="subscriptionId"/> is null. </exception>
        public AsyncPageable<LocationData> ListAsync(string subscriptionId = null, CancellationToken token = default(CancellationToken))
        {
            async Task<AsyncPageable<LocationData>> PageableFunc()
            {
                if (string.IsNullOrWhiteSpace(subscriptionId))
                {
                    subscriptionId = await GetDefaultSubscription(token);
                    if (subscriptionId == null)
                    {
                        throw new InvalidOperationException("Please select a default subscription");
                    }
                }

                return new PhWrappingAsyncPageable<Azure.ResourceManager.Resources.Models.Location, LocationData>(SubscriptionsClient.ListLocationsAsync(subscriptionId, token), s => s.DisplayName);
            }

            return new PhTaskDeferringAsyncPageable<LocationData>(PageableFunc);
        }
    }
}
