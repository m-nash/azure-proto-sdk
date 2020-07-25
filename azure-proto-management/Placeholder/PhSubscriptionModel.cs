using azure_proto_core;
using Azure.ResourceManager.Resources.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace azure_proto_management
{
    public class PhSubscriptionModel : TrackedResource<Subscription>
    {
        public PhSubscriptionModel(Subscription s) : base(s.Id, null, s)
        {
            Data = s;
        }

        public string SubscriptionId => Data.SubscriptionId;
        public string DisplayName => Data.DisplayName;
        public SubscriptionState? State => Data.State;
        public SubscriptionPolicies SubscriptionPolicies => Data.SubscriptionPolicies;
        public string AuthorizationSource => Data.AuthorizationSource;

    }
}
