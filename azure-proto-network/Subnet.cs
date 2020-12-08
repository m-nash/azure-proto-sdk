using azure_proto_core;

namespace azure_proto_network
{
    public class Subnet : SubnetOperations
    {
        public Subnet(ArmClientContext context, SubnetData resource, ArmClientOptions options)
            : base(context, resource.Id, options)
        {
            Model = resource;
        }

        public SubnetData Model { get; private set; }
    }
}
