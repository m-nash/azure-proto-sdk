using Azure.Identity;
using Azure.ResourceManager.Compute;
using azure_proto_core;
using System.Collections.Generic;

namespace azure_proto_compute
{
    public static class ClientFactoryExtension
    {
        private static Dictionary<string, ComputeManagementClient> computeClients = new Dictionary<string, ComputeManagementClient>();
        private static readonly object computeClientLock = new object();
        public static ComputeManagementClient GetComputeClient(this ClientFactory factory, string subscriptionId)
        {
            ComputeManagementClient retValue;
            if (!computeClients.TryGetValue(subscriptionId, out retValue))
            {
                lock (computeClientLock)
                {
                    if (!computeClients.TryGetValue(subscriptionId, out retValue))
                    {
                        retValue = new ComputeManagementClient(subscriptionId, new DefaultAzureCredential());
                        computeClients.Add(subscriptionId, retValue);
                    }
                }
            }
            return retValue;
        }
    }
}
