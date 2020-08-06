using Azure.ResourceManager.Compute;
using Azure.ResourceManager.Compute.Models;
using azure_proto_core;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace azure_proto_compute
{
    public class VmCollection : AzureTaggableCollection<AzureVm, PhVirtualMachine>
    {
        public VmCollection(TrackedResource resourceGroup) : base(resourceGroup) { }

        private ComputeManagementClient Client => ClientFactory.Instance.GetComputeClient(Parent.Id.Subscription);

        public AzureVm CreateOrUpdateVm(string name, AzureVm vm)
        {
            var vmResult = Client.VirtualMachines.StartCreateOrUpdate(Parent.Name, name, vm.Model).WaitForCompletionAsync().Result;
            return new AzureVm(Parent, new PhVirtualMachine(vmResult.Value));
        }

        //TODO: implement the following
        //public AzureVm StartCreateOrUpdateVm(string name, AzureVm vm)
        //{
        //    //convert to azurevm valuetask<>
        //    var vmResult = Client.VirtualMachines.StartCreateOrUpdate(Parent.Name, name, vm.Model).WaitForCompletionAsync().Result;
        //    return new AzureVm(Parent, new PhVirtualMachine(vmResult.Value));
        //}

        public async Task<AzureVm> CreateOrUpdateVmAsync(string name, AzureVm vm, CancellationToken cancellationToken = default)
        {
            var puller = await Client.VirtualMachines.StartCreateOrUpdateAsync(Parent.Name, name, vm.Model, cancellationToken);
            var vmResult = await puller.WaitForCompletionAsync();
            return new AzureVm(Parent, new PhVirtualMachine(vmResult.Value));
        }

        //TODO: implement the following
        //public async Task<AzureVm> StartCreateOrUpdateVmAsync(string name, AzureVm vm, CancellationToken cancellationToken = default)
        //{
        //    return await Client.VirtualMachines.StartCreateOrUpdateAsync(Parent.Name, name, vm.Model, cancellationToken);
        //}

        public static AzureVm GetVm(string subscriptionId, string rgName, string vmName)
        {
            var client = ClientFactory.Instance.GetComputeClient(subscriptionId);
            var vmResult = client.VirtualMachines.Get(rgName, vmName);
            return new AzureVm(null, new PhVirtualMachine(vmResult.Value));
        }

        protected override AzureVm Get(string vmName)
        {
            var vmResult = Client.VirtualMachines.Get(Parent.Name, vmName);
            return new AzureVm(Parent, new PhVirtualMachine(vmResult.Value));
        }

        protected override IEnumerable<AzureVm> GetItems()
        {
            foreach (var vm in Client.VirtualMachines.List(Parent.Name))
            {
                yield return new AzureVm(Parent, new PhVirtualMachine(vm));
            }
        }

        public AzureVm ConstructVm(string vmName, string adminUser, string adminPw, ResourceIdentifier nicId, AzureAvailabilitySet aset)
        {
            var vm = new VirtualMachine(Parent.Location)
            {
                NetworkProfile = new NetworkProfile { NetworkInterfaces = new[] { new NetworkInterfaceReference() { Id = nicId } } },
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
            return new AzureVm(Parent, new PhVirtualMachine(vm));
        }
    }
}
