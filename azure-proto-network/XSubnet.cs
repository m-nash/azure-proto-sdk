using azure_proto_core;

namespace azure_proto_network
{
    public class XSubnet : SubnetOperations
    {
        public XSubnet(ArmClientContext context, PhSubnet resource):base(context, resource.Id)
        {
            Model = resource;
        }

        public PhSubnet Model { get; private set; }
    }
}
