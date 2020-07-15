using azure_proto_compute;
using azure_proto_management;
using azure_proto_network;
using System;
using System.Linq;

namespace client
{
    class Program
    {
        private static string vmName = String.Format("{0}-quickstartvm", Environment.UserName);
        private static string rgName = String.Format("{0}-test-rg", Environment.UserName);
        private static string subscriptionId = Environment.GetEnvironmentVariable("AZURE_SUBSCRIPTION_ID");
        private static string loc = "westus2";
        private static string subnetName = "mySubnet";

        static void Main(string[] args)
        {
            CreateSingleVmExample();
            //CreateMultipleVmShutdownSome();
            //StartStopVm();
            //StartFromVm();
        }

        private static void StartFromVm()
        {

        }

        private static void StartStopVm()
        {
            AzureClient client = new AzureClient();
            var subscription = client.Subscriptions[subscriptionId];
            var location = subscription.Locations[loc]; //intended to be removed
            var resourceGroup = location.ResourceGroups[rgName];
            //var vm = resourceGroup.Vms[vmName]; //create shortcut constructor
            var vm = resourceGroup.Vms()[vmName];
            Console.WriteLine("Found VM {0}", vmName);
            Console.WriteLine("--------Stopping VM--------");
            vm.Stop();
            Console.WriteLine("--------Starting VM--------");
            vm.Start();
        }

        private static void CreateMultipleVmShutdownSome()
        {
            AzureResourceGroup resourceGroup;
            AzureAvailabilitySet aset;
            AzureNic nic;
            SetupVmHost(out resourceGroup, out aset, out nic);

            for (int i = 0; i < 10; i++)
            {
                // Create VM
                string name = String.Format("{0}-{1}-z", vmName, i);
                Console.WriteLine("--------Start create VM {0}--------", i);
                var vm = resourceGroup.ConstructVm(name, "admin-user", "!@#$%asdfA", nic, aset);
                vm = resourceGroup.Vms().CreateOrUpdateVm(name, vm);
            }

            resourceGroup.Vms().Select(pair =>
            {
                var parts = pair.Value.Name.Split('-');
                return (pair, Convert.ToInt32(parts[parts.Length - 2]));
            })
                .Where(n => n.Item2 % 2 == 0)
                .ToList()
                .ForEach(pair => pair.pair.Value.Stop());
        }

        private static void CreateSingleVmExample()
        {
            AzureResourceGroup resourceGroup;
            AzureAvailabilitySet aset;
            AzureNic nic;
            SetupVmHost(out resourceGroup, out aset, out nic);

            // Create VM
            Console.WriteLine("--------Start create VM--------");
            var vm = resourceGroup.ConstructVm(vmName, "admin-user", "!@#$%asdfA", nic, aset);
            vm = resourceGroup.Vms().CreateOrUpdateVm(vmName, vm);

            Console.WriteLine("VM ID: " + vm.Id);
            Console.WriteLine("--------Done create VM--------");
        }

        private static void SetupVmHost(out AzureResourceGroup resourceGroup, out AzureAvailabilitySet aset, out AzureNic nic)
        {
            AzureClient client = new AzureClient();
            var subscription = client.Subscriptions[subscriptionId];

            // Set Location
            var location = subscription.Locations[loc];

            // Create Resource Group
            Console.WriteLine("--------Start create group--------");
            resourceGroup = location.ResourceGroups.CreateOrUpdate(rgName);

            // Create AvailabilitySet
            Console.WriteLine("--------Start create AvailabilitySet--------");
            aset = resourceGroup.ConstructAvailabilitySet("Aligned");
            aset = resourceGroup.AvailabilitySets().CreateOrUpdateAvailabilityset(vmName + "_aSet", aset);

            // Create IP Address
            Console.WriteLine("--------Start create IP Address--------");
            var ipAddress = resourceGroup.ConstructIPAddress();
            ipAddress = resourceGroup.IpAddresses().CreateOrUpdatePublicIpAddress(vmName + "_ip", ipAddress);

            // Create VNet
            Console.WriteLine("--------Start create VNet--------");
            string vnetName = vmName + "_vnet";
            var vnet = resourceGroup.VNets()[vnetName];
            if (vnet == null)
            {
                vnet = resourceGroup.ConstructVnet("10.0.0.0/16");
                vnet = resourceGroup.VNets().CreateOrUpdateVNet(vmName + "_vnet", vnet);
            }

            //create subnet
            Console.WriteLine("--------Start create Subnet--------");
            var subnet = vnet.Subnets[subnetName];
            if (subnet == null)
            {
                subnet = vnet.ConstructSubnet(subnetName, "10.0.0.0/24");
                subnet = vnet.Subnets.CreateOrUpdateSubnets(subnet);
            }

            // Create Network Interface
            Console.WriteLine("--------Start create Network Interface--------");
            nic = resourceGroup.ConstructNic(ipAddress, subnet.Model.Id);
            nic = resourceGroup.Nics().CreateOrUpdateNic(vmName + "_nic", nic);
        }
    }
}
