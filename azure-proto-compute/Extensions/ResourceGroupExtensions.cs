using Azure;
using Azure.ResourceManager.Compute.Models;
using azure_proto_compute.Convenience;
using azure_proto_core;
using azure_proto_core.Adapters;
using azure_proto_core.Resources;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace azure_proto_compute
{
    /// <summary>
    /// Naming of these methods
    /// </summary>
    public static class ResourceGroupExtensions
    {
        #region VirtualMachines

        public static VirtualMachineOperations Vm(this ResourceGroupOperations operations, string name)
        {
            return new VirtualMachineOperations(operations, $"{operations.Context}/providers/Microsoft.Compute/virtualMachines/{name}");
        }
        public static VirtualMachineOperations Vm(this ResourceGroupOperations operations, ResourceIdentifier vm)
        {
            return new VirtualMachineOperations(operations, vm);
        }

        public static VirtualMachineOperations Vm(this ResourceGroupOperations operations, TrackedResource vm)
        {
            return new VirtualMachineOperations(operations, vm);
        }

        public static ArmOperation<ResourceClientBase<PhVirtualMachine>> CreateVm(this ResourceGroupOperations operations, string name, PhVirtualMachine resourceDetails)
        {
            var container = new VirtualMachineContainer(operations, operations.Context);
            return container.Create(name, resourceDetails);
        }

        public static Task<ArmOperation<ResourceClientBase<PhVirtualMachine>>> CreateVmAsync(this ResourceGroupOperations operations, string name, PhVirtualMachine resourceDetails, CancellationToken cancellationToken = default)
        {
            var container = new VirtualMachineContainer(operations, operations.Context);
            return container.CreateAsync(name, resourceDetails);
        }

        public static VirtualMachineContainer ConstructVm(this ResourceGroupOperations operations, string vmName, string adminUser, string adminPw, ResourceIdentifier nicId, PhAvailabilitySet aset, Location location = null)
        {
            var vm = new VirtualMachine(location ?? operations.DefaultLocation)
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

            return new VirtualMachineContainer(operations, new PhVirtualMachine(vm));
        }

        public static VirtualMachineModelBuilder VmBuilder(this ResourceGroupOperations operations, string name, Location location)
        {
            return new VirtualMachineModelBuilder(name, location);
        }

        /// <summary>
        /// List vms at the given subscription context
        /// </summary>
        /// <param name="subscription"></param>
        /// <param name="filter"></param>
        /// <param name="top"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public static Pageable<VirtualMachineOperations> ListVms(this ResourceGroupOperations operations, ArmSubstringFilter filter = null, int? top = null, CancellationToken cancellationToken = default)
        {
            var collection = new VirtualMachineCollection(operations, operations.Context);
            return new WrappingPageable<ResourceClientBase<PhVirtualMachine>, VirtualMachineOperations>(collection.List(filter, top, cancellationToken), vm => new VirtualMachineOperations(vm, vm.Context));
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="subscription"></param>
        /// <param name="filter"></param>
        /// <param name="top"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public static AsyncPageable<VirtualMachineOperations> ListVmsAsync(this ResourceGroupOperations operations, ArmSubstringFilter filter = null, int? top = null, CancellationToken cancellationToken = default)
        {
            var collection = new VirtualMachineCollection(operations, operations.Context);
            return new WrappingAsyncPageable<ResourceClientBase<PhVirtualMachine>, VirtualMachineOperations>(collection.ListAsync(filter, top, cancellationToken), vm => new VirtualMachineOperations(vm, vm.Context));
        }

        public static Pageable<VirtualMachineOperations> ListVmsByTag(this ResourceGroupOperations operations, ArmTagFilter filter, int? top = null, CancellationToken cancellationToken = default)
        {
            var collection = new VirtualMachineCollection(operations, operations.Context);
            return new WrappingPageable<ResourceClientBase<PhVirtualMachine>, VirtualMachineOperations>(collection.ListByTag(filter, top, cancellationToken), vm => new VirtualMachineOperations(vm, vm.Context));
        }

        #endregion

        #region AvailabilitySets

        public static AvailabilitySetOperations AvailabilitySets(this ResourceGroupOperations operations, string name)
        {
            return new AvailabilitySetOperations(operations, $"{operations.Context}/providers/Microsoft.Compute/virtualMachines/{name}");
        }
        public static AvailabilitySetOperations AvailabilitySets(this ResourceGroupOperations operations, ResourceIdentifier set)
        {
            return new AvailabilitySetOperations(operations, set);
        }

        public static AvailabilitySetOperations AvailabilitySets(this ResourceGroupOperations operations, TrackedResource set)
        {
            return new AvailabilitySetOperations(operations, set);
        }

        public static ArmOperation<ResourceClientBase<PhAvailabilitySet>> CreateAvailabilitySet(this ResourceGroupOperations operations, string name, PhAvailabilitySet resourceDetails)
        {
            var container = new AvailabilitySetContainer(operations, operations.Context);
            return container.Create(name, resourceDetails);
        }

        public static Task<ArmOperation<ResourceClientBase<PhAvailabilitySet>>> CreateAvailabilitySetAsync(this ResourceGroupOperations operations, string name, PhAvailabilitySet resourceDetails, CancellationToken cancellationToken = default)
        {
            var container = new AvailabilitySetContainer(operations, operations.Context);
            return container.CreateAsync(name, resourceDetails);
        }


        public static AvailabilitySetContainer ConstructAvailabilitySet(this ResourceGroupOperations operations, string skuName, Location location = null)
        {
            var availabilitySet = new AvailabilitySet(location ?? operations.DefaultLocation)
            {
                PlatformUpdateDomainCount = 5,
                PlatformFaultDomainCount = 2,
                Sku = new Azure.ResourceManager.Compute.Models.Sku() { Name = skuName },
            };

            return new AvailabilitySetContainer( operations, new PhAvailabilitySet(availabilitySet));
        }

        #endregion

    }
}
