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
        public VirtualMachineContainer(ArmClientContext parent, ResourceIdentifier context) : base(parent, context)
        {
        }

        public VirtualMachineContainer(ArmClientContext parent, azure_proto_core.Resource context) : base(parent, context)
        {
        }

        public VirtualMachineContainer(OperationsBase parent, ResourceIdentifier context) : base(parent, context)
        {
        }

        public VirtualMachineContainer(OperationsBase parent, azure_proto_core.Resource context) : base(parent, context)
        {
        }

        public override ResourceType ResourceType => "Microsoft.Compute/virtualMachines";

        public override ArmOperation<ResourceOperationsBase<PhVirtualMachine>> Create(string name, PhVirtualMachine resourceDetails = null)
        {
            resourceDetails ??= Resource as PhVirtualMachine;
            var operation = Operations.StartCreateOrUpdate(base.Id.ResourceGroup, name, resourceDetails.Model);
            return new PhArmOperation<ResourceOperationsBase<PhVirtualMachine>, VirtualMachine>(operation.WaitForCompletionAsync().ConfigureAwait(false).GetAwaiter().GetResult(), v => Vm(new PhVirtualMachine(v)));
        }

        public async override Task<ArmOperation<ResourceOperationsBase<PhVirtualMachine>>> CreateAsync(string name, PhVirtualMachine resourceDetails, CancellationToken cancellationToken = default)
        {
            return new PhArmOperation<ResourceOperationsBase<PhVirtualMachine>, VirtualMachine>(await Operations.StartCreateOrUpdateAsync(base.Id.ResourceGroup, name, resourceDetails.Model, cancellationToken), v =>  Vm(new PhVirtualMachine(v)));
        }

        public VirtualMachineOperations Vm(string vmName)
        {
            return new VirtualMachineOperations(this, new ResourceIdentifier($"{Id}/providers/Microsoft.Compute/virtualMachines/{vmName}"));
        }

        public VirtualMachineOperations vm(ResourceIdentifier vm)
        {
            return new VirtualMachineOperations(this, vm);
        }

        public VirtualMachineOperations Vm(TrackedResource vm)
        {
            return new VirtualMachineOperations(this, vm);
        }

        internal VirtualMachinesOperations Operations => this.GetClient<ComputeManagementClient>((baseUri, cred) =>  new ComputeManagementClient(baseUri, Id.Subscription, cred)).VirtualMachines;

    }
}
