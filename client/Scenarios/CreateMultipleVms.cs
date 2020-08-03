using azure_proto_compute;
using azure_proto_management;
using azure_proto_network;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace client
{
    class CreateMultipleVms : Scenario
    {
        public CreateMultipleVms() : base() { }

        public CreateMultipleVms(ScenarioContext context) : base(context) { }

        public override void Execute()
        {
            AzureClient client = new AzureClient();
            var subscription = client.Subscriptions[Context.SubscriptionId];

            // Create Resource Group
            Console.WriteLine("--------Start create group--------");
            var resourceGroup = subscription.ResourceGroups.CreateOrUpdate(Context.RgName, Context.Loc);

            // Create AvailabilitySet
            Console.WriteLine("--------Start create AvailabilitySet--------");
            var aset = resourceGroup.AvailabilitySets().ConstructAvailabilitySet("Aligned");
            aset = resourceGroup.AvailabilitySets().CreateOrUpdateAvailabilityset(Context.VmName + "_aSet", aset);

            // Create VNet
            Console.WriteLine("--------Start create VNet--------");
            string vnetName = Context.VmName + "_vnet";
            var vnet = resourceGroup.VNets().ConstructVnet("10.0.0.0/16");
            vnet = resourceGroup.VNets().CreateOrUpdateVNet(vnetName, vnet);

            //create subnet
            Console.WriteLine("--------Start create Subnet--------");
            var nsg = resourceGroup.Nsgs().ConstructNsg(Context.NsgName, 80);
            nsg = resourceGroup.Nsgs().CreateOrUpdateNsgs(nsg);
            var subnet = vnet.Subnets.ConstructSubnet(Context.SubnetName, "10.0.0.0/24");
            subnet = vnet.Subnets.CreateOrUpdateSubnets(subnet);

            CreateVmsAsync(resourceGroup, aset, subnet).Wait();
        }

        private async Task CreateVmsAsync(AzureResourceGroup resourceGroup, AzureAvailabilitySet aset, AzureSubnet subnet)
        {
            List<Task<AzureVm>> tasks = new List<Task<AzureVm>>();
            for (int i = 0; i < 10; i++)
            {
                // Create IP Address
                Console.WriteLine("--------Start create IP Address--------");
                var ipAddress = resourceGroup.IpAddresses().ConstructIPAddress();
                ipAddress = resourceGroup.IpAddresses().CreateOrUpdatePublicIpAddress($"{Context.VmName}_{i}_ip", ipAddress);

                // Create Network Interface
                Console.WriteLine("--------Start create Network Interface--------");
                var nic = resourceGroup.Nics().ConstructNic(ipAddress, subnet.Id);
                nic = resourceGroup.Nics().CreateOrUpdateNic($"{Context.VmName}_{i}_nic", nic);

                // Create VM
                string name = $"{Context.VmName}-{i}-z";
                Console.WriteLine("--------Start create VM {0}--------", i);
                var vm = resourceGroup.Vms().ConstructVm(name, "admin-user", "!@#$%asdfA", nic.Id, aset);
                tasks.Add(resourceGroup.Vms().CreateOrUpdateVmAsync(name, vm));
            }

            foreach (var task in tasks)
            {
                var vm = await task;
                Console.WriteLine($"--------Finished creating VM {vm.Name}");
            }
        }
    }
}
