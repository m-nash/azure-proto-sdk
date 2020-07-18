namespace azure_proto_core
{
    public interface IModel
    {
        string Name { get; }
        string Id { get; }
        string Location { get; }
        object Data { get; }
    }
}
