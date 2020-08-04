using azure_proto_management;
using System;

namespace client
{
    class ShutdownVmsByNameAcrossSubscriptions : Scenario
    {
        public override void Execute()
        {
            AzureClient client = new AzureClient();

            foreach(var subscription in client.Subscriptions)
            {
                Console.WriteLine(subscription.Id);
            }
        }
    }
}
