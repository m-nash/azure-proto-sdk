namespace azure_proto_core
{
    public sealed class AzureClientManager
    {
        private static readonly object g_padlock = new object();
        private static AzureClientManager g_instance;

        private AzureClientManager()
        {
            Creds = new LoginCredentials();
        }

        public LoginCredentials Creds { get; private set; }

        public static AzureClientManager Instance
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
    }
}