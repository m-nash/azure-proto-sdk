using azure_proto_compute;
using Azure.ResourceManager.Core;
using System;

namespace client
{
    class SetTagsOnVm : Scenario
    {
        public override void Execute()
        {
            var createVm = new CreateSingleVmExample(Context);
            createVm.Execute();

            var rgOp = new AzureResourceManagerClient().ResourceGroup(Context.SubscriptionId, Context.RgName);
            var vmOp = rgOp.VirtualMachine(Context.VmName);

            var vm = vmOp.Get().Value;
            Console.WriteLine($"Adding tags to {vm.Data.Name}");
            vm.StartAddTag("tagkey", "tagvalue");
        }
    }
}
