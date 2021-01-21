// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT License.

using Azure.ResourceManager.Resources;
using System;
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
    }
}
