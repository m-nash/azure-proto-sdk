using azure_proto_compute;
using Azure.ResourceManager.Core;
using azure_proto_network;
using System;
using System.Collections.Generic;

namespace client
{
    class CreateMultipleVms : Scenario
    {
        public CreateMultipleVms() : base() { }

        public CreateMultipleVms(ScenarioContext context) : base(context) { }

        public override void Execute()
        {
            var client = new AzureResourceManagerClient();
            var subscription = client.Subscription(Context.SubscriptionId);

            // Create Resource Group
            Console.WriteLine($"--------Start create group {Context.RgName}--------");
            var resourceGroup = subscription.ResourceGroups().Create(Context.RgName, Context.Loc).Value;
            CleanUp.Add(resourceGroup.Id);

            // Create AvailabilitySet
            Console.WriteLine("--------Start create AvailabilitySet--------");
            var aset = resourceGroup.AvailabilitySets().Construct("Aligned").Create(Context.VmName + "_aSet").Value;

            // Create VNet
            Console.WriteLine("--------Start create VNet--------");
            string vnetName = Context.VmName + "_vnet";
            var vnet = resourceGroup.VirtualNetworks().Construct("10.0.0.0/16").Create(vnetName).Value;

            //create subnet
            Console.WriteLine("--------Start create Subnet--------");
            var subnet = vnet.Subnets().Construct(Context.SubnetName, "10.0.0.0/24").Create(Context.SubnetName).Value;

            //create network security group
            Console.WriteLine("--------Start create NetworkSecurityGroup--------");
            _ = resourceGroup.NetworkSecurityGroups().Construct(Context.NsgName, 80).Create(Context.NsgName).Value;

            CreateVms(resourceGroup, aset, subnet);
        }

        private void CreateVms(ResourceGroup resourceGroup, AvailabilitySet aset, SubnetOperations subnet)
        {
            List<ArmOperation<VirtualMachine>> operations = new List<ArmOperation<VirtualMachine>>();
            for (int i = 0; i < 10; i++)
            {
                // Create IP Address
                Console.WriteLine("--------Start create IP Address--------");
                var ipAddress = resourceGroup.PublicIpAddresses().Construct().Create($"{Context.VmName}_{i}_ip").Value;

                // Create Network Interface
                Console.WriteLine("--------Start create Network Interface--------");
                var nic = resourceGroup.NetworkInterfaces().Construct(ipAddress.Data, subnet.Id).Create($"{Context.VmName}_{i}_nic").Value;

                // Create VM
                string num = i % 2 == 0 ? "-e" : "-o";
                string name = $"{Context.VmName}{i}{num}";
                Console.WriteLine("--------Start create VM {0}--------", i);
                var vmOp = resourceGroup.VirtualMachines().Construct(name, "admin-user", "!@#$%asdfA", nic.Id, aset.Data).StartCreate(name);
                operations.Add(vmOp);
            }

            foreach (var operation in operations)
            {
                var vm = operation.WaitForCompletionAsync().ConfigureAwait(false).GetAwaiter().GetResult().Value;
                Console.WriteLine($"--------Finished creating VM {vm.Id.Name}");
            }
        }
    }
}
