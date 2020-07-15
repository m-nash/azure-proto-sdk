namespace azure_proto_core
{
    public interface IResource
    {
        string Name { get; }
        string Id { get; }
        ClientFactory Clients { get; }
    }
}
