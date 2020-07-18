using azure_proto_core;

namespace azure_proto_management
{
    public class AzureResourceGroup : AzureResourceGroupBase
    {
        public AzureResourceGroup(IResource location, PhResourceGroup resourceGroup) : base(location, resourceGroup) { }
    }
}
