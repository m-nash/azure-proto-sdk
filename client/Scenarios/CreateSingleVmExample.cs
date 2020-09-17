using azure_proto_compute;
using azure_proto_core;
using azure_proto_network;
using System;

namespace client
{
    class CreateSingleVmExample : Scenario
    {
        public CreateSingleVmExample() : base() { }

        public CreateSingleVmExample(ScenarioContext context) : base(context) { }

        public override void Execute()
        {
            var client = new ArmClient();
            var subscription = client.Subscription(Context.SubscriptionId);

            // Create Resource Group
            Console.WriteLine($"--------Start create group {Context.RgName}--------");
            var resourceGroup = subscription.CreateResourceGroup(Context.RgName, Context.Loc).Value;
            CleanUp.Add(resourceGroup.Id);

            // Create AvailabilitySet
            Console.WriteLine("--------Start create AvailabilitySet--------");
            var aset = resourceGroup.ConstructAvailabilitySet("Aligned").Create(Context.VmName + "_aSet").Value;

            // Create VNet
            Console.WriteLine("--------Start create VNet--------");
            string vnetName = Context.VmName + "_vnet";
            var vnet = resourceGroup.ConstructVirtualNetwork("10.0.0.0/16").Create(vnetName).Value;

            //create subnet
            Console.WriteLine("--------Start create Subnet--------");
            var nsg = resourceGroup.ConstructNetworkSecurityGroup(Context.NsgName, 80).Create(Context.NsgName).Value;
            var subnet = vnet.ConstructSubnet(Context.SubnetName, "10.0.0.0/24").Create(Context.SubnetName).Value;

            // Create IP Address
            Console.WriteLine("--------Start create IP Address--------");
            var ipAddress = resourceGroup.ConstructIPAddress().Create($"{Context.VmName}_ip").Value;

            // Create Network Interface
            Console.WriteLine("--------Start create Network Interface--------");
            var nic = resourceGroup.ConstructNetworkInterface(ipAddress.GetModelIfNewer(), subnet.Id).Create($"{Context.VmName}_nic").Value;

            // Create VM
            Console.WriteLine("--------Start create VM--------");
            var vm = resourceGroup.ConstructVirtualMachine(Context.VmName, "admin-user", "!@#$%asdfA", nic.Id, aset.GetModelIfNewer()).Create(Context.VmName).Value;

            Console.WriteLine("VM ID: " + vm.Id);
            Console.WriteLine("--------Done create VM--------");
        }
    }
}
