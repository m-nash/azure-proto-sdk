using Azure.Identity;
using Azure.ResourceManager.Core;
using System;

namespace client
{
    class DefaultSubscription : Scenario
    {
        public override void Execute()
        {
            var client = new AzureResourceManagerClient(Context.SubscriptionId, new DefaultAzureCredential());

            var sub = client.DefaultSubscription;

            if (sub.Data.Guid != Context.SubscriptionId)
                throw new Exception($"Didn't get back expected subscription.  Got {sub.Data.Guid} expected {Context.SubscriptionId}");

            Console.WriteLine("Found correct subscription");

            client = new AzureResourceManagerClient();

            sub = client.DefaultSubscription;

            if (sub.Data.Guid != Context.SubscriptionId)
                throw new Exception($"Didn't get back expected subscription.  Got {sub.Data.Guid} expected {Context.SubscriptionId}");

            Console.WriteLine("Found correct subscription");

        }
    }
}
