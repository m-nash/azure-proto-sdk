using azure_proto_compute;
using azure_proto_management;
using azure_proto_network;
using System;
using System.Linq;
using System.Threading.Tasks;
using azure_proto_compute.Convenience;
using azure_proto_core;

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

        static void Main(string[] args)
        {
            try
            {
                //CreateSingleVmExample();
                CreateMultipleVmShutdownSome();
                //StartStopVm();
                //StartFromVm();
                //SetTagsOnVm();
                CreateMultipleVmShutdownByTag();
            }
            finally
            {
                CleanUp();
            }
        }

        private static void CleanUp()
        {
            Console.WriteLine($"--------Deleting {rgName}--------");

            // TODO: For delete scenario, non-OOB patter would be easier and less chatty. Check with Nick on any indication of user preference
            // GetRG() on Client seems to break the encapsulation.
            //      client
            //          .Subscriptions[subscriptionId]
            //          .ResourceGroups[rg]
            //          .Delete
            // BTW, using indexer[] does not give clear indication to user that it is actually issuing a GET.
            // What about method call GetOneAsync(id)?
            AzureResourceGroup rg = AzureClient.GetResourceGroup(subscriptionId, rgName);
            rg.Delete();
        }

        private static void CreateMultipleVmShutdownByTag()
        {
            var rg = CreateMultipleVms();

            //set tags on random vms
            Random rand = new Random(Environment.TickCount);
            foreach(var vm in rg.Vms())
            {
                if (rand.NextDouble() > 0.5)
                {
                    Console.WriteLine("adding tag to {0}", vm.Name);
                    vm.AddTag("tagkey", "tagvalue");
                }
            }

            foreach(var vm in rg.Vms().GetItemsByTag("tagkey", "tagvalue"))
            {
                Console.WriteLine("--------Stopping VM {0}--------", vm.Name);
                vm.Stop();
                Console.WriteLine("--------Starting VM {0}--------", vm.Name);
                vm.Start();
            }
        }

        private static void SetTagsOnVm()
        {
            //make sure vm exists
            CreateSingleVmExample();

            AzureResourceGroup rg = AzureClient.GetResourceGroup(subscriptionId, rgName);
            AzureVm vm = rg.Vms()[vmName];

            vm.AddTag("tagkey", "tagvalue");
        }

        private static void StartFromVm()
        {
            // TODO: Look at VM nic/nsg operations on VM
            //make sure vm exists
            CreateSingleVmExample();

            //retrieve from lowest level, doesn't give ability to walk up and down the container structure
            AzureVm vm = VmCollection.GetVm(subscriptionId, rgName, vmName);
            Console.WriteLine("Found VM {0}", vm.Id);


            //retrieve from lowest level inside management package gives ability to walk up and down
            AzureResourceGroup rg = AzureClient.GetResourceGroup(subscriptionId, rgName);
            AzureVm vm2 = rg.Vms()[vmName];
            Console.WriteLine("Found VM {0}", vm2.Id);
        }

        private static void StartStopVm()
        {
            AzureClient client = new AzureClient();
            var subscription = client.Subscriptions[subscriptionId];
            var resourceGroup = subscription.ResourceGroups[rgName];
            var vm = resourceGroup.Vms()[vmName];
            Console.WriteLine("Found VM {0}", vmName);
            Console.WriteLine("--------Stopping VM--------");
            vm.Stop();
            Console.WriteLine("--------Starting VM--------");
            vm.Start();
        }

        private static void CreateMultipleVmShutdownSome()
        {
            AzureResourceGroup resourceGroup = CreateMultipleVms();

            resourceGroup.Vms().Select(vm =>
            {
                var parts = vm.Name.Split('-');
                var n = Convert.ToInt32(parts[parts.Length - 2]);
                return (vm, n);
            })
                .Where(tuple => tuple.n % 2 == 0)
                .ToList()
                .ForEach(tuple =>
                {
                    Console.WriteLine("Stopping {0}", tuple.vm.Name);
                    tuple.vm.Stop();
                    Console.WriteLine("Starting {0}", tuple.vm.Name);
                    tuple.vm.Start();
                });
        }

        private static AzureResourceGroup CreateMultipleVms()
        {
            AzureResourceGroup resourceGroup;
            AzureAvailabilitySet aset;
            AzureSubnet subnet;
            SetupVmHost(out resourceGroup, out aset, out subnet);

            for (int i = 0; i < 10; i++)
            {
                AzureNic nic = CreateNic(resourceGroup, subnet, i);

                // Create VM
                string name = String.Format("{0}-{1}-z", vmName, i);
                Console.WriteLine("--------Start create VM {0}--------", i);
                var vm = resourceGroup.Vms().ConstructVm(name, "admin-user", "!@#$%asdfA", nic.Id, aset);
                vm = resourceGroup.Vms().CreateOrUpdateVm(name, vm);
            }

            return resourceGroup;
        }

        private static void CreateSingleVmExample()
        {
            AzureResourceGroup resourceGroup;
            AzureAvailabilitySet aset;
            AzureSubnet subnet;
            SetupVmHost(out resourceGroup, out aset, out subnet);

            AzureNic nic = CreateNic(resourceGroup, subnet, 1000);

            // Create VM
            Console.WriteLine("--------Start create VM--------");
            var vm = resourceGroup.Vms().ConstructVm(vmName, "admin-user", "!@#$%asdfA", nic.Id, aset);
            vm = resourceGroup.Vms().CreateOrUpdateVm(vmName, vm);

            Console.WriteLine("VM ID: " + vm.Id);
            Console.WriteLine("--------Done create VM--------");
        }

        private static AzureNic CreateNic(AzureResourceGroup resourceGroup, AzureSubnet subnet, int i)
        {
            // Create IP Address
            Console.WriteLine("--------Start create IP Address--------");
            var ipAddress = resourceGroup.IpAddresses().ConstructIPAddress();
            ipAddress = resourceGroup.IpAddresses().CreateOrUpdatePublicIpAddress(String.Format("{0}_{1}_ip", vmName, i), ipAddress);

            // Create Network Interface
            Console.WriteLine("--------Start create Network Interface--------");
            var nic = resourceGroup.Nics().ConstructNic(ipAddress, subnet.Id);
            nic = resourceGroup.Nics().CreateOrUpdateNic(String.Format("{0}_{1}_nic", vmName, i), nic);
            return nic;
        }

        private static Task<AzureVm> CreateVmWithBuilderAsync()
        {
            AzureResourceGroup resourceGroup;
            AzureAvailabilitySet aset;
            AzureSubnet subnet;
            SetupVmHost(out resourceGroup, out aset, out subnet);

            AzureNic nic = CreateNic(resourceGroup, subnet, 0);

            // Create VM
            string name = String.Format("{0}-{1}-z", vmName, 0);
            Console.WriteLine("--------Start create VM {0}--------", 0);
            var ip = new AzurePublicIpAddress(null, null);

            // TODO: Open questions
            // 0. Builder is an convenience feature. Simpler model would just use new xxx()
            // 1. Wish we can do compile time check for required properties. And now, ToModel() will do validation. 
            // 2. Is there a risk that the referenced model has not been created in ARM yet resource id is populated?
            var vm = resourceGroup.VmBuilder(name)
                .Location(new Location("uswest2"))
                .ConfigureWith(nic)
                .ConfigureWith(aset)
                .ConfigureWith(ip)  // here it should throw since PublicIP is not associated with VM
                .UseWindowsImage("admin-user", "!@#$%asdfA")
                .ToModel();

            vm = resourceGroup.Vms().CreateOrUpdateVm(name, vm);

            return Task.FromResult(vm);
        }

        private static void SetupVmHost(out AzureResourceGroup resourceGroup, out AzureAvailabilitySet aset, out AzureSubnet subnet)
        {
            AzureClient client = new AzureClient();
            var subscription = client.Subscriptions[subscriptionId];

            // Create Resource Group
            Console.WriteLine("--------Start create group--------");
            resourceGroup = subscription.ResourceGroups.CreateOrUpdate(rgName, loc);

            // Create AvailabilitySet
            Console.WriteLine("--------Start create AvailabilitySet--------");
            aset = resourceGroup.AvailabilitySets().ConstructAvailabilitySet("Aligned");
            aset = resourceGroup.AvailabilitySets().CreateOrUpdateAvailabilityset(vmName + "_aSet", aset);

            // Create VNet
            Console.WriteLine("--------Start create VNet--------");
            string vnetName = vmName + "_vnet";
            AzureVnet vnet;
            if (!resourceGroup.VNets().TryGetValue(vnetName, out vnet))
            {
                vnet = resourceGroup.VNets().ConstructVnet("10.0.0.0/16");
                vnet = resourceGroup.VNets().CreateOrUpdateVNet(vnetName, vnet);
            }

            //create subnet
            Console.WriteLine("--------Start create Subnet--------");
            if (!vnet.Subnets.TryGetValue(subnetName, out subnet))
            {
                var nsg = resourceGroup.Nsgs().ConstructNsg(nsgName, 80);
                nsg = resourceGroup.Nsgs().CreateOrUpdateNsgs(nsg);
                subnet = vnet.Subnets.ConstructSubnet(subnetName, "10.0.0.0/24");
                subnet = vnet.Subnets.CreateOrUpdateSubnets(subnet);
            }
        }
    }
}
