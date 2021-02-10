using Azure.ResourceManager.Core;
using azure_proto_compute;
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
            var client = new AzureResourceManagerClient();
            var subscription = client.GetSubscriptionOperations(Context.SubscriptionId);

            // Create Resource Group
            Console.WriteLine($"--------Start create group {Context.RgName}--------");
            var resourceGroup = subscription.GetResourceGroupContainer().Construct(Context.Loc).Create(Context.RgName).Value;
            CleanUp.Add(resourceGroup.Id);

            // Create AvailabilitySet
            Console.WriteLine("--------Start create AvailabilitySet--------");
            var aset = resourceGroup.GetAvailabilitySetContainer().Construct("Aligned").Create(Context.VmName + "_aSet").Value;

            // Create VNet
            Console.WriteLine("--------Start create VNet--------");
            string vnetName = Context.VmName + "_vnet";
            var vnet = resourceGroup.GetVirtualNetworkContainer().Construct("10.0.0.0/16").Create(vnetName).Value;

            //create subnet
            Console.WriteLine("--------Start create Subnet async--------");
            var subnet = vnet.GetSubnetContainer().Construct("10.0.0.0/24").CreateAsync(Context.SubnetName).ConfigureAwait(false).GetAwaiter().GetResult().Value;

            //create network security group
            Console.WriteLine("--------Start create NetworkSecurityGroup--------");
            _ = resourceGroup.GetNetworkSecurityGroupContainer().Construct(80).Create(Context.NsgName).Value;

            // Create IP Address
            Console.WriteLine("--------Start create IP Address--------");
            var ipAddress = resourceGroup.GetPublicIpAddressContainer().Construct().Create($"{Context.VmName}_ip").Value;

            // Create Network Interface
            Console.WriteLine("--------Start create Network Interface--------");
            var nic = resourceGroup.GetNetworkInterfaceContainer().Construct(ipAddress.Data, subnet.Id).Create($"{Context.VmName}_nic").Value;

            // Create VM
            Console.WriteLine("--------Start create VM--------");
            var vm = resourceGroup.GetVirtualMachineContainer().Construct(Context.Hostname, "admin-user", "!@#$%asdfA", nic.Id, aset.Id).Create(Context.VmName).Value;

            Console.WriteLine("VM ID: " + vm.Id);
            Console.WriteLine("--------Done create VM--------");
            
        }
    }
}
