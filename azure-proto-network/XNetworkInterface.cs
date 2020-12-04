using azure_proto_core;

namespace azure_proto_network
{
    public class XNetworkInterface : NetworkInterfaceOperations
    {
        public XNetworkInterface(ArmClientContext context, PhNetworkInterface resource) : base(context, resource.Id)
        {
            Model = resource;
        }

        public PhNetworkInterface Model { get; private set; }
    }
}
