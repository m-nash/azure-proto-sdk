using azure_proto_sdk.Management;

namespace azure_proto_sdk
{
    public class AzureClient
    {
        public SubscriptionCollection Subscriptions { get; private set; }

        public AzureClient()
        {
            Subscriptions = new SubscriptionCollection(this);
        }
    }
}
