using Azure.ResourceManager.Core;

namespace azure_proto_network
{
    public class Subnet : SubnetOperations
    {
        public Subnet(AzureResourceManagerClientContext context, SubnetData resource)
            : base(context, resource.Id)
        {
            Data = resource;
        }

        public SubnetData Data { get; private set; }
    }
}
