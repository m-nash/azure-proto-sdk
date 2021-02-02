using azure_proto_compute;
using Azure.ResourceManager.Core;
using System;

namespace client
{
    class GenericEntityLoop : Scenario
    {
        public override System.Threading.Tasks.Task Execute()
        {
            var createVm = new CreateSingleVmExample(Context);
            createVm.Execute();

            var rgOp = new AzureResourceManagerClient().GetResourceGroupOperations(Context.SubscriptionId, Context.RgName);
            foreach(var entity in rgOp.GetVirtualMachineContainer().List())
            {
                Console.WriteLine($"{entity.Id.Name}");
                entity.StartAddTag("name", "Value");
            }
            return System.Threading.Tasks.Task.FromResult<object>(null);
        }
    }
}
