using Azure.ResourceManager.Core;

namespace azure_proto_network
{
    public class PublicIpAddress : PublicIpAddressOperations
    {
        internal PublicIpAddress(ResourceOperationsBase options, PublicIPAddressData resource)
            : base(options, resource.Id)
        {
            Data = resource;
        }

        public PublicIPAddressData Data { get; private set; }
    }
}
