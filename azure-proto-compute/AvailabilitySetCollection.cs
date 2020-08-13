using Azure.ResourceManager.Compute;
using Azure.ResourceManager.Compute.Models;
using azure_proto_core;
using System.Collections.Generic;

namespace azure_proto_compute
{
    /// <summary>
    /// Class conatinaing list operations for availabiliuty sets
    /// </summary>
    public class AvailabilitySetCollection : ArmResourceCollectionOperations
    {

        public AvailabilitySetCollection(ArmOperations parent, azure_proto_core.Resource resourceGroup) : base(parent, resourceGroup) { }
        public AvailabilitySetCollection(ArmOperations parent, ResourceIdentifier resourceGroup) : base(parent, resourceGroup) { }

        protected override ResourceType ResourceType => "Microsoft.Compute/availabilitySets";

        public AvailabilitySetOperations AvailabilitySet(string resourceGroupName, string name)
        {
            return new AvailabilitySetOperations(this, new ResourceIdentifier($"{Context}/resourceGroups/{resourceGroupName}/providers/Microsoft.Compute/availabilitySets/{name}"));
        }

        public AvailabilitySetOperations AvailabilitySet(ResourceIdentifier vm)
        {
            return new AvailabilitySetOperations(this, vm);
        }

        public AvailabilitySetOperations AvailabilitySet(TrackedResource vm)
        {
            return new AvailabilitySetOperations(this, vm);
        }

    }
}
