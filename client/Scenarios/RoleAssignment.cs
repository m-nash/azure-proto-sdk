using Azure.ResourceManager.Core;
using azure_proto_authorization;
using azure_proto_compute;
using azure_proto_network;
using System;

namespace client
{
    class RoleAssignment : Scenario
    {
        public override void Execute()
        {
            var client = new AzureResourceManagerClient();
            var subscription = client.GetSubscriptionOperations(Context.SubscriptionId);

            // Create Resource Group
            Console.WriteLine($"--------Start create group {Context.RgName}--------");
            var resourceGroup = subscription.GetResourceGroupContainer().Construct(Context.Loc).Create(Context.RgName).Value;
            CleanUp.Add(resourceGroup.Id);

            Console.WriteLine("--------Start create Assignment--------");
            var input = new RoleAssignmentCreateParameters($"/subscriptions/{Context.SubscriptionId}/resourceGroups/{Context.RgName}/providers/Microsoft.Authorization/roleDefinitions/{Context.RoleId}", Context.PrincipalId);
            var assign = resourceGroup.GetRoleAssignmentContainer().Create(Guid.NewGuid().ToString(), input).Value;
            Console.WriteLine("--------Done create Assignment--------");

            assign = assign.Get().Value;

            // Create AvailabilitySet
            Console.WriteLine("--------Start create AvailabilitySet--------");
            var aset = resourceGroup.GetAvailabilitySetContainer().Construct("Aligned").Create(Context.VmName + "_aSet").Value;

            // Create VNet
            Console.WriteLine("--------Start create VNet--------");
            string vnetName = Context.VmName + "_vnet";
            var vnet = resourceGroup.GetVirtualNetworkContainer().Construct("10.0.0.0/16").Create(vnetName).Value;

            //create subnet
            Console.WriteLine("--------Start create Subnet--------");
            var subnet = vnet.GetSubnetContainer().Construct(Context.SubnetName, "10.0.0.0/24").Create(Context.SubnetName).Value;

            //create network security group
            Console.WriteLine("--------Start create NetworkSecurityGroup--------");
            _ = resourceGroup.GetNetworkSecurityGroupContainer().Construct(Context.NsgName, 80).Create(Context.NsgName).Value;

            // Create IP Address
            Console.WriteLine("--------Start create IP Address--------");
            var ipAddress = resourceGroup.GetPublicIpAddressContainer().Construct().Create($"{Context.VmName}_ip").Value;

            // Create Network Interface
            Console.WriteLine("--------Start create Network Interface--------");
            var nic = resourceGroup.GetNetworkInterfaceContainer().Construct(ipAddress.Data, subnet.Id).Create($"{Context.VmName}_nic").Value;

            // Create VM
            Console.WriteLine("--------Start create VM--------");
            var vm = resourceGroup.GetVirtualMachineContainer().Construct(Context.VmName, "admin-user", "!@#$%asdfA", nic.Id, aset.Data).Create(Context.VmName).Value;

            Console.WriteLine("VM ID: " + vm.Id);
            Console.WriteLine("--------Done create VM--------");


            Console.WriteLine("--------Start create Assignment--------");
            var input2 = new RoleAssignmentCreateParameters($"{vm.Id}/providers/Microsoft.Authorization/roleDefinitions/{Context.RoleId}", Context.PrincipalId);
            var assign2 = vm.GetRoleAssignmentContainer().Create(Guid.NewGuid().ToString(), input2).Value;
            Console.WriteLine("--------Done create Assignment--------");

            assign2 = assign2.Get().Value;
            Console.WriteLine($"Created assignment: '{assign.Data.Id}'");
        }
    }
}
