using azure_proto_compute;
using Azure.ResourceManager.Core;
using System;
using System.Linq;

namespace client
{
    class ShutdownVmsByLINQ : Scenario
    {
        public override void Execute()
        {
            var createMultipleVms = new CreateMultipleVms(Context);
            createMultipleVms.Execute();

            var client = new ArmClient();
            foreach (var sub in client.Subscriptions().List())
            {
                foreach (var vm in sub.ListVirtualMachines("mc").Where(vm => vm.Data.Name.Contains("foo")))
                {
                    vm.PowerOff();
                }
            }

            var resourceGroup = new ArmClient().ResourceGroup(Context.SubscriptionId, Context.RgName);

            resourceGroup.VirtualMachines().List().Select(vm =>
            {
                var parts = vm.Id.Name.Split('-');
                var n = Convert.ToInt32(parts[parts.Length - 2]);
                return (vm, n);
            })
                .Where(tuple => tuple.n % 2 == 0)
                .ToList()
                .ForEach(tuple =>
                {
                    Console.WriteLine($"Stopping {tuple.vm.Id.Name}");
                    tuple.vm.PowerOff();
                    Console.WriteLine($"Starting {tuple.vm.Id.Name}");
                    tuple.vm.PowerOn();
                });
        }
    }
}
