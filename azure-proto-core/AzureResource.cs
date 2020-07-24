namespace azure_proto_core
{
    public abstract class AzureResource : TrackedResource 
    {
        public override ResourceIdentifier Id { get; protected set; }

    }

    public abstract class AzureResource<T> : AzureResource
    {
        public AzureResource(ResourceIdentifier id)
        {
            Id = id;
            Location = Location.Default;
        }

        public AzureResource(ResourceIdentifier id, Location location)
        {
            Id = id;
            Location = location;
        }

        public abstract T Data { get; protected set; }

    }
}
