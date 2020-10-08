using Azure;
using Azure.ResourceManager.Compute;
using Azure.ResourceManager.Compute.Models;
using azure_proto_compute.Convenience;
using azure_proto_core;
using azure_proto_core.Resources;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Threading;
using System.Threading.Tasks;

namespace azure_proto_compute
{
    /// <summary>
    /// Vm Operations over a resource group
    /// </summary>
    public class VirtualMachineContainer : ResourceContainerOperations<VirtualMachineOperations, PhVirtualMachine>
    {
        public VirtualMachineContainer(ArmClientContext parent, ResourceIdentifier context) : base(parent, context) { }

        public VirtualMachineContainer(ArmClientContext parent, azure_proto_core.Resource context) : base(parent, context) { }

        public VirtualMachineContainer(OperationsBase parent, ResourceIdentifier context) : base(parent, context) { }

        public VirtualMachineContainer(OperationsBase parent, azure_proto_core.Resource context) : base(parent, context) { }

        public override ResourceType ResourceType => "Microsoft.Compute/virtualMachines";

        public override ArmResponse<VirtualMachineOperations> Create(string name, PhVirtualMachine resourceDetails, CancellationToken cancellationToken = default)
        {
            var operation = Operations.StartCreateOrUpdate(base.Id.ResourceGroup, name, resourceDetails.Model, cancellationToken);
            return new PhArmResponse<VirtualMachineOperations, VirtualMachine>(
                operation.WaitForCompletionAsync(cancellationToken).ConfigureAwait(false).GetAwaiter().GetResult(),
                v => new VirtualMachineOperations(this, new PhVirtualMachine(v)));
        }

        public async override Task<ArmResponse<VirtualMachineOperations>> CreateAsync(string name, PhVirtualMachine resourceDetails, CancellationToken cancellationToken = default)
        {
            var operation = await Operations.StartCreateOrUpdateAsync(Id.ResourceGroup, name, resourceDetails.Model, cancellationToken).ConfigureAwait(false);
            return new PhArmResponse<VirtualMachineOperations, VirtualMachine>(
                await operation.WaitForCompletionAsync(cancellationToken).ConfigureAwait(false),
                v => new VirtualMachineOperations(this, new PhVirtualMachine(v)));
        }

        public override ArmOperation<VirtualMachineOperations> StartCreate(string name, PhVirtualMachine resourceDetails, CancellationToken cancellationToken = default)
        {
            return new PhArmOperation<VirtualMachineOperations, VirtualMachine>(
                Operations.StartCreateOrUpdate(Id.ResourceGroup, name, resourceDetails.Model, cancellationToken),
                v => new VirtualMachineOperations(this, new PhVirtualMachine(v)));
        }

        public async override Task<ArmOperation<VirtualMachineOperations>> StartCreateAsync(string name, PhVirtualMachine resourceDetails, CancellationToken cancellationToken = default)
        {
            return new PhArmOperation<VirtualMachineOperations, VirtualMachine>(
                await Operations.StartCreateOrUpdateAsync(Id.ResourceGroup, name, resourceDetails.Model, cancellationToken).ConfigureAwait(false),
                v => new VirtualMachineOperations(this, new PhVirtualMachine(v)));
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

            return new VirtualMachineModelBuilder(new VirtualMachineContainer(this, Id), new PhVirtualMachine(vm));
        }

        public VirtualMachineModelBuilder Construct(string name, Location location)
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
        public Pageable<VirtualMachineOperations> List(ArmSubstringFilter filter = null, int? top = null, CancellationToken cancellationToken = default)
        {
            return ResourceListOperations.ListAtContext<VirtualMachineOperations, PhVirtualMachine>(ClientContext, Id.Parent, filter, top, cancellationToken);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="subscription"></param>
        /// <param name="filter"></param>
        /// <param name="top"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public AsyncPageable<VirtualMachineOperations> ListAsync(ArmSubstringFilter filter = null, int? top = null, CancellationToken cancellationToken = default)
        {
            return ResourceListOperations.ListAtContextAsync<VirtualMachineOperations, PhVirtualMachine>(ClientContext, Id.Parent, filter, top, cancellationToken);
        }

        //TODO: add rp specific filter example
        public IEnumerable<VirtualMachineOperations> ListByTag(ArmTagFilter filter, int? top = null, CancellationToken cancellationToken = default)
        {
            var vms = ResourceListOperations.ListAtContext<VirtualMachineOperations, PhVirtualMachine>(ClientContext, Id.Parent, null, top, cancellationToken);
            foreach(var vm in vms)
            {
                string value;
                if (vm.Model.Tags.TryGetValue(filter.Key, out value))
                {
                    if (value == filter.Value)
                        yield return vm;
                }
            }
        }

        public async IAsyncEnumerable<VirtualMachineOperations> ListByTagAsync(ArmTagFilter filter, int? top = null, [EnumeratorCancellation]CancellationToken cancellationToken = default)
        {
            var vms = ResourceListOperations.ListAtContextAsync<VirtualMachineOperations, PhVirtualMachine>(ClientContext, Id.Parent, null, top, cancellationToken);
            await foreach (var vm in vms)
            {
                string value;
                if (vm.Model.Tags.TryGetValue(filter.Key, out value))
                {
                    if (value == filter.Value)
                        yield return vm;
                }
            }
        }

        internal VirtualMachinesOperations Operations => this.GetClient((baseUri, cred) =>  new ComputeManagementClient(baseUri, Id.Subscription, cred)).VirtualMachines;
    }
}
