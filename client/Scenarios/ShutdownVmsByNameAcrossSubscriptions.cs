using Azure.ResourceManager.Compute;
using azure_proto_compute;
using azure_proto_core;
using System;
using System.Threading.Tasks;

namespace client
{
    class ShutdownVmsByNameAcrossSubscriptions : Scenario
    {
        public async void ShutdownAsync()
        {
            var client = new ArmClient();

            await foreach (var subscription in client.ListSubscriptionsAsync())
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
            foreach (var sub in client.ListSubscriptions())
            {
                //sub.ListVirtualMachines("even").PowerOff
                await foreach (var vm in sub.ListVirtualMachinesAsync("even"))
                {
                       // client.ResourceOperations<VirtualMachine>(vm).PowerOff()
                       //vmOps.PowerOff(vm.Id)
                       Console.WriteLine($"Stopping {vm.Id.Subscription} {vm.Id.ResourceGroup} {vm.Id.Name}");
                       vm.PowerOff();
                    var newVm = vm.Get();
                       var model = vm.Model;
                       Console.WriteLine($"Starting {vm.Id.Subscription} {vm.Id.ResourceGroup} {vm.Id.Name}");
                       vm.PowerOn();
                }
            }
        }
    }
}
