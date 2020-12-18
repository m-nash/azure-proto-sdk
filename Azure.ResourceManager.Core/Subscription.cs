// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT License.

namespace Azure.ResourceManager.Core
{
    public class Subscription : SubscriptionOperations
    {
        internal Subscription(AzureResourceManagerClientOptions options, SubscriptionData resource)
            : base(options, resource)
        {
            Data = resource;
        }

        public SubscriptionData Data { get; private set; }
    }
}
