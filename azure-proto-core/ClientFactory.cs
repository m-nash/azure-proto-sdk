namespace azure_proto_core
{
    public class ClientFactory
    {
        private static ClientFactory g_instance;
        private static readonly object g_padlock = new object();

        private ClientFactory()
        {
        }

        public static ClientFactory Instance
        {
            get
            {
                if(g_instance == null)
                {
                    lock(g_padlock)
                    {
                        if(g_instance == null)
                        {
                            g_instance = new ClientFactory();
                        }
                    }
                }
                return g_instance;
            }
        }
    }
}
