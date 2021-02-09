using Azure.ResourceManager.Core;
using Azure.ResourceManager.Network;
using azure_proto_compute;
using azure_proto_network;
using System;
using System.Linq;

namespace client
{
    class GetVMTaskExamples : Scenario
    {
        public GetVMTaskExamples() : base() { }

        public GetVMTaskExamples(ScenarioContext context) : base(context) { }

        public override void Execute()
        {
            var client = new AzureResourceManagerClient();
            //var subscription = client.GetSubscriptionOperations(Context.SubscriptionId);

            //// Create Resource Group
            //Console.WriteLine($"--------Start create group nbhatia-test--------");
            //var resourceGroup = subscription.GetResourceGroupContainer().Create("nbhatia-test", Context.Loc).Value;
            ////CleanUp.Add(resourceGroup.Id);

            //// Create AvailabilitySet
            //Console.WriteLine("--------Start create AvailabilitySet--------");
            //var aset = resourceGroup.GetAvailabilitySetContainer().Construct("Aligned").Create(Context.VmName + "_aSet").Value;

            //// Create VNet
            //Console.WriteLine("--------Start create VNet--------");
            //string vnetName = Context.VmName + "_vnet";
            //var vnet = resourceGroup.GetVirtualNetworkContainer().Construct("10.0.0.0/16").Create(vnetName).Value;

            ////create subnet
            //Console.WriteLine("--------Start create Subnet--------");
            //var subnet = vnet.Subnets().Construct(Context.SubnetName, "10.0.0.0/24").Create(Context.SubnetName).Value;
            //Console.WriteLine("SUBNET ID: " + subnet.Id);

            ////create network security group
            //Console.WriteLine("--------Start create NetworkSecurityGroup--------");
            //_ = resourceGroup.GetNetworkSecurityGroupContainer().Construct(Context.NsgName, 80).Create(Context.NsgName).Value;

            //// Create IP Address
            //Console.WriteLine("--------Start create IP Address--------");
            //var ipAddress = resourceGroup.GetPublicIpAddressContainer().Construct().Create($"{Context.VmName}_ip").Value;

            //// Create Network Interface
            //Console.WriteLine("--------Start create Network Interface--------");
            //var nic = resourceGroup.GetNetworkInterfaceContainer().Construct(ipAddress.Data, subnet.Id).Create($"{Context.VmName}_nic").Value;

            //// Create VM
            //Console.WriteLine("--------Start create VM--------");
            //var vm = resourceGroup.GetVirtualMachineContainer().Construct(Context.VmName, "admin-user", "!@#$%asdfA", nic.Id, aset.Data).Create(Context.VmName).Value;

            //Console.WriteLine("VM ID: " + vm.Id);
            string vmID = "/subscriptions/db1ab6f0-4769-4b27-930e-01e2ef9c123c/resourceGroups/nbhatia-test/providers/Microsoft.Compute/virtualMachines/nibhati";
            var vmResourceId = new ResourceIdentifier(vmID);
            string subnetID = "/subscriptions/db1ab6f0-4769-4b27-930e-01e2ef9c123c/resourceGroups/nbhatia-test/providers/Microsoft.Network/virtualNetworks/nibhati_vnet/subnets/nibhati-subnet";
            var subnetResourceId = new ResourceIdentifier(subnetID);

            // OPTION 1
            var vmOps = client.GetVirtualMachineOperations(vmResourceId);
            vmOps.PowerOff();
            Console.WriteLine("Option 1 vm is " + vmOps.Get().Value.Data.InstanceView.Statuses.Last().Code);
            vmOps.PowerOn();
            Console.WriteLine("Option 2 vm is " + vmOps.Get().Value.Data.InstanceView.Statuses.Last().Code);

            var subnetOps = client.GetSubnetOperations(subnetResourceId);
            Console.WriteLine("Option 1 subnet is " + subnetOps.Id);

            //// OPTION 2
            //vmOps = VirtualMachineOperations.FromId(vmResourceId, client);
            //vmOps.PowerOff();
            //Console.WriteLine("Option 2 vm is " + vmOps.Get().Value.Data.InstanceView.Statuses.Last().Code);
            //vmOps.PowerOn();
            //Console.WriteLine("Option 2 vm is " + vmOps.Get().Value.Data.InstanceView.Statuses.Last().Code);

            //subnetOps = SubnetOperations.FromId(subnetResourceId, client);
            //Console.WriteLine("Option 2 subnet is " + subnetOps.Id);

            //// OPTION 3
            //vmOps = client.GetResourceOperations<VirtualMachineOperations>(vmResourceId);
            //vmOps.PowerOff();
            //Console.WriteLine("Option 3 vm is " + vmOps.Get().Value.Data.InstanceView.Statuses.Last().Code);
            //vmOps.PowerOn();
            //Console.WriteLine("Option 3 vm is " + vmOps.Get().Value.Data.InstanceView.Statuses.Last().Code);

            subnetOps = client.GetResourceOperations<SubnetOperations>(subnetResourceId);
            Console.WriteLine("Option 3 subnet is " + subnetOps.Id);

            Console.WriteLine("--------Done create VM--------");
        }
    }
}
