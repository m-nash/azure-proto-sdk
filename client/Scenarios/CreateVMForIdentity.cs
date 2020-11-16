using azure_proto_compute;
using azure_proto_core;
using azure_proto_network;
using System;

namespace client
{
    class CreateVMForIdentity : Scenario
    {
        public CreateVMForIdentity() : base() { }

        public CreateVMForIdentity(ScenarioContext context) : base(context) { }

        public override void Execute()
        {
            var client = new ArmClient();
            var subscription = client.Subscription(Context.SubscriptionId);

            // Create Resource Group
            Console.WriteLine($"--------Start create group IDENTITY TEST {Context.RgName}--------");

            // Create Resource Group
            //Console.WriteLine($"--------Start create group {"nbhatia-rg"}--------");
            //var resourceGroup = subscription.ResourceGroups().Create("nbhatia-rg", "East US").Value;
            //CleanUp.Add(resourceGroup.Id);            

            //// Create AvailabilitySet
            //Console.WriteLine("--------Start create AvailabilitySet--------");
            //var aset = resourceGroup.AvailabilitySets().Construct("Aligned").Create(Context.VmName + "_aSet").Value;

            //// Create VNet
            //Console.WriteLine("--------Start create VNet--------");
            //string vnetName = Context.VmName + "_vnet";
            //var vnet = resourceGroup.VirtualNetworks().Construct("10.0.0.0/16").Create(vnetName).Value;

            ////create subnet
            //Console.WriteLine("--------Start create Subnet--------");
            //var subnet = vnet.Subnets().Construct(Context.SubnetName, "10.0.0.0/24").Create(Context.SubnetName).Value;

            ////create network security group
            //Console.WriteLine("--------Start create NetworkSecurityGroup--------");
            //_ = resourceGroup.NetworkSecurityGroups().Construct(Context.NsgName, 80).Create(Context.NsgName).Value;

            //// Create IP Address
            //Console.WriteLine("--------Start create IP Address--------");
            //var ipAddress = resourceGroup.PublicIpAddresses().Construct().Create($"{Context.VmName}_ip").Value;

            //// Create Network Interface
            //Console.WriteLine("--------Start create Network Interface--------");
            //var nic = resourceGroup.NetworkInterfaces().Construct(ipAddress.GetModelIfNewer(), subnet.Id).Create($"{Context.VmName}_nic").Value;

            //// Create VM
            //Console.WriteLine("--------Start create VM--------");
            //var vm = resourceGroup.VirtualMachines().Construct(Context.VmName, "admin-user", "!@#$%asdfA", nic.Id, aset.GetModelIfNewer()).Create(Context.VmName).Value;

            //Console.WriteLine("VM ID: " + vm.Id);
            //Console.WriteLine("VM Name: " + Context.VmName);
            //Console.WriteLine("--------Done create VM--------");
            var resourceGroup = subscription.ResourceGroup("nbhatia-rg");
            var vmValue = resourceGroup.VirtualMachine("myVM2").Get().Value;
            Console.WriteLine("VM Value: " + vmValue);
            Console.WriteLine("VM Id: " + vmValue.Model.Id);
            var vmIdentity = vmValue.Model.Identity;
            Console.WriteLine("VM Identity: " + vmIdentity);
            //Console.WriteLine("VM Identity Type: " + vmIdentity.Type);
        }
    }
}
