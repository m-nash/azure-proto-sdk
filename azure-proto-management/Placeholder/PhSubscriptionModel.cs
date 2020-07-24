using azure_proto_core;
using Microsoft.Azure.Management.Subscription.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace azure_proto_management
{
    public class PhSubscriptionModel : SubscriptionModel, IModel
    {
        public SubscriptionModel Data { get; private set; }

        public PhSubscriptionModel(SubscriptionModel s)
        {
            Data = s;
        }

        public string Name => Data.DisplayName;

        new public string Id => Data.Id;
        new public string SubscriptionId => Data.SubscriptionId;
        new public string DisplayName => Data.DisplayName;
        new public SubscriptionState? State => Data.State;
        new public SubscriptionPolicies SubscriptionPolicies => Data.SubscriptionPolicies;
        new public string AuthorizationSource => Data.AuthorizationSource;

        object IModel.Data => Data;

        public string Location => throw new NotImplementedException();
    }
}
