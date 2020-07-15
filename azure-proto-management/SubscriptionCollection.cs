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
    }
}
