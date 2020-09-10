using azure_proto_compute;
using azure_proto_core;
using System;

namespace client
{
    class SetTagsOnVm : Scenario
    {
        public override void Execute()
        {
            var createVm = new CreateSingleVmExample(Context);
            createVm.Execute();

            var rg = new ArmClient().ResourceGroup(Context.SubscriptionId, Context.RgName);
            var vm = rg.VirtualMachine(Context.VmName);

            Console.WriteLine($"Adding tags to {vm.GetModelIfNewer().Name}");
            vm.AddTag("tagkey", "tagvalue");
        }
    }
}
