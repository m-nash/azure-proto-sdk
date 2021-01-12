using azure_proto_compute;
using Azure.ResourceManager.Core;
using System;

namespace client
{
    class GenericEntityLoop : Scenario
    {
        public override void Execute()
        {
            var createVm = new CreateSingleVmExample(Context);
            createVm.Execute();

            var rgOp = new AzureResourceManagerClient().ResourceGroup(Context.SubscriptionId, Context.RgName);
            foreach(var entity in rgOp.VirtualMachines().List())
            {
                Console.WriteLine($"{entity.Id.Name}");
                entity.StartAddTag("name", "Value");
            }
        }
    }
}
