using azure_proto_management;
using System;

namespace client
{
    class Program
    {
        private static string vmName = String.Format("{0}-quickstartvm", Environment.UserName);
        private static string rgName = String.Format("{0}-test-rg", Environment.UserName);
        private static string subscriptionId = Environment.GetEnvironmentVariable("AZURE_SUBSCRIPTION_ID");
        private static string loc = "westus2";

        static void Main(string[] args)
        {
            CreateSingleVmExample();
            //CreateMultipleVmShutdownSome();
            //StartStopVm();
        }

        private static void StartStopVm()
        {
            AzureClient client = new AzureClient();
            var subscription = client.Subscriptions[subscriptionId];
            var location = subscription.Locations[loc];
            var resourceGroup = location.ResourceGroups[rgName];
            var vm = resourceGroup.Vms[vmName];
            Console.WriteLine("Found VM {0}", vmName);
            Console.WriteLine("--------Stopping VM--------");
            vm.Stop();
            Console.WriteLine("--------Starting VM--------");
            vm.Start();
        }

        private static void CreateMultipleVmShutdownSome()
        {
            throw new NotImplementedException();
        }

        private static void CreateSingleVmExample()
        {
            AzureClient client = new AzureClient();
            var subscription = client.Subscriptions[subscriptionId];

            // Set Location
            var location = subscription.Locations[loc];

            // Create Resource Group
            Console.WriteLine("--------Start create group--------");
            var resourceGroup = location.ResourceGroups.CreateOrUpdate(rgName);

            // Create AvailabilitySet
            Console.WriteLine("--------Start create AvailabilitySet--------");
            var aset = resourceGroup.ConstructAvailabilitySet("Aligned");
            aset = resourceGroup.AvailabilitySets.CreateOrUpdateAvailabilityset(vmName + "_aSet", aset);

            // Create IP Address
            Console.WriteLine("--------Start create IP Address--------");
            var ipAddress = resourceGroup.ConstructIPAddress();
            ipAddress = resourceGroup.IpAddresses.CreateOrUpdatePublicIpAddress(vmName + "_ip", ipAddress);

            // Create VNet
            Console.WriteLine("--------Start create VNet--------");
            var vnet = resourceGroup.ConstructVnet("10.0.0.0/16");
            vnet = resourceGroup.VNets.CreateOrUpdateVNet(vmName + "_vnet", vnet);

            //create subnet
            Console.WriteLine("--------Start create Subnet--------");
            var subnet = vnet.ConstructSubnet("mySubnet", "10.0.0.0/24");
            subnet = vnet.Subnets.CreateOrUpdateSubnets(subnet);

            // Create Network Interface
            Console.WriteLine("--------Start create Network Interface--------");
            var nic = resourceGroup.ConstructNic(ipAddress, subnet.Model.Id);
            nic = resourceGroup.Nics.CreateOrUpdateNic(vmName + "_nic", nic);

            // Create VM
            Console.WriteLine("--------Start create VM--------");
            var vm = resourceGroup.ConstructVm(vmName, "admin-user", "!@#$%asdfA", nic, aset);
            vm = resourceGroup.Vms.CreateOrUpdateVm(vmName, vm);

            Console.WriteLine("VM ID: " + vm.Id);
            Console.WriteLine("--------Done create VM--------");
        }
    }
}
