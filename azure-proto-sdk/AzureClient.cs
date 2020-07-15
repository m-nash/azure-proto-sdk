using azure_proto_core;

namespace azure_proto_management
{
    public class AzureClient : IResource
    {
        public SubscriptionCollection Subscriptions { get; private set; }

        public string Name => "MainClient";

        public string Id => "1";

        public ClientFactory Clients { get; set; }

        public AzureClient()
        {
            Subscriptions = new SubscriptionCollection(this);
        }
    }
}
