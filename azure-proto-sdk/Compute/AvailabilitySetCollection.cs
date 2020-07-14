using Azure.ResourceManager.Compute.Models;
using azure_proto_sdk.Management;
using System;

namespace azure_proto_sdk.Compute
{
    public class AvailabilitySetCollection : AzureCollection<AzureAvailabilitySet>
    {
        private AzureResourceGroup resourceGroup;

        public AvailabilitySetCollection(AzureResourceGroup resourceGroup)
        {
            this.resourceGroup = resourceGroup;
        }

        protected override void LoadValues()
        {
            var computeClient = resourceGroup.Parent.Parent.ComputeClient;
            foreach(var aset in computeClient.AvailabilitySets.List(resourceGroup.Name))
            {
                this.Add(aset.Name, new AzureAvailabilitySet(resourceGroup, aset));
            }
        }

        internal AzureAvailabilitySet CreateOrUpdateAvailabilityset(string name, AzureAvailabilitySet availabilitySet)
        {
            var computeClient = resourceGroup.Parent.Parent.ComputeClient;
            var aSet = computeClient.AvailabilitySets.CreateOrUpdate(resourceGroup.Name, name, availabilitySet.Model);
            AzureAvailabilitySet azureAvailabilitySet = new AzureAvailabilitySet(resourceGroup, aSet.Value);
            this.Add(aSet.Value.Id, azureAvailabilitySet);
            return azureAvailabilitySet;
        }
    }
}
