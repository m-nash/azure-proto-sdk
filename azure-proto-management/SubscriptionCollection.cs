using azure_proto_core;
using Azure.ResourceManager.Resources;
using System.Collections.Generic;

namespace azure_proto_management
{
    public class SubscriptionCollection : AzureCollection<AzureSubscription>
    {
        public SubscriptionCollection(AzureClient client) : base(client) { }

        private SubscriptionsOperations Client => ClientFactory.Instance.GetSubscriptionClient();

        protected override AzureSubscription Get(string subscriptionId)
        {
            AzureClient client = Parent as AzureClient;
            var subResult = Client.Get(subscriptionId);
            return new AzureSubscription(client, new PhSubscriptionModel(subResult));
        }

        protected override IEnumerable<AzureSubscription> GetItems()
        {
            AzureClient client = Parent as AzureClient;
            foreach (var s in Client.List())
            {
                yield return new AzureSubscription(client, new PhSubscriptionModel(s));
            }
        }
    }
}
