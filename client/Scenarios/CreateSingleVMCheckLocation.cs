﻿using Azure.ResourceManager.Core;
using azure_proto_compute;
using azure_proto_network;
using System;
using System.Linq;

namespace client
{
    class CreateSingleVMCheckLocation : Scenario
    {
        public CreateSingleVMCheckLocation() : base() { }

        public CreateSingleVMCheckLocation(ScenarioContext context) : base(context) { }

        public override void Execute()
        {
            var client = new AzureResourceManagerClient();
            var subscription = client.GetSubscriptionOperations(Context.SubscriptionId);

            // Create Resource Group
            Console.WriteLine($"--------Start create group {Context.RgName}--------");
            var resourceGroup = subscription.GetResourceGroupContainer().Create(Context.RgName, Context.Loc).Value;
            CleanUp.Add(resourceGroup.Id);
            Console.WriteLine("\nResource Group List Available Locations: ");
            var loc = resourceGroup.ListAvailableLocations();
            foreach(var l in loc)
            {
                Console.WriteLine(l.DisplayName);
            }

            // Create AvailabilitySet
            Console.WriteLine("--------Start create AvailabilitySet--------");
            var aset = resourceGroup.GetAvailabilitySetContainer().Construct("Aligned").Create(Context.VmName + "_aSet").Value;
            Console.WriteLine("\nAvailability Set List Available Locations: ");
            loc = aset.ListAvailableLocations();
            foreach (var l in loc)
            {
                Console.WriteLine(l.DisplayName);
            }            

            // Create VNet
            Console.WriteLine("--------Start create VNet--------");
            string vnetName = Context.VmName + "_vnet";
            var vnet = resourceGroup.GetVirtualNetworkContainer().Construct("10.0.0.0/16").Create(vnetName).Value;
            Console.WriteLine("\nVirtual Network List Available Locations: ");
            loc = vnet.ListAvailableLocations();
            foreach (var l in loc)
            {
                Console.WriteLine(l.DisplayName);
            }

            //create subnet
            Console.WriteLine("--------Start create Subnet--------");
            var subnet = vnet.Subnets().Construct(Context.SubnetName, "10.0.0.0/24").Create(Context.SubnetName).Value;

            //create network security group
            Console.WriteLine("--------Start create NetworkSecurityGroup--------");
            var nsg = resourceGroup.GetNetworkSecurityGroupContainer().Construct(Context.NsgName, 80).Create(Context.NsgName).Value;
            Console.WriteLine("\nNetwork Security Group List Available Locations: ");
            loc = nsg.ListAvailableLocations();
            foreach (var l in loc)
            {
                Console.WriteLine(l.DisplayName);
            }

            // Create IP Address
            Console.WriteLine("--------Start create IP Address--------");
            var ipAddress = resourceGroup.GetPublicIpAddressContainer().Construct().Create($"{Context.VmName}_ip").Value;
            Console.WriteLine("\nPublicIP Address List Available Locations: ");
            loc = ipAddress.ListAvailableLocations();
            foreach (var l in loc)
            {
                Console.WriteLine(l.DisplayName);
            }

            // Create Network Interface
            Console.WriteLine("--------Start create Network Interface--------");
            var nic = resourceGroup.GetNetworkInterfaceContainer().Construct(ipAddress.Data, subnet.Id).Create($"{Context.VmName}_nic").Value;
            Console.WriteLine("\nNetwork Interface Container List Available Locations: ");
            loc = nic.ListAvailableLocations();
            foreach (var l in loc)
            {
                Console.WriteLine(l.DisplayName);
            }

            // Create VM
            Console.WriteLine("--------Start create VM--------");
            var vm = resourceGroup.GetVirtualMachineContainer().Construct(Context.VmName, "admin-user", "!@#$%asdfA", nic.Id, aset.Data).Create(Context.VmName).Value;
            Console.WriteLine("\nVirtual Machine List Available Locations: ");
            loc = vm.ListAvailableLocations();
            foreach (var l in loc)
            {
                Console.WriteLine(l.DisplayName);
            }

            Console.WriteLine("VM ID: " + vm.Id);
            Console.WriteLine("--------Done create VM--------");
        }
    }
}