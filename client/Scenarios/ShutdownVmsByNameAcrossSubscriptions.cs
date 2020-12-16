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


            var client = new AzureResourceManagerClient();
            foreach (var sub in client.Subscriptions().List())
            {
                //sub.ListVirtualMachines("even").PowerOff
                await foreach (var vm in sub.ListVirtualMachinesAsync("even"))
                {
                       // client.ResourceOperations<VirtualMachine>(vm).PowerOff()
                       //vmOps.PowerOff(vm.Id)
                       Console.WriteLine($"Stopping {vm.Id.Subscription} {vm.Id.ResourceGroup} {vm.Id.Name}");
                       vm.PowerOff();
                    var newVm = vm.Get();
                       var model = vm.Data;
                       Console.WriteLine($"Starting {vm.Id.Subscription} {vm.Id.ResourceGroup} {vm.Id.Name}");
                       vm.PowerOn();
                }
            }
        }
    }
}
