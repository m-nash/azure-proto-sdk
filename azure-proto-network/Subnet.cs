using azure_proto_core;

namespace azure_proto_network
{
    public class Subnet : SubnetOperations
    {
        public Subnet(ArmClientContext context, SubnetData resource):base(context, resource.Id)
        {
            Model = resource;
        }

        public SubnetData Model { get; private set; }
    }
}
