using Azure.ResourceManager.Compute;
using azure_proto_compute.Placeholder;
using azure_proto_core;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace azure_proto_compute
{
    /// <summary>
    /// Vm Operations over a resource group
    /// </summary>
    public class VmContainer : ArmResourceContainerOperations<PhVirtualMachine, PhVmValueOperation>
    {
        public VmContainer(ArmOperations parent, ResourceIdentifier context) : base(parent, context)
        {
        }

        public VmContainer(ArmOperations parent, azure_proto_core.Resource context) : base(parent, context)
        {
        }

        protected override ResourceType ResourceType => "Microsoft.Compute/virtualMachines";

        public override PhVmValueOperation Create(string name, PhVirtualMachine resourceDetails)
        {
            return new PhVmValueOperation(VmOperations.StartCreateOrUpdate(Context.ResourceGroup, name, resourceDetails.Model));
        }

        public async override Task<PhVmValueOperation> CreateAsync(string name, PhVirtualMachine resourceDetails, CancellationToken cancellationToken = default)
        {
            return new PhVmValueOperation(await VmOperations.StartCreateOrUpdateAsync(Context.ResourceGroup, name, resourceDetails.Model, cancellationToken));
        }

        public VmOperations Operations(string vmName)
        {
            return new VmOperations(this, new ResourceIdentifier($"{Context}/providers/Microsoft.Compute/virtualMachines/{vmName}"));
        }

        public VmOperations Operations(ResourceIdentifier vm)
        {
            return new VmOperations(this, vm);
        }

        public VmOperations Operations(TrackedResource vm)
        {
            return new VmOperations(this, vm);
        }

        internal VirtualMachinesOperations VmOperations => new ComputeManagementClient(BaseUri, Context.Subscription, Credential).VirtualMachines;

    }
}
