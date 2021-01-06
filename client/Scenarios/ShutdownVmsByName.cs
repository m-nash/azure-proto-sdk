using azure_proto_compute;
using Azure.ResourceManager.Core;
using System;

namespace client
{
    class ShutdownVmsByName: Scenario
    {
        public override void Execute()
        {
            var createMultipleVms = new CreateMultipleVms(Context);
            createMultipleVms.Execute();

            var sub = new AzureResourceManagerClient().Subscription(Context.SubscriptionId);

            foreach(var vm in sub.ListVirtualMachinesByName("even"))
            {
                var instance = new VirtualMachineOperations(vm);
                Console.WriteLine($"Stopping {vm.Id.Name}");
                instance.PowerOff();
                Console.WriteLine($"Starting {vm.Id.Name}");
                instance.PowerOn();
            }
        }
    }
}
