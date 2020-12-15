using Azure.ResourceManager.Core;

namespace azure_proto_network
{
    public class NetworkInterface : NetworkInterfaceOperations
    {
        internal NetworkInterface(ArmClientContext context, NetworkInterfaceData resource, ArmClientOptions options)
            : base(context, resource.Id, options)
        {
            Data = resource;
        }

        public NetworkInterfaceData Data { get; private set; }
    }
}
