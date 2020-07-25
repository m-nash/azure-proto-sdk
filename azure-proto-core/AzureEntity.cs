namespace azure_proto_core
{
    /// <summary>
    /// Reporesents a generic Azure Resource, with operations
    /// </summary>
    public abstract class AzureEntity : TrackedResource 
    {
        public override ResourceIdentifier Id { get; protected set; }

    }

    /// <summary>
    /// Represents a typed resource and operations
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public abstract class AzureEntity<T> : AzureEntity
    {
        public AzureEntity(ResourceIdentifier id)
        {
            Id = id;
            Location = Location.Default;
        }

        public AzureEntity(ResourceIdentifier id, Location location)
        {
            Id = id;
            Location = location;
        }

        public abstract T Data { get; protected set; }
    }
}
