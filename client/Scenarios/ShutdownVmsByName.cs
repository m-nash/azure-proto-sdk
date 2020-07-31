using azure_proto_compute;
using azure_proto_management;
using System;
using System.Linq;

namespace client
{
    class ShutdownVmsByName: Scenario
    {
        public override void Execute()
        {
            var createMultipleVms = new CreateMultipleVms(Context);
            createMultipleVms.Execute();

            var resourceGroup = AzureClient.GetResourceGroup(Context.SubscriptionId, Context.RgName);

            resourceGroup.Vms().Select(vm =>
            {
                var parts = vm.Name.Split('-');
                var n = Convert.ToInt32(parts[parts.Length - 2]);
                return (vm, n);
            })
                .Where(tuple => tuple.n % 2 == 0)
                .ToList()
                .ForEach(tuple =>
                {
                    Console.WriteLine($"Stopping {tuple.vm.Name}");
                    tuple.vm.Stop();
                    Console.WriteLine($"Starting {tuple.vm.Name}");
                    tuple.vm.Start();
                });
        }
    }
}
