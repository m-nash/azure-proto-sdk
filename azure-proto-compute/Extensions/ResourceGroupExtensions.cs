using Azure;
using Azure.ResourceManager.Compute.Models;
using azure_proto_compute.Convenience;
using azure_proto_core;
using azure_proto_core.Resources;
using System.Collections.Generic;
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

        public static VirtualMachineOperations VirtualMachine(this ResourceGroupOperations operations, string name)
        {
            return new VirtualMachineOperations(operations, $"{operations.Id}/providers/Microsoft.Compute/virtualMachines/{name}");
        }
        public static VirtualMachineOperations VirtualMachine(this ResourceGroupOperations operations, ResourceIdentifier vm)
        {
            return new VirtualMachineOperations(operations, vm);
        }

        public static VirtualMachineOperations VirtualMachine(this ResourceGroupOperations operations, TrackedResource vm)
        {
            return new VirtualMachineOperations(operations, vm);
        }

        public static ArmOperation<ResourceOperationsBase<PhVirtualMachine>> CreateVirtualMachine(this ResourceGroupOperations operations, string name, PhVirtualMachine resourceDetails)
        {
            var container = new VirtualMachineContainer(operations, operations.Id);
            return container.Create(name, resourceDetails);
        }

        public static Task<ArmOperation<ResourceOperationsBase<PhVirtualMachine>>> CreateVirtualMachineAsync(this ResourceGroupOperations operations, string name, PhVirtualMachine resourceDetails, CancellationToken cancellationToken = default)
        {
            var container = new VirtualMachineContainer(operations, operations.Id);
            return container.CreateAsync(name, resourceDetails);
        }

        public static ArmBuilder<PhVirtualMachine> ConstructVirtualMachine(this ResourceGroupOperations operations, string vmName, string adminUser, string adminPw, ResourceIdentifier nicId, PhAvailabilitySet aset, Location location = null)
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

            return new ArmBuilder<PhVirtualMachine>(new VirtualMachineContainer(operations, operations.Id), new PhVirtualMachine(vm));
        }

        public static VirtualMachineModelBuilder VirtualMachineBuilder(this ResourceGroupOperations operations, string name, Location location)
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
        public static Pageable<VirtualMachineOperations> ListVirtualMachines(this ResourceGroupOperations resourceGroup, ArmSubstringFilter filter = null, int? top = null, CancellationToken cancellationToken = default)
        {
            return ResourceListOperations.ListAtContext<VirtualMachineOperations, PhVirtualMachine>(resourceGroup, filter, top, cancellationToken);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="subscription"></param>
        /// <param name="filter"></param>
        /// <param name="top"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public static AsyncPageable<VirtualMachineOperations> ListVirtualMachinesAsync(this ResourceGroupOperations resourceGroup, ArmSubstringFilter filter = null, int? top = null, CancellationToken cancellationToken = default)
        {
            return ResourceListOperations.ListAtContextAsync<VirtualMachineOperations, PhVirtualMachine>(resourceGroup, filter, top, cancellationToken);
        }

        //TODO: add rp specific filter example
        public static Pageable<VirtualMachineOperations> ListVirtualMachinesByTag(this ResourceGroupOperations resourceGroup, ArmTagFilter filter, int? top = null, CancellationToken cancellationToken = default)
        {
            return ResourceListOperations.ListAtContext<VirtualMachineOperations, PhVirtualMachine>(resourceGroup, filter, top, cancellationToken);
        }

        public static AsyncPageable<VirtualMachineOperations> ListVirtualMachinesByTagAsync(this ResourceGroupOperations resourceGroup, ArmTagFilter filter, int? top = null, CancellationToken cancellationToken = default)
        {
            return ResourceListOperations.ListAtContextAsync<VirtualMachineOperations, PhVirtualMachine>(resourceGroup, filter, top, cancellationToken);
        }

        #endregion

        #region AvailabilitySets

        public static AvailabilitySetOperations AvailabilitySets(this ResourceGroupOperations operations, string name)
        {
            return new AvailabilitySetOperations(operations, $"{operations.Id}/providers/Microsoft.Compute/virtualMachines/{name}");
        }
        public static AvailabilitySetOperations AvailabilitySets(this ResourceGroupOperations operations, ResourceIdentifier set)
        {
            return new AvailabilitySetOperations(operations, set);
        }

        public static AvailabilitySetOperations AvailabilitySets(this ResourceGroupOperations operations, TrackedResource set)
        {
            return new AvailabilitySetOperations(operations, set);
        }

        public static ArmOperation<ResourceOperationsBase<PhAvailabilitySet>> CreateAvailabilitySet(this ResourceGroupOperations operations, string name, PhAvailabilitySet resourceDetails)
        {
            var container = new AvailabilitySetContainer(operations, operations.Id);
            return container.Create(name, resourceDetails);
        }

        public static Task<ArmOperation<ResourceOperationsBase<PhAvailabilitySet>>> CreateAvailabilitySetAsync(this ResourceGroupOperations operations, string name, PhAvailabilitySet resourceDetails, CancellationToken cancellationToken = default)
        {
            var container = new AvailabilitySetContainer(operations, operations.Id);
            return container.CreateAsync(name, resourceDetails);
        }

        public static ArmBuilder<PhAvailabilitySet> ConstructAvailabilitySet(this ResourceGroupOperations operations, string skuName, Location location = null)
        {
            var availabilitySet = new AvailabilitySet(location ?? operations.DefaultLocation)
            {
                PlatformUpdateDomainCount = 5,
                PlatformFaultDomainCount = 2,
                Sku = new Azure.ResourceManager.Compute.Models.Sku() { Name = skuName }
            };

            return new ArmBuilder<PhAvailabilitySet>(new AvailabilitySetContainer(operations, operations.Id), new PhAvailabilitySet(availabilitySet));
        }

        #endregion
    }
}
