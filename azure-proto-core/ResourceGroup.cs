namespace azure_proto_core
{
    public class ResourceGroup : ResourceGroupOperations
    {
        internal ResourceGroup(ArmClientContext context, ResourceGroupData resource, ArmClientOptions options)
            : base(context, resource, options)
        {
            Model = resource;
        }

        public ResourceGroupData Model { get; private set; }
    }
}
