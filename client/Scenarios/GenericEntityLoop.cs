using Azure.ResourceManager.Compute.Models;
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

            var rg = new ArmClient().ResourceGroup(Context.SubscriptionId, Context.RgName);
            foreach(var entity in rg.ListResource<PhVirtualMachine>())
            {
                Console.WriteLine($"{entity.Context.Name}");
                entity.AddTag("name", "Value");
            }
        }
    }
}
