using azure_proto_compute;
using azure_proto_management;
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
            AzureClient client = new AzureClient();
            var subscription = client.Subscriptions[Context.SubscriptionId];

            // Create Resource Group
            Console.WriteLine($"--------Start create group {Context.RgName}--------");
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

            // Create IP Address
            Console.WriteLine("--------Start create IP Address--------");
            var ipAddress = resourceGroup.IpAddresses().ConstructIPAddress();
            ipAddress = resourceGroup.IpAddresses().CreateOrUpdatePublicIpAddress($"{Context.VmName}_ip", ipAddress);

            // Create Network Interface
            Console.WriteLine("--------Start create Network Interface--------");
            var nic = resourceGroup.Nics().ConstructNic(ipAddress, subnet.Id);
            nic = resourceGroup.Nics().CreateOrUpdateNic($"{Context.VmName}_nic", nic);

            // Create VM
            Console.WriteLine("--------Start create VM--------");
            var vm = resourceGroup.Vms().ConstructVm(Context.VmName, "admin-user", "!@#$%asdfA", nic.Id, aset);
            vm = resourceGroup.Vms().CreateOrUpdateVm(Context.VmName, vm);

            Console.WriteLine("VM ID: " + vm.Id);
            Console.WriteLine("--------Done create VM--------");
        }
    }
}
