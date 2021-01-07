// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT License.

namespace Azure.ResourceManager.Core
{
    /// <summary>
    /// A class representing a Subscription along with the instance operations that can be performed on it.
    /// </summary>
    public class Subscription : SubscriptionOperations
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Subscription"/> class.
        /// </summary>
        /// <param name="options">The client parameters to use in these operations.</param>
        /// <param name="resource">The resource data model.</param>
        internal Subscription(AzureResourceManagerClientOptions options, SubscriptionData resource)
            : base(options, resource)
        {
            Data = resource;
        }

        /// <summary>
        /// Gets the subscription data model.
        /// </summary>
        public SubscriptionData Data { get; private set; }
    }
}
