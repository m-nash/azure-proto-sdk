using azure_proto_compute;
using azure_proto_core;
using System;

namespace client
{
    class ListAllResourceGroupsByName: Scenario
    {
        public override void Execute()
        {
            var client = new ArmClient();
            var subscription = client.Subscription(Context.SubscriptionId);

            // Create Resource Group
            Console.WriteLine($"--------Start create group {Context.RgName}--------");
            for(int i = 5; i > 0; i--){
                var odd_or_even = i % 2 == 0 ? "even" : "odd";
                var resourceGroup = subscription.ResourceGroups().Create(Context.RgName + "_" + odd_or_even + "_" + i, Context.Loc).Value;
                CleanUp.Add(resourceGroup.Id);
            }
            var sub = new ArmClient().Subscription(Context.SubscriptionId);
            var xx = sub.ResourceGroups().ListResourceGroupsByName("even");
            foreach(var rg in sub.ResourceGroups().ListResourceGroupsByName("even"))
            {
                Console.WriteLine($"even {rg.Id.Name}");
            }
        }
    }
}
