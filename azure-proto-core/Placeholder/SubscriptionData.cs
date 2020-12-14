// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT License.

using Azure.ResourceManager.Resources.Models;

namespace azure_proto_core
{
    public class SubscriptionData : TrackedResource<Subscription>
    {
        public SubscriptionData(Subscription s)
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
