using Azure.ResourceManager.Compute;
using Azure.ResourceManager.Compute.Models;
using azure_proto_core;
using System.Threading;
using System.Threading.Tasks;

namespace azure_proto_compute
{
    /// <summary>
    /// Vm Operations over a resource group
    /// </summary>
    public class VirtualMachineContainer : ResourceContainerOperations<PhVirtualMachine>
    {
        public VirtualMachineContainer(ArmClientContext parent, ResourceIdentifier context) : base(parent, context) { }

        public VirtualMachineContainer(ArmClientContext parent, azure_proto_core.Resource context) : base(parent, context) { }

        public VirtualMachineContainer(OperationsBase parent, ResourceIdentifier context) : base(parent, context) { }

        public VirtualMachineContainer(OperationsBase parent, azure_proto_core.Resource context) : base(parent, context) { }

        public override ResourceType ResourceType => "Microsoft.Compute/virtualMachines";

        public VirtualMachineOperations VirtualMachine(string vmName)
        {
            return new VirtualMachineOperations(this, new ResourceIdentifier($"{Id}/providers/Microsoft.Compute/virtualMachines/{vmName}"));
        }

        public VirtualMachineOperations VirtualMachine(ResourceIdentifier vm)
        {
            return new VirtualMachineOperations(this, vm);
        }

        public VirtualMachineOperations VirtualMachine(TrackedResource vm)
        {
            return new VirtualMachineOperations(this, vm);
        }
        public override ArmOperation<ResourceOperationsBase<PhVirtualMachine>> Create(string name, PhVirtualMachine resourceDetails, CancellationToken cancellationToken = default)
        {
            var operation = Operations.StartCreateOrUpdate(base.Id.ResourceGroup, name, resourceDetails.Model, cancellationToken);
            return new PhArmOperation<ResourceOperationsBase<PhVirtualMachine>, VirtualMachine>(
                operation.WaitForCompletionAsync().ConfigureAwait(false).GetAwaiter().GetResult(),
                v => VirtualMachine(new PhVirtualMachine(v)));
        }

        public async override Task<ArmOperation<ResourceOperationsBase<PhVirtualMachine>>> CreateAsync(string name, PhVirtualMachine resourceDetails, CancellationToken cancellationToken = default)
        {
            var operation = await Operations.StartCreateOrUpdateAsync(Id.ResourceGroup, name, resourceDetails.Model, cancellationToken);
            return new PhArmOperation<ResourceOperationsBase<PhVirtualMachine>, VirtualMachine>(
                operation.WaitForCompletionAsync().ConfigureAwait(false).GetAwaiter().GetResult(),
                v => VirtualMachine(new PhVirtualMachine(v)));
        }

        public override ArmOperation<ResourceOperationsBase<PhVirtualMachine>> StartCreate(string name, PhVirtualMachine resourceDetails, CancellationToken cancellationToken = default)
        {
            return new PhArmOperation<ResourceOperationsBase<PhVirtualMachine>, VirtualMachine>(
                Operations.StartCreateOrUpdate(Id.ResourceGroup, name, resourceDetails.Model, cancellationToken),
                v => VirtualMachine(new PhVirtualMachine(v)));
        }

        public async override Task<ArmOperation<ResourceOperationsBase<PhVirtualMachine>>> StartCreateAsync(string name, PhVirtualMachine resourceDetails, CancellationToken cancellationToken = default)
        {
            return new PhArmOperation<ResourceOperationsBase<PhVirtualMachine>, VirtualMachine>(
                await Operations.StartCreateOrUpdateAsync(Id.ResourceGroup, name, resourceDetails.Model, cancellationToken),
                v => VirtualMachine(new PhVirtualMachine(v)));
        }

        internal VirtualMachinesOperations Operations => this.GetClient<ComputeManagementClient>((baseUri, cred) =>  new ComputeManagementClient(baseUri, Id.Subscription, cred)).VirtualMachines;
    }
}
