namespace azure_proto_core
{
    internal sealed class AzureClientManager
    {
        private static readonly object g_padlock = new object();
        private static AzureClientManager g_instance;

        private AzureClientManager()
        {
            Creds = new LoginCredentials();
        }

        internal LoginCredentials Creds { get; private set; }

        internal static AzureClientManager Instance
        {
            get
            {
                if (g_instance == null)
                {
                    lock (g_padlock)
                    {
                        if (g_instance == null)
                        {
                            g_instance = new AzureClientManager();
                        }
                    }
                }
                return g_instance;
            }
        }

        private Microsoft.Azure.Management.Subscription.SubscriptionClient subscriptionClient;
        internal Microsoft.Azure.Management.Subscription.SubscriptionClient SubscriptionClient
        {
            get
            {
                if (subscriptionClient == null)
                {
                    subscriptionClient = new Microsoft.Azure.Management.Subscription.SubscriptionClient(Creds);
                }
                return subscriptionClient;
            }
        }
    }
}