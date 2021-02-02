﻿using Azure.ResourceManager.Core;
using azure_proto_compute;
using azure_proto_network;
using System;

namespace client
{
    class StartCreateSingleVmExample : Scenario
    {
        public StartCreateSingleVmExample() : base() { }

        public StartCreateSingleVmExample(ScenarioContext context) : base(context) { }

        public override async System.Threading.Tasks.Task Execute()
        {
            var client = new AzureResourceManagerClient();
            var subscription = client.GetSubscriptionOperations(Context.SubscriptionId);

            // Create Resource Group
            Console.WriteLine($"--------Start StartCreate group {Context.RgName}--------");
            var resourceGroup = (await (subscription.GetResourceGroupContainer().Construct(Context.Loc).StartCreate(Context.RgName)).WaitForCompletionAsync()).Value;
            CleanUp.Add(resourceGroup.Id);

            // Create AvailabilitySet
            Console.WriteLine("--------Start StartCreate AvailabilitySet async--------");
            var aset = (await (resourceGroup.GetAvailabilitySetContainer().Construct("Aligned").StartCreate(Context.VmName + "_aSet")).WaitForCompletionAsync()).Value;

            // Create VNet
            Console.WriteLine("--------Start StartCreate VNet--------");
            string vnetName = Context.VmName + "_vnet";
            var vnet = (await (resourceGroup.GetVirtualNetworkContainer().Construct("10.0.0.0/16").StartCreate(vnetName)).WaitForCompletionAsync()).Value;

            //create subnet
            Console.WriteLine("--------Start StartCreate Subnet--------");
            var subnet = (await (vnet.GetSubnetContainer().Construct(Context.SubnetName, "10.0.0.0/24").StartCreate(Context.SubnetName)).WaitForCompletionAsync()).Value;

            //create network security group
            Console.WriteLine("--------Start StartCreate NetworkSecurityGroup--------");
            _ = (await (resourceGroup.GetNetworkSecurityGroupContainer().Construct(Context.NsgName, 80).StartCreate(Context.NsgName)).WaitForCompletionAsync()).Value;

            // Create IP Address
            Console.WriteLine("--------Start StartCreate IP Address--------");
            var ipAddress = (await (resourceGroup.GetPublicIpAddressContainer().Construct().StartCreate($"{Context.VmName}_ip")).WaitForCompletionAsync()).Value;

            // Create Network Interface
            Console.WriteLine("--------Start StartCreate Network Interface--------");
            var nic = (await (resourceGroup.GetNetworkInterfaceContainer().Construct(ipAddress.Data, subnet.Id).StartCreate($"{Context.VmName}_nic")).WaitForCompletionAsync()).Value;

            // Create VM
            Console.WriteLine("--------Start StartCreate VM --------");
            var vm = (await (resourceGroup.GetVirtualMachineContainer().Construct(Context.VmName, "admin-user", "!@#$%asdfA", nic.Id, aset.Data).StartCreate(Context.VmName)).WaitForCompletionAsync()).Value;

            Console.WriteLine("VM ID: " + vm.Id);
            Console.WriteLine("--------Done StartCreate VM--------");
        }
    }
}
