using azure_proto_compute;
using azure_proto_core;
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
        private static string nsgName = String.Format("{0}-test-nsg", Environment.UserName);
        private static string subscriptionId = Environment.GetEnvironmentVariable("AZURE_SUBSCRIPTION_ID");
        private static string loc = "westus2";
        private static string subnetName = "mySubnet";
        private static ArmClient client = new ArmClient();

        static void Main(string[] args)
        {
            try
            {
                //CreateSingleVmExample();
                //CreateMultipleVmShutdownSome();
                //StartStopVm();
                //StartFromVm();
                SetTagsOnVm();
                //CreateMultipleVmShutdownByTag();
            }
            finally
            {
                CleanUp();
            }
        }

        private static void CleanUp()
        {
            Console.WriteLine($"--------Deleting {rgName}--------");
            var rg = client.Subscriptions.ResourceGroups(subscriptionId).ResourceGroup(rgName);
            /*client.ListSubscriptions()
            client.Subscriptions(subscriptionId).ListResourceGroups();
            client.Subscriptions(subscriptionId).ResourceGroups(rgName)

            client.Subscriptions(options) 
            client.ListSubscriptions()
            client.Subscriptions("foo", options)
            client.Vms(filter, options) //uses arm
            client.Vms(a, b, options) //uses vm client
            client.GetVm(subscriptionId, rgName, vmName, options))
            client.Subscriptions.GetResourceGroup(subscription, rgName).Get();*/
            rg.Delete();
        }

        private static void CreateMultipleVmShutdownByTag()
        {
            var rg = CreateMultipleVms();

            //set tags on random vms
            Random rand = new Random(Environment.TickCount);
            foreach(var vm in rg.Vms().List())
            {
                if (rand.NextDouble() > 0.5)
                {
                    Console.WriteLine("adding tag to {0}", vm.Name);
                    rg.Vm(vm).AddTag("tagkey", "tagvalue");
                }
            }

            foreach(var vm in rg.Vms().ListByTag(new azure_proto_core.Resources.ArmTagFilter("tagkey", "tagvalue")))
            {
                Console.WriteLine("--------Stopping VM {0}--------", vm.Name);
                rg.Vm(vm).Stop();
                Console.WriteLine("--------Starting VM {0}--------", vm.Name);
                rg.Vm(vm).Start();
            }
        }

        private static void SetTagsOnVm()
        {
            //make sure vm exists
            CreateSingleVmExample();

            client.Subscription(sub).ResourceGroup(rgName)
                client.ListSubscriptions()
                client.Suscription(subscriptionId).ListResourceGroups()
            var rg = client.Subscriptions.ResourceGroups(subscriptionId).ResourceGroup(rgName);
            rg.Vm(vmName).AddTag("tagkey", "tagvalue");
        }

        private static void StartFromVm()
        {
            // TODO: Look at VM nic/nsg operations on VM
            //make sure vm exists
            CreateSingleVmExample();

            //retrieve from lowest level, doesn't give ability to walk up and down the container structure
            var vm = client.Subscriptions.ResourceGroups(subscriptionId).ResourceGroup(rgName).Vm(vmName).Get().Value;
            Console.WriteLine("Found VM {0}", vm.Id);


            //retrieve from lowest level inside management package gives ability to walk up and down
            var rg = client.Subscriptions.ResourceGroups(subscriptionId).ResourceGroup(rgName);
            var vm2 = rg.Vm(vmName);
            Console.WriteLine("Found VM {0}", vm2.Context);
        }

        private static void StartStopVm()
        {
            ArmClient client = new ArmClient();
            var subscription = client.Subscriptions.ResourceGroups(subscriptionId);
            var resourceGroup = subscription.ResourceGroup(rgName);
            var vm = resourceGroup.Vms().Vm(vmName);
            Console.WriteLine("Found VM {0}", vmName);
            Console.WriteLine("--------Stopping VM--------");
            vm.Stop();
            Console.WriteLine("--------Starting VM--------");
            vm.Start();
        }

        private static void CreateMultipleVmShutdownSome()
        {
            var resourceGroup = CreateMultipleVms();

            foreach (var vm in resourceGroup.Vms().List("-"))
            {
                resourceGroup.Vm(vm).Stop();
                resourceGroup.Vm(vm).Start();
            }
        }

        private static ResourceGroupOperations CreateMultipleVms()
        {
            PhResourceGroup resourceGroup;
            PhAvailabilitySet aset;
            PhSubnet subnet;
            SetupVmHost(out resourceGroup, out aset, out subnet);
            var rgClient = client.ResourceGroup(resourceGroup);
            for (int i = 0; i < 10; i++)
            {
                var nic = CreateNic(rgClient, subnet, i);

                // Create VM
                string name = String.Format("{0}-{1}-z", vmName, i);
                Console.WriteLine("--------Start create VM {0}--------", i);
                var vm = rgClient.Vms().ConstructVm(name, "admin-user", "!@#$%asdfA", nic.Id, aset);
                vm = rgClient.Vms().Create(name, vm).Value;
            }

            return rgClient;
        }

        private static void CreateSingleVmExample()
        {
            PhResourceGroup resourceGroup;
            PhAvailabilitySet aset;
            PhSubnet subnet;
            SetupVmHost(out resourceGroup, out aset, out subnet);
            var rgClient = client.ResourceGroup(resourceGroup);

            var nic = CreateNic(client.ResourceGroup(resourceGroup), subnet, 1000);

            // Create VM
            Console.WriteLine("--------Start create VM--------");
            var vm = rgClient.Vms().ConstructVm(vmName, "admin-user", "!@#$%asdfA", nic.Id, aset);
            vm = rgClient.Vms().Create(vmName, vm).Value;

            Console.WriteLine("VM ID: " + vm.Id);
            Console.WriteLine("--------Done create VM--------");
        }

        private static PhNetworkInterface CreateNic(ResourceGroupOperations resourceGroup, PhSubnet subnet, int i)
        {
            // Create IP Address
            Console.WriteLine("--------Start create IP Address--------");
            var ipAddress = resourceGroup.PublicIps().ConstructIPAddress();
            ipAddress = resourceGroup.PublicIps().Create(String.Format("{0}_{1}_ip", vmName, i), ipAddress).Value;

            // Create Network Interface
            Console.WriteLine("--------Start create Network Interface--------");
            var nic = resourceGroup.Nics().ConstructNic(ipAddress, subnet.Id);
            nic = resourceGroup.Nics().Create(String.Format("{0}_{1}_nic", vmName, i), nic).Value;
            return nic;
        }

        private static void SetupVmHost(out PhResourceGroup resourceGroup, out PhAvailabilitySet aset, out PhSubnet subnet)
        {
            ArmClient client = new ArmClient();
            var subscription = client.Subscriptions.ResourceGroups(subscriptionId);

            // Create Resource Group
            Console.WriteLine("--------Start create group--------");
            resourceGroup = subscription.Create(rgName, loc);
            var rgClient = client.ResourceGroup(resourceGroup);

            // Create AvailabilitySet
            Console.WriteLine("--------Start create AvailabilitySet--------");
            aset = rgClient.AvailabilitySets().ConstructAvailabilitySet("Aligned");
            aset = rgClient.AvailabilitySets().Create(vmName + "_aSet", aset).Value;

            // Create VNet
            Console.WriteLine("--------Start create VNet--------");
            string vnetName = vmName + "_vnet";
            var vnetClient = rgClient.Vnets();
            var vnet = vnetClient.List(vnetName).FirstOrDefault();
            PhVirtualNetwork vnetModel;
            if (vnet == null)
            {
               vnetModel = vnetClient.ConstructVnet("10.0.0.0/16");
               vnetModel = vnetClient.Create(vnetName, vnetModel).Value;
            }
            else
            {
                vnetModel = vnetClient.Vnet(vnet).Get().Value;
            }

            //create subnet
            Console.WriteLine("--------Start create Subnet--------");

            var sub = vnetModel.Subnets.FirstOrDefault( s => string.Equals(s.Name, subnetName, StringComparison.InvariantCultureIgnoreCase));
            if (sub == null)
            {
                var nsgClient = rgClient.Nsgs();
                var nsg = nsgClient.ConstructNsg(nsgName, 80);
                nsg = rgClient.Nsgs().Create(nsgName, nsg).Value;
                subnet = vnetClient.Vnet(vnetModel).Subnets().ConstructSubnet(subnetName, "10.0.0.0/24");
                subnet = vnetClient.Vnet(vnetModel).Subnets().Create(subnetName, subnet).Value;
            }
            else
            {
                subnet = rgClient.Vnet(vnetModel).Subnet(new ResourceIdentifier(sub.Id)).Get().Value;
            }
        }
    }
}
