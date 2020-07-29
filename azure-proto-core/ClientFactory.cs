namespace azure_proto_core
{
    public class ClientFactory
    {
        private static ClientFactory _instance;
        private static readonly object _padlock = new object();

        private ClientFactory()
        {
        }

        public static ClientFactory Instance
        {
            get
            {
                if(_instance == null)
                {
                    lock(_padlock)
                    {
                        if(_instance == null)
                        {
                            _instance = new ClientFactory();
                        }
                    }
                }
                return _instance;
            }
        }
    }
}
