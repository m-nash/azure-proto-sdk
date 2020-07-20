using Azure.ResourceManager.Compute.Models;
using azure_proto_core;
using System.Collections.Generic;

namespace azure_proto_compute
{
    public class AvailabilitySetCollection : AzureCollection<AzureAvailabilitySet>
    {

        public AvailabilitySetCollection(IResource resourceGroup) : base(resourceGroup) { }

        public AzureAvailabilitySet CreateOrUpdateAvailabilityset(string name, AzureAvailabilitySet availabilitySet)
        {
            var computeClient = Parent.Clients.ComputeClient;
            var aSet = computeClient.AvailabilitySets.CreateOrUpdate(Parent.Name, name, availabilitySet.Model.Data as AvailabilitySet);
            AzureAvailabilitySet azureAvailabilitySet = new AzureAvailabilitySet(Parent, new PhAvailabilitySet(aSet.Value));
            return azureAvailabilitySet;
        }

        protected override AzureAvailabilitySet Get(string availabilitySetName)
        {
            var computeClient = Parent.Clients.ComputeClient;
            var asetResult = computeClient.AvailabilitySets.Get(Parent.Name, availabilitySetName);
            return new AzureAvailabilitySet(Parent, new PhAvailabilitySet(asetResult.Value));
        }

        protected override IEnumerable<AzureAvailabilitySet> GetItems()
        {
            var computeClient = Parent.Clients.ComputeClient;
            foreach (var aset in computeClient.AvailabilitySets.List(Parent.Name))
            {
                yield return new AzureAvailabilitySet(Parent, new PhAvailabilitySet(aset));
            }
        }
    }
}
