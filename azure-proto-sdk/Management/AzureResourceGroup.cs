using Azure.Identity;
using Azure.ResourceManager.Compute;
using Azure.ResourceManager.Compute.Models;
using Azure.ResourceManager.Network;
using Azure.ResourceManager.Network.Models;
using azure_proto_sdk.Compute;
using azure_proto_sdk.Network;
using Microsoft.Azure.Management.ResourceManager.Models;
using System.Collections.Generic;
using System.Linq;

namespace azure_proto_sdk.Management
{
    public class AzureResourceGroup : AzureResource<AzureLocation, ResourceGroup>
    {
        public PublicIpAddressCollection IpAddresses { get; private set; }
        public VnetCollection VNets { get; private set; }
        public NicCollection Nics { get; private set; }
        public VmCollection Vms { get; private set; }
        public AvailabilitySetCollection AvailabilitySets { get; private set; }

        public string Name { get { return Model.Name; } }

        public AzureResourceGroup(AzureLocation location, ResourceGroup resourceGroup) : base(location, resourceGroup)
        {
            AvailabilitySets = new AvailabilitySetCollection(this);
            Vms = new VmCollection(this);
            VNets = new VnetCollection(this);
            IpAddresses = new PublicIpAddressCollection(this);
            Nics = new NicCollection(this);
        }

        public AzureAvailabilitySet ConstructAvailabilitySet(string skuName)
        {
            var availabilitySet = new AvailabilitySet(Parent.Name)
            {
                PlatformUpdateDomainCount = 5,
                PlatformFaultDomainCount = 2,
                Sku = new Azure.ResourceManager.Compute.Models.Sku() { Name = skuName },
            };
            return new AzureAvailabilitySet(this, availabilitySet);
        }

        public AzurePublicIpAddress ConstructIPAddress()
        {
            var ipAddress = new PublicIPAddress()
            {
                PublicIPAddressVersion = Azure.ResourceManager.Network.Models.IPVersion.IPv4.ToString(),
                PublicIPAllocationMethod = IPAllocationMethod.Dynamic,
                Location = Parent.Name,
            };
            return new AzurePublicIpAddress(this, ipAddress);
        }

        public AzureVnet ConstructVnet(string vnetCidr)
        {
            var vnet = new VirtualNetwork()
            {
                Location = Parent.Name,
                AddressSpace = new AddressSpace() { AddressPrefixes = new List<string>() { vnetCidr } },
            };
            return new AzureVnet(this, vnet);
        }

        public AzureNic ConstructNic(AzurePublicIpAddress ip, string subnetId)
        {
            var nic = new NetworkInterface()
            {
                Location = Parent.Name,
                IpConfigurations = new List<NetworkInterfaceIPConfiguration>()
                {
                    new NetworkInterfaceIPConfiguration()
                    {
                        Name = "Primary",
                        Primary = true,
                        Subnet = new Subnet() { Id = subnetId },
                        PrivateIPAllocationMethod = IPAllocationMethod.Dynamic,
                        PublicIPAddress = new PublicIPAddress() { Id = ip.Model.Id }
                    }
                }
            };
            return new AzureNic(this, nic);
        }

        public AzureVm ConstructVm(string vmName, string adminUser, string adminPw, AzureNic nic, AzureAvailabilitySet aset)
        {
            var vm = new VirtualMachine(Parent.Name)
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
            return new AzureVm(this, vm);
        }
    }
}
