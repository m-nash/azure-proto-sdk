namespace azure
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
