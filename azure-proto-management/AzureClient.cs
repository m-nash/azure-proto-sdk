using azure_proto_core;

namespace azure_proto_management
{
    public class AzureClient : TrackedResource
    {
        public SubscriptionCollection Subscriptions { get; private set; }

        new public string Name => "MainClient";

        new public string Location => "westus2";

        public object Data => throw new System.NotImplementedException();

        public override ResourceIdentifier Id { get => throw new System.NotImplementedException(); protected set => throw new System.NotImplementedException(); }

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
