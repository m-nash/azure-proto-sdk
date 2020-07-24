using azure_proto_core;
using Azure.ResourceManager.Resources;
using System.Collections.Generic;
using Azure.Identity;
using System;

namespace azure_proto_management
{
    public static class ClientFactoryExtension
    {
        private static Dictionary<string, ResourcesManagementClient> resourceClients = new Dictionary<string, ResourcesManagementClient>();
        private static readonly object resourceClientLock = new object();
        public static ResourcesManagementClient GetResourceClient(this ClientFactory factory, string subscriptionId)
        {
            var split = subscriptionId.Split('/');
            var subId = split[2];
            ResourcesManagementClient retValue;
            if (!resourceClients.TryGetValue(subId, out retValue))
            {
                lock (resourceClientLock)
                {
                    if (!resourceClients.TryGetValue(subId, out retValue))
                    {
                        retValue = new ResourcesManagementClient(subId, new DefaultAzureCredential());
                        resourceClients.Add(subId, retValue);
                    }
                }
            }
            return retValue;
        }

        private static SubscriptionsOperations subscriptionClient;
        private static readonly object subscriptionClientLock = new object();
        public static SubscriptionsOperations GetSubscriptionClient(this ClientFactory factoryd)
        {
            if (subscriptionClient == null)
            {
                lock (subscriptionClientLock)
                {
                    if (subscriptionClient == null)
                    {
                        subscriptionClient = new ResourcesManagementClient(Guid.NewGuid().ToString(), new DefaultAzureCredential()).Subscriptions;
                    }
                }
            }
            return subscriptionClient;
        }
    }
}
