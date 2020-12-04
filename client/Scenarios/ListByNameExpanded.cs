using azure_proto_compute;
using azure_proto_core;
using System;

namespace client
{
    class ListByNameExpanded : Scenario
    {
        public override async void Execute()
        {
            var createMultipleVms = new CreateMultipleVms(Context);
            createMultipleVms.Execute();

            var rg = new ArmClient().ResourceGroup(Context.SubscriptionId, Context.RgName).Get().Value;
            foreach (var vm in rg.VirtualMachines().ListByNameExpanded(Environment.UserName))
            {
                Console.WriteLine($"--------VM id--------: {vm.Id}");
            }
            await foreach (var vm in rg.VirtualMachines().ListByNameExpandedAsync(Environment.UserName))
            {
                Console.WriteLine($"--------VM id--------: {vm.Id}");
            }
        }
    }
}