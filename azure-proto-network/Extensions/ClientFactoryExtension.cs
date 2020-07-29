using Azure.Identity;
using Azure.ResourceManager.Network;
using azure_proto_core;
using System.Collections.Generic;

namespace azure_proto_network
{
    public static class ClientFactoryExtension
    {
        private static Dictionary<string, NetworkManagementClient> networkClients = new Dictionary<string, NetworkManagementClient>();
        private static readonly object networkClientLock = new object();
        public static NetworkManagementClient GetNetworkClient(this ClientFactory factory, string subscriptionId)
        {
            NetworkManagementClient retValue;
            if (!networkClients.TryGetValue(subscriptionId, out retValue))
            {
                lock (networkClientLock)
                {
                    if (!networkClients.TryGetValue(subscriptionId, out retValue))
                    {
                        retValue = new NetworkManagementClient(subscriptionId, new DefaultAzureCredential());
                        networkClients.Add(subscriptionId, retValue);
                    }
                }
            }
            return retValue;
        }
    }
}
