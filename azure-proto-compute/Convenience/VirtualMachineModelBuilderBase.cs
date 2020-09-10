using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using azure_proto_core;

namespace azure_proto_compute.Convenience
{
    public abstract class VirtualMachineModelBuilderBase : LroArmBuilder<VirtualMachineContainer, PhVirtualMachine>
    {
        protected VirtualMachineModelBuilderBase(VirtualMachineContainer containerOperations, PhVirtualMachine vm): base(containerOperations, vm){ }

        public abstract VirtualMachineModelBuilderBase UseWindowsImage(string adminUser, string password);

        public abstract VirtualMachineModelBuilderBase UseLinuxImage(string adminUser, string password);

        public abstract VirtualMachineModelBuilderBase RequiredNetworkInterface(ResourceIdentifier nicResourceId);

        public abstract VirtualMachineModelBuilderBase RequiredAvalabilitySet(ResourceIdentifier asetResourceId);

        public abstract PhVirtualMachine ToModel();

        public override ArmOperation<ResourceOperationsBase<PhVirtualMachine>> StartCreate(string name)
        {
            return _containerOperations.StartCreate(name, _resource);
        }
    }
}
