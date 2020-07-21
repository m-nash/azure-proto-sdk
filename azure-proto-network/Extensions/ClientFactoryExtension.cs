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
            var split = subscriptionId.Split('/');
            var subId = split[2];
            NetworkManagementClient retValue;
            if (!networkClients.TryGetValue(subId, out retValue))
            {
                lock (networkClientLock)
                {
                    if (!networkClients.TryGetValue(subId, out retValue))
                    {
                        retValue = new NetworkManagementClient(subId, new DefaultAzureCredential());
                        networkClients.Add(subId, retValue);
                    }
                }
            }
            return retValue;
        }
    }
}
