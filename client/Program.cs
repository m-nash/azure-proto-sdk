using Azure.ResourceManager.Network.Models;
using azure_proto_compute;
using azure_proto_core;
using azure_proto_network;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace client
{
    class Program
    {
        private static string vmName = String.Format("{0}-quickstartvm", Environment.UserName);
        private static string rgName = String.Format("{0}-test-rg", Environment.UserName);
        private static string nsgName = String.Format("{0}-test-nsg", Environment.UserName);
        private static string subscriptionId = Environment.GetEnvironmentVariable("AZURE_SUBSCRIPTION_ID");
        private static string loc = "westus2";
        private static string subnetName = "mySubnet";
        private static ArmClient client = new ArmClient();

        static void Main(string[] args)
        {
            try
            {
                CreateSingleVmExample();
                CreateMultipleVmShutdownSome();
                StartStopVm();
                StartFromVm();
                SetTagsOnVm();
                CreateMultipleVmShutdownByTag();
                StartStopAllVmsAsync().Wait();
            }
            finally
            {
                CleanUp();
            }
        }

        private static void CleanUp()
        {
            Console.WriteLine($"--------Deleting {rgName}--------");
            var rg = client.Subscriptions(subscriptionId).ResourceGroup(rgName);
            rg.Delete();
        }

        private static void CreateMultipleVmShutdownByTag()
        {
            var rg = CreateMultipleVms();

            //set tags on random vms
            Random rand = new Random(Environment.TickCount);
            foreach(var vm in rg.ListVms())
            {
                if (rand.NextDouble() > 0.5)
                {
                    Console.WriteLine("adding tag to {0}", vm.Context.Name);
                    vm.AddTag("tagkey", "tagvalue");
                }
            }

            foreach(var vm in rg.ListVmsByTag(new azure_proto_core.Resources.ArmTagFilter("tagkey", "tagvalue")))
            {
                Console.WriteLine("--------Stopping VM {0}--------", vm.Context.Name);
                vm.Stop();
                Console.WriteLine("--------Starting VM {0}--------", vm.Context.Name);
                vm.Start();
            }
        }

        private static void SetTagsOnVm()
        {
            //make sure vm exists
            CreateSingleVmExample();
            var rg = client.Subscriptions(subscriptionId).ResourceGroup(rgName);
            rg.Vm(vmName).AddTag("tagkey", "tagvalue");
        }

        private static void StartFromVm()
        {
            // TODO: Look at VM nic/nsg operations on VM
            //make sure vm exists
            CreateSingleVmExample();

            //retrieve from lowest level, doesn't give ability to walk up and down the container structure
            var vm = client.Subscriptions(subscriptionId).ResourceGroup(rgName).Vm(vmName).Get().Value;
            Console.WriteLine("Found VM {0}", vm.Context);


            //retrieve from lowest level inside management package gives ability to walk up and down
            var rg = client.Subscriptions(subscriptionId).ResourceGroup(rgName);
            var vm2 = rg.Vm(vmName);
            Console.WriteLine("Found VM {0}", vm2.Context);
        }

        private static void StartStopVm()
        {
            ArmClient client = new ArmClient();
            var vm = client.Subscriptions(subscriptionId).ResourceGroup(rgName).Vm(vmName);
            Console.WriteLine("Found VM {0}", vmName);
            Console.WriteLine("--------Stopping VM--------");
            vm.Stop();
            Console.WriteLine("--------Starting VM--------");
            vm.Start();
        }

        private static async Task StartStopAllVmsAsync()
        {
            var client = new ArmClient();
            await foreach (var subscription in client.ListSubscriptionsAsync())
            {
                await foreach (var vm in subscription.ListVmsAsync(filter:"-", top:100))
                {
                    await vm.StartAsync();
                    await vm.StopAsync();
                }
            }
        }

        private static void CreateMultipleVmShutdownSome()
        {
            var resourceGroup = CreateMultipleVms();

            foreach (var vm in resourceGroup.ListVms("-"))
            {
                vm.Stop();
                vm.Start();
            }
        }

        private static ResourceGroupOperations CreateMultipleVms()
        {
            ResourceGroupOperations rgClient;
            PhAvailabilitySet aset;
            PhSubnet subnet;
            SetupVmHost(out rgClient, out aset, out subnet);
            for (int i = 0; i < 10; i++)
            {
                var nic = CreateNic(rgClient, subnet, i);

                // Create VM
                string name = String.Format("{0}-{1}-z", vmName, i);
                Console.WriteLine("--------Start create VM {0}--------", i);
                var vm = rgClient.ConstructVm(name, "admin-user", "!@#$%asdfA", nic.Context, aset);
                var result = rgClient.CreateVm(name, vm).Value;
            }

            return rgClient;
        }

        private static void CreateSingleVmExample()
        {
            ResourceGroupOperations rgClient;
            PhAvailabilitySet aset;
            PhSubnet subnet;
            SetupVmHost(out rgClient, out aset, out subnet);

            var nic = CreateNic(rgClient, subnet, 1000);

            // Create VM
            Console.WriteLine("--------Start create VM--------");
            var vm = rgClient.ConstructVm(vmName, "admin-user", "!@#$%asdfA", nic.Context, aset);
            var result = rgClient.CreateVm(vmName, vm).Value;

            Console.WriteLine("VM ID: " + vm.Id);
            Console.WriteLine("--------Done create VM--------");
        }

        private static ResourceOperations<PhNetworkInterface> CreateNic(ResourceGroupOperations resourceGroup, PhSubnet subnet, int i)
        {
            // Create IP Address
            Console.WriteLine("--------Start create IP Address--------");
            var address = resourceGroup.ConstructIPAddress();
            var response = resourceGroup.CreatePublicIp(String.Format("{0}_{1}_ip", vmName, i), address).Value;

            // Create Network Interface
            Console.WriteLine("--------Start create Network Interface--------");
            var nic = resourceGroup.ConstructNic(address, subnet.Id);
            return resourceGroup.CreateNic(String.Format("{0}_{1}_nic", vmName, i), nic).Value;
        }

        private static void SetupVmHost(out ResourceGroupOperations rgClient, out PhAvailabilitySet aset, out PhSubnet subnet)
        {
            ArmClient client = new ArmClient();
            var subscription = client.Subscriptions(subscriptionId);

            // Create Resource Group
            Console.WriteLine("--------Start create group--------");
            rgClient = subscription.CreateResourceGroup(rgName, loc).Value;

            // Create AvailabilitySet
            Console.WriteLine("--------Start create AvailabilitySet--------");
            aset = rgClient.ConstructAvailabilitySet("Aligned");
            var result = rgClient.CreateAvailabilitySet(vmName + "_aSet", aset).Value;

            // Create VNet
            Console.WriteLine("--------Start create VNet--------");
            string vnetName = vmName + "_vnet";
            var vnet = rgClient.ListVnets(vnetName).FirstOrDefault();
            PhVirtualNetwork vnetModel;
            if (vnet == null)
            {
               vnetModel = rgClient.ConstructVnet("10.0.0.0/16");
               vnet = rgClient.CreateVnet(vnetName, vnetModel).Value;
            }
                
            vnet.Get().Value.TryGetModel(out vnetModel);

            //create subnet
            Console.WriteLine("--------Start create Subnet--------");

            var sub = vnet.ListSubnets().FirstOrDefault( s => string.Equals(s.Context.Name, subnetName, StringComparison.InvariantCultureIgnoreCase));
            if (sub == null)
            {
                var nsgModel = rgClient.ConstructNsg(nsgName, 80);
                var nsg = rgClient.CreateNsg(nsgName, nsgModel).Value;
                var subnetModel = vnet.ConstructSubnet(subnetName, "10.0.0.0/24");
                sub  = vnet.CreateSubnet(subnetName, subnetModel).Value;
            }

            sub.Get().Value.TryGetModel(out subnet);
            
        }
    }
}
