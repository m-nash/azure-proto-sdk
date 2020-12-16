namespace Azure.ResourceManager.Core
{
    public class ResourceGroup : ResourceGroupOperations
    {
        internal ResourceGroup(AzureResourceManagerClientContext context, ResourceGroupData resource, AzureResourceManagerClientOptions options)
            : base(context, resource, options)
        {
            Data = resource;
        }

        public ResourceGroupData Data { get; private set; }
    }
}
