using azure_proto_compute;
using azure_proto_core;
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
            }

            var subscription = new ArmClient().Subscription(Context.SubscriptionId);

            Regex reg = new Regex($"{Context.VmName}.*even");
            Parallel.ForEach(subscription.ListVms(), vm =>
            {
                if (reg.IsMatch(vm.Context.Name))
                {
                    Console.WriteLine($"Stopping {vm.Context.ResourceGroup} {vm.Context.Name}");
                    vm.Stop();
                    Console.WriteLine($"Starting {vm.Context.ResourceGroup} {vm.Context.Name}");
                    vm.Start();
                }
            });
        }
    }
}
