using Azure.ResourceManager.Compute.Models;
using azure_proto_core;
using azure_proto_network;
using System.Collections.Generic;

namespace azure_proto_compute
{
    public static class AzureResourceGroupExtension
    {
        private static Dictionary<string, VmCollection> vmCollections = new Dictionary<string, VmCollection>();
        private static readonly object vmLock = new object();

        public static VmCollection Vms(this AzureResourceGroupBase resourceGroup)
        {
            VmCollection result;
            if(!vmCollections.TryGetValue(resourceGroup.Id, out result))
            {
                lock (vmLock)
                {
                    if (!vmCollections.TryGetValue(resourceGroup.Id, out result))
                    {
                        result = new VmCollection(resourceGroup);
                        vmCollections.Add(resourceGroup.Id, result);
                    }
                }
            }
            return result;
        }

        private static Dictionary<string, AvailabilitySetCollection> asetCollections = new Dictionary<string, AvailabilitySetCollection>();
        private static readonly object asetLock = new object();

        public static AvailabilitySetCollection AvailabilitySets(this AzureResourceGroupBase resourceGroup)
        {
            AvailabilitySetCollection result;
            if (!asetCollections.TryGetValue(resourceGroup.Id, out result))
            {
                lock (asetLock)
                {
                    if (!asetCollections.TryGetValue(resourceGroup.Id, out result))
                    {
                        result = new AvailabilitySetCollection(resourceGroup);
                        asetCollections.Add(resourceGroup.Id, result);
                    }
                }
            }
            return result;
        }

        public static AzureVm ConstructVm(this AzureResourceGroupBase resourceGroup, string vmName, string adminUser, string adminPw, AzureNic nic, AzureAvailabilitySet aset)
        {
            var vm = new VirtualMachine(resourceGroup.Location)
            {
                NetworkProfile = new NetworkProfile { NetworkInterfaces = new[] { new NetworkInterfaceReference() { Id = nic.Model.Id } } },
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
                AvailabilitySet = new SubResource() { Id = aset.Id }
            };
            return new AzureVm(resourceGroup, new PhVirtualMachine(vm));
        }

        public static AzureAvailabilitySet ConstructAvailabilitySet(this AzureResourceGroupBase resourceGroup, string skuName)
        {
            var availabilitySet = new AvailabilitySet(resourceGroup.Location)
            {
                PlatformUpdateDomainCount = 5,
                PlatformFaultDomainCount = 2,
                Sku = new Sku() { Name = skuName },
            };
            return new AzureAvailabilitySet(resourceGroup, new PhAvailabilitySet(availabilitySet));
        }
    }
}
