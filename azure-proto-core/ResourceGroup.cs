namespace azure_proto_core
{
    public class ResourceGroup : ResourceGroupOperations
    {
        public ResourceGroup(ArmClientContext context, ResourceGroupData resource)
            : base(context, resource)
        {
            Model = resource;
        }

        public ResourceGroupData Model { get; private set; }
    }
}
