using Azure.ResourceManager.Network;
using azure_proto_core;

namespace azure_proto_network
{
    public static class ClientFactoryExtension
    {
        public static NetworkManagementClient GetNetworkClient(this ClientFactory factory, string subscriptionId)
        {
            return factory.GetClient(subscriptionId, (subscriptionId, credentials) => { return new NetworkManagementClient(subscriptionId, credentials); });
        }
    }
}
