using azure_proto_core;
using Microsoft.Azure.Management.Subscription;
using System.Collections.Generic;

namespace azure_proto_management
{
    public class SubscriptionCollection : AzureCollection<AzureSubscription>
    {
        public SubscriptionCollection(AzureClient client) : base(client) { }

        protected override AzureSubscription Get(string subscriptionId)
        {
            AzureClient client = Parent as AzureClient;
            var subClient = ClientFactory.SubscriptionClient;
            var subResult = subClient.Subscriptions.Get(subscriptionId);
            client.Clients = new ClientFactory(subscriptionId);
            return new AzureSubscription(client, new PhSubscriptionModel(subResult));
        }

        public override IEnumerable<AzureSubscription> GetItems()
        {
            AzureClient client = Parent as AzureClient;
            foreach (var s in ClientFactory.SubscriptionClient.Subscriptions.List())
            {
                client.Clients = new ClientFactory(s.SubscriptionId);
                yield return new AzureSubscription(client, new PhSubscriptionModel(s));
            }
        }
    }
}
