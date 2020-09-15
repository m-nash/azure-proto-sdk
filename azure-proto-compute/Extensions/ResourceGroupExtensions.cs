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
        public static VirtualMachineOperations VirtualMachine(this ResourceGroupOperations resourceGroup, string name)
        {
            return new VirtualMachineOperations(resourceGroup, $"{resourceGroup.Id}/providers/Microsoft.Compute/virtualMachines/{name}");
        }

        public static VirtualMachineOperations VirtualMachine(this ResourceGroupOperations resourceGroup, ResourceIdentifier vm)
        {
            return new VirtualMachineOperations(resourceGroup, vm);
        }

        public static VirtualMachineOperations VirtualMachine(this ResourceGroupOperations resourceGroup, TrackedResource vm)
        {
            return new VirtualMachineOperations(resourceGroup, vm);
        }

        public static ArmResponse<ResourceOperationsBase<PhVirtualMachine>> CreateVirtualMachine(this ResourceGroupOperations resourceGroup, string name, PhVirtualMachine resourceDetails, CancellationToken cancellationToken = default)
        {
            var container = new VirtualMachineContainer(resourceGroup, resourceGroup.Id);
            return container.Create(name, resourceDetails, cancellationToken);
        }

        public static Task<ArmResponse<ResourceOperationsBase<PhVirtualMachine>>> CreateVirtualMachineAsync(this ResourceGroupOperations resourceGroup, string name, PhVirtualMachine resourceDetails, CancellationToken cancellationToken = default)
        {
            var container = new VirtualMachineContainer(resourceGroup, resourceGroup.Id);
            return container.CreateAsync(name, resourceDetails, cancellationToken);
        }

        public static ArmOperation<ResourceOperationsBase<PhVirtualMachine>> StartCreateVirtualMachine(this ResourceGroupOperations resourceGroup, string name, PhVirtualMachine resourceDetails, CancellationToken cancellationToken = default)
        {
            var container = new VirtualMachineContainer(resourceGroup, resourceGroup.Id);
            return container.StartCreate(name, resourceDetails, cancellationToken);
        }

        public static Task<ArmOperation<ResourceOperationsBase<PhVirtualMachine>>> StartCreateVirtualMachineAsync(this ResourceGroupOperations resourceGroup, string name, PhVirtualMachine resourceDetails, CancellationToken cancellationToken = default)
        {
            var container = new VirtualMachineContainer(resourceGroup, resourceGroup.Id);
            return container.StartCreateAsync(name, resourceDetails, cancellationToken);
        }

        //TODO: Should this hang off of resourceGroup.Compute.ConstructVirtualMachine??
        public static VirtualMachineModelBuilder ConstructVirtualMachine(this ResourceGroupOperations operations, string vmName, string adminUser, string adminPw, ResourceIdentifier nicId, PhAvailabilitySet aset, Location location = null)
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

            return new VirtualMachineModelBuilder(new VirtualMachineContainer(operations, operations.Id), new PhVirtualMachine(vm));
        }

        public static VirtualMachineModelBuilder ConstructVirtualMachine(this ResourceGroupOperations operations, string name, Location location)
        {
            //TODO: Fix this case
            return new VirtualMachineModelBuilder(null, null);
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
        public static AvailabilitySetOperations AvailabilitySets(this ResourceGroupOperations resourceGroup, string name)
        {
            return new AvailabilitySetOperations(resourceGroup, $"{resourceGroup.Id}/providers/Microsoft.Compute/virtualMachines/{name}");
        }
        public static AvailabilitySetOperations AvailabilitySets(this ResourceGroupOperations resourceGroup, ResourceIdentifier set)
        {
            return new AvailabilitySetOperations(resourceGroup, set);
        }

        public static AvailabilitySetOperations AvailabilitySets(this ResourceGroupOperations resourceGroup, TrackedResource set)
        {
            return new AvailabilitySetOperations(resourceGroup, set);
        }

        public static ArmResponse<ResourceOperationsBase<PhAvailabilitySet>> CreateAvailabilitySet(this ResourceGroupOperations resourceGroup, string name, PhAvailabilitySet resourceDetails, CancellationToken cancellationToken = default)
        {
            var container = new AvailabilitySetContainer(resourceGroup, resourceGroup.Id);
            return container.Create(name, resourceDetails, cancellationToken);
        }

        public static Task<ArmResponse<ResourceOperationsBase<PhAvailabilitySet>>> CreateAvailabilitySetAsync(this ResourceGroupOperations resourceGroup, string name, PhAvailabilitySet resourceDetails, CancellationToken cancellationToken = default)
        {
            var container = new AvailabilitySetContainer(resourceGroup, resourceGroup.Id);
            return container.CreateAsync(name, resourceDetails, cancellationToken);
        }

        public static ArmOperation<ResourceOperationsBase<PhAvailabilitySet>> StartCreateAvailabilitySet(this ResourceGroupOperations resourceGroup, string name, PhAvailabilitySet resourceDetails, CancellationToken cancellationToken = default)
        {
            var container = new AvailabilitySetContainer(resourceGroup, resourceGroup.Id);
            return container.StartCreate(name, resourceDetails, cancellationToken);
        }

        public static Task<ArmOperation<ResourceOperationsBase<PhAvailabilitySet>>> StartCreateAvailabilitySetAsync(this ResourceGroupOperations resourceGroup, string name, PhAvailabilitySet resourceDetails, CancellationToken cancellationToken = default)
        {
            var container = new AvailabilitySetContainer(resourceGroup, resourceGroup.Id);
            return container.StartCreateAsync(name, resourceDetails, cancellationToken);
        }

        public static ArmBuilder<PhAvailabilitySet> ConstructAvailabilitySet(this ResourceGroupOperations resourceGroup, string skuName, Location location = null)
        {
            var availabilitySet = new AvailabilitySet(location ?? resourceGroup.DefaultLocation)
            {
                PlatformUpdateDomainCount = 5,
                PlatformFaultDomainCount = 2,
                Sku = new Azure.ResourceManager.Compute.Models.Sku() { Name = skuName }
            };

            return new ArmBuilder<PhAvailabilitySet>(new AvailabilitySetContainer(resourceGroup, resourceGroup.Id), new PhAvailabilitySet(availabilitySet));
        }
        #endregion
    }
}
