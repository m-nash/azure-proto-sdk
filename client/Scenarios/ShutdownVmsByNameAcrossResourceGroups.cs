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
            Parallel.ForEach(subscription.ListVirtualMachines(), vm =>
            {
                if (reg.IsMatch(vm.Id.Name))
                {
                    Console.WriteLine($"Stopping {vm.Id.ResourceGroup} {vm.Id.Name}");
                    vm.PowerOff();
                    Console.WriteLine($"Starting {vm.Id.ResourceGroup} {vm.Id.Name}");
                    vm.PowerOn();
                }
            });
        }
    }
}
