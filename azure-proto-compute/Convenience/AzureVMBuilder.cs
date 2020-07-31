using System;
using System.Collections.Generic;
using System.Security;
using System.Text;
using Azure.ResourceManager.Compute.Models;
using azure_proto_core;

namespace azure_proto_compute.Convenience
{
    public class AzureVmModelBuilder : IAzureVmModelBuilder<AzureVm>
    {
        private Dictionary<ResourceType, Action<VirtualMachine, AzureEntity>> DependentResourceSetter =
            new Dictionary<ResourceType, Action<VirtualMachine, AzureEntity>>()
        {
            {
                new ResourceType("Microsoft.Compute/AzureAvailabilitySet"),
                (VirtualMachine m, AzureEntity e) =>  m.AvailabilitySet.Id = e.Id
            },
            {
                new ResourceType("Microsoft.Network/NetworkInterface"),
                (VirtualMachine m, AzureEntity e) =>
                {
                    m.NetworkProfile = new NetworkProfile
                    {
                        NetworkInterfaces = new[] {new NetworkInterfaceReference() {Id = e.Id}}
                    };
                }
            }
        };

        // TODO: Azure or Ph model?
        // has to use generated model for now
        private VirtualMachine _model;

        internal AzureVmModelBuilder(TrackedResource resourceGroup, string name)
        {
            // TODO: Ph model should allow default constructor and property individually settable
            _model = new VirtualMachine(resourceGroup.Location);
        }

        public IAzureVmModelBuilder<AzureVm> ConfigureWith(AzureEntity azureEntity)
        {
            if (DependentResourceSetter.ContainsKey(azureEntity.Type))
                DependentResourceSetter[azureEntity.Type](_model, azureEntity);
            else
                throw new NotSupportedException("Not supported type");

            return this;
        }

        public IAzureVmModelBuilder<AzureVm> AttachDataDisk(AzureEntity azureEntity)
        {
            throw new NotImplementedException();
        }

        public IAzureVmModelBuilder<AzureVm> Location(Location location)
        {
            _model.Location = location;
            return this;
        }

        public IAzureVmModelBuilder<AzureVm> Name(string name)
        {
            // Name is not settable !?
            // _model.Name = name;
            return this;
        }

        public IAzureVmModelBuilder<AzureVm> UseWindowsImage(string adminUser, string password)
        {
            throw new NotImplementedException();
        }

        public IAzureVmModelBuilder<AzureVm> UseLinuxImage(string adminUser, string password)
        {
            throw new NotImplementedException();
        }

        public AzureVm ToModel()
        {
            throw new NotImplementedException();
        }
    }
}
