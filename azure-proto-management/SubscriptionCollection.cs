using azure_proto_core;
using Microsoft.Azure.Management.Subscription;

namespace azure_proto_management
{
    public class SubscriptionCollection : AzureCollection<AzureSubscription>
    {
        public SubscriptionCollection(AzureClient client) : base(client) { }

        protected override void LoadValues()
        {
            AzureClient client = Parent as AzureClient;
            foreach (var s in ClientFactory.SubscriptionClient.Subscriptions.List())
            {
                client.Clients = new ClientFactory(s.SubscriptionId);
                this.Add(s.SubscriptionId, new AzureSubscription(client, new PhSubscriptionModel(s)));
            }
        }

        protected override AzureSubscription GetSingleValue(string key)
        {
            AzureClient client = Parent as AzureClient;
            var subClient = ClientFactory.SubscriptionClient;
            var subResult = subClient.Subscriptions.Get(key);
            client.Clients = new ClientFactory(key);
            return new AzureSubscription(client, new PhSubscriptionModel(subResult));
        }
    }
}
