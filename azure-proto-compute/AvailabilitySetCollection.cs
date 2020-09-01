using azure_proto_core;

namespace azure_proto_compute
{
    /// <summary>
    /// Class conatinaing list operations for availabiliuty sets
    /// </summary>
    public class AvailabilitySetCollection : ResourceCollectionOperations<PhAvailabilitySet>
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

        protected override ResourceOperations<PhAvailabilitySet> GetOperations(ResourceIdentifier identifier, Location location)
        {
            var resource = new ArmResource(identifier, location);
            return AvailabilitySet(resource);
        }
    }
}
