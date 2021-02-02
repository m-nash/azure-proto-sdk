using azure_proto_compute;
using Azure.ResourceManager.Core;
using System;

namespace client
{
    class ShutdownVmsByName: Scenario
    {
        public override System.Threading.Tasks.Task Execute()
        {
            var createMultipleVms = new CreateMultipleVms(Context);
            createMultipleVms.Execute();

            var sub = new AzureResourceManagerClient().GetSubscriptionOperations(Context.SubscriptionId);

            foreach(var armResource in sub.ListVirtualMachinesByName("-e"))
            {
                var vmOperations = VirtualMachineOperations.FromGeneric(armResource);
                Console.WriteLine($"Stopping {armResource.Id.ResourceGroup} : {armResource.Id.Name}");
                vmOperations.PowerOff();
                Console.WriteLine($"Starting {armResource.Id.ResourceGroup} : {armResource.Id.Name}");
                vmOperations.PowerOn();
            }
            
            return System.Threading.Tasks.Task.FromResult<object>(null);
        }
    }
}
