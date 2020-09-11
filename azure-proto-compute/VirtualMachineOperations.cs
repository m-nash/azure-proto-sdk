using Azure;
using Azure.ResourceManager.Compute;
using Azure.ResourceManager.Compute.Models;
using azure_proto_core;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace azure_proto_compute
{
    /// <summary>
    /// Operations over a single virtual machine
    /// </summary>
    public class VirtualMachineOperations : ResourceOperationsBase<PhVirtualMachine>
    {
        public VirtualMachineOperations(ArmClientContext parent, TrackedResource context) : base(parent, context) { }

        public VirtualMachineOperations(ArmClientContext parent, ResourceIdentifier context) : base(parent, context) { }

        public VirtualMachineOperations(OperationsBase parent, TrackedResource context) : base(parent, context) { }

        public VirtualMachineOperations(OperationsBase parent, ResourceIdentifier context) : base(parent, context) { }

        public override ResourceType ResourceType => "Microsoft.Compute/virtualMachines";

        public override ArmOperation<Response> Delete()
        {
            return new ArmVoidOperation(Operations.StartDelete(Id.ResourceGroup, Id.Name));
        }

        public async override Task<ArmOperation<Response>> DeleteAsync(CancellationToken cancellationToken = default)
        {
            return new ArmVoidOperation(await Operations.StartDeleteAsync(Id.ResourceGroup, Id.Name, cancellationToken));
        }

        public ArmOperation<Response> Start()
        {
            return new ArmVoidOperation(Operations.StartStart(Id.ResourceGroup, Id.Name));
        }

        public async Task<ArmOperation<Response>> StartAsync(CancellationToken cancellationToken = default)
        {
            return new ArmVoidOperation(await Operations.StartStartAsync(Id.ResourceGroup, Id.Name, cancellationToken));
        }

        public ArmOperation<Response> Stop(bool? skipShutdown = null)
        {
            return new ArmVoidOperation(Operations.StartPowerOff(Id.ResourceGroup, Id.Name, skipShutdown));
        }

        public async Task<ArmOperation<Response>> StopAsync(bool? skipShutdown = null, CancellationToken cancellationToken = default)
        {
            return new ArmVoidOperation(await Operations.StartPowerOffAsync(Id.ResourceGroup, Id.Name, skipShutdown, cancellationToken));
        }

        public override Response<ResourceOperationsBase<PhVirtualMachine>> Get()
        {
            return new PhArmResponse<ResourceOperationsBase<PhVirtualMachine>, VirtualMachine>(Operations.Get(Id.ResourceGroup, Id.Name), v => { Resource = new PhVirtualMachine(v); return this; } );
        }

        public async override Task<Response<ResourceOperationsBase<PhVirtualMachine>>> GetAsync(CancellationToken cancellationToken = default)
        {
            return new PhArmResponse<ResourceOperationsBase<PhVirtualMachine>, VirtualMachine>(await Operations.GetAsync(Id.ResourceGroup, Id.Name, cancellationToken), v => { Resource = new PhVirtualMachine(v); return this; });
        }

        public ArmOperation<ResourceOperationsBase<PhVirtualMachine>> Update(VirtualMachineUpdate patchable)
        {
            return new PhArmOperation<ResourceOperationsBase<PhVirtualMachine>, VirtualMachine>(Operations.StartUpdate(Id.ResourceGroup, Id.Name, patchable), v => { Resource = new PhVirtualMachine(v); return this; });
        }

        public async Task<ArmOperation<ResourceOperationsBase<PhVirtualMachine>>> UpdateAsync(VirtualMachineUpdate patchable, CancellationToken cancellationToken = default)
        {
            return new PhArmOperation<ResourceOperationsBase<PhVirtualMachine>, VirtualMachine>(await Operations.StartUpdateAsync(Id.ResourceGroup, Id.Name, patchable, cancellationToken), v => { Resource = new PhVirtualMachine(v); return this; });
        }

        public override ArmOperation<ResourceOperationsBase<PhVirtualMachine>> AddTag(string key, string value)
        {
            var patchable = new VirtualMachineUpdate { Tags= new Dictionary<string, string>(StringComparer.InvariantCultureIgnoreCase)};
            patchable.Tags.Add(key, value);
            return new PhArmOperation<ResourceOperationsBase<PhVirtualMachine>, VirtualMachine>(Operations.StartUpdate(Id.ResourceGroup, Id.Name, patchable), v => { Resource = new PhVirtualMachine(v); return this; });
        }

        public override async Task<ArmOperation<ResourceOperationsBase<PhVirtualMachine>>> AddTagAsync(string key, string value, CancellationToken cancellationToken = default)
        {
            var patchable = new VirtualMachineUpdate { Tags = new Dictionary<string, string>(StringComparer.InvariantCultureIgnoreCase) };
            patchable.Tags.Add(key, value);
            return new PhArmOperation<ResourceOperationsBase<PhVirtualMachine>, VirtualMachine>(await Operations.StartUpdateAsync(Id.ResourceGroup, Id.Name, patchable, cancellationToken), v => { Resource = new PhVirtualMachine(v); return this; });
        }

        internal VirtualMachinesOperations Operations => GetClient<ComputeManagementClient>((baseUri, creds) => new ComputeManagementClient(baseUri, Id.Subscription, creds)).VirtualMachines;
    }
}
