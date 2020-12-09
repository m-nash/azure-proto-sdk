using Azure;
using Azure.ResourceManager.Compute;
using Azure.ResourceManager.Compute.Models;
using azure_proto_compute.Convenience;
using azure_proto_core;
using azure_proto_core.Adapters;
using azure_proto_core.Resources;
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
    public class VirtualMachineContainer : ResourceContainerOperations<XVirtualMachine, PhVirtualMachine>
    {
        public VirtualMachineContainer(ArmClientContext context, PhResourceGroup resourceGroup, ArmClientOptions clientOptions) : base(context, resourceGroup, clientOptions) { }

        internal VirtualMachineContainer(ArmClientContext context, ResourceIdentifier id, ArmClientOptions clientOptions):base(context, id, clientOptions){ }

        public override ResourceType ResourceType => "Microsoft.Compute/virtualMachines";

        public override ArmResponse<XVirtualMachine> Create(string name, PhVirtualMachine resourceDetails, CancellationToken cancellationToken = default)
        {
            var operation = Operations.StartCreateOrUpdate(base.Id.ResourceGroup, name, resourceDetails.Model, cancellationToken);
            return new PhArmResponse<XVirtualMachine, VirtualMachine>(
                operation.WaitForCompletionAsync(cancellationToken).ConfigureAwait(false).GetAwaiter().GetResult(),
                v => new XVirtualMachine(ClientContext, new PhVirtualMachine(v), ClientOptions));
        }

        public async override Task<ArmResponse<XVirtualMachine>> CreateAsync(string name, PhVirtualMachine resourceDetails, CancellationToken cancellationToken = default)
        {
            var operation = await Operations.StartCreateOrUpdateAsync(Id.ResourceGroup, name, resourceDetails.Model, cancellationToken).ConfigureAwait(false);
            return new PhArmResponse<XVirtualMachine, VirtualMachine>(
                await operation.WaitForCompletionAsync(cancellationToken).ConfigureAwait(false),
                v => new XVirtualMachine(ClientContext, new PhVirtualMachine(v), ClientOptions));
        }

        public override ArmOperation<XVirtualMachine> StartCreate(string name, PhVirtualMachine resourceDetails, CancellationToken cancellationToken = default)
        {
            return new PhArmOperation<XVirtualMachine, VirtualMachine>(
                Operations.StartCreateOrUpdate(Id.ResourceGroup, name, resourceDetails.Model, cancellationToken),
                v => new XVirtualMachine(ClientContext, new PhVirtualMachine(v), ClientOptions));
        }

        public async override Task<ArmOperation<XVirtualMachine>> StartCreateAsync(string name, PhVirtualMachine resourceDetails, CancellationToken cancellationToken = default)
        {
            return new PhArmOperation<XVirtualMachine, VirtualMachine>(
                await Operations.StartCreateOrUpdateAsync(Id.ResourceGroup, name, resourceDetails.Model, cancellationToken).ConfigureAwait(false),
                v => new XVirtualMachine(ClientContext, new PhVirtualMachine(v), ClientOptions));
        }

        public VirtualMachineModelBuilder Construct(string vmName, string adminUser, string adminPw, ResourceIdentifier nicId, PhAvailabilitySet aset, Location location = null)
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

            return new VirtualMachineModelBuilder(this, new PhVirtualMachine(vm));
        }

        public VirtualMachineModelBuilder Construct(string name, Location location)
        {
            return new VirtualMachineModelBuilder(null, null);
        }

        public Pageable<XVirtualMachine> List(CancellationToken cancellationToken = default)
        {
            var result = Operations.List(Id.Name, cancellationToken);
            return new PhWrappingPageable<VirtualMachine, XVirtualMachine>(
                result,
                s => new XVirtualMachine(ClientContext, new PhVirtualMachine(s), ClientOptions));
        }

        public AsyncPageable<XVirtualMachine> ListAsync(CancellationToken cancellationToken = default)
        {
            var result = Operations.ListAsync(Id.Name, cancellationToken);
            return new PhWrappingAsyncPageable<VirtualMachine, XVirtualMachine>(
                result,
                s => new XVirtualMachine(ClientContext, new PhVirtualMachine(s), ClientOptions));
        }

        public Pageable<ArmResourceOperations> ListByName(ArmSubstringFilter filter, int? top = null, CancellationToken cancellationToken = default)
        {
            ArmFilterCollection filters = new ArmFilterCollection(PhVirtualMachine.ResourceType);
            filters.SubstringFilter = filter;
            return ResourceListOperations.ListAtContext<ArmResourceOperations, ArmResource>(ClientContext, ClientOptions, Id, filters, top, cancellationToken);
        }

        public AsyncPageable<ArmResourceOperations> ListByNameAsync(ArmSubstringFilter filter, int? top = null, CancellationToken cancellationToken = default)
        {
            ArmFilterCollection filters = new ArmFilterCollection(PhVirtualMachine.ResourceType);
            filters.SubstringFilter = filter;
            return ResourceListOperations.ListAtContextAsync<ArmResourceOperations, ArmResource>(ClientContext, ClientOptions, Id, filters, top, cancellationToken);
        }

        public Pageable<XVirtualMachine> ListByNameExpanded(ArmSubstringFilter filter, int? top = null, CancellationToken cancellationToken = default)
        {
            var results = ListByName(filter, top, cancellationToken);
            return new PhWrappingPageable<ArmResourceOperations, XVirtualMachine>(results, s => (new VirtualMachineOperations(s)).Get().Value);
        }

        public AsyncPageable<XVirtualMachine> ListByNameExpandedAsync(ArmSubstringFilter filter, int? top = null, CancellationToken cancellationToken = default)
        {
            var results = ListByNameAsync(filter, top, cancellationToken);
            return new PhWrappingAsyncPageable<ArmResourceOperations, XVirtualMachine>(results, s => (new VirtualMachineOperations(s)).Get().Value);
        }

        internal VirtualMachinesOperations Operations => this.GetClient((baseUri, cred) => new ComputeManagementClient(baseUri, Id.Subscription, cred, 
                    ArmClientOptions.Convert<ComputeManagementClientOptions>(ClientOptions))).VirtualMachines;
    }
}
