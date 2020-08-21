using azure_proto_compute;
using azure_proto_management;
using System;

namespace client
{
    class ShutdownVmsByName: Scenario
    {
        public override void Execute()
        {
            var createMultipleVms = new CreateMultipleVms(Context);
            createMultipleVms.Execute();

            var resourceGroup = AzureClient.GetResourceGroup(Context.SubscriptionId, Context.RgName);

            foreach(var vm in resourceGroup.Vms().GetItemsByName("even"))
            {
                Console.WriteLine($"Stopping {vm.Name}");
                vm.Stop();
                Console.WriteLine($"Starting {vm.Name}");
                vm.Start();
            }
        }
    }
}
