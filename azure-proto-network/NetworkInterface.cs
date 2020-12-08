using azure_proto_core;

namespace azure_proto_network
{
    public class NetworkInterface : NetworkInterfaceOperations
    {
        internal NetworkInterface(ArmClientContext context, NetworkInterfaceData resource, ArmClientOptions options)
            : base(context, resource.Id, options)
        {
            Model = resource;
        }

        public NetworkInterfaceData Model { get; private set; }
    }
}
