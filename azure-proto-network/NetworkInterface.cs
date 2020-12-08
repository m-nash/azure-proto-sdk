using azure_proto_core;

namespace azure_proto_network
{
    public class NetworkInterface : NetworkInterfaceOperations
    {
        public NetworkInterface(ArmClientContext context, NetworkInterfaceData resource) : base(context, resource.Id)
        {
            Model = resource;
        }

        public NetworkInterfaceData Model { get; private set; }
    }
}
