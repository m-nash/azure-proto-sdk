using azure_proto_compute;
using azure_proto_management;
using System;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace client
{
    class ShutdownVmsByNameAcrossSubscriptions : Scenario
    {
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

            var client = new AzureClient();

            Regex reg = new Regex($"{Context.VmName}.*even");
            Parallel.ForEach(client.Vms(), vm =>
            {
                if (reg.IsMatch(vm.Name))
                {
                    Console.WriteLine($"Stopping {vm.Id.Subscription} {vm.Id.ResourceGroup} {vm.Name}");
                    vm.Stop();
                    Console.WriteLine($"Starting {vm.Id.Subscription} {vm.Id.ResourceGroup} {vm.Name}");
                    vm.Start();
                }
            });
        }
    }
}
