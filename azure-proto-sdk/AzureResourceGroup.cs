using Azure.Identity;
using Azure.ResourceManager.Compute;
using Azure.ResourceManager.Compute.Models;
using Azure.ResourceManager.Network;
using Azure.ResourceManager.Network.Models;
using Microsoft.Azure.Management.ResourceManager.Models;

namespace azure
{
    public class AzureResourceGroup
    {
        private ResourceGroup resourceGroup;

        public AzureLocation Location { get; private set; }
        public PublicIPAddress IpAddress { get; private set; }
        public VirtualNetwork VNet { get; private set; }
        public NetworkInterface Nic { get; private set; }
        public VirtualMachine Vm { get; private set; }
        public AvailabilitySetCollection AvailabilitySets { get; private set; }

        public string Name { get { return resourceGroup.Name; } }

        public AzureResourceGroup(AzureLocation location, ResourceGroup resourceGroup)
        {
            this.resourceGroup = resourceGroup;
            Location = location;
            AvailabilitySets = new AvailabilitySetCollection(this);
        }

        internal void CreateOrUpdatePublicIpAddress(string name, PublicIPAddress ipAddress)
        {
            var networkClient = Location.Subscription.NetworkClient;
            IpAddress = networkClient.PublicIPAddresses.StartCreateOrUpdate(Name, name, ipAddress).WaitForCompletionAsync().Result;
        }

        internal void CreateOrUpdateVNet(string name, VirtualNetwork vnet)
        {
            var networkClient = Location.Subscription.NetworkClient;
            VNet = networkClient.VirtualNetworks.StartCreateOrUpdate(Name, name, vnet).WaitForCompletionAsync().Result;
        }

        internal void CreateOrUpdateNic(string name, NetworkInterface nic)
        {
            var networkClient = Location.Subscription.NetworkClient;
            Nic = networkClient.NetworkInterfaces.StartCreateOrUpdate(Name, name, nic).WaitForCompletionAsync().Result;
        }

        internal void CreateOrUpdateVm(string name, VirtualMachine vm)
        {
            var computeClient = Location.Subscription.ComputeClient;
            Vm = computeClient.VirtualMachines.StartCreateOrUpdate(Name, name, vm).WaitForCompletionAsync().Result;
        }
    }
}
