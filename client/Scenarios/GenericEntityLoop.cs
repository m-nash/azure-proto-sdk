using azure_proto_management;
using System;

namespace client
{
    class GenericEntityLoop : Scenario
    {
        public override void Execute()
        {
            var createVm = new CreateSingleVmExample(Context);
            createVm.Execute();

            AzureResourceGroup rg = AzureClient.GetResourceGroup(Context.SubscriptionId, Context.RgName);
            foreach(var entity in rg.Entities)
            {
                Console.WriteLine($"{entity.Name}");
            }
        }
    }
}
