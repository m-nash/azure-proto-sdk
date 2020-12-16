using Azure.ResourceManager.Core;

namespace azure_proto_network
{
    public class Subnet : SubnetOperations
    {
        public Subnet(AzureResourceManagerClientContext context, SubnetData resource, AzureResourceManagerClientOptions options)
            : base(context, resource.Id, options)
        {
            Data = resource;
        }

        public SubnetData Data { get; private set; }
    }
}
