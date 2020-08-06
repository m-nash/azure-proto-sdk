using azure_proto_core;
using System.Collections.Generic;

namespace azure_proto_management
{
    public class AzureClient : AzureClientBase
    {
        protected override IEnumerable<AzureSubscriptionBase> SubscriptionsGeneric { get; set; }

        public SubscriptionCollection Subscriptions => SubscriptionsGeneric as SubscriptionCollection;

        new public string Name => "MainClient";

        new public string Location => "westus2";

        public object Data => throw new System.NotImplementedException();

        public override ResourceIdentifier Id { get => throw new System.NotImplementedException(); protected set => throw new System.NotImplementedException(); }

        public static AzureSubscription GetSubscription(string subscriptionId)
        {
            AzureClient client = new AzureClient();
            return client.Subscriptions[subscriptionId];
        }

        //TODO: overload to allow for passing in a context which will include credential, uri, ?clientoptions?
        //TODO: are we overloading Cloud with Tenant?
        public AzureClient()
        {
            SubscriptionsGeneric = new SubscriptionCollection(this);
        }

        public static AzureResourceGroup GetResourceGroup(string subscriptionId, string rgName)
        {
            AzureClient client = new AzureClient();
            var subscription = client.Subscriptions[subscriptionId];
            return subscription.ResourceGroups[rgName];
        }
    }
}
