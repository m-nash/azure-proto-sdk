
namespace azure_proto_core
{
    // TODO: Think about other base classes for different resource 'containers'
    public class AzureResourceGroupBase : AzureEntity
    {
        public AzureResourceGroupBase(ResourceIdentifier id) { Id = id; }

        public AzureResourceGroupBase(ResourceIdentifier id, Location location) { Id = id; Location = location; }

    }
}
