using Azure.ResourceManager.Compute;
using Azure.ResourceManager.Compute.Models;
using Azure.ResourceManager.Network;
using Azure.ResourceManager.Network.Models;
using Azure.ResourceManager.Resources;
using Azure.ResourceManager.Resources.Models;
using System;
using System.Collections.Generic;

namespace client
{
    class CreateSingleVmExample : Scenario
    {
        public CreateSingleVmExample() : base() { }

        public CreateSingleVmExample(ScenarioContext context) : base(context) { }

        public override void Execute()
        {
            // Note that we need to create 3 clients from this scenario; customer must know which client contains which resource
            var rmClient = new ResourcesManagementClient(Context.SubscriptionId, Context.Credential);
            var computeClient = new ComputeManagementClient(Context.SubscriptionId, Context.Credential);
            var networkClient = new NetworkManagementClient(Context.SubscriptionId, Context.Credential);

            // Create Resource Group
            Console.WriteLine($"--------Start create group {Context.RgName}--------");

            // note that some resources use the StartCreate and some use Create - this can make it a bit difficult to handle resource creation generically
            var resourceGroup = rmClient.ResourceGroups.CreateOrUpdate(Context.RgName, new ResourceGroup(Context.Loc)).Value;
            CleanUp.Add(resourceGroup.Id);

            // Create AvailabilitySet
            Console.WriteLine("--------Start create AvailabilitySet--------");
            var aset = new AvailabilitySet(Context.Loc)
            {
                PlatformUpdateDomainCount = 5,
                PlatformFaultDomainCount = 2,
                Sku = new Azure.ResourceManager.Compute.Models.Sku() { Name = "Aligned" }
            };

            aset = computeClient.AvailabilitySets.CreateOrUpdate(Context.RgName, Context.VmName + "_aSet", aset).Value;

            // Create VNet
            Console.WriteLine("--------Start create VNet--------");
            string vnetName = Context.VmName + "_vnet";
            var vnet = new VirtualNetwork()
            {
                Location = Context.Loc,
                AddressSpace = new AddressSpace() { AddressPrefixes = new List<string>() { "10.0.0.0/16" } }
            };

            vnet = networkClient.VirtualNetworks.StartCreateOrUpdate(Context.RgName, vnetName, vnet).WaitForCompletionAsync().ConfigureAwait(false).GetAwaiter().GetResult().Value;

            //create subnet
            Console.WriteLine("--------Start create Subnet--------");
            var nsg = new NetworkSecurityGroup { Location = Context.Loc };
            nsg.SecurityRules = new List<SecurityRule> { new SecurityRule
            {
                Name = "Port80",
                Priority = 1001,
                Protocol = SecurityRuleProtocol.Tcp,
                Access = SecurityRuleAccess.Allow,
                Direction = SecurityRuleDirection.Inbound,
                SourcePortRange = "*",
                SourceAddressPrefix = "*",
                DestinationPortRange = "80",
                DestinationAddressPrefix = "*",
                Description = $"Port_80"
            } };

            nsg = networkClient.NetworkSecurityGroups.StartCreateOrUpdate(Context.RgName, Context.NsgName, nsg).WaitForCompletionAsync().ConfigureAwait(false).GetAwaiter().GetResult().Value;
            var subnet = new Subnet()
            {
                Name = Context.SubnetName,
                AddressPrefix = "10.0.0.0/24",
                NetworkSecurityGroup = nsg
            };

            subnet = networkClient.Subnets.StartCreateOrUpdate(Context.RgName, vnetName, Context.SubnetName, subnet).WaitForCompletionAsync().ConfigureAwait(false).GetAwaiter().GetResult().Value;

            // Create IP Address
            Console.WriteLine("--------Start create IP Address--------");
            var ipAddress = new PublicIPAddress()
            {
                PublicIPAddressVersion = Azure.ResourceManager.Network.Models.IPVersion.IPv4.ToString(),
                PublicIPAllocationMethod = IPAllocationMethod.Dynamic,
                Location = Context.Loc,
            };

            ipAddress = networkClient.PublicIPAddresses.StartCreateOrUpdate(Context.RgName, $"{Context.VmName}_ip", ipAddress).WaitForCompletionAsync().ConfigureAwait(false).GetAwaiter().GetResult().Value;

            // Create Network Interface
            Console.WriteLine("--------Start create Network Interface--------");
            var nic = new NetworkInterface()
            {
                Location = Context.Loc,
                IpConfigurations = new List<NetworkInterfaceIPConfiguration>()
                {
                    new NetworkInterfaceIPConfiguration()
                    {
                        Name = "Primary",
                        Primary = true,
                        Subnet = new Subnet() { Id = subnet.Id },
                        PrivateIPAllocationMethod = IPAllocationMethod.Dynamic,
                        PublicIPAddress = new PublicIPAddress() { Id = ipAddress.Id }
                    }
                }
            };
            
            nic = networkClient.NetworkInterfaces.StartCreateOrUpdate(Context.RgName, $"{Context.VmName}_nic", nic).WaitForCompletionAsync().ConfigureAwait(false).GetAwaiter().GetResult().Value;

            // Create VM
            Console.WriteLine("--------Start create VM--------");
            var vm = new VirtualMachine(Context.Loc)
            {
                NetworkProfile = new Azure.ResourceManager.Compute.Models.NetworkProfile { NetworkInterfaces = new[] { new NetworkInterfaceReference() { Id = nic.Id } } },
                OsProfile = new OSProfile
                {
                    ComputerName = vmName,
                    AdminUsername = adminUser,
                    AdminPassword = adminPw,
                    WindowsConfiguration = new WindowsConfiguration { TimeZone = "Pacific Standard Time", ProvisionVMAgent = true }
                },
                StorageProfile = new StorageProfile()
                {
                    ImageReference = new ImageReference()
                    {
                        Offer = "WindowsServer",
                        Publisher = "MicrosoftWindowsServer",
                        Sku = "2019-Datacenter",
                        Version = "latest"
                    },
                    DataDisks = new List<DataDisk>()
                },
                HardwareProfile = new HardwareProfile() { VmSize = VirtualMachineSizeTypes.StandardB1Ms },
                // The namespace-qualified type for SubResource is needed because all 3 libraries define an identical SubResource type. In the proposed model, the common type would be part of the core library
                AvailabilitySet = new Azure.ResourceManager.Compute.Models.SubResource() { Id = aset.Id }
            };
            vm = computeClient.VirtualMachines.StartCreateOrUpdate(Context.RgName, Context.VmName, vm).WaitForCompletionAsync().ConfigureAwait(false).GetAwaiter().GetResult().Value;

            Console.WriteLine("VM ID: " + vm.Id);
            Console.WriteLine("--------Done create VM--------");
        }
    }
}
