using azure_proto_compute;
using azure_proto_management;
using System;

namespace client
{
    class SetTagsOnVm : Scenario
    {
        public override void Execute()
        {
            var createVm = new CreateSingleVmExample(Context);
            createVm.Execute();

            AzureResourceGroup rg = AzureClient.GetResourceGroup(Context.SubscriptionId, Context.RgName);
            AzureVm vm = rg.Vms()[Context.VmName];

            Console.WriteLine($"Adding tags to {vm.Name}");
            vm.AddTag("tagkey", "tagvalue");
        }
    }
}
