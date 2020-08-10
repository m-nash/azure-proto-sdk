
namespace azure_proto_core
{
    // TODO: Think about other base classes for different resource 'containers'
    public class AzureProviderBase : AzureOperations
    {
        public AzureProviderBase(ResourceIdentifier id) { Id = id; }

        public AzureProviderBase(ResourceIdentifier id, Location location) { Id = id; Location = location; }
    }
}
