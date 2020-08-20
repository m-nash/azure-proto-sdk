using Azure.ResourceManager.Compute;
using Azure.ResourceManager.Compute.Models;
using azure_proto_compute.Placeholder;
using azure_proto_core;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace azure_proto_compute
{
    /// <summary>
    /// Vm Operations over a resource group
    /// </summary>
    public class VmContainer : ResourceContainerOperations<PhVirtualMachine>
    {
        public VmContainer(ArmOperations parent, ResourceIdentifier context) : base(parent, context)
        {
        }

        public VmContainer(ArmOperations parent, azure_proto_core.Resource context) : base(parent, context)
        {
        }

        protected override ResourceType ResourceType => "Microsoft.Compute/virtualMachines";

        public override ArmOperation<PhVirtualMachine> Create(string name, PhVirtualMachine resourceDetails)
        {
            return new PhVmValueOperation(VmOperations.StartCreateOrUpdate(Context.ResourceGroup, name, resourceDetails.Model));
        }

        public async override Task<ArmOperation<PhVirtualMachine>> CreateAsync(string name, PhVirtualMachine resourceDetails, CancellationToken cancellationToken = default)
        {
            return new PhVmValueOperation(await VmOperations.StartCreateOrUpdateAsync(Context.ResourceGroup, name, resourceDetails.Model, cancellationToken));
        }

        public VmOperations Vm(string vmName)
        {
            return new VmOperations(this, new ResourceIdentifier($"{Context}/providers/Microsoft.Compute/virtualMachines/{vmName}"));
        }

        public VmOperations vm(ResourceIdentifier vm)
        {
            return new VmOperations(this, vm);
        }

        public VmOperations Vm(TrackedResource vm)
        {
            return new VmOperations(this, vm);
        }

        public PhVirtualMachine ConstructVm(string vmName, string adminUser, string adminPw, ResourceIdentifier nicId, PhAvailabilitySet aset, Location location = null)
        {
            var vm = new VirtualMachine(location ?? DefaultLocation)
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
            return new PhVirtualMachine(vm);
        }

        internal VirtualMachinesOperations VmOperations => new ComputeManagementClient(BaseUri, Context.Subscription, Credential).VirtualMachines;

    }
}
