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

            foreach(var armResource in sub.ListVirtualMachinesByName("even"))
            {
                var vmOperations = new VirtualMachineOperations(armResource);
                Console.WriteLine($"Stopping {armResource.Id.Name}");
                vmOperations.PowerOff();
                Console.WriteLine($"Starting {armResource.Id.Name}");
                vmOperations.PowerOn();
            }
        }
    }
}
