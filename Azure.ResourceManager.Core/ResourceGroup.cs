namespace Azure.ResourceManager.Core
{
    public class ResourceGroup : ResourceGroupOperations
    {
        internal ResourceGroup(AzureResourceManagerClientContext context, ResourceGroupData resource)
            : base(context, resource)
        {
            Data = resource;
        }

        public ResourceGroupData Data { get; private set; }
    }
}
