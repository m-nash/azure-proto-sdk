
namespace azure_proto_core
{
    public class AzureResourceGroupBase : AzureResource
    {
        public AzureResourceGroupBase(ResourceIdentifier id) { Id = id; }

        public AzureResourceGroupBase(ResourceIdentifier id, Location location) { Id = id; Location = location; }

    }
}
