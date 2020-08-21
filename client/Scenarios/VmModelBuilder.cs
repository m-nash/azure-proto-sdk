// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT License.

using System;
using System.Threading.Tasks;
using azure_proto_compute;
using azure_proto_management;
using azure_proto_network;

namespace client
{
    class VmModelBuilder : Scenario
    {
        public override void Execute()
        {
            throw new NotImplementedException();
        }

        private Task<AzureVm> CreateVmWithBuilderAsync()
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

            // TODO:
            // 0. Builder is an convenience feature. Simpler model would just use new xxx()
            // 1. Wish we can do compile time check for required properties. And now, ToModel() will do validation. 
            // 2. Is there a risk that the referenced model has not been created in ARM yet resource id is populated?

            // Options: required parameters on in the constructor
            var vmModel = AzureVm.ModelBuilder(Context.VmName, Context.Loc)
                .UseWindowsImage("admin-user", "!@#$%asdfA")
                .RequiredNetworkInterface(nic.Id)
                .RequiredAvalabilitySet(aset.Id)
                .ToModel();

            var vm = resourceGroup.Vms().CreateOrUpdateVm(Context.VmName, vmModel);

            return Task.FromResult(vm);
        }
    }
}
