namespace azure_proto_core
{
    public abstract class AzureResource : IResource
    {
        public IModel Model { get; private set; }

        public IResource Parent { get; private set; }

        public virtual string Name => Model.Name;

        public string Id => Model.Id;

        public string Location => Model.Location;

        public ClientFactory Clients { get; private set; }

        public object Data => throw new System.NotImplementedException();

        public AzureResource(IResource parent, IModel model)
        {
            Parent = parent;
            Model = model;
            Clients = parent?.Clients;
        }
    }
}
