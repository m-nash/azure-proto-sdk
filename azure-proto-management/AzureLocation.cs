using azure_proto_core;

namespace azure_proto_management
{
    public class AzureLocation: AzureEntity
    {
        public AzureLocation(AzureSubscription subscription, PhLocation location) { Id = location.Id; Location = location.Name; }
    }
}
