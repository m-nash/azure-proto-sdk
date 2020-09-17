using azure_proto_compute;
using azure_proto_core;
using System;

namespace client
{
    class StartStopVm : Scenario
    {
        public override void Execute()
        {
            var createVm = new CreateSingleVmExample(Context);
            createVm.Execute();

            var client = new ArmClient();
            var subscription = client.Subscription(Context.SubscriptionId);
            var resourceGroup = subscription.ResourceGroup(Context.RgName);
            var vm = resourceGroup.VirtualMachine(Context.VmName);
            Console.WriteLine($"Found VM {Context.VmName}");
            Console.WriteLine("--------Stopping VM--------");
            vm.PowerOff();
            Console.WriteLine("--------Starting VM--------");
            vm.PowerOn();
        }
    }
}
