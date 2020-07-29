namespace azure_proto_core
{
    /// <summary>
    /// Resource managed by another resource
    /// </summary>
    public interface IManagedByResource
    {
        string ManagedBy { get; set; }
    }



}
