using Azure;
using Azure.ResourceManager.Compute;
using Azure.ResourceManager.Compute.Models;
using azure_proto_compute.Convenience;
using Azure.ResourceManager.Core;
using Azure.ResourceManager.Core.Adapters;
using Azure.ResourceManager.Core.Resources;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace azure_proto_compute
{
    /// <summary>
    /// Vm Operations over a resource group
    /// </summary>
    /// We should not expose Create method when a container is constructed at a subscription level as an example for a virtual machine.
    /// Likewise we should not expose create when a subnet container is constructed at a resource group level
    public class VirtualMachineContainer : ResourceContainerBase<VirtualMachine, VirtualMachineData>
    {
        internal VirtualMachineContainer(AzureResourceManagerClientOptions options, ResourceGroupData resourceGroup)
            : base(options, resourceGroup)
        {
        }

        internal VirtualMachineContainer(AzureResourceManagerClientOptions options, ResourceIdentifier id)
            : base(options, id)
        {
        }

        public override ArmResponse<VirtualMachine> Create(string name, VirtualMachineData resourceDetails, CancellationToken cancellationToken = default)
        {
            var operation = Operations.StartCreateOrUpdate(Id.ResourceGroup, name, resourceDetails.Model, cancellationToken);
            return new PhArmResponse<VirtualMachine, Azure.ResourceManager.Compute.Models.VirtualMachine>(
                operation.WaitForCompletionAsync(cancellationToken).ConfigureAwait(false).GetAwaiter().GetResult(),
                v => new VirtualMachine(ClientOptions, new VirtualMachineData(v)));
        }

        public async override Task<ArmResponse<VirtualMachine>> CreateAsync(string name, VirtualMachineData resourceDetails, CancellationToken cancellationToken = default)
        {
            var operation = await Operations.StartCreateOrUpdateAsync(Id.ResourceGroup, name, resourceDetails.Model, cancellationToken).ConfigureAwait(false);
            return new PhArmResponse<VirtualMachine, Azure.ResourceManager.Compute.Models.VirtualMachine>(
                await operation.WaitForCompletionAsync(cancellationToken).ConfigureAwait(false),
                v => new VirtualMachine(ClientOptions, new VirtualMachineData(v)));
        }

        public override ArmOperation<VirtualMachine> StartCreate(string name, VirtualMachineData resourceDetails, CancellationToken cancellationToken = default)
        {
            return new PhArmOperation<VirtualMachine, Azure.ResourceManager.Compute.Models.VirtualMachine>(
                Operations.StartCreateOrUpdate(Id.ResourceGroup, name, resourceDetails.Model, cancellationToken),
                v => new VirtualMachine(ClientOptions, new VirtualMachineData(v)));
        }

        public async override Task<ArmOperation<VirtualMachine>> StartCreateAsync(string name, VirtualMachineData resourceDetails, CancellationToken cancellationToken = default)
        {
            return new PhArmOperation<VirtualMachine, Azure.ResourceManager.Compute.Models.VirtualMachine>(
                await Operations.StartCreateOrUpdateAsync(Id.ResourceGroup, name, resourceDetails.Model, cancellationToken).ConfigureAwait(false),
                v => new VirtualMachine(ClientOptions, new VirtualMachineData(v)));
        }

        public VirtualMachineModelBuilder Construct(string vmName, string adminUser, string adminPw, ResourceIdentifier nicId, AvailabilitySetData aset, Location location = null)
        {
            var vm = new Azure.ResourceManager.Compute.Models.VirtualMachine(location ?? DefaultLocation)
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

            return new VirtualMachineModelBuilder(this, new VirtualMachineData(vm));
        }

        public VirtualMachineModelBuilder Construct(string name, Location location)
        {
            return new VirtualMachineModelBuilder(null, null);
        }

        public Pageable<VirtualMachine> List(CancellationToken cancellationToken = default)
        {
            var result = Operations.List(Id.Name, cancellationToken);
            return new PhWrappingPageable<Azure.ResourceManager.Compute.Models.VirtualMachine, VirtualMachine>(
                result,
                s => new VirtualMachine(ClientOptions, new VirtualMachineData(s)));
        }

        public AsyncPageable<VirtualMachine> ListAsync(CancellationToken cancellationToken = default)
        {
            var result = Operations.ListAsync(Id.Name, cancellationToken);
            return new PhWrappingAsyncPageable<Azure.ResourceManager.Compute.Models.VirtualMachine, VirtualMachine>(
                result,
                s => new VirtualMachine(ClientOptions, new VirtualMachineData(s)));
        }

        public Pageable<ArmResource> ListByName(ArmSubstringFilter filter, int? top = null, CancellationToken cancellationToken = default)
        {
            ArmFilterCollection filters = new ArmFilterCollection(VirtualMachineData.ResourceType);
            filters.SubstringFilter = filter;
            return ResourceListOperations.ListAtContext<ArmResource, ArmResourceData>(ClientOptions, Id, filters, top, cancellationToken);
        }

        public AsyncPageable<ArmResource> ListByNameAsync(ArmSubstringFilter filter, int? top = null, CancellationToken cancellationToken = default)
        {
            ArmFilterCollection filters = new ArmFilterCollection(VirtualMachineData.ResourceType);
            filters.SubstringFilter = filter;
            return ResourceListOperations.ListAtContextAsync<ArmResource, ArmResourceData>(ClientOptions, Id, filters, top, cancellationToken);
        }

        public Pageable<VirtualMachine> ListByNameExpanded(ArmSubstringFilter filter, int? top = null, CancellationToken cancellationToken = default)
        {
            var results = ListByName(filter, top, cancellationToken);
            return new PhWrappingPageable<ArmResource, VirtualMachine>(results, s => (new VirtualMachineOperations(s)).Get().Value);
        }

        public AsyncPageable<VirtualMachine> ListByNameExpandedAsync(ArmSubstringFilter filter, int? top = null, CancellationToken cancellationToken = default)
        {
            var results = ListByNameAsync(filter, top, cancellationToken);
            return new PhWrappingAsyncPageable<ArmResource, VirtualMachine>(results, s => (new VirtualMachineOperations(s)).Get().Value);
        }

        internal VirtualMachinesOperations Operations => this.GetClient((baseUri, cred) => new ComputeManagementClient(baseUri, Id.Subscription, cred, 
                    ClientOptions.Convert<ComputeManagementClientOptions>())).VirtualMachines;
    }
}
