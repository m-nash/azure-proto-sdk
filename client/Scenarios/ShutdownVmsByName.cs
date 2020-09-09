using azure_proto_compute;
using azure_proto_core;
using System;

namespace client
{
    class ShutdownVmsByName: Scenario
    {
        public override void Execute()
        {
            var createMultipleVms = new CreateMultipleVms(Context);
            createMultipleVms.Execute();

            var sub = new ArmClient().Subscription(Context.SubscriptionId);

            foreach(var vm in sub.ListVms("even"))
            {
                Console.WriteLine($"Stopping {vm.Context.Name}");
                vm.Stop();
                Console.WriteLine($"Starting {vm.Context.Name}");
                vm.Start();
            }
        }
    }
}
