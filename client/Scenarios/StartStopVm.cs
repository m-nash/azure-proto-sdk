using azure_proto_compute;
using azure_proto_management;
using System;

namespace client
{
    class StartStopVm : Scenario
    {
        public override void Execute()
        {
            var createVm = new CreateSingleVmExample(Context);
            createVm.Execute();

            AzureClient client = new AzureClient();
            var subscription = client.Subscriptions[Context.SubscriptionId];
            var resourceGroup = subscription.ResourceGroups[Context.RgName];
            var vm = resourceGroup.Vms()[Context.VmName];
            Console.WriteLine($"Found VM {Context.VmName}");
            Console.WriteLine("--------Stopping VM--------");
            vm.Stop();
            Console.WriteLine("--------Starting VM--------");
            vm.Start();
        }
    }
}
