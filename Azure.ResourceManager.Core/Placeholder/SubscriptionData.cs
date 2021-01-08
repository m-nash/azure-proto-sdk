// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT License.

using Azure.ResourceManager.Resources.Models;

namespace Azure.ResourceManager.Core
{
    /// <summary>
    /// A class representing the subscription data model.
    /// </summary>
    public class SubscriptionData : TrackedResource<Azure.ResourceManager.Resources.Models.Subscription>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="SubscriptionData"/> class.
        /// </summary>
        /// <param name="subscription"> The subscription model. </param>
        public SubscriptionData(Azure.ResourceManager.Resources.Models.Subscription subscription)
            : base(subscription.Id, null, subscription)
        {
        }

        /// <summary>
        /// Gets the subscription id.
        /// </summary>
        public override string Name => Model.SubscriptionId;

        /// <summary>
        /// Gets the Id of the Subscription.
        /// </summary>
        public string SubscriptionId => Model.SubscriptionId;

        /// <summary>
        /// Gets the display name of the subscription.
        /// </summary>
        public string DisplayName => Model.DisplayName;

        /// <summary>
        /// Gets the state of the subscription.
        /// </summary>
        public SubscriptionState? State => Model.State;

        /// <summary>
        /// Gets the policies of the subscription.
        /// </summary>
        public SubscriptionPolicies SubscriptionPolicies => Model.SubscriptionPolicies;

        /// <summary>
        /// Gets the authorization source of the subscription.
        /// </summary>
        public string AuthorizationSource => Model.AuthorizationSource;
    }
}
