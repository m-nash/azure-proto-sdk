using azure_proto_compute;
using azure_proto_core;
using azure_proto_network;
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
                Console.WriteLine($"--------AvailabilitySet id--------: {availabilitySet.Data.Id}");
            }

            foreach (var vm in rg.VirtualMachines().ListByName(Environment.UserName))
            {
                Console.WriteLine($"--------VM operation id--------: {vm.Id}");
            }

            foreach (var vm in rg.VirtualMachines().ListByNameExpanded(Environment.UserName))
            {
                Console.WriteLine($"--------VM id--------: {vm.Data.Id}");
            }

            foreach (var networkInterface in rg.NetworkInterfaces().ListByName(Environment.UserName))
            {
                Console.WriteLine($"--------NetworkInterface operation id--------: {networkInterface.Id}");
            }

            foreach (var networkInterface in rg.NetworkInterfaces().ListByNameExpanded(Environment.UserName))
            {
                Console.WriteLine($"--------NetworkInterface id--------: {networkInterface.Data.Id}");
            }

            foreach (var networkSecurityGroup in rg.NetworkSecurityGroups().ListByName(Environment.UserName))
            {
                Console.WriteLine($"--------NetworkSecurityGroup operation id--------: {networkSecurityGroup.Id}");
            }

            foreach (var networkSecurityGroup in rg.NetworkSecurityGroups().ListByNameExpanded(Environment.UserName))
            {
                Console.WriteLine($"--------NetworkSecurityGroup id--------: {networkSecurityGroup.Data.Id}");
            }

            foreach (var publicIpAddress in rg.PublicIpAddresses().ListByName(Environment.UserName))
            {
                Console.WriteLine($"--------PublicIpAddress operation id--------: {publicIpAddress.Id}");
            }

            foreach (var publicIpAddress in rg.NetworkSecurityGroups().ListByNameExpanded(Environment.UserName))
            {
                Console.WriteLine($"--------PublicIpAddress id--------: {publicIpAddress.Data.Id}");
            }

            foreach (var VNet in rg.VirtualNetworks().ListByName(Environment.UserName))
            {
                Console.WriteLine($"--------VNet operation id--------: {VNet.Id}");
            }

            foreach (var VNet in rg.VirtualNetworks().ListByNameExpanded(Environment.UserName))
            {
                Console.WriteLine($"--------VNet id--------: {VNet.Data.Id}");
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
                Console.WriteLine($"--------AvailabilitySet id--------: {availabilitySet.Data.Id}");
            }

            await foreach (var vm in rg.VirtualMachines().ListByNameAsync(Environment.UserName))
            {
                Console.WriteLine($"--------VM operation id--------: {vm.Id}");
            }

            await foreach (var vm in rg.VirtualMachines().ListByNameExpandedAsync(Environment.UserName))
            {
                Console.WriteLine($"--------VM id--------: {vm.Data.Id}");
            }

            await foreach (var networkInterface in rg.NetworkInterfaces().ListByNameAsync(Environment.UserName))
            {
                Console.WriteLine($"--------NetworkInterface operation id--------: {networkInterface.Id}");
            }

            await foreach (var networkInterface in rg.NetworkInterfaces().ListByNameExpandedAsync(Environment.UserName))
            {
                Console.WriteLine($"--------NetworkInterface id--------: {networkInterface.Data.Id}");
            }

            await foreach (var networkSecurityGroup in rg.NetworkSecurityGroups().ListByNameAsync(Environment.UserName))
            {
                Console.WriteLine($"--------NetworkSecurityGroup operation id--------: {networkSecurityGroup.Id}");
            }

            await foreach (var networkSecurityGroup in rg.NetworkSecurityGroups().ListByNameExpandedAsync(Environment.UserName))
            {
                Console.WriteLine($"--------NetworkSecurityGroup id--------: {networkSecurityGroup.Data.Id}");
            }

            await foreach (var publicIpAddress in rg.PublicIpAddresses().ListByNameAsync(Environment.UserName))
            {
                Console.WriteLine($"--------PublicIpAddress operation id--------: {publicIpAddress.Id}");
            }

            await foreach (var publicIpAddress in rg.NetworkSecurityGroups().ListByNameExpandedAsync(Environment.UserName))
            {
                Console.WriteLine($"--------PublicIpAddress id--------: {publicIpAddress.Data.Id}");
            }

            await foreach (var VNet in rg.VirtualNetworks().ListByNameAsync(Environment.UserName))
            {
                Console.WriteLine($"--------VNet operation id--------: {VNet.Id}");
            }

            await foreach (var VNet in rg.VirtualNetworks().ListByNameExpandedAsync(Environment.UserName))
            {
                Console.WriteLine($"--------VNet id--------: {VNet.Data.Id}");
            }
        }
    }
}
