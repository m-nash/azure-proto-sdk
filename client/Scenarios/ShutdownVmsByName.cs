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

            var resourceGroup = new ArmClient().ResourceGroup(Context.SubscriptionId, Context.RgName);

            foreach(var vm in resourceGroup.ListVms("even"))
            {
                Console.WriteLine($"Stopping {vm.Context.Name}");
                vm.Stop();
                Console.WriteLine($"Starting {vm.Context.Name}");
                vm.Start();
            }
        }
    }
}
