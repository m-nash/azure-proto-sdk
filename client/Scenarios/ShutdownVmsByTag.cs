using azure_proto_compute;
using azure_proto_management;
using System;

namespace client
{
    class ShutdownVmsByTag : Scenario
    {
        public override void Execute()
        {
            var createMultipleVms = new CreateMultipleVms(Context);
            createMultipleVms.Execute();

            var rg = AzureClient.GetResourceGroup(Context.SubscriptionId, Context.RgName);

            //set tags on random vms
            Random rand = new Random(Environment.TickCount);
            foreach (var vm in rg.Vms())
            {
                if (rand.NextDouble() > 0.5)
                {
                    Console.WriteLine("adding tag to {0}", vm.Name);
                    vm.AddTag("tagkey", "tagvalue");
                }
            }

            foreach (var vm in rg.Vms().GetItemsByTag("tagkey", "tagvalue"))
            {
                Console.WriteLine("--------Stopping VM {0}--------", vm.Name);
                vm.Stop();
                Console.WriteLine("--------Starting VM {0}--------", vm.Name);
                vm.Start();
            }
        }
    }
}
