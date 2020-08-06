using Azure.ResourceManager.Resources;
using azure_proto_core;
using System;

namespace azure_proto_management
{
    public static class ClientFactoryExtension
    {
        public static ResourcesManagementClient GetResourceClient(this ClientFactory factory, string subscriptionId)
        {
            return factory.GetClient(subscriptionId, (subscriptionId, credential) => { return new ResourcesManagementClient(subscriptionId, credential); });
        }

        public static SubscriptionsOperations GetSubscriptionClient(this ClientFactory factory)
        {
            return factory.GetClient(Guid.Empty.ToString(), (subscriptionId, credential) => { return new ResourcesManagementClient(subscriptionId, credential).Subscriptions; });
        }
    }
}
