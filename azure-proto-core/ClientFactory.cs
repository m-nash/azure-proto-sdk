using Azure.Identity;
using System;
using System.Collections.Generic;

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

        private static readonly object _credentialLock = new object();
        private DefaultAzureCredential _creds;
        private DefaultAzureCredential Creds
        {
            get
            {
                if(_creds==null)
                {
                    lock(_credentialLock)
                    {
                        if(_creds == null)
                        {
                            _creds = new DefaultAzureCredential();
                        }
                    }
                }
                return _creds;
            }
        }

        private static readonly object _clientLock = new object();
        private readonly Dictionary<Type, Dictionary<string, object>> _clients = new Dictionary<Type, Dictionary<string, object>>();

        public T GetClient<T>(string subscriptionId, Func<string, DefaultAzureCredential, T> constructor)
            where T: class
        {
            Dictionary<string, object> clientDictionary;
            if (!_clients.TryGetValue(typeof(T), out clientDictionary))
            {
                lock (_clientLock)
                {
                    if (!_clients.TryGetValue(typeof(T), out clientDictionary))
                    {
                        clientDictionary = new Dictionary<string, object>();
                        _clients.Add(typeof(T), clientDictionary);
                    }
                }
            }

            object client;
            if (!clientDictionary.TryGetValue(subscriptionId, out client))
            {
                lock (_clientLock)
                {
                    if (!clientDictionary.TryGetValue(subscriptionId, out client))
                    {
                        client = constructor(subscriptionId, Creds);
                        clientDictionary.Add(subscriptionId, client);
                    }
                }
            }

            return client as T;
        }
    }
}
