// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT License.

namespace Azure.ResourceManager.Core
{
    public class Subscription : SubscriptionOperations
    {
        internal Subscription(ArmClientContext context, SubscriptionData resource, ArmClientOptions options)
            : base(context, resource, options)
        {
            Data = resource;
        }

        public SubscriptionData Data { get; private set; }
    }
}
