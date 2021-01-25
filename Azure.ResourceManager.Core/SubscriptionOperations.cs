// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT License.

using Azure.ResourceManager.Core.Adapters;
using Azure.ResourceManager.Resources;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading;
using System.Threading.Tasks;

namespace Azure.ResourceManager.Core
{
    /// <summary>
    /// A class representing the operations that can be performed over a specific subscription.
    /// </summary>
    public class SubscriptionOperations : ResourceOperationsBase<Subscription>
    {
        /// <summary>
        /// The resource type for subscription
        /// </summary>
        public static readonly ResourceType ResourceType = "Microsoft.Resources/subscriptions";

        /// <summary>
        /// Initializes a new instance of the <see cref="SubscriptionOperations"/> class.
        /// </summary>
        /// <param name="options"> The client parameters to use in these operations. </param>
        /// <param name="subscriptionId"> The Id of the subscription. </param>
        internal SubscriptionOperations(AzureResourceManagerClientOptions options, string subscriptionId)
            : base(options, $"/subscriptions/{subscriptionId}")
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="SubscriptionOperations"/> class.
        /// </summary>
        /// <param name="options"> The client parameters to use in these operations. </param>
        /// <param name="id"> The identifier of the subscription. </param>
        internal SubscriptionOperations(AzureResourceManagerClientOptions options, ResourceIdentifier id)
            : base(options, id)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="SubscriptionOperations"/> class.
        /// </summary>
        /// <param name="options"> The client parameters to use in these operations. </param>
        /// <param name="subscription"> The subscription resource. </param>
        internal SubscriptionOperations(AzureResourceManagerClientOptions options, Resource subscription)
            : base(options, subscription)
        {
        }

        protected override ResourceType ValidResourceType => ResourceType;

        /// <summary>
        /// Gets the subscription client.
        /// </summary>
        private SubscriptionsOperations SubscriptionsClient => GetClient((uri, cred) =>
            new ResourcesManagementClient(
                uri,
                Guid.NewGuid().ToString(),
                cred,
                ClientOptions.Convert<ResourcesManagementClientOptions>())).Subscriptions;

        /// <summary>
        /// Gets the resource group operations for a given resource group.
        /// </summary>
        /// <param name="resourceGroupData"> The resource group. </param>
        /// <returns> The resource group operations. </returns>
        public ResourceGroup GetResourceGroup(ResourceGroupData resourceGroupData)
        {
            return new ResourceGroup(ClientOptions, resourceGroupData);
        }

        /// <summary>
        /// Gets the resource group operations for a given resource group.
        /// </summary>
        /// <param name="resourceGroupId"> The resource group identifier. </param>
        /// <returns> The resource group operations. </returns>
        public ResourceGroupOperations GetResourceGroupOperations(ResourceIdentifier resourceGroupId)
        {
            return new ResourceGroupOperations(ClientOptions, resourceGroupId);
        }

        /// <summary>
        /// Gets the resource group container under this subscription
        /// </summary>
        /// <returns> The resource group container. </returns>
        public ResourceGroupContainer GetResourceGroupContainer()
        {
            return new ResourceGroupContainer(ClientOptions, this);
        }

        /// <summary>
        /// Gets the location group container under this subscription
        /// </summary>
        /// <returns> The resource group container. </returns>
        public LocationContainer GetLocationContainer()
        {
            return new LocationContainer(ClientOptions, this);
        }

        /// <inheritdoc/>
        public override ArmResponse<Subscription> Get()
        {
            return new PhArmResponse<Subscription, Azure.ResourceManager.Resources.Models.Subscription>(
                SubscriptionsClient.Get(Id.Name),
                Converter());
        }

        /// <inheritdoc/>
        public override async Task<ArmResponse<Subscription>> GetAsync(CancellationToken cancellationToken = default)
        {
            return new PhArmResponse<Subscription, Azure.ResourceManager.Resources.Models.Subscription>(
                await SubscriptionsClient.GetAsync(Id.Name, cancellationToken),
                Converter());
        }

        private Func<Azure.ResourceManager.Resources.Models.Subscription, Subscription> Converter()
        {
            return s => new Subscription(ClientOptions, new SubscriptionData(s));
        }

        /// <summary>
        /// Lists all available geo-locations.
        /// </summary>
        /// <param name="cancellationToken"> A token to allow the caller to cancel the call to the service. The default value is <see cref="P:System.Threading.CancellationToken.None" />. </param>
        /// <returns> A collection of location that may take multiple service requests to iterate over. </returns>
        public Pageable<LocationData> ListAvailableLocations(CancellationToken cancellationToken = default(CancellationToken))
        {
            var client = GetResourcesClient(Id.Subscription).Subscriptions;
            var result = client.ListLocations(Id.Subscription, cancellationToken);
            return new PhWrappingPageable<Azure.ResourceManager.Resources.Models.Location, LocationData>(
                result,
                location =>
                {
                    return location.DisplayName;
                });
        }

        /// <summary>
        /// Lists all available geo-locations.
        /// </summary>
        /// <param name="cancellationToken"> A token to allow the caller to cancel the call to the service. The default value is <see cref="P:System.Threading.CancellationToken.None" />. </param>
        /// <returns> An async collection of location that may take multiple service requests to iterate over. </returns>
        /// <exception cref="InvalidOperationException"> The default subscription id is null. </exception>
        public async IAsyncEnumerable<LocationData> ListAvailableLocationsAsync([EnumeratorCancellation] CancellationToken cancellationToken = default)
        {
            if (Id.Subscription == null)
            {
                throw new InvalidOperationException("Please select a default subscription");
            }

            await foreach (var provider in GetResourcesClient(Id.Subscription).Providers.ListAsync(expand: "metadata", cancellationToken: cancellationToken).WithCancellation(cancellationToken))
            {
                if (string.Equals(provider.Namespace, ResourceType?.Namespace, StringComparison.InvariantCultureIgnoreCase))
                {
                    var foundResource = provider.ResourceTypes.FirstOrDefault(p => ResourceType.Equals(p.ResourceType));
                    foreach (var location in foundResource.Locations)
                    {
                        yield return location;
                    }
                }
            }
        }
    }
}
