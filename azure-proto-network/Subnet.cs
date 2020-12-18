using Azure.ResourceManager.Core;

namespace azure_proto_network
{
    public class Subnet : SubnetOperations
    {
        public Subnet(AzureResourceManagerClientOptions SubnetOptions, SubnetData resource)
            : base(SubnetOptions, resource.Id)
        {
            Data = resource;
        }

        public SubnetData Data { get; private set; }
    }
}
