using Azure.ResourceManager.Compute.Models;
using azure_proto_core;
using azure_proto_network;
using System;
using System.Collections.Generic;
using System.Text;

namespace azure_proto_compute
{
    public static class AzureResourceGroupExtension
    {
        private static Dictionary<string, VmCollection> vmCollections = new Dictionary<string, VmCollection>();

        public static VmCollection Vms(this AzureResourceGroupBase resourceGroup)
        {
            VmCollection result;
            if(!vmCollections.TryGetValue(resourceGroup.Id, out result))
            {
                result = new VmCollection(resourceGroup);
                vmCollections.Add(resourceGroup.Id, result);
            }
            return result;
        }

        private static Dictionary<string, AvailabilitySetCollection> asetCollections = new Dictionary<string, AvailabilitySetCollection>();

        public static AvailabilitySetCollection AvailabilitySets(this AzureResourceGroupBase resourceGroup)
        {
            AvailabilitySetCollection result;
            if (!asetCollections.TryGetValue(resourceGroup.Id, out result))
            {
                result = new AvailabilitySetCollection(resourceGroup);
                asetCollections.Add(resourceGroup.Id, result);
            }
            return result;
        }

        public static AzureVm ConstructVm(this AzureResourceGroupBase resourceGroup, string vmName, string adminUser, string adminPw, AzureNic nic, AzureAvailabilitySet aset)
        {
            var vm = new VirtualMachine(resourceGroup.Parent.Name)
            {
                NetworkProfile = new Azure.ResourceManager.Compute.Models.NetworkProfile { NetworkInterfaces = new[] { new NetworkInterfaceReference() { Id = nic.Model.Id } } },
                OsProfile = new OSProfile
                {
                    ComputerName = vmName,
                    AdminUsername = adminUser,
                    AdminPassword = adminPw,
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
            return new AzureVm(resourceGroup, new PhVirtualMachine(vm));
        }

        public static AzureAvailabilitySet ConstructAvailabilitySet(this AzureResourceGroupBase resourceGroup, string skuName)
        {
            var availabilitySet = new AvailabilitySet(resourceGroup.Parent.Name)
            {
                PlatformUpdateDomainCount = 5,
                PlatformFaultDomainCount = 2,
                Sku = new Azure.ResourceManager.Compute.Models.Sku() { Name = skuName },
            };
            return new AzureAvailabilitySet(resourceGroup, new PhAvailabilitySet(availabilitySet));
        }
    }
}
