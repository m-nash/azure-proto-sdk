using System;
using System.Collections.Generic;
using System.Security;
using System.Text;
using Azure.ResourceManager.Compute.Models;
using azure_proto_core;

namespace azure_proto_compute.Convenience
{
    public class VirtualMachineModelBuilder : VirtualMachineModelBuilderBase
    {
        // TODO: Azure or Ph model?
        // has to use generated model for now
        private VirtualMachine _model;

        internal VirtualMachineModelBuilder(string vmName, Location location)
        {
            // TODO: Ph model should allow default constructor and property individually settable
            // _model.Name = vmName;
            _model = new VirtualMachine(location);
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

        public override PhVirtualMachine ToModel()
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
