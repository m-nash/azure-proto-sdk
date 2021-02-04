using Azure.ResourceManager.Core;
using azure_proto_compute;
using azure_proto_network;
using System;

namespace client
{
    class CreateSingleVmExampleAsync : Scenario
    {
        public CreateSingleVmExampleAsync() : base() { }

        public CreateSingleVmExampleAsync(ScenarioContext context) : base(context) { }

        public override void Execute()
        {
            ExcuteAsync().ConfigureAwait(false).GetAwaiter().GetResult();
        }

        private async System.Threading.Tasks.Task ExcuteAsync()
        {
                var client = new AzureResourceManagerClient();
            var subscription = client.GetSubscriptionOperations(Context.SubscriptionId);

            // Create Resource Group
            Console.WriteLine($"--------Start create group async {Context.RgName}--------");
            var resourceGroup = (await subscription.GetResourceGroupContainer().Construct(Context.Loc).CreateAsync(Context.RgName)).Value;
            CleanUp.Add(resourceGroup.Id);

            // Create AvailabilitySet
            Console.WriteLine("--------Start create AvailabilitySet async--------");
            var aset = (await resourceGroup.GetAvailabilitySetContainer().Construct("Aligned").CreateAsync(Context.VmName + "_aSet")).Value;

            // Create VNet
            Console.WriteLine("--------Start create VNet async--------");
            string vnetName = Context.VmName + "_vnet";
            var vnet = (await resourceGroup.GetVirtualNetworkContainer().Construct("10.0.0.0/16").CreateAsync(vnetName)).Value;

            //create subnet
            Console.WriteLine("--------Start create Subnet async--------");
            var subnet = (await vnet.GetSubnetContainer().Construct("10.0.0.0/24").CreateAsync(Context.SubnetName)).Value;

            //create network security group
            Console.WriteLine("--------Start create NetworkSecurityGroup async--------");
            _ = (await resourceGroup.GetNetworkSecurityGroupContainer().Construct(80).CreateAsync(Context.NsgName)).Value;

            // Create IP Address
            Console.WriteLine("--------Start create IP Address async--------");
            var ipAddress = (await resourceGroup.GetPublicIpAddressContainer().Construct().CreateAsync($"{Context.VmName}_ip")).Value;

            // Create Network Interface
            Console.WriteLine("--------Start create Network Interface async--------");
            var nic = (await resourceGroup.GetNetworkInterfaceContainer().Construct(ipAddress.Data, subnet.Id).CreateAsync($"{Context.VmName}_nic")).Value;

            // Create VM
            Console.WriteLine("--------Start create VM async--------");
            var vm = (await resourceGroup.GetVirtualMachineContainer().Construct(Context.Hostname, "admin-user", "!@#$%asdfA", nic.Id, aset.Data).CreateAsync(Context.VmName)).Value;

            Console.WriteLine("VM ID: " + vm.Id);
            Console.WriteLine("--------Done create VM--------");
        }
    }
}
