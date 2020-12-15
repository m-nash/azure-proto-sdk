using Azure.ResourceManager.Core;
using System;

namespace azure_proto_compute.Convenience
{
    public class VirtualMachineModelBuilder : VirtualMachineModelBuilderBase
    {
        // TODO: GENERATOR Update Builder after models are incorporated in generated models

        internal VirtualMachineModelBuilder(VirtualMachineContainer containerOperations, VirtualMachineData vm): base(containerOperations, vm)
        {
            // _model.Name = vmName;
            //_model = new VirtualMachine(location);
        }

        public VirtualMachineModelBuilderBase AttachDataDisk(TrackedResource azureEntity)
        {
            throw new NotImplementedException();
        }

        public override VirtualMachineModelBuilderBase UseWindowsImage(string adminUser, string password)
        {
            throw new NotImplementedException();
        }

        public override VirtualMachineModelBuilderBase UseLinuxImage(string adminUser, string password)
        {
            throw new NotImplementedException();
        }

        public override VirtualMachineModelBuilderBase RequiredNetworkInterface(ResourceIdentifier nicResourceId)
        {
            throw new NotImplementedException();
        }

        public override VirtualMachineModelBuilderBase RequiredAvalabilitySet(ResourceIdentifier asetResourceId)
        {
            throw new NotImplementedException();
        }
    }
}
