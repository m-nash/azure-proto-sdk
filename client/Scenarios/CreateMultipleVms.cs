using azure_proto_compute;
using azure_proto_core;
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
            var client = new ArmClient();
            var subscription = client.Subscriptions(Context.SubscriptionId);

            // Create Resource Group
            Console.WriteLine($"--------Start create group {Context.RgName}--------");
            var resourceGroup = subscription.CreateResourceGroup(Context.RgName, Context.Loc).Value;
            CleanUp.Add(resourceGroup.Context);

            // Create AvailabilitySet
            Console.WriteLine("--------Start create AvailabilitySet--------");
            var aset = resourceGroup.ConstructAvailabilitySet("Aligned").Create(Context.VmName + "_aSet").Value as AvailabilitySetOperations;

            // Create VNet
            Console.WriteLine("--------Start create VNet--------");
            string vnetName = Context.VmName + "_vnet";
            var vnet = resourceGroup.ConstructVnet("10.0.0.0/16").Create(vnetName).Value as VnetOperations;

            //create subnet
            Console.WriteLine("--------Start create Subnet--------");
            var nsg = resourceGroup.ConstructNsg(Context.NsgName, 80).Create().Value;
            var subnet = vnet.ConstructSubnet(Context.SubnetName, "10.0.0.0/24").Create().Value as SubnetOperations;

            CreateVmsAsync(resourceGroup, aset, subnet).Wait();
        }

        private async Task CreateVmsAsync(ResourceGroupOperations resourceGroup, AvailabilitySetOperations aset, SubnetOperations subnet)
        {
            List<Task<VmOperations>> tasks = new List<Task<VmOperations>>();
            for (int i = 0; i < 10; i++)
            {
                // Create IP Address
                Console.WriteLine("--------Start create IP Address--------");
                var ipAddress = resourceGroup.ConstructIPAddress().Create($"{Context.VmName}_{i}_ip").Value;

                // Create Network Interface
                Console.WriteLine("--------Start create Network Interface--------");
                var nic = resourceGroup.ConstructNic(ipAddress.SafeGet(), subnet.Context).Create($"{Context.VmName}_{i}_nic");

                // Create VM
                string num = i % 2 == 0 ? "even" : "odd";
                string name = $"{Context.VmName}-{i}-{num}";
                Console.WriteLine("--------Start create VM {0}--------", i);
                var vm = resourceGroup.ConstructVm(name, "admin-user", "!@#$%asdfA", nic.Id, aset.SafeGet()).Create(name).Value;
            }

            foreach (var task in tasks)
            {
                var vm = await task;
                Console.WriteLine($"--------Finished creating VM {vm.Context.Name}");
            }
        }
    }
}
