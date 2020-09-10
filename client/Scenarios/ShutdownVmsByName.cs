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

            foreach(var vm in sub.ListVirtualMachines("even"))
            {
                Console.WriteLine($"Stopping {vm.Id.Name}");
                vm.Stop();
                Console.WriteLine($"Starting {vm.Id.Name}");
                vm.Start();
            }
        }
    }
}
