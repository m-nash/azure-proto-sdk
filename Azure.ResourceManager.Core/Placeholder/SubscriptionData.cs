// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT License.

using Azure.ResourceManager.Resources.Models;

namespace Azure.ResourceManager.Core
{
    public class SubscriptionData : TrackedResource<Azure.ResourceManager.Resources.Models.Subscription>
    {
        public SubscriptionData(Azure.ResourceManager.Resources.Models.Subscription s)
            : base(s.Id, null, s)
        {
        }

        public override string Name => Model.SubscriptionId;

        public string SubscriptionId => Model.SubscriptionId;

        public string DisplayName => Model.DisplayName;

        public SubscriptionState? State => Model.State;

        public SubscriptionPolicies SubscriptionPolicies => Model.SubscriptionPolicies;

        public string AuthorizationSource => Model.AuthorizationSource;
    }
}
