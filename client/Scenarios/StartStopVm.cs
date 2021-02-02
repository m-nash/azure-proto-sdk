using azure_proto_compute;
using Azure.ResourceManager.Core;
using System;

namespace client
{
    class StartStopVm : Scenario
    {
        public override System.Threading.Tasks.Task Execute()
        {
            var createVm = new CreateSingleVmExample(Context);
            createVm.Execute();

            var client = new AzureResourceManagerClient();
            var subscription = client.GetSubscriptionOperations(Context.SubscriptionId);
            var resourceGroup = subscription.GetResourceGroupOperations(Context.RgName);
            var vm = resourceGroup.GetVirtualMachineOperations(Context.VmName);
            Console.WriteLine($"Found VM {Context.VmName}");
            Console.WriteLine("--------Stopping VM--------");
            vm.PowerOff();
            Console.WriteLine("--------Starting VM--------");
            vm.PowerOn();

            return System.Threading.Tasks.Task.FromResult<object>(null);
        }
    }
}
