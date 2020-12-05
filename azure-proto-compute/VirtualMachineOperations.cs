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
    public class VirtualMachineOperations : ResourceOperationsBase<VirtualMachineOperations, PhVirtualMachine>, ITaggable<VirtualMachineOperations, PhVirtualMachine>, IDeletableResource<VirtualMachineOperations, PhVirtualMachine>
    {
        public VirtualMachineOperations(ArmResourceOperations genericOperations, ArmClientOptions clientOptions) : base(genericOperations, clientOptions) { }

        public VirtualMachineOperations(ArmClientContext parent, TrackedResource context, ArmClientOptions clientOptions) : base(parent, context, clientOptions) { }

        public VirtualMachineOperations(ArmClientContext parent, ResourceIdentifier context, ArmClientOptions clientOptions) : base(parent, context, clientOptions) { }

        public override ResourceType ResourceType => "Microsoft.Compute/virtualMachines";

        public  ArmOperation<Response> Delete()
        {
            return new ArmVoidOperation(Operations.StartDelete(Id.ResourceGroup, Id.Name));
        }

        public async Task<ArmOperation<Response>> DeleteAsync(CancellationToken cancellationToken = default)
        {
            return new ArmVoidOperation(await Operations.StartDeleteAsync(Id.ResourceGroup, Id.Name, cancellationToken));
        }

        #region PowerOn
        public ArmResponse<Response> PowerOn(CancellationToken cancellationToken = default)
        {
            var operation = Operations.StartStart(Id.ResourceGroup, Id.Name, cancellationToken);
            return new ArmVoidResponse(operation.WaitForCompletionAsync(cancellationToken).ConfigureAwait(false).GetAwaiter().GetResult());
        }

        public async Task<ArmResponse<Response>> PowerOnAsync(CancellationToken cancellationToken = default)
        {
            var operation = await Operations.StartStartAsync(Id.ResourceGroup, Id.Name, cancellationToken).ConfigureAwait(false);
            return new ArmVoidResponse(await operation.WaitForCompletionAsync(cancellationToken).ConfigureAwait(false));
        }

        public ArmOperation<Response> StartPowerOn(CancellationToken cancellationToken = default)
        {
            return new ArmVoidOperation(Operations.StartStart(Id.ResourceGroup, Id.Name, cancellationToken));
        }

        public async Task<ArmOperation<Response>> StartPowerOnAsync(CancellationToken cancellationToken = default)
        {
            return new ArmVoidOperation(await Operations.StartStartAsync(Id.ResourceGroup, Id.Name, cancellationToken).ConfigureAwait(false));
        }
        #endregion

        #region PowerOff
        public ArmResponse<Response> PowerOff(bool? skipShutdown = null, CancellationToken cancellationToken = default)
        {
            var operation = Operations.StartPowerOff(Id.ResourceGroup, Id.Name, skipShutdown, cancellationToken);
            return new ArmVoidResponse(operation.WaitForCompletionAsync(cancellationToken).ConfigureAwait(false).GetAwaiter().GetResult());
        }

        public async Task<ArmResponse<Response>> PowerOffAsync(bool? skipShutdown = null, CancellationToken cancellationToken = default)
        {
            var operation = await Operations.StartPowerOffAsync(Id.ResourceGroup, Id.Name, skipShutdown, cancellationToken).ConfigureAwait(false);
            return new ArmVoidResponse(await operation.WaitForCompletionAsync(cancellationToken).ConfigureAwait(false));
        }

        public ArmOperation<Response> StartPowerOff(bool? skipShutdown = null, CancellationToken cancellationToken = default)
        {
            return new ArmVoidOperation(Operations.StartPowerOff(Id.ResourceGroup, Id.Name, skipShutdown, cancellationToken));
        }

        public async Task<ArmOperation<Response>> StartPowerOffAsync(bool? skipShutdown = null, CancellationToken cancellationToken = default)
        {
            return new ArmVoidOperation(await Operations.StartPowerOffAsync(Id.ResourceGroup, Id.Name, skipShutdown, cancellationToken).ConfigureAwait(false));
        }
        #endregion

        public override ArmResponse<VirtualMachineOperations> Get()
        {
            return new PhArmResponse<VirtualMachineOperations, VirtualMachine>(Operations.Get(Id.ResourceGroup, Id.Name), v => { Resource = new PhVirtualMachine(v); return this; } );
        }

        public async override Task<ArmResponse<VirtualMachineOperations>> GetAsync(CancellationToken cancellationToken = default)
        {
            return new PhArmResponse<VirtualMachineOperations, VirtualMachine>(await Operations.GetAsync(Id.ResourceGroup, Id.Name, cancellationToken), v => { Resource = new PhVirtualMachine(v); return this; });
        }

        public ArmOperation<VirtualMachineOperations> Update(VirtualMachineUpdate patchable)
        {
            return new PhArmOperation<VirtualMachineOperations, VirtualMachine>(Operations.StartUpdate(Id.ResourceGroup, Id.Name, patchable), v => { Resource = new PhVirtualMachine(v); return this; });
        }

        public async Task<ArmOperation<VirtualMachineOperations>> UpdateAsync(VirtualMachineUpdate patchable, CancellationToken cancellationToken = default)
        {
            return new PhArmOperation<VirtualMachineOperations, VirtualMachine>(await Operations.StartUpdateAsync(Id.ResourceGroup, Id.Name, patchable, cancellationToken), v => { Resource = new PhVirtualMachine(v); return this; });
        }

        public ArmOperation<VirtualMachineOperations> AddTag(string key, string value)
        {
            var patchable = new VirtualMachineUpdate { Tags= new Dictionary<string, string>(StringComparer.InvariantCultureIgnoreCase)};
            patchable.Tags.Add(key, value);
            return new PhArmOperation<VirtualMachineOperations, VirtualMachine>(Operations.StartUpdate(Id.ResourceGroup, Id.Name, patchable), v => { Resource = new PhVirtualMachine(v); return this; });
        }

        public async Task<ArmOperation<VirtualMachineOperations>> AddTagAsync(string key, string value, CancellationToken cancellationToken = default)
        {
            var patchable = new VirtualMachineUpdate { Tags = new Dictionary<string, string>(StringComparer.InvariantCultureIgnoreCase) };
            patchable.Tags.Add(key, value);
            return new PhArmOperation<VirtualMachineOperations, VirtualMachine>(await Operations.StartUpdateAsync(Id.ResourceGroup, Id.Name, patchable, cancellationToken), v => { Resource = new PhVirtualMachine(v); return this; });
        }

        internal VirtualMachinesOperations Operations => GetClient<ComputeManagementClient>((baseUri, creds) => new ComputeManagementClient(baseUri, Id.Subscription, creds)).VirtualMachines;
    }
}
