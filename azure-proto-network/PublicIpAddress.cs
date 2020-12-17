using Azure.ResourceManager.Core;

namespace azure_proto_network
{
    public class PublicIpAddress : PublicIpAddressOperations
    {
        internal PublicIpAddress(AzureResourceManagerClientContext context, PublicIPAddressData resource, AzureResourceManagerClientOptions options)
            : base(context, resource.Id, options)
        {
            Data = resource;
        }

        public PublicIPAddressData Data { get; private set; }
    }
}
