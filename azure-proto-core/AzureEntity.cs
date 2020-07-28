using System.Collections.Generic;

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
    /// Represents a typed resource and operations.  This is a wrapper class that allows us to use models
    /// to determine the standard properties
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public abstract class AzureEntity<T> : AzureEntity where T: TrackedResource
    {

        protected AzureEntity(TrackedResource parent, T data)
        {
            Parent = parent;
            Model = data;
        }

        public virtual TrackedResource Parent { get; protected set; }
        public virtual T Model { get; protected set; }

        public override ResourceIdentifier Id => Model.Id;

        public override Location Location => Model.Location;

        public override IDictionary<string, string> Tags => Model.Tags;

    }
}
