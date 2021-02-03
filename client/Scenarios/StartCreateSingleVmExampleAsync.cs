using Azure.ResourceManager.Core;
using azure_proto_compute;
using azure_proto_network;
using System;

namespace client
{
    class StartCreateSingleVmExampleAsync : Scenario
    {
        public StartCreateSingleVmExampleAsync() : base() { }

        public StartCreateSingleVmExampleAsync(ScenarioContext context) : base(context) { }

        public override void Execute()
        {
            ExecuteAsync().ConfigureAwait(false).GetAwaiter().GetResult();
        }
        private async System.Threading.Tasks.Task ExecuteAsync()
        {
            var client = new AzureResourceManagerClient();
            var subscription = client.GetSubscriptionOperations(Context.SubscriptionId);

            // Create Resource Group
            Console.WriteLine($"--------Start StartCreate group async {Context.RgName}--------");
            var resourceGroup = (await (await subscription.GetResourceGroupContainer().Construct(Context.Loc).StartCreateAsync(Context.RgName)).WaitForCompletionAsync()).Value;
            CleanUp.Add(resourceGroup.Id);

            // Create AvailabilitySet
            Console.WriteLine("--------Start StartCreate AvailabilitySet async--------");
            var aset = (await (await resourceGroup.GetAvailabilitySetContainer().Construct("Aligned").StartCreateAsync(Context.VmName + "_aSet")).WaitForCompletionAsync()).Value;

            // Create VNet
            Console.WriteLine("--------Start StartCreate VNet async--------");
            string vnetName = Context.VmName + "_vnet";
            var vnet = (await (await resourceGroup.GetVirtualNetworkContainer().Construct("10.0.0.0/16").StartCreateAsync(vnetName)).WaitForCompletionAsync()).Value;

            //create subnet
            Console.WriteLine("--------Start StartCreate Subnet async--------");
            var subnet = (await (await vnet.GetSubnetContainer().Construct("10.0.0.0/24").StartCreateAsync(Context.SubnetName)).WaitForCompletionAsync()).Value;

            //create network security group
            Console.WriteLine("--------Start StartCreate NetworkSecurityGroup async--------");
            _ = (await (await resourceGroup.GetNetworkSecurityGroupContainer().Construct(80).StartCreateAsync(Context.NsgName)).WaitForCompletionAsync()).Value;

            // Create IP Address
            Console.WriteLine("--------Start StartCreate IP Address async--------");
            var ipAddress = (await (await resourceGroup.GetPublicIpAddressContainer().Construct().StartCreateAsync($"{Context.VmName}_ip")).WaitForCompletionAsync()).Value;

            // Create Network Interface
            Console.WriteLine("--------Start StartCreate Network Interface async--------");
            var nic = (await (await resourceGroup.GetNetworkInterfaceContainer().Construct(ipAddress.Data, subnet.Id).StartCreateAsync($"{Context.VmName}_nic")).WaitForCompletionAsync()).Value;

            // Create VM
            Console.WriteLine("--------Start StartCreate VM async--------");
            var vm = (await (await resourceGroup.GetVirtualMachineContainer().Construct(Context.VmName, "admin-user", "!@#$%asdfA", nic.Id, aset.Data).StartCreateAsync(Context.VmName)).WaitForCompletionAsync()).Value;

            Console.WriteLine("VM ID: " + vm.Id);
            Console.WriteLine("--------Done StartCreate VM--------");
        }
    }
}
