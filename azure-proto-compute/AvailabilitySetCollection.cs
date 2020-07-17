﻿using Azure.ResourceManager.Compute.Models;
using azure_proto_core;

namespace azure_proto_compute
{
    public class AvailabilitySetCollection : AzureCollection<AzureAvailabilitySet>
    {

        public AvailabilitySetCollection(IResource resourceGroup) : base(resourceGroup) { }

        protected override void LoadValues()
        {
            var computeClient = Parent.Clients.ComputeClient;
            foreach(var aset in computeClient.AvailabilitySets.List(Parent.Name))
            {
                this.Add(aset.Name, new AzureAvailabilitySet(Parent, new PhAvailabilitySet(aset)));
            }
        }

        public AzureAvailabilitySet CreateOrUpdateAvailabilityset(string name, AzureAvailabilitySet availabilitySet)
        {
            var computeClient = Parent.Clients.ComputeClient;
            var aSet = computeClient.AvailabilitySets.CreateOrUpdate(Parent.Name, name, availabilitySet.Model.Data as AvailabilitySet);
            AzureAvailabilitySet azureAvailabilitySet = new AzureAvailabilitySet(Parent, new PhAvailabilitySet(aSet.Value));
            this.Add(aSet.Value.Id, azureAvailabilitySet);
            return azureAvailabilitySet;
        }

        protected override AzureAvailabilitySet GetSingleValue(string key)
        {
            var computeClient = Parent.Clients.ComputeClient;
            var asetResult = computeClient.AvailabilitySets.Get(Parent.Name, key);
            return new AzureAvailabilitySet(Parent, new PhAvailabilitySet(asetResult.Value));
        }
    }
}
