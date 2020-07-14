using Azure.ResourceManager.Compute.Models;
using Azure.ResourceManager.Network.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace azure_proto_sdk
{
    class Program
    {
        static void Main(string[] args)
        {
            CreateSingleVmExample();
            //CreateMultipleVmShutdownSome();
        }

        private static void CreateMultipleVmShutdownSome()
        {
            throw new NotImplementedException();
        }

        private static void CreateSingleVmExample()
        {
            AzureClient client = new AzureClient();
            var subscription = client.Subscriptions[Environment.GetEnvironmentVariable("AZURE_SUBSCRIPTION_ID")];

            // Set Location
            var location = subscription.Locations["westus2"];

            // Create Resource Group
            Console.WriteLine("--------Start create group--------");
            var resourceGroup = location.ResourceGroups.CreateOrUpdate("mnash-test-rg");
            Console.WriteLine("--------Finish create group--------");

            string vmName = "mnash-quickstartvm";

            // Create AvailabilitySet
            Console.WriteLine("--------Start create AvailabilitySet--------");
            var availabilitySet = new AvailabilitySet(location.Name)
            {
                PlatformUpdateDomainCount = 5,
                PlatformFaultDomainCount = 2,
                Sku = new Sku() { Name = "Aligned" },
            };
            var aset = resourceGroup.AvailabilitySets.CreateOrUpdateAvailabilityset(vmName + "_aSet", availabilitySet);

            // Create IP Address
            Console.WriteLine("--------Start create IP Address--------");
            var ipAddress = new PublicIPAddress()
            {
                PublicIPAddressVersion = Azure.ResourceManager.Network.Models.IPVersion.IPv4.ToString(),
                PublicIPAllocationMethod = IPAllocationMethod.Dynamic,
                Location = location.Name,
            };
            resourceGroup.CreateOrUpdatePublicIpAddress(vmName + "_ip", ipAddress);

            // Create VNet
            Console.WriteLine("--------Start create VNet--------");
            var vnet = new VirtualNetwork()
            {
                Location = location.Name,
                AddressSpace = new AddressSpace() { AddressPrefixes = new List<string>() { "10.0.0.0/16" } },
                Subnets = new List<Subnet>()
                {
                    new Subnet()
                    {
                        Name = "mySubnet",
                        AddressPrefix = "10.0.0.0/24",
                    }
                },
            };
            resourceGroup.CreateOrUpdateVNet(vmName + "_vnet", vnet);

            // Create Network Interface
            Console.WriteLine("--------Start create Network Interface--------");
            var nic = new NetworkInterface()
            {
                Location = location.Name,
                IpConfigurations = new List<NetworkInterfaceIPConfiguration>()
                {
                    new NetworkInterfaceIPConfiguration()
                    {
                        Name = "Primary",
                        Primary = true,
                        Subnet = new Subnet() { Id = resourceGroup.VNet.Subnets.First().Id },
                        PrivateIPAllocationMethod = IPAllocationMethod.Dynamic,
                        PublicIPAddress = new PublicIPAddress() { Id = resourceGroup.IpAddress.Id }
                    }
                }
            };
            resourceGroup.CreateOrUpdateNic(vmName + "_nic", nic);

            // Create VM
            Console.WriteLine("--------Start create VM--------");
            var vm = new VirtualMachine(location.Name)
            {
                NetworkProfile = new Azure.ResourceManager.Compute.Models.NetworkProfile { NetworkInterfaces = new[] { new NetworkInterfaceReference() { Id = resourceGroup.Nic.Id } } },
                OsProfile = new OSProfile
                {
                    ComputerName = vmName,
                    AdminUsername = "admin-user",
                    AdminPassword = "!@#$%asdfA",
                    LinuxConfiguration = new LinuxConfiguration { DisablePasswordAuthentication = false, ProvisionVMAgent = true }
                },
                StorageProfile = new StorageProfile()
                {
                    ImageReference = new ImageReference()
                    {
                        Offer = "UbuntuServer",
                        Publisher = "Canonical",
                        Sku = "18.04-LTS",
                        Version = "latest"
                    },
                    DataDisks = new List<DataDisk>()
                },
                HardwareProfile = new HardwareProfile() { VmSize = VirtualMachineSizeTypes.StandardB1Ms },
                AvailabilitySet = new Azure.ResourceManager.Compute.Models.SubResource() { Id = aset.Id }
            };
            var avm = resourceGroup.Vms.CreateOrUpdateVm(vmName, vm);

            Console.WriteLine("VM ID: " + avm.Id);
            Console.WriteLine("--------Done create VM--------");
        }
    }
}
