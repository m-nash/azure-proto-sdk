using azure_proto_core;
using System.Collections.Generic;

namespace azure_proto_management
{
    public class AzureResourceGroup : AzureResourceGroupBase
    {
        public override string Name => Model.Name;

        public override string Id => Model.Id;

        public AzureResourceGroup(AzureLocation location, PhResourceGroup resourceGroup) : base(location, resourceGroup) { }
    }
}
