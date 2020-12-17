// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT License.

namespace Azure.ResourceManager.Core
{
    public class Subscription : SubscriptionOperations
    {
        internal Subscription(AzureResourceManagerClientContext context, SubscriptionData resource)
            : base(context, resource)
        {
            Data = resource;
        }

        public SubscriptionData Data { get; private set; }
    }
}
