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
        public VirtualMachineContainer(ArmClientBase parent, ResourceIdentifier context) : base(parent, context)
        {
        }

        public VirtualMachineContainer(ArmClientBase parent, azure_proto_core.Resource context) : base(parent, context)
        {
            
        }

        protected override ResourceType ResourceType => "Microsoft.Compute/virtualMachines";

        public override ArmOperation<ResourceClientBase<PhVirtualMachine>> Create(string name, PhVirtualMachine resourceDetails = null)
        {
            resourceDetails ??= Resource as PhVirtualMachine;
            return new PhArmOperation<ResourceClientBase<PhVirtualMachine>, VirtualMachine>(Operations.StartCreateOrUpdate(base.Context.ResourceGroup, name, resourceDetails.Model), v => Vm(new PhVirtualMachine(v)));
        }

        public async override Task<ArmOperation<ResourceClientBase<PhVirtualMachine>>> CreateAsync(string name, PhVirtualMachine resourceDetails, CancellationToken cancellationToken = default)
        {
            return new PhArmOperation<ResourceClientBase<PhVirtualMachine>, VirtualMachine>(await Operations.StartCreateOrUpdateAsync(base.Context.ResourceGroup, name, resourceDetails.Model, cancellationToken), v =>  Vm(new PhVirtualMachine(v)));
        }

        public VirtualMachineOperations Vm(string vmName)
        {
            return new VirtualMachineOperations(this, new ResourceIdentifier($"{Context}/providers/Microsoft.Compute/virtualMachines/{vmName}"));
        }

        public VirtualMachineOperations vm(ResourceIdentifier vm)
        {
            return new VirtualMachineOperations(this, vm);
        }

        public VirtualMachineOperations Vm(TrackedResource vm)
        {
            return new VirtualMachineOperations(this, vm);
        }

        internal VirtualMachinesOperations Operations => new ComputeManagementClient(BaseUri, Context.Subscription, Credential).VirtualMachines;

    }
}
