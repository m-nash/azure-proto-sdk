using Azure.ResourceManager.Compute;
using azure_proto_compute;
using Azure.ResourceManager.Core;
using System;
using System.Threading.Tasks;

namespace client
{
    class ShutdownVmsByNameAcrossSubscriptions : Scenario
    {
        public async void ShutdownAsync()
        {
            var client = new AzureResourceManagerClient();

            await foreach (var subscription in client.Subscriptions().ListAsync())
            {
                await foreach (var armResource in subscription.ListVirtualMachinesByNameAsync("even"))
                {
                    var vmOperations = VirtualMachineOperations.FromGeneric(armResource);
                    await vmOperations.PowerOffAsync();
                    await vmOperations.PowerOnAsync();
                }
            }
        }

        public async override void Execute()
        {
            #region SETUP
            ScenarioContext[] contexts = new ScenarioContext[] { new ScenarioContext(), new ScenarioContext("c9cbd920-c00c-427c-852b-8aaf38badaeb") };
            ParallelOptions options = new ParallelOptions
            {
                MaxDegreeOfParallelism = 1
            };

            Parallel.ForEach(contexts, options, context =>
            {
                var createMultipleVms = new CreateMultipleVms(context);
                createMultipleVms.Execute();
            });
            #endregion


            var client = new AzureResourceManagerClient();
            foreach (var sub in client.Subscriptions().List())
            {
                await foreach (var armResource in sub.ListVirtualMachinesByNameAsync("even"))
                {
                    var vmOperations = VirtualMachineOperations.FromGeneric(armResource);
                    Console.WriteLine($"Stopping {vmOperations.Id.Subscription} {vmOperations.Id.ResourceGroup} {vmOperations.Id.Name}");
                    vmOperations.PowerOff();
                    Console.WriteLine($"Starting {vmOperations.Id.Subscription} {vmOperations.Id.ResourceGroup} {vmOperations.Id.Name}");
                    vmOperations.PowerOn();
                }
            }
        }
    }
}
