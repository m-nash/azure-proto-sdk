using azure_proto_compute;
using azure_proto_core;
using System;
using System.Threading.Tasks;

namespace client
{
    class ListByNameExpanded : Scenario
    {
        public override void Execute()
        {
            var createMultipleVms = new CreateMultipleVms(Context);
            createMultipleVms.Execute();

            var rg = new ArmClient().ResourceGroup(Context.SubscriptionId, Context.RgName).Get().Value;
            foreach (var availabilitySet in rg.AvailabilitySets().ListByName(Environment.UserName))
            {
                Console.WriteLine($"--------AvailabilitySet operation id--------: {availabilitySet.Id}");
            }
            foreach (var availabilitySet in rg.AvailabilitySets().ListByNameExpanded(Environment.UserName))
            {
                Console.WriteLine($"--------AvailabilitySet id--------: {availabilitySet.Model.Id}");
            }
            foreach (var vm in rg.VirtualMachines().ListByName(Environment.UserName))
            {
                Console.WriteLine($"--------VM operation id--------: {vm.Id}");
            }
            foreach (var vm in rg.VirtualMachines().ListByNameExpanded(Environment.UserName))
            {
                Console.WriteLine($"--------VM id--------: {vm.Model.Id}");
            }
            ExecuteAsync(rg).GetAwaiter().GetResult();
        }

        private async Task ExecuteAsync(ResourceGroup rg)
        {
            await foreach (var availabilitySet in rg.AvailabilitySets().ListByNameAsync(Environment.UserName))
            {
                Console.WriteLine($"--------AvailabilitySet operation id--------: {availabilitySet.Id}");
            }
            await foreach (var availabilitySet in rg.AvailabilitySets().ListByNameExpandedAsync(Environment.UserName))
            {
                Console.WriteLine($"--------AvailabilitySet id--------: {availabilitySet.Model.Id}");
            }
            await foreach (var vm in rg.VirtualMachines().ListByNameAsync(Environment.UserName))
            {
                Console.WriteLine($"--------VM operation id--------: {vm.Id}");
            }
            await foreach (var vm in rg.VirtualMachines().ListByNameExpandedAsync(Environment.UserName))
            {
                Console.WriteLine($"--------VM id--------: {vm.Model.Id}");
            }
        }
    }
}