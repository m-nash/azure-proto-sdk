using azure_proto_compute;
using azure_proto_core;
using System;

namespace client
{
    class GenericEntityLoop : Scenario
    {
        public override void Execute()
        {
            var createVm = new CreateSingleVmExample(Context);
            createVm.Execute();

            var rgOp = new ArmClient().ResourceGroup(Context.SubscriptionId, Context.RgName);
            foreach(var entity in rgOp.VirtualMachines().List())
            {
                Console.WriteLine($"{entity.Id.Name}");
                entity.AddTag("name", "Value");
            }
        }
    }
}
