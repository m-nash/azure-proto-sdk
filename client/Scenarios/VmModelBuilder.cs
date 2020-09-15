// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT License.

using System;
using System.Threading.Tasks;
using azure_proto_compute;
using azure_proto_core;
using azure_proto_network;

namespace client
{
    class VmModelBuilder : Scenario
    {
        public override void Execute()
        {
            throw new NotImplementedException();
        }

        private Task<VirtualMachineOperations> CreateVmWithBuilderAsync()
        {
            var client = new ArmClient();
            var subscription = client.Subscription(Context.SubscriptionId);

            // Create Resource Group
            Console.WriteLine($"--------Start create group {Context.RgName}--------");
            var resourceGroup = subscription.CreateResourceGroup(Context.RgName, Context.Loc).Value;

            // Create AvailabilitySet
            Console.WriteLine("--------Start create AvailabilitySet--------");
            var aset = resourceGroup.ConstructAvailabilitySet("Aligned").Create(Context.VmName + "_aSet").Value;

            // Create VNet
            Console.WriteLine("--------Start create VNet--------");
            string vnetName = Context.VmName + "_vnet";
            var vnet = resourceGroup.ConstructVnet("10.0.0.0/16").Create(vnetName).Value;

            //create subnet
            Console.WriteLine("--------Start create Subnet--------");
            var nsg = resourceGroup.ConstructNsg(Context.NsgName, 80).Create(Context.NsgName).Value;
            var subnet = vnet.ConstructSubnet(Context.SubnetName, "10.0.0.0/24").Create(Context.SubnetName).Value;

            // Create IP Address
            Console.WriteLine("--------Start create IP Address--------");
            var ipAddress = resourceGroup.ConstructIPAddress().Create($"{Context.VmName}_ip").Value;

            // Create Network Interface
            Console.WriteLine("--------Start create Network Interface--------");
            var nic = resourceGroup.ConstructNic(ipAddress.GetModelIfNewer(), subnet.Id).Create($"{Context.VmName}_nic").Value;

            // TODO:
            // 0. Builder is an convenience feature. Simpler model would just use new xxx()
            // 1. Wish we can do compile time check for required properties. And now, ToModel() will do validation. 
            // 2. Is there a risk that the referenced model has not been created in ARM yet resource id is populated?

            // Options: required parameters on in the constructor
            var vm = resourceGroup.ConstructVirtualMachine(Context.VmName, Context.Loc)
                .UseWindowsImage("admin-user", "!@#$%asdfA")
                .RequiredNetworkInterface(nic.Id)
                .RequiredAvalabilitySet(aset.Id)
                .Create(Context.VmName).Value;

            return Task.FromResult(vm as VirtualMachineOperations);
        }
    }
}
