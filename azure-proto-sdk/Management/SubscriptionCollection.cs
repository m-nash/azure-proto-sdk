using Microsoft.Azure.Management.Subscription;

namespace azure_proto_sdk.Management
{
    public class SubscriptionCollection : AzureCollection<AzureSubscription>
    {
        private AzureClient client;

        public SubscriptionCollection(AzureClient client)
        {
            this.client = client;
        }

        protected override void LoadValues()
        {
            foreach (var s in AzureClientManager.Instance.SubscriptionClient.Subscriptions.List())
            {
                this.Add(s.SubscriptionId, new AzureSubscription(client, s));
            }
        }
    }
}
