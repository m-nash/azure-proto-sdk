using azure_proto_compute;
using azure_proto_core;
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

            var resourceGroup = new ArmClient().ResourceGroup(Context.SubscriptionId, Context.RgName);

            resourceGroup.ListVms().Select(vm =>
            {
                var parts = vm.Context.Name.Split('-');
                var n = Convert.ToInt32(parts[parts.Length - 2]);
                return (vm, n);
            })
                .Where(tuple => tuple.n % 2 == 0)
                .ToList()
                .ForEach(tuple =>
                {
                    Console.WriteLine($"Stopping {tuple.vm.Context.Name}");
                    tuple.vm.Stop();
                    Console.WriteLine($"Starting {tuple.vm.Context.Name}");
                    tuple.vm.Start();
                });
        }
    }
}
