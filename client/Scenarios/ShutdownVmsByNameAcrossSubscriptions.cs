using azure_proto_compute;
using azure_proto_core;
using System;
using System.Text.RegularExpressions;
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
                await foreach (var vm in subscription.ListVmsAsync("even"))
                {
                    await vm.StopAsync();
                    await vm.StartAsync();
                }
            }
        }

        public override void Execute()
        {
            #region SETUP
            ScenarioContext[] contexts = new ScenarioContext[] { new ScenarioContext(), new ScenarioContext("c9cbd920-c00c-427c-852b-8aaf38badaeb") };
            //TODO: there is a concurency issue in "new DefaultAzureCredential()" that needs to get investigated
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

            Regex reg = new Regex($"{Context.VmName}.*even");

            Parallel.ForEach(client.ListSubscriptions(), sub =>
           {
               Parallel.ForEach(sub.ListVms(), vm =>
               {
                   if (reg.IsMatch(vm.Context.Name))
                   {
                       Console.WriteLine($"Stopping {vm.Context.Subscription} {vm.Context.ResourceGroup} {vm.Context.Name}");
                       vm.Stop();
                       Console.WriteLine($"Starting {vm.Context.Subscription} {vm.Context.ResourceGroup} {vm.Context.Name}");
                       vm.Start();
                   }
               });
           });          
        }
    }
}
