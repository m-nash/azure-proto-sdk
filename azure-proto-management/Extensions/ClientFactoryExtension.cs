using azure_proto_core;
using Microsoft.Azure.Management.ResourceManager;
using System.Collections.Generic;

namespace azure_proto_management
{
    public static class ClientFactoryExtension
    {
        private static Dictionary<string, ResourceManagementClient> resourceClients = new Dictionary<string, ResourceManagementClient>();
        private static readonly object resourceClientLock = new object();
        public static ResourceManagementClient GetResourceClient(this ClientFactory factory, string subscriptionId)
        {
            var split = subscriptionId.Split('/');
            var subId = split[2];
            ResourceManagementClient retValue;
            if (!resourceClients.TryGetValue(subId, out retValue))
            {
                lock (resourceClientLock)
                {
                    if (!resourceClients.TryGetValue(subId, out retValue))
                    {
                        retValue = new ResourceManagementClient(AzureClientManager.Instance.Creds);
                        retValue.SubscriptionId = subId;
                        resourceClients.Add(subId, retValue);
                    }
                }
            }
            return retValue;
        }

        private static Microsoft.Azure.Management.Subscription.SubscriptionClient subscriptionClient;
        private static readonly object subscriptionClientLock = new object();
        public static Microsoft.Azure.Management.Subscription.SubscriptionClient GetSubscriptionClient(this ClientFactory factoryd)
        {
            if (subscriptionClient == null)
            {
                lock (subscriptionClientLock)
                {
                    if (subscriptionClient == null)
                    {
                        subscriptionClient = new Microsoft.Azure.Management.Subscription.SubscriptionClient(AzureClientManager.Instance.Creds);
                    }
                }
            }
            return subscriptionClient;
        }
    }
}
