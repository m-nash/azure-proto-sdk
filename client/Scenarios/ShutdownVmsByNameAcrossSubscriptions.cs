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
            var client = new ArmClient();

            await foreach (var subscription in client.Subscriptions().ListAsync())
            {
                await foreach (var vm in subscription.ListVirtualMachinesAsync("even"))
                {
                    await vm.PowerOffAsync();
                    await vm.PowerOnAsync();
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


            var client = new ArmClient();
            foreach (var sub in client.Subscriptions().List())
            {
                await foreach (var vm in sub.ListVirtualMachinesAsync("even"))
                {
                       Console.WriteLine($"Stopping {vm.Id.Subscription} {vm.Id.ResourceGroup} {vm.Id.Name}");
                       vm.PowerOff();
                       Console.WriteLine($"Starting {vm.Id.Subscription} {vm.Id.ResourceGroup} {vm.Id.Name}");
                       vm.PowerOn();
                }
            }
        }
    }
}
