using Azure.ResourceManager.Compute.Models;
using azure_proto_core;
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

        public static VmOperations Vm(this ResourceGroupOperations operations, string name)
        {
            return new VmOperations(operations, $"{operations.Context}/providers/Microsoft.Compute/virtualMachines/{name}");
        }
        public static VmOperations Vm(this ResourceGroupOperations operations, ResourceIdentifier vm)
        {
            return new VmOperations(operations, vm);
        }

        public static VmOperations Vm(this ResourceGroupOperations operations, TrackedResource vm)
        {
            return new VmOperations(operations, vm);
        }

        public static ArmOperation<ResourceOperations<PhVirtualMachine>> CreateVm(this ResourceGroupOperations operations, string name, PhVirtualMachine resourceDetails)
        {
            var container = new VmContainer(operations, operations.Context);
            return container.Create(name, resourceDetails);
        }

        public static Task<ArmOperation<ResourceOperations<PhVirtualMachine>>> CreateVmAsync(this ResourceGroupOperations operations, string name, PhVirtualMachine resourceDetails, CancellationToken cancellationToken = default)
        {
            var container = new VmContainer(operations, operations.Context);
            return container.CreateAsync(name, resourceDetails);
        }

        public static PhVirtualMachine ConstructVm(this ResourceGroupOperations operations, string vmName, string adminUser, string adminPw, ResourceIdentifier nicId, PhAvailabilitySet aset, Location location = null)
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

            return new PhVirtualMachine(vm);
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

        public static ArmOperation<ResourceOperations<PhAvailabilitySet>> CreateAvailabilitySet(this ResourceGroupOperations operations, string name, PhAvailabilitySet resourceDetails)
        {
            var container = new AvailabilitySetContainer(operations, operations.Context);
            return container.Create(name, resourceDetails);
        }

        public static Task<ArmOperation<ResourceOperations<PhAvailabilitySet>>> CreateAvailabilitySetAsync(this ResourceGroupOperations operations, string name, PhAvailabilitySet resourceDetails, CancellationToken cancellationToken = default)
        {
            var container = new AvailabilitySetContainer(operations, operations.Context);
            return container.CreateAsync(name, resourceDetails);
        }


        public static PhAvailabilitySet ConstructAvailabilitySet(this ResourceGroupOperations operations, string skuName, Location location = null)
        {
            var availabilitySet = new AvailabilitySet(location ?? operations.DefaultLocation)
            {
                PlatformUpdateDomainCount = 5,
                PlatformFaultDomainCount = 2,
                Sku = new Azure.ResourceManager.Compute.Models.Sku() { Name = skuName },
            };

            return new PhAvailabilitySet(availabilitySet);
        }

        #endregion

    }
}
