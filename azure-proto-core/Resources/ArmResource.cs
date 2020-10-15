namespace azure_proto_core
{
    /// <summary>
    /// Generic representation of an ARM resource.  Resources in the ARM RP should extend this resource.
    /// </summary>
    public class ArmResource : TrackedResource, IManagedByResource, ISkuResource
    {
        public ArmResource(Azure.ResourceManager.Resources.Models.Resource genericResource)
        {
            Id = genericResource.Id;
            Location = genericResource.Location;
            Tags.Clear();
            foreach(var tag in genericResource.Tags)
            {
                Tags.Add(tag);
            }
        }

        public ArmResource(ResourceIdentifier id)
        {
            Id = id;
            Location = Location.Default;
        }

        public ArmResource(ResourceIdentifier id, Location location)
        {
            Id = id;
            Location = location;
        }

        public override ResourceIdentifier Id { get; protected set; }
        public string ManagedBy { get; set; }
        public Sku Sku { get; set; }
        public Plan Plan { get; set; }
        public string Kind { get; set; }

    }
}
