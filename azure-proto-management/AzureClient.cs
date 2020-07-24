using azure_proto_core;

namespace azure_proto_management
{
    public class AzureClient : IResource
    {
        public SubscriptionCollection Subscriptions { get; private set; }

        public string Name => "MainClient";

        public string Id => "1";

        public string Location => "westus2";

        public object Data => throw new System.NotImplementedException();

        public AzureClient()
        {
            Subscriptions = new SubscriptionCollection(this);
        }

        public static AzureResourceGroup GetResourceGroup(string subscriptionId, string rgName)
        {
            AzureClient client = new AzureClient();
            var subscription = client.Subscriptions[subscriptionId];
            return subscription.ResourceGroups[rgName];
        }
    }
}
