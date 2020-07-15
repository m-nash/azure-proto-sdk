namespace azure_proto_core
{
    public abstract class AzureResource<T> : IResource
    {
        public T Model { get; private set; }

        public IResource Parent { get; private set; }

        public abstract string Name { get; }

        public abstract string Id { get; }

        public ClientFactory Clients { get; private set; }

        public AzureResource(IResource parent, T model)
        {
            Parent = parent;
            Model = model;
            Clients = parent.Clients;
        }
    }
}
