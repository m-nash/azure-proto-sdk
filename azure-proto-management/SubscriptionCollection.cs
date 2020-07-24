using azure_proto_core;
using Microsoft.Azure.Management.Subscription;
using System.Collections.Generic;

namespace azure_proto_management
{
    public class SubscriptionCollection : AzureCollection<AzureSubscription>
    {
        public SubscriptionCollection(AzureClient client) : base(client) { }

        private SubscriptionClient Client => ClientFactory.Instance.GetSubscriptionClient();

        protected override AzureSubscription Get(string subscriptionId)
        {
            AzureClient client = Parent as AzureClient;
            var subResult = Client.Subscriptions.Get(subscriptionId);
            return new AzureSubscription(client, new PhSubscriptionModel(subResult));
        }

        protected override IEnumerable<AzureSubscription> GetItems()
        {
            AzureClient client = Parent as AzureClient;
            foreach (var s in Client.Subscriptions.List())
            {
                yield return new AzureSubscription(client, new PhSubscriptionModel(s));
            }
        }
    }
}
