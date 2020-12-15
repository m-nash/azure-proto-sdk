using Azure.ResourceManager.Core;

namespace azure_proto_network
{
    public class Subnet : SubnetOperations
    {
        public Subnet(ArmClientContext context, SubnetData resource, ArmClientOptions options)
            : base(context, resource.Id, options)
        {
            Data = resource;
        }

        public SubnetData Data { get; private set; }
    }
}
