using System;
using System.Collections.Generic;
using System.Security;
using System.Text;
using Azure.ResourceManager.Compute.Models;
using azure_proto_core;

namespace azure_proto_compute.Convenience
{
    public class VmModelBuilder : VmModelBuilderBase
    {
        // TODO: Azure or Ph model?
        // has to use generated model for now
        private VirtualMachine _model;

        internal VmModelBuilder(string vmName, Location location)
        {
            // TODO: Ph model should allow default constructor and property individually settable
            // _model.Name = vmName;
            _model = new VirtualMachine(location);
        }

        public VmModelBuilderBase AttachDataDisk(TrackedResource azureEntity)
        {
            throw new NotImplementedException();
        }

        public override VmModelBuilderBase UseWindowsImage(string adminUser, string password)
        {
            throw new NotImplementedException();
        }

        public override VmModelBuilderBase UseLinuxImage(string adminUser, string password)
        {
            throw new NotImplementedException();
        }

        public override PhVirtualMachine ToModel()
        {
            throw new NotImplementedException();
        }

        public override VmModelBuilderBase RequiredNetworkInterface(ResourceIdentifier nicResourceId)
        {
            throw new NotImplementedException();
        }

        public override VmModelBuilderBase RequiredAvalabilitySet(ResourceIdentifier asetResourceId)
        {
            throw new NotImplementedException();
        }
    }
}
