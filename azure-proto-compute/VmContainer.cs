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
    public class VmContainer : ResourceContainerOperations<PhVirtualMachine>
    {
        public VmContainer(ArmOperations parent, ResourceIdentifier context) : base(parent, context)
        {
        }

        public VmContainer(ArmOperations parent, azure_proto_core.Resource context) : base(parent, context)
        {
            
        }

        protected override ResourceType ResourceType => "Microsoft.Compute/virtualMachines";

        public override ArmOperation<ResourceOperations<PhVirtualMachine>> Create(string name, PhVirtualMachine resourceDetails = null)
        {
            resourceDetails ??= Resource as PhVirtualMachine;
            return new PhArmOperation<ResourceOperations<PhVirtualMachine>, VirtualMachine>(Operations.StartCreateOrUpdate(base.Context.ResourceGroup, name, resourceDetails.Model), v => Vm(new PhVirtualMachine(v)));
        }

        public async override Task<ArmOperation<ResourceOperations<PhVirtualMachine>>> CreateAsync(string name, PhVirtualMachine resourceDetails, CancellationToken cancellationToken = default)
        {
            return new PhArmOperation<ResourceOperations<PhVirtualMachine>, VirtualMachine>(await Operations.StartCreateOrUpdateAsync(base.Context.ResourceGroup, name, resourceDetails.Model, cancellationToken), v =>  Vm(new PhVirtualMachine(v)));
        }

        public VmOperations Vm(string vmName)
        {
            return new VmOperations(this, new ResourceIdentifier($"{Context}/providers/Microsoft.Compute/virtualMachines/{vmName}"));
        }

        public VmOperations vm(ResourceIdentifier vm)
        {
            return new VmOperations(this, vm);
        }

        public VmOperations Vm(TrackedResource vm)
        {
            return new VmOperations(this, vm);
        }

        internal VirtualMachinesOperations Operations => new ComputeManagementClient(BaseUri, Context.Subscription, Credential).VirtualMachines;

    }
}
