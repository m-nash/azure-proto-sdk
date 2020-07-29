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
        }

        public override string Name => Model.SubscriptionId;
        public string SubscriptionId => Model.SubscriptionId;
        public string DisplayName => Model.DisplayName;
        public SubscriptionState? State => Model.State;
        public SubscriptionPolicies SubscriptionPolicies => Model.SubscriptionPolicies;
        public string AuthorizationSource => Model.AuthorizationSource;

    }
}
