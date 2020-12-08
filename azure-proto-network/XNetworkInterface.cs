using azure_proto_core;

namespace azure_proto_network
{
    public class XNetworkInterface : NetworkInterfaceOperations
    {
        internal XNetworkInterface(ArmClientContext context, PhNetworkInterface resource, ArmClientOptions clientOptions) : base(context, resource.Id, clientOptions)
        {
            Model = resource;
        }

        public PhNetworkInterface Model { get; private set; }
    }
}
