using azure_proto_core;

namespace azure_proto_network
{
    public class XSubnet : SubnetOperations
    {
        public XSubnet(ArmClientContext context, PhSubnet resource, ArmClientOptions clientOptions):base(context, resource.Id, clientOptions)
        {
            Model = resource;
        }

        public PhSubnet Model { get; private set; }
    }
}
