namespace Azure.ResourceManager.Core
{
    public class ResourceGroup : ResourceGroupOperations
    {
        internal ResourceGroup(AzureResourceManagerClientOptions options, ResourceGroupData resource)
            : base(options, resource)
        {
            Data = resource;
        }

        public ResourceGroupData Data { get; private set; }
    }
}
