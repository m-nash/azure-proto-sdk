namespace azure_proto_core
{
    public sealed class AzureClientManager
    {
        private static readonly object _padlock = new object();
        private static AzureClientManager _instance;

        public static AzureClientManager Instance
        {
            get
            {
                if (_instance == null)
                {
                    lock (_padlock)
                    {
                        if (_instance == null)
                        {
                            _instance = new AzureClientManager();
                        }
                    }
                }
                return _instance;
            }
        }
    }
}