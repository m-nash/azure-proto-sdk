using Azure.ResourceManager.Compute.Models;
using System;

namespace azure
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
            var computeClient = resourceGroup.Location.Subscription.ComputeClient;
            foreach(var aset in computeClient.AvailabilitySets.List(resourceGroup.Name))
            {
                this.Add(aset.Name, new AzureAvailabilitySet(resourceGroup, aset));
            }
        }

        internal AzureAvailabilitySet CreateOrUpdateAvailabilityset(string name, AvailabilitySet availabilitySet)
        {
            var computeClient = resourceGroup.Location.Subscription.ComputeClient;
            var aSet = computeClient.AvailabilitySets.CreateOrUpdate(resourceGroup.Name, name, availabilitySet);
            AzureAvailabilitySet azureAvailabilitySet = new AzureAvailabilitySet(resourceGroup, aSet.Value);
            this.Add(aSet.Value.Id, azureAvailabilitySet);
            return azureAvailabilitySet;
        }
    }
}
