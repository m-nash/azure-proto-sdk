using azure_proto_core;

namespace azure_proto_management
{
    public class AzureResourceGroup : AzureResourceGroupBase
    {
        public override string Name => Model.Name;

        public override string Id => Model.Id;

        public AzureResourceGroup(IResource location, PhResourceGroup resourceGroup) : base(location, resourceGroup) { }
    }
}
