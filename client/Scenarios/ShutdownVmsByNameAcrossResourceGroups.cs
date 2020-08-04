using azure_proto_compute;
using azure_proto_management;
using System;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace client
{
    class ShutdownVmsByNameAcrossResourceGroups : Scenario
    {
        public override void Execute()
        {
            int numberOfRgs = 2;
            var context = Context;
            for (int i = 0; i < numberOfRgs; i++)
            {
                var createMultipleVms = new CreateMultipleVms(context);
                createMultipleVms.Execute();
                context = new ScenarioContext();
                if (i < numberOfRgs - 1)
                    CleanUp.Add(context.RgName);
            }

            var subscription = AzureClient.GetSubscription(Context.SubscriptionId);

            Regex reg = new Regex("even");
            Parallel.ForEach(subscription.Vms(), vm =>
            {
                if (reg.IsMatch(vm.Name))
                {
                    Console.WriteLine($"Stopping {vm.Parent.Name} {vm.Name}");
                    vm.Stop();
                    Console.WriteLine($"Starting {vm.Parent.Name} {vm.Name}");
                    vm.Start();
                }
            });
        }
    }
}
