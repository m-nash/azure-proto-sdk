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
                var vmOperations = new VirtualMachineOperations(vm);
                Console.WriteLine($"Stopping {vm.Id.Name}");
                vmOperations.PowerOff();
                Console.WriteLine($"Starting {vm.Id.Name}");
                vmOperations.PowerOn();
            }
        }
    }
}
