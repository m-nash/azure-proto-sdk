using System;
using System.Collections.Generic;
using System.Security;
using System.Text;
using Azure.ResourceManager.Compute.Models;
using azure_proto_core;

namespace azure_proto_compute.Convenience
{
    public class AzureVmModelBuilderBase : IAzureVmModelBuilder
    {
        // TODO: Azure or Ph model?
        // has to use generated model for now
        private VirtualMachine _model;

        internal AzureVmModelBuilderBase(string vmName, Location location)
        {
            // TODO: Ph model should allow default constructor and property individually settable
            // _model.Name = vmName;
            _model = new VirtualMachine(location);
        }

        public IAzureVmModelBuilder AttachDataDisk(AzureEntity azureEntity)
        {
            throw new NotImplementedException();
        }

        public IAzureVmModelBuilder Location(Location location)
        {
            _model.Location = location;
            return this;
        }

        public IAzureVmModelBuilder Name(string name)
        {
            // Name is not settable !?
            // _model.Name = name;
            return this;
        }

        public IAzureVmModelBuilder UseWindowsImage(string adminUser, string password)
        {
            throw new NotImplementedException();
        }

        public IAzureVmModelBuilder UseLinuxImage(string adminUser, string password)
        {
            throw new NotImplementedException();
        }

        public AzureVm ToModel()
        {
            throw new NotImplementedException();
        }

        public IAzureVmModelBuilder RequiredNetworkInterface(ResourceIdentifier nicResourceId)
        {
            throw new NotImplementedException();
        }

        public IAzureVmModelBuilder RequiredAvalabilitySet(ResourceIdentifier asetResourceId)
        {
            throw new NotImplementedException();
        }
    }
}
