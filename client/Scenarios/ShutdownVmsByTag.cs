using azure_proto_compute;
using azure_proto_core;
using System;

namespace client
{
    class ShutdownVmsByTag : Scenario
    {
        public override void Execute()
        {
            var createMultipleVms = new CreateMultipleVms(Context);
            createMultipleVms.Execute();

            var rg = new ArmClient().ResourceGroup(Context.SubscriptionId, Context.RgName);

            //set tags on random vms
            Random rand = new Random(Environment.TickCount);
            foreach (var vm in rg.ListVms())
            {
                if (rand.NextDouble() > 0.5)
                {
                    Console.WriteLine("adding tag to {0}", vm.Context.Name);
                    vm.AddTag("tagkey", "tagvalue");
                }
            }

            foreach (var vm in rg.ListVmsByTag(new azure_proto_core.Resources.ArmTagFilter("tagkey", "tagvalue")))
            {
                Console.WriteLine("--------Stopping VM {0}--------", vm.Context.Name);
                vm.Stop();
                Console.WriteLine("--------Starting VM {0}--------", vm.Context.Name);
                vm.Start();
            }
        }
    }
}
